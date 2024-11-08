using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using TP6MVC.Models;

namespace TP6MVC.Repositories{
public class PresupuestosRepository
{
    private string connectionString = "Data Source=db/Tienda.db;Cache=Shared";

    public void CrearPresupuesto(Presupuesto presupuesto)
    {
        var query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion);";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            
            command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.FechaCreacion1));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion1));

            connection.Close();
            
            int presupuestoId = Convert.ToInt32(command.ExecuteScalar());

            foreach (var detalle in presupuesto.Detalle)
            {
                var detalleCommand = new SqliteCommand("INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad);", connection);
                detalleCommand.Parameters.Add(new SqliteParameter("@idPresupuesto", presupuestoId));
                detalleCommand.Parameters.Add(new SqliteParameter("@idProducto", detalle?.Producto?.IdProducto));
                detalleCommand.Parameters.Add(new SqliteParameter("@Cantidad", detalle?.Cantidad1));
                command.ExecuteNonQuery();
            }
        }
    }

    public Presupuesto ObtenerPresupuestoPorId(int id)
    {
        var query = "SELECT * FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
        Presupuesto presupuesto = new Presupuesto();
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            
            var command = new SqliteCommand(query, connection);

            command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));

            SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                presupuesto.NombreDestinatario1 = reader["NombreDestinatario"].ToString();
                presupuesto.FechaCreacion1 = reader["FechaCreacion"].ToString();
                presupuesto.Detalle = obtenerDetalles(id);
            }
        }
        return presupuesto;
    }

    private List<PresupuestoDetalle> obtenerDetalles(int id)
    {
        var detalles = new List<PresupuestoDetalle>();
        var query = "SELECT idProducto, Cantidad FROM PresupuestosDetalle WHERE idPresupuesto = @idPresupuesto";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto",id));

            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var producto = obtenerProductoPorId(reader.GetInt32(0));
                    var detalle = new PresupuestoDetalle
                    {
                        Producto = (Producto)producto,
                        Cantidad1 = reader.GetInt32(1)
                    };
                    detalles.Add(detalle);
                }
            }
        }
        return detalles;
    }

    private object obtenerProductoPorId(int v)
    {
        var productosRepository = new ProductosRepository();
        return productosRepository.ObtenerProductoID(v);
    }

    public void AgregarProductoAPresupuesto(int presupuestoId, PresupuestoDetalle detalle)
    {
        var query = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad)";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@PresupuestoId", presupuestoId));
            command.Parameters.Add(new SqliteParameter("@ProductoId", detalle?.Producto?.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Cantidad", detalle?.Cantidad1));
            command.ExecuteNonQuery();
        }
    }

    public void EliminarPresupuesto(int id)
    {
        var query = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@idPresupuesto", id);
            command.ExecuteNonQuery();
        }
    }

        public List<Presupuesto> ListarPresupuestos()
    {
        var presupuestos = new List<Presupuesto>();
        var query = "SELECT * FROM Presupuestos";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var presupuesto = new Presupuesto
                    {
                        IdPresupuesto = reader.GetInt32(0),
                        NombreDestinatario1 = reader.GetString(1),
                        FechaCreacion1 = reader.GetString(2),
                        Detalle = obtenerDetalles(reader.GetInt32(0))
                    };
                    presupuestos.Add(presupuesto);
                }
            }
        }

        return presupuestos;
    }

}
}