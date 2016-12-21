using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace linkscloud.Models
{
    public class Coneccion
    {
        public MySqlConnection conexion;
        public MySqlCommand comando;

        public void parametro()
        {
            conexion = new MySqlConnection();
            comando = new MySqlCommand();
            //strconexion = "Provider=SQLOLEDB;Data Source=scopecedes.database.windows.net;Initial Catalog=ScopeCedes;PersisSecurityInfo=True;User ID=scope;Password=Cedes123;Poling=False";
            conexion.ConnectionString = "Database=linksclouds;Data Source=us-cdbr-azure-central-a.cloudapp.net;User Id=b7fe8bcdfc29e8;Password=e464963d";
            //conexion.ConnectionString = "server=127.0.0.1;user id=root;database=linkscloud;Password=adiviname";
        }


        public bool inicializa()
        {
            //conexion = new OleDbConnection(strconexion);
            try
            {
                conexion.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public MySqlDataReader busca()
        {
            MySqlDataReader busca_int;
            comando.Prepare();
            busca_int = comando.ExecuteReader();
            comando.CommandTimeout = 0;
            return busca_int;
            conexion.Close();
            conexion.Dispose();
        }
        
        
        public bool annadir_consulta(String _Consulta)
        {
            //comando = new MySqlCommand(_Consulta, conexion);
            comando.Connection = conexion;
            comando.CommandText = _Consulta;
            comando.CommandType = CommandType.StoredProcedure;
            return false;
        }

        public bool annadir_parametro(String _INPUT, Object _PARAMETRO)
        {
            comando.Parameters.AddWithValue("@" + _INPUT, _PARAMETRO);
            comando.Parameters["@" + _INPUT].Direction = ParameterDirection.Input;
            return false;
        }
    }
}