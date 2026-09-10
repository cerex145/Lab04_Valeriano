using System.Data;
using Lab04_Valeriano.Data;
using Lab04_Valeriano.Models;
using Microsoft.Data.SqlClient;

namespace Lab04_Valeriano.Repositories;

public class ProveedorRepository
{
    public List<Proveedor> List()
    {
        var result = new List<Proveedor>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_Listar", connection) { CommandType = CommandType.StoredProcedure };
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    public Proveedor? GetById(int proveedorId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_ObtenerPorId", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProveedorID", proveedorId);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public int Insert(Proveedor proveedor)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_Insertar", connection) { CommandType = CommandType.StoredProcedure };
        AddCommonParameters(command, proveedor);
        connection.Open();
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public void Update(Proveedor proveedor)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_Actualizar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);
        AddCommonParameters(command, proveedor);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int proveedorId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_Eliminar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@ProveedorID", proveedorId);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public List<Proveedor> BuscarPorContactoCiudad(string? nombreContacto, string? ciudad)
    {
        var result = new List<Proveedor>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Proveedor_BuscarPorContactoCiudad", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@NombreContacto", (object?)nombreContacto ?? DBNull.Value);
        command.Parameters.AddWithValue("@Ciudad", (object?)ciudad ?? DBNull.Value);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    private static void AddCommonParameters(SqlCommand command, Proveedor proveedor)
    {
        command.Parameters.AddWithValue("@CompaniaNombre", proveedor.CompaniaNombre);
        command.Parameters.AddWithValue("@NombreContacto", (object?)proveedor.NombreContacto ?? DBNull.Value);
        command.Parameters.AddWithValue("@CargoContacto", (object?)proveedor.CargoContacto ?? DBNull.Value);
        command.Parameters.AddWithValue("@Direccion", (object?)proveedor.Direccion ?? DBNull.Value);
        command.Parameters.AddWithValue("@Ciudad", (object?)proveedor.Ciudad ?? DBNull.Value);
        command.Parameters.AddWithValue("@CodigoPostal", (object?)proveedor.CodigoPostal ?? DBNull.Value);
        command.Parameters.AddWithValue("@Pais", (object?)proveedor.Pais ?? DBNull.Value);
        command.Parameters.AddWithValue("@Telefono", (object?)proveedor.Telefono ?? DBNull.Value);
        command.Parameters.AddWithValue("@Fax", (object?)proveedor.Fax ?? DBNull.Value);
    }

    private static Proveedor Map(SqlDataReader reader) => new()
    {
        ProveedorID = reader.GetInt32(reader.GetOrdinal("ProveedorID")),
        CompaniaNombre = reader.GetString(reader.GetOrdinal("CompaniaNombre")),
        NombreContacto = reader.IsDBNull(reader.GetOrdinal("NombreContacto")) ? null : reader.GetString(reader.GetOrdinal("NombreContacto")),
        CargoContacto = reader.IsDBNull(reader.GetOrdinal("CargoContacto")) ? null : reader.GetString(reader.GetOrdinal("CargoContacto")),
        Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
        Ciudad = reader.IsDBNull(reader.GetOrdinal("Ciudad")) ? null : reader.GetString(reader.GetOrdinal("Ciudad")),
        CodigoPostal = reader.IsDBNull(reader.GetOrdinal("CodigoPostal")) ? null : reader.GetString(reader.GetOrdinal("CodigoPostal")),
        Pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? null : reader.GetString(reader.GetOrdinal("Pais")),
        Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
        Fax = reader.IsDBNull(reader.GetOrdinal("Fax")) ? null : reader.GetString(reader.GetOrdinal("Fax"))
    };
}
