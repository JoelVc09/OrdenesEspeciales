using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace OrdenesEspeciales
{
    public static class ConexionODBC
    {
        public static OdbcConnection connection;


        public static void Conectar(string usuario, string clave)
        {
            string connectionString = $"DSN=CENTRAL;UID={usuario};PWD={clave};";
            connection = new OdbcConnection(connectionString);
            connection.Open();
        }










        public static void CerrarConexion()
        {
            // Cerrar la conexión si está abierta
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            //connection.Close();
        }

    }
}

