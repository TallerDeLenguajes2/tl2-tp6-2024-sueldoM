using Microsoft.AspNetCore.Mvc;
using TP6MVC.Models; // Asegúrate de ajustar el namespace
using System.Collections.Generic;

public class ProductoController : Controller
{
    private readonly ProductosRepository repositorioProd = new ProductosRepository();

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

    public IActionResult Modificar(int id)
    {
        Producto producto = repositorioProd.ObtenerProductoID(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Modificar(int id, Producto producto)
    {
        repositorioProd.modificarProducto(id, producto);
        return RedirectToAction("Listar");
    }

    public IActionResult Eliminar(int id)
    {
        repositorioProd.eliminarProducto(id);
        return RedirectToAction("Listar");
    }
}
