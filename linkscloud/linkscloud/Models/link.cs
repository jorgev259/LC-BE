using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace linkscloud.Models
{
    public class link
    {
        //all the code here 


        //private variables 
        private int id;
        private int id_owner;
        private string title;
        private string url;
        private string desc;

        //solving the pics problem to introduce later

        //public variables
        public int _id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public int _id_owner
        {
            get
            {
                return _id_owner;
            }
            set
            {
                _id_owner = value;
            }
        }

        public string _title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string _url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }

        public string _desc
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }


        public link(int _id_owner, string _title, string _url, string _desc)
        {
            id_owner = _id_owner;
            title = _title;
            url = _url;
            desc = _desc;
        }

        public static void new_link(link data)
        {
            var response = "";

            //starting conection 
            Coneccion cnx = new Coneccion();
            cnx.parametro();
            cnx.inicializa();
            String Consulta = "NEW_LINK";
            cnx.annadir_consulta(Consulta);
            cnx.annadir_parametro("id_owner", data.id_owner);
            cnx.annadir_parametro("title", data.title);
            cnx.annadir_parametro("url", data.url);
            cnx.annadir_parametro("desc", data.desc);

            MySqlDataReader Container;
            try
            {
                Container = cnx.busca();
                Container.Close();
                response = "Complete";

            } //If it fails it will capture the exception, e.Number returns the error number indetifier
            catch (MySqlException e)
            {
                switch (e.Number) //Add case for each e.Number
                {
                    case 1062: //Needs checking with e.Message to know if its email or username that's wrong
                        var split = e.Message.Split(new string[] { "key " }, StringSplitOptions.None);
                        response = split[split.Length - 1] + " already exists";
                        break;
                    default:
                        response = "Unexpected Error, try again later";
                        break;
                }
            }


            cnx.conexion.Close();
            cnx.conexion.Dispose();


        } 





    }
}