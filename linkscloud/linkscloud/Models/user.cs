using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace linkscloud.Models
{
    public class user
    {//startup
        private int id;
        private string username;
        private string email;
        private string password;
        private DateTime joined;

        public user() { }

        public int _id { get { return id; } set { id = value; } }
        public string _username { get { return username; } set { username = value; } }
        public string _email { get { return email; } set { email = value; } }
        public string _password { get { return password; } set { password = value; } }
        public DateTime _joined { get { return joined; } set { joined = value; } }

        public static String add_user(string username, string email, string password)
        {
            String response = "";

            Coneccion conx_detalles = new Coneccion();

            conx_detalles.parametro();
            conx_detalles.inicializa();            

            string CONSULTA;
            MySqlDataReader CONTENEDOR;

            CONSULTA = "ADD_USER";
            conx_detalles.annadir_consulta(CONSULTA);
            conx_detalles.annadir_parametro("_username", username);
            conx_detalles.annadir_parametro("_email", email);
            conx_detalles.annadir_parametro("_password", password);            
            conx_detalles.annadir_parametro("_joined", DateTime.Now);

            //This set will try to execute the query
            try
            {
                CONTENEDOR = conx_detalles.busca();
                while (CONTENEDOR.Read())
                {
                    response = "Complete";
                }
                
                CONTENEDOR.Close();
            } //If it fails it will capture the exception, e.Number returns the error number indetifier
            catch (MySqlException e)
            {
                switch (e.Number) //Add case for each e.Number
                {
                    case 1062: //Neds checking with e.Error to know if its email or username that's wrong
                        response = "Username or email already exists";
                        break;
                    default:
                        response = "Unexpected Error";
                        break;
                }
            }
            
            conx_detalles.conexion.Close();
            conx_detalles.conexion.Dispose();
            
            return response;
        }

        //public static int eliminar_Bebidas(int Cod_bebida)
        //{
        //    int respuesta = 0;

        //    Coneccion conx_detalles = new Coneccion();
        //    conx_detalles.parametro();
        //    conx_detalles.inicializa();
        //    string CONSULTA;
        //   MySqlDataReader CONTENEDOR;

        //    CONSULTA = "EXEC ELIMINAR_BEBIDAS ?";
        //    conx_detalles.annadir_consulta(CONSULTA);
        //    conx_detalles.annadir_parametro(Cod_bebida.ToString(), 1);
        //    CONTENEDOR = conx_detalles.busca();
        //    while (CONTENEDOR.Read())
        //    {
        //        respuesta = Convert.ToInt32(CONTENEDOR[0].ToString());
        //    }
        //    conx_detalles.conexion.Close();
        //    conx_detalles.conexion.Dispose();
        //    CONTENEDOR.Close();
        //    return respuesta;
        //}

        //public static int modificar_Bebidas(string username, string email, string password)
        //{
        //    int respuesta = 0;

        //    Coneccion conx_detalles = new Coneccion();
        //    conx_detalles.parametro();
        //    conx_detalles.inicializa();
        //    string CONSULTA;
        //   MySqlDataReader CONTENEDOR;

        //    CONSULTA = "EXEC MODIFICAR_BEBIDAS ?,?,?,?,?,?,?,?";
        //    conx_detalles.annadir_consulta(CONSULTA);
        //    //conx_detalles.annadir_parametro(Cod_bebida, 1);
        //    //conx_detalles.annadir_parametro(Nombre, 2);
        //    //conx_detalles.annadir_parametro(ingredientes, 2);
        //    //conx_detalles.annadir_parametro(precio_ind, 1);
        //    //conx_detalles.annadir_parametro(precio_por, 1);
        //    //conx_detalles.annadir_parametro(Precio_T_ind, 1);
        //    //conx_detalles.annadir_parametro(Precio_T_porc, 1);
        //    //conx_detalles.annadir_parametro(foto, 2);
        //    CONTENEDOR = conx_detalles.busca();
        //    while (CONTENEDOR.Read())
        //    {
        //        respuesta = Convert.ToInt32(CONTENEDOR[0].ToString());
        //    }
        //    conx_detalles.conexion.Close();
        //    conx_detalles.conexion.Dispose();
        //    CONTENEDOR.Close();
        //    return respuesta;
        //}

        public static user info_user(string criteria,Object _parametro)
        {
            user user = new user();
            Coneccion cnx = new Coneccion();
            cnx.parametro();
            cnx.inicializa();
            String CONSULTA = "";
            MySqlDataReader CONTENEDOR;
            switch (criteria)
            {
                case "id":
                    CONSULTA = "USERBYID";
                    cnx.annadir_parametro("_id", _parametro.ToString());
                    break;
                case "username":
                    CONSULTA = "USERBYNAME";
                    cnx.annadir_parametro("_user", _parametro.ToString());
                    break;
            }

            cnx.annadir_consulta(CONSULTA);
            
            CONTENEDOR = cnx.busca();
            while (CONTENEDOR.Read())
            {
                user.id = Convert.ToInt32(CONTENEDOR["id"]);
                user.email = CONTENEDOR["email"].ToString();
                user.username = CONTENEDOR["username"].ToString();
                user.password = CONTENEDOR["password"].ToString();
                user.joined = Convert.ToDateTime(CONTENEDOR["joined"]);

                //try
                //{
                //    byte[] bytes = Convert.FromBase64String(CONTENEDOR["FOTO"].ToString());
                //    MemoryStream memory_foto = new MemoryStream(bytes);
                //    Bitmap bmp = (Bitmap)Bitmap.FromStream(memory_foto);

                //    NuevaBebida.foto = bmp;
                //    memory_foto.Close();
                //}
                //catch (System.FormatException h)
                //{
                //    NuevaBebida.foto = null;
                //}
            }
            cnx.conexion.Close();
            cnx.conexion.Dispose();
            CONTENEDOR.Close();

            return user;
        }
    }
}