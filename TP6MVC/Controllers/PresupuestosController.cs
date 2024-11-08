using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models; // Asegúrate de ajustar el namespace
using System.Collections.Generic;
namespace TP6MVC.Repositories{
public class PresupuestoController : Controller
{
    private readonly PresupuestosRepository repositorioPresup = new PresupuestosRepository();

    public IActionResult Listar()
    {
        List<Presupuesto> presupuestos = repositorioPresup.ListarPresupuestos();
        return View(presupuestos);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Presupuesto presupuesto)
    {
        repositorioPresup.CrearPresupuesto(presupuesto);
        return RedirectToAction("Listar");
    }

    public IActionResult Modificar(int id)
    {
        Presupuesto presupuesto = repositorioPresup.ObtenerPresupuestoPorId(id);
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

    public IActionResult AgregarProducto(int idPresupuesto, PresupuestoDetalle presupuestoDetalle)
    {
        repositorioPresup.AgregarProductoAPresupuesto(idPresupuesto, presupuestoDetalle);
        return RedirectToAction("Listar");
    }

    public IActionResult VerDetalles(int id)
    {
        Presupuesto presupuesto = repositorioPresup.ObtenerPresupuestoPorId(id);
        return View(presupuesto);
    }
}
}