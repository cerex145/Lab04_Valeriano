using System.Data;
using Lab04_Valeriano.Data;
using Lab04_Valeriano.Models;
using Microsoft.Data.SqlClient;

namespace Lab04_Valeriano.Repositories;

public class PedidoRepository
{
    public List<Pedido> List()
    {
        var result = new List<Pedido>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pedido_Listar", connection) { CommandType = CommandType.StoredProcedure };
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(Map(reader));
        }
        return result;
    }

    public Pedido? GetById(int pedidoId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pedido_ObtenerPorId", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@PedidoID", pedidoId);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? Map(reader) : null;
    }

    public int Insert(Pedido pedido)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pedido_Insertar", connection) { CommandType = CommandType.StoredProcedure };
        AddCommonParameters(command, pedido);
        connection.Open();
        var result = command.ExecuteScalar();
        return Convert.ToInt32(result);
    }

    public void Update(Pedido pedido)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pedido_Actualizar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
        AddCommonParameters(command, pedido);
        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int pedidoId)
    {
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_Pedido_Eliminar", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@PedidoID", pedidoId);
        connection.Open();
        command.ExecuteNonQuery();
    }

    private static void AddCommonParameters(SqlCommand command, Pedido pedido)
    {
        command.Parameters.AddWithValue("@ClienteID", (object?)pedido.ClienteID ?? DBNull.Value);
        command.Parameters.AddWithValue("@EmpleadoID", (object?)pedido.EmpleadoID ?? DBNull.Value);
        command.Parameters.AddWithValue("@FechaPedido", pedido.FechaPedido);
        command.Parameters.AddWithValue("@FechaRequerida", (object?)pedido.FechaRequerida ?? DBNull.Value);
        command.Parameters.AddWithValue("@FechaEnvio", (object?)pedido.FechaEnvio ?? DBNull.Value);
        command.Parameters.AddWithValue("@TransportistaID", (object?)pedido.TransportistaID ?? DBNull.Value);
        command.Parameters.AddWithValue("@Destinatario", (object?)pedido.Destinatario ?? DBNull.Value);
        command.Parameters.AddWithValue("@CiudadDestino", (object?)pedido.CiudadDestino ?? DBNull.Value);
        command.Parameters.AddWithValue("@PaisDestino", (object?)pedido.PaisDestino ?? DBNull.Value);
    }

    private static Pedido Map(SqlDataReader reader) => new()
    {
        PedidoID = reader.GetInt32(reader.GetOrdinal("PedidoID")),
        ClienteID = reader.IsDBNull(reader.GetOrdinal("ClienteID")) ? null : reader.GetInt32(reader.GetOrdinal("ClienteID")),
        EmpleadoID = reader.IsDBNull(reader.GetOrdinal("EmpleadoID")) ? null : reader.GetInt32(reader.GetOrdinal("EmpleadoID")),
        FechaPedido = reader.GetDateTime(reader.GetOrdinal("FechaPedido")),
        FechaRequerida = reader.IsDBNull(reader.GetOrdinal("FechaRequerida")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaRequerida")),
        FechaEnvio = reader.IsDBNull(reader.GetOrdinal("FechaEnvio")) ? null : reader.GetDateTime(reader.GetOrdinal("FechaEnvio")),
        TransportistaID = reader.IsDBNull(reader.GetOrdinal("TransportistaID")) ? null : reader.GetInt32(reader.GetOrdinal("TransportistaID")),
        Destinatario = reader.IsDBNull(reader.GetOrdinal("Destinatario")) ? null : reader.GetString(reader.GetOrdinal("Destinatario")),
        CiudadDestino = reader.IsDBNull(reader.GetOrdinal("CiudadDestino")) ? null : reader.GetString(reader.GetOrdinal("CiudadDestino")),
        PaisDestino = reader.IsDBNull(reader.GetOrdinal("PaisDestino")) ? null : reader.GetString(reader.GetOrdinal("PaisDestino"))
    };
}
