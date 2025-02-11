using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models; // Asegúrate de ajustar el namespace
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace TP6MVC.Repositories{
public class PresupuestoController : Controller
{
    private readonly PresupuestosRepository repositorioPresup;
    private readonly ILogger<PresupuestoController> _logger;

    private readonly IPresupuestosRepository _repository;

    public PresupuestosController(IPresupuestosRepository repository)
    {
        _repository = repository;
    }

        public PresupuestoController(ILogger<PresupuestoController> logger)
    {
        _logger = logger;
        repositorioPresup = new PresupuestosRepository();
    }

    public IActionResult Listar()
        {
            if (HttpContext.Session.GetString("Rol") != "Administrador" && HttpContext.Session.GetString("Rol") != "Cliente")
            {
                return RedirectToAction("Index", "Login");
            }
            return View(_repository.ObtenerTodos());
        }
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Presupuesto presupuesto)
    {
        try
        {
        repositorioPresup.CrearPresupuesto(presupuesto);
        return RedirectToAction("Listar");
        }
        catch (System.Exception)
        {
            logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult Modificar(int id)
    {
        if (HttpContext.Session.GetString("Rol") != "Administrador")
        {
            return RedirectToAction("Index", "Login");
        }
        var presupuesto = _repository.ObtenerPorId(id);
        if (presupuesto == null) return NotFound();
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult Modificar(int id, Presupuesto presupuesto)
    {
        repositorioPresup.ModificarPresupuesto(id, presupuesto);
        return RedirectToAction("Listar");
    }

    public IActionResult Eliminar(int id)
    {
        repositorioPresup.EliminarPresupuesto(id);
        return RedirectToAction("Listar");
    }

 [HttpGet]
public IActionResult AgregarProducto(int idPresupuesto)
{
    var presupuesto = repositorioPresup.ObtenerPresupuestoPorId(idPresupuesto);
    return View(presupuesto);
}

[HttpPost]
public IActionResult AgregarProducto(int idPresupuesto,PresupuestoDetalle detalle)
{

    repositorioPresup.AgregarProductoAPresupuesto(idPresupuesto, detalle);
    return RedirectToAction("VerDetallesPresupuesto");
}

    public IActionResult VerDetalles(int id)
    {
        Presupuesto presupuesto = repositorioPresup.ObtenerPresupuestoPorId(id);
        return View(presupuesto);
    }

    public IActionResult VerDetallesPresupuesto(int id)
    {
        return View(repositorioPresup.obtenerDetalles(id));
    }
}
}