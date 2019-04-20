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
    public partial class ModProducto : ContentPage
    {
        // Lista que alimenta el picker
        private List<string> _listaMarca;
        private string nombreBD = "Inventario.db3";
        private DBContext db;

        private string ubicacion = "";

        public ModProducto ()
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

        private async void BtnCancelar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Producto _actualizarProducto = new Producto(db);
                _actualizarProducto.Id = Int32.Parse(lblId.Text);
                _actualizarProducto.NombreProd = txtProducto.Text;
                _actualizarProducto.Marca = (string)ListaMarca.SelectedItem;
                _actualizarProducto.Precio = double.Parse(txtPrecio.Text);
                _actualizarProducto.Descripcion = txtDescripcion.Text;
                _actualizarProducto.ImagProd = "Item.png";

                var resultado = _actualizarProducto.ActualizarRegistro(_actualizarProducto).Result;

                if (resultado)
                {
                    await DisplayAlert("Exito","El registro fue módificado","Aceptar");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error","El registro no fue módificado","Aceptar");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}