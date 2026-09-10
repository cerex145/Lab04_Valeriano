using System.Data;
using Lab04_Valeriano.Data;
using Lab04_Valeriano.Models;
using Microsoft.Data.SqlClient;

namespace Lab04_Valeriano.Repositories;

public class ProductoRepository
{
    public List<Producto> List()
    {
        var result = new List<Producto>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Producto_Listar", connection) { CommandType = CommandType.StoredProcedure };
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    public Producto? GetById(int productoId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Producto_ObtenerPorId", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProductoID", productoId);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public int Insert(Producto producto)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Producto_Insertar", connection) { CommandType = CommandType.StoredProcedure };
        AddCommonParameters(command, producto);
        connection.Open();
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public void Update(Producto producto)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Producto_Actualizar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
        AddCommonParameters(command, producto);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int productoId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Producto_Eliminar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProductoID", productoId);
        connection.Open();
        command.ExecuteNonQuery();
    }

    private static void AddCommonParameters(SqlCommand command, Producto producto)
    {
        command.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
        command.Parameters.AddWithValue("@ProveedorID", (object?)producto.ProveedorID ?? DBNull.Value);
        command.Parameters.AddWithValue("@CategoriaID", (object?)producto.CategoriaID ?? DBNull.Value);
        command.Parameters.AddWithValue("@CantidadPorUnidad", (object?)producto.CantidadPorUnidad ?? DBNull.Value);
        command.Parameters.AddWithValue("@PrecioUnidad", producto.PrecioUnidad);
        command.Parameters.AddWithValue("@UnidadesEnExistencia", producto.UnidadesEnExistencia);
        command.Parameters.AddWithValue("@UnidadesEnPedido", producto.UnidadesEnPedido);
        command.Parameters.AddWithValue("@NivelDeReorden", producto.NivelDeReorden);
        command.Parameters.AddWithValue("@Descontinuado", producto.Descontinuado);
    }

    private static Producto Map(SqlDataReader reader) => new()
    {
        ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
        NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
        ProveedorID = reader.IsDBNull(reader.GetOrdinal("ProveedorID")) ? null : reader.GetInt32(reader.GetOrdinal("ProveedorID")),
        CategoriaID = reader.IsDBNull(reader.GetOrdinal("CategoriaID")) ? null : reader.GetInt32(reader.GetOrdinal("CategoriaID")),
        CantidadPorUnidad = reader.IsDBNull(reader.GetOrdinal("CantidadPorUnidad")) ? null : reader.GetString(reader.GetOrdinal("CantidadPorUnidad")),
        PrecioUnidad = reader.GetDecimal(reader.GetOrdinal("PrecioUnidad")),
        UnidadesEnExistencia = reader.GetInt16(reader.GetOrdinal("UnidadesEnExistencia")),
        UnidadesEnPedido = reader.GetInt16(reader.GetOrdinal("UnidadesEnPedido")),
        NivelDeReorden = reader.GetInt16(reader.GetOrdinal("NivelDeReorden")),
        Descontinuado = reader.GetBoolean(reader.GetOrdinal("Descontinuado"))
    };
}
