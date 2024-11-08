using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models;
using TP6MVC.Repositories;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TP6MVC.Controllers;

public class ProductoController : Controller
{
    private readonly ProductosRepository repositorioProd;
    private readonly ILogger<ProductoController> _logger;

    public ProductoController(ILogger<ProductoController> logger)
    {
        _logger = logger;
        repositorioProd = new ProductosRepository();
    }
    
    public IActionResult Listar()
    {
        List<Producto> productos = repositorioProd.ListarProductos();
        return View(productos);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        repositorioProd.CrearProducto(producto);
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Modificar(int id)
    {
        Producto producto = repositorioProd.ObtenerProductoID(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Modificar(Producto producto)
    {
        repositorioProd.modificarProducto(producto);
        return RedirectToAction("Listar");
    }

    public IActionResult Eliminar(int id)
    {
        repositorioProd.eliminarProducto(id);
        return RedirectToAction("Listar");
    }
}
