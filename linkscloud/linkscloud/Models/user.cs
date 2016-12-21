using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int _id { get { return id; } set { id = value; } }
        public string _username { get { return username; } set { username = value; } }
        public string _email { get { return email; } set { email = value; } }
        public string _password { get { return password; } set { password = value; } }
        public DateTime _joined { get { return joined; } set { joined = value; } }

        public user(string _username, string _email, string _password)
        {
            username = _username;
            password = _password;
            email = _email;
            joined = DateTime.Now;
        }

        public static String add_user(user data)
        {
            String response = "";

            Coneccion conx_detalles = new Coneccion();

            conx_detalles.parametro();
            conx_detalles.inicializa();            

            string CONSULTA;
            MySqlDataReader CONTENEDOR;

            CONSULTA = "ADD_USER";
            conx_detalles.annadir_consulta(CONSULTA);
            conx_detalles.annadir_parametro("_username", data.username);
            conx_detalles.annadir_parametro("_email", data.email);
            conx_detalles.annadir_parametro("_password", data.password);            
            conx_detalles.annadir_parametro("_joined", DateTime.Now);

            //This set will try to execute the query
            try
            {
                CONTENEDOR = conx_detalles.busca();                
                CONTENEDOR.Close();
                response = "Complete";
            } //If it fails it will capture the exception, e.Number returns the error number indetifier
            catch (MySqlException e)
            {
                switch (e.Number) //Add case for each e.Number
                {
                    case 1062: //Needs checking with e.Message to know if its email or username that's wrong
                        var split = e.Message.Split(new string[] { "key " }, StringSplitOptions.None);
                        response = split[split.Length-1] + " already exists";
                        break;
                    default:
                        response = "Unexpected Error, try again later";
                        break;
                }
            }
            
            conx_detalles.conexion.Close();
            conx_detalles.conexion.Dispose();
            
            return response;
        }


        public static user info_user(string criteria,Object _parametro)
        {
            user user = new user("","","");
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
                user.password = CONTENEDOR["passkey"].ToString();
                user.joined = Convert.ToDateTime(CONTENEDOR["joined"]);
            }
            cnx.conexion.Close();
            cnx.conexion.Dispose();
            CONTENEDOR.Close();

            return user;
        }

        public static String login(String email,String pass)
        {
            var cnx = new Coneccion();
            cnx.parametro();
            cnx.inicializa();

            MySqlCommand cmd = cnx.conexion.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE email=@email AND password=@pass LIMIT 1;";
            cmd.Parameters.AddWithValue("@email",email);
            cmd.Parameters.AddWithValue("@pass",pass);

            MySqlDataReader dataReader = cmd.ExecuteReader();


            if (dataReader.HasRows)
            {
                dataReader.Close();
                return "logged";
            }
            else
            {
                dataReader.Close();
                //No login
                cmd.CommandText = "SELECT * FROM users WHERE email=@email LIMIT 1;";
                MySqlDataReader dataReader2 = cmd.ExecuteReader();

                 if (dataReader2.HasRows)
                 {
                   return "Wrong Password";
                 }
                 else
                 {      
                   return "Non-valid user";
                 }
             }
             if(dataReader.IsClosed == false)
             {
                        dataReader.Close();
             }
        }
    }
}