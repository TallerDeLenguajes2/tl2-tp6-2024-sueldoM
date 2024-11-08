using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TP6API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController: ControllerBase
{
    private ProductosRepository repositorioProd;
    public ProductosController()
    {
        repositorioProd = new ProductosRepository();
    }

    [HttpGet("Productos")]
    public ActionResult<IEnumerable<Producto>> ListarProductos(){
        var productos = repositorioProd.ListarProductos();
        return Ok(productos);
    }

    [HttpPost("Add Producto")]
    public ActionResult PostProducto(Producto producto){
        repositorioProd.CrearProducto(producto);
        return Ok();
    }

        [HttpGet("GetProducto/{id}")]   
    public ActionResult<Producto> Get(int id)
    {
        Producto producto = repositorioProd.ObtenerProductoID(id);
        
        if(producto==null) return NotFound();
        
        return Ok(producto);
    }

    [HttpDelete("deleteProducto/{id}")]
    public ActionResult<Producto> Delete(int id)
    {
        repositorioProd.eliminarProducto(id);
        return Ok();
    }
}