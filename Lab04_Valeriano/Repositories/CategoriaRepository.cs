using System.Data;
using Lab04_Valeriano.Data;
using Lab04_Valeriano.Models;
using Microsoft.Data.SqlClient;

namespace Lab04_Valeriano.Repositories;

public class CategoriaRepository
{
    public List<Categoria> List()
    {
        var result = new List<Categoria>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Categoria_Listar", connection) { CommandType = CommandType.StoredProcedure };
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    public Categoria? GetById(int categoriaId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Categoria_ObtenerPorId", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@CategoriaID", categoriaId);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public int Insert(Categoria categoria)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Categoria_Insertar", connection) { CommandType = CommandType.StoredProcedure };
        AddCommonParameters(command, categoria);
        connection.Open();
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public void Update(Categoria categoria)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Categoria_Actualizar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@CategoriaID", categoria.CategoriaID);
        AddCommonParameters(command, categoria);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int categoriaId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Categoria_Eliminar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@CategoriaID", categoriaId);
        connection.Open();
        command.ExecuteNonQuery();
    }

    private static void AddCommonParameters(SqlCommand command, Categoria categoria)
    {
        command.Parameters.AddWithValue("@NombreCategoria", categoria.NombreCategoria);
        command.Parameters.AddWithValue("@Descripcion", (object?)categoria.Descripcion ?? DBNull.Value);
    }

    private static Categoria Map(SqlDataReader reader) => new()
    {
        CategoriaID = reader.GetInt32(reader.GetOrdinal("CategoriaID")),
        NombreCategoria = reader.GetString(reader.GetOrdinal("NombreCategoria")),
        Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString(reader.GetOrdinal("Descripcion"))
    };
}
