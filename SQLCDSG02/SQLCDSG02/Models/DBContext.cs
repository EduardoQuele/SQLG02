using System;
using System.Collections.Generic;
using System.Text;

//Referencia hacia SQLite
using SQLite;

namespace SQLCDSG02.Models
{
    // Definir como publica la clase 
     public class DBContext
    {
        //Creamos la propiedad de conexion donde se almacenara la conexión
        public SQLiteAsyncConnection Conexion { get; set; }

        //Creamos el constructo con ctor doble tab para recibir unos parametros que solicita la ruta donde se almacenara la BD
        // Definición de constructor que recibe la ruta de la ubicación de la DB
        public DBContext(string SqlitePath)
        {
            try
            {
                //mandamos la ruta de la DB al metodo para poder realizar la conexión
                //Inicializar la propiedad enviando la ruta de la DB
                Conexion = new SQLiteAsyncConnection(SqlitePath);

                // Creación de la tabla Producto
                Conexion.CreateTableAsync<Producto>().Wait();// El wait es para que devuelva un valor los diamantes son para definir un tipo de datos
            }
            catch (Exception)
            {

                throw;
            }
        }
        

       
    }
}
