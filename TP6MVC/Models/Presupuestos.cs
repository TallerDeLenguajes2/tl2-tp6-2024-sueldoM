public class Presupuesto{
    private int idPresupuesto;
    private string? NombreDestinatario;
    private string? FechaCreacion;
    List<PresupuestoDetalle>? detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string? NombreDestinatario1 { get => NombreDestinatario; set => NombreDestinatario = value; }
    public string? FechaCreacion1 { get => FechaCreacion; set => FechaCreacion = value; }
    public List<PresupuestoDetalle>? Detalle { get => detalle; set => detalle = value; }
}