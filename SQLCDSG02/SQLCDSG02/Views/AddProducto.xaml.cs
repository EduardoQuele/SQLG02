using SQLCDSG02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SQLCDSG02.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddProducto : ContentPage
	{
        // Variables a utilizar
        private string ubicacion = "";
        private string nombreBD = "Inventario.db3";
        private DBContext db;
 
        // Creamos una lista para utilizarla
        // Lista que alimenta el picker
        private List<string> _listaMarca;

		public AddProducto ()
		{
            try
            {
                InitializeComponent();

                // Inicialización de variables a utilizar
                ubicacion = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), nombreBD); // Obtener la ruta de la BD //Buscador de archivo local -> Environment ambiente

                // Inicializar el contexto
                db = new DBContext(ubicacion); // Le mandamos la ubicacion de la bd

                // Definimos valores para la lista
                // Inicializar la lista con valores
                _listaMarca = new List<string>()
            {
                "Nike",
                "Adidas",
                "Reebok",
                "Jordan",
                "Lebron"
            };

                // Cargamos valores a la lista
                // Se alimenta la lista
                ListaMarca.ItemsSource = _listaMarca;
            }
            catch (Exception)
            {

                throw;
            }
            
		}

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Creamos una instancia de producto para obtener los metodos de producto
                Producto _nuevoProducto = new Producto(db);

                // Asignar valores a los atributos del objeto
                _nuevoProducto.NombreProd = txtProducto.Text.Trim(); // Trim para eliminar los espacio al incio y al final
                _nuevoProducto.Descripcion = txtDescripcion.Text.Trim();
                _nuevoProducto.Precio = double.Parse(txtPrecio.Text);
                _nuevoProducto.ImagProd = "item.jpg";
                _nuevoProducto.Marca = (string)ListaMarca.SelectedItem;// (string) casteo de datos se obliga o se fuerza a que sea un tipo de dato especifico

                // Se procede a la inserción
                bool resultado = _nuevoProducto.IngresarProducto(_nuevoProducto).Result;

                // Chequeamos si se ingreso el dato correctamente
                if (resultado)
                {
                    await DisplayAlert("Exito", "El producto fue ingresado con exito", "Aceptar");

                    // Se limpian los entry depues de agregado el registro
                    txtProducto.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                    txtDescripcion.Text = string.Empty;
                }
                else
                {
                    await DisplayAlert("Error", "El producto  no fue ingresado", "Aceptar");
                }
            }
            catch (Exception )
            {

                throw;
            }
            
        } 
    }
}