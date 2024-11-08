using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TP6API.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController: ControllerBase
{
    private PresupuestosRepository presupuestoRepository;

        public PresupuestosController()
    {
        presupuestoRepository = new PresupuestosRepository();
    }


    [HttpPost]
    public IActionResult CrearPresupuesto([FromBody] Presupuesto presupuesto)
    {
        presupuestoRepository.CrearPresupuesto(presupuesto);
        return Ok("Presupuesto creado exitosamente.");
    }

    [HttpGet]
    public ActionResult<List<Presupuesto>> ListarPresupuestos()
    {
        var presupuestos = presupuestoRepository.ListarPresupuestos();
        return Ok(presupuestos);
    }

    [HttpGet("{id}")]
    public ActionResult<Presupuesto> ObtenerPresupuestoPorId(int id)
    {
        var presupuesto = presupuestoRepository.ObtenerPresupuestoPorId(id);
        if (presupuesto == null)
            return NotFound("Presupuesto no encontrado.");

        return Ok(presupuesto);
    }

    [HttpPut("{id}/productos")]
    public IActionResult AgregarProductoAPresupuesto(int id, [FromBody] PresupuestoDetalle detalle)
    {
        presupuestoRepository.AgregarProductoAPresupuesto(id, detalle);
        return Ok("Producto agregado al presupuesto.");
    }

    [HttpDelete("{id}")]
    public IActionResult EliminarPresupuesto(int id)
    {
        presupuestoRepository.EliminarPresupuesto(id);
        return Ok("Presupuesto eliminado.");
    }
}