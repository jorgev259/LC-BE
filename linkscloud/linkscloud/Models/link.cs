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
        public int id;
        public int id_owner;
        public string title;
        public string url;
        public string desc;

        //solving the pics problem to introduce later


        public link(int id_owner, string title, string url, string desc)
        {
            this.id_owner = id_owner;
            this.title = title;
            this.url = url;
            this.desc = desc;
        }

        public static void new_link()
        {
            //starting conection 
            Coneccion cnx = new Coneccion();
            cnx.parametro();
            cnx.inicializa();
            String Consulta = "NEW_LINK";
            cnx.annadir_consulta(Consulta);
            cnx.annadir_parametro("id_owner", this.id_owner);
            cnx.annadir_parametro("title", this.title);
            cnx.annadir_parametro("url", this.url);
            cnx.annadir_parametro("desc", this.desc);

            MySqlDataReader Container;
            try
            {
                Container = cnx.busca();
                while (Container.Read())
                {
                    this.id = Convert.ToInt32(Container["id"]);
                    this.id_owner = Convert.ToInt32(Container["id_owner"]);
                    this.title = CONTENEDOR["title"].ToString();
                    this.url = CONTENEDOR["url"].ToString();
                    this.desc = CONTENEDOR["desc"].ToString();
                }

                Container.Close();

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