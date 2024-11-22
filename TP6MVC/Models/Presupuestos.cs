namespace TP6MVC.Models{
public class Presupuesto{
    private int idPresupuesto;
    private int PresupuestoId;
    private Cliente? Cliente; 
    private string? FechaCreacion;
    List<PresupuestoDetalle>? detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string? FechaCreacion1 { get => FechaCreacion; set => FechaCreacion = value; }
    public List<PresupuestoDetalle>? Detalle { get => detalle; set => detalle = value; }
    public int PresupuestoId1 { get => PresupuestoId; set => PresupuestoId = value; }
    public Cliente Cliente1 { get => Cliente; set => Cliente = value; }
    }
}