using System.Data;
using Lab04_Valeriano.Data;
using Lab04_Valeriano.Models;
using Microsoft.Data.SqlClient;

namespace Lab04_Valeriano.Repositories;

public class DetallePedidoRepository
{
    public List<DetallePedidoReporte> ListarPorFechas(DateTime desde, DateTime hasta)
    {
        var result = new List<DetallePedidoReporte>();
        using var connection = DbConnectionFactory.CreateConnection();
        using var command = new SqlCommand("sp_DetallePedido_ListarPorFechas", connection) { CommandType = CommandType.StoredProcedure };
        command.Parameters.AddWithValue("@FechaInicio", desde.Date);
        command.Parameters.AddWithValue("@FechaFin", hasta.Date);
        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new DetallePedidoReporte
            {
                PedidoID = reader.GetInt32(reader.GetOrdinal("PedidoID")),
                FechaPedido = reader.GetDateTime(reader.GetOrdinal("FechaPedido")),
                ProductoID = reader.GetInt32(reader.GetOrdinal("ProductoID")),
                PrecioUnidad = reader.GetDecimal(reader.GetOrdinal("PrecioUnidad")),
                Cantidad = reader.GetInt16(reader.GetOrdinal("Cantidad")),
                Descuento = reader.GetDecimal(reader.GetOrdinal("Descuento")),
                Subtotal = reader.GetDecimal(reader.GetOrdinal("Subtotal"))
            });
        }
        return result;
    }
}
