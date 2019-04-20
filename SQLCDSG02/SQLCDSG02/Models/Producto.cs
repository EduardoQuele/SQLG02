using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Agregar la libreria de la paqueteria que se agrego
using SQLite;

namespace SQLCDSG02.Models
{
    // Definir como publica la clase producto
    public class Producto
    {
        //Definición de los campos de la tabla producto a traves del modelo
        [PrimaryKey, AutoIncrement] // para espeficar que el id sera la clave primaria y autoincrementable
        public int Id { get; set; }
        public string NombreProd { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string ImagProd { get; set; }

        //Variable que contendra el contexto  a la BD
        private DBContext db;

        //Agregar los contructores
        public Producto()
        {

        }

        //Inicializacion del contexto de DB
        public Producto(DBContext pBaseDatos)
        {
            db = pBaseDatos;
        }

        // Funcion CRUD

        //Create o Ingresar
        public Task<bool> IngresarProducto(Producto pProducto)
        {
            try
            {
                if (db.Conexion.InsertAsync(pProducto).Result == 1)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Update o Actualizar
        public Task<bool> ActualizarRegistro(Producto pProducto)
        {
            try
            {
                if (db.Conexion.UpdateAsync(pProducto).Result == 1)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Delete o Eliminar
        public Task<bool> EliminarProducto(Producto pProducto)
        {
            try
            {
                if (db.Conexion.DeleteAsync(pProducto).Result == 1)
                {
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Read o Leer
        public Task<List<Producto>> ObtenerProducto(string pSQL)
        {
            try
            {
                return db.Conexion.QueryAsync<Producto>(pSQL);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
