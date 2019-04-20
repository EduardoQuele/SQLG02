using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

// Librerias
using System.Collections.ObjectModel;
using System.IO;
using SQLCDSG02.Views;
using SQLCDSG02.Models;

namespace SQLCDSG02
{
    public partial class MainPage : ContentPage
    {
        // variables a utilizar
        private string ubicacion = "";
        private string nombreBD = "Inventario.db3";
        private DBContext db;

        private ObservableCollection<Producto> _ListaProductos; // Colección para cargar la ListView

        public MainPage()
        {
            InitializeComponent();

            // Inicialización de variables
            ubicacion = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),nombreBD);

            db = new DBContext(ubicacion);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarProductos();
        }

        private async void NaveAddProducto_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProducto());
        }

        // Método para cargar productos
        private async void CargarProductos()
        {
            try
            {
                // crear una instancia de Producto
                Producto _Productos = new Producto(db);

                // Obtengo todos los productos de la tabla
                var _obtenerLista = _Productos.ObtenerProducto("Select * from [Producto]").Result;

                if (_obtenerLista != null || _obtenerLista.Count != 0)
                {
                    _ListaProductos = new ObservableCollection<Producto>(_obtenerLista);

                    ListaProductos.ItemsSource = _ListaProductos;
                }
            }
            catch (Exception)
            {

                await DisplayAlert("Error","No se pudo cargar la lista de productos", "Aceptar");
            }
        }

        private async void MenuEliminar(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos toda la información del objeto sender y lo convertimos en MenuItem
                var opcion = (MenuItem)sender; // Forzamos a que lo que capturemos se comporte como un menuitem

                // Extraemos la información del MenuItem y la convertimos en Producto
                var _Producto = (Producto)opcion.CommandParameter; // lo forzamos con el casteo y que sea un producto porque se obtiene la informacion pero no esta bien definido el objeto

                bool opcionEliminar = await DisplayAlert("Advertencia", "¿Está seguro de eliminar el elemento?", "Si", "No"); // Se utiliza el display alert para darle al usuario las opciones de proceder con la eliminarción o cancelar la eliminación

                if (opcionEliminar)
                {
                    // Instancia de Producto
                    Producto _eliminarProducto = new Producto(db);

                    // Ejecutamos la función eliminar
                    var resultado = _eliminarProducto.EliminarProducto(_Producto).Result;

                    // Chequamos si el producto fue eliminado con exito y recargamos la lista
                    if (resultado)
                    {
                        await DisplayAlert("Exito", "El producto fue eliminado", "Aceptar");
                        CargarProductos();
                    }
                    else
                    {
                        await DisplayAlert("Error", "El producto no fue eliminado", "Aceptar");
                    }
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Ocurrio un problema, no se puede eliminar el producto", "Ok");
            }
            
        }

        private async void MenuModificar(object sender, EventArgs e)
        {
            try
            {
                var opcion = (MenuItem)sender;
                var _producto = opcion.CommandParameter;

                // Creamos una instancia de la página modificar
                var _modProducto = new ModProducto();

                // Establecemos el enlace a datos
                _modProducto.BindingContext = _producto;

                await Navigation.PushModalAsync(_modProducto); //
            }
            catch (Exception)
            {
                await DisplayAlert("Error","Ocurrio un problema, no se puede modificar el registro","Aceptar");
            }
        }
    }
}
