using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using TP6MVC.Models;

namespace TP6MVC.Repositories
{
public class ProductosRepository{
    private string connectionString = "Data Source=db/Tienda.db;Cache=Shared";
    public void CrearProducto(Producto producto)
    {
        var query = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion1));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
            command.ExecuteNonQuery();

            connection.Close();
            
        }
    }

    public List<Producto> ListarProductos()
    {
        var query = "SELECT * FROM Productos";
        List<Producto> productos = new List<Producto>();
        
        SqliteConnection connection = new SqliteConnection(connectionString);
        
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   Producto producto = new Producto();
                   producto.IdProducto=Convert.ToInt32(reader["idProducto"]);
                   producto.Descripcion1=reader["descripcion"].ToString();
                   producto.Precio=Convert.ToInt32(reader["precio"]);
                   productos.Add(producto);
                }
            }
        
        return productos;
    }

    public Producto ObtenerProductoID(int idProducto)
    {   
        string query = "SELECT * FROM Productos WHERE idProducto = @idProducto";
        SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();
        Producto producto = new Producto();
        SqliteCommand command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            
        SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                producto.IdProducto=Convert.ToInt32(reader["idProducto"]);
                producto.Descripcion1=reader["descripcion"].ToString();
                producto.Precio=Convert.ToInt32(reader["precio"]); 
            }                    
        connection.Close();       
        return producto;
    }

    public void eliminarProducto(int id)
    {
        string query = "DELETE FROM Productos WHERE idProducto = @idProducto";
        using(SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@idProducto", id));
            command.ExecuteNonQuery();
            
            connection.Close();
        }
    }

    public void modificarProducto(int id, Producto producto)
    {
        var query = "UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProducto";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SqliteCommand(query, connection))
            {
                command.Parameters.Add(new SqliteParameter("@Nombre", producto.Descripcion1));
                command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));
                command.Parameters.Add(new SqliteParameter("@idProducto", id));
                
                command.ExecuteNonQuery();
            }
        }
    }
}
}
