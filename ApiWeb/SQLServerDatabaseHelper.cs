using Microsoft.Data.SqlClient;

namespace ApiWeb
{
    public class SQLServerDatabaseHelper
    {
        private SqlConnection connection;
        private string connectionString;
        private SqlCommand command;
        private SqlDataReader reader;
        public SQLServerDatabaseHelper(string cadenaConexion)
        {
            connectionString = cadenaConexion;
        }
        public async Task<Response> ExtraeVueloMayor()
        {
            connection = new SqlConnection(connectionString);
            Response response = new Response();
            string query = "EXEC [dbo].[Ciudades_Mas_Visitadas]";
            using (connection)
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                reader = await command.ExecuteReaderAsync();
                try
                {
                    while (reader.Read())
                    {
                        response.Ciudad = reader["Ciudad"].ToString();
                        response.Cantidad = int.Parse(reader["Cantidad"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    reader.Close();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return response;
        }
        public async Task<Response> ExtraeVueloMenor()
        {
            connection = new SqlConnection(connectionString);
            Response response = new Response();
            string query = "EXEC [dbo].[Ciudades_Menos_Visitadas]";
            using (connection)
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                reader = await command.ExecuteReaderAsync();
                try
                {
                    while (reader.Read())
                    {
                        response.Ciudad = reader["Ciudad"].ToString();
                        response.Cantidad = int.Parse(reader["Cantidad"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    reader.Close();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return response;
        }
        public int? ExtraeHoras(long id)
        {
            connection = new SqlConnection(connectionString);
            int? Horas=0;
            string query = "Horas_Vuelo";
            using (connection)
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Avion", id);
                reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                       
                        Horas = int.Parse(reader["Horas"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    reader.Close();
                    connection.Dispose();
                    connection.Close();
                }
            }
            return Horas;
        }


    }
}
