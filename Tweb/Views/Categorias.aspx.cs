using ClasesData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Tweb.Views
{
    public partial class Categorias : Page
    {

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarModal", "$('#miModal').modal('show');", true);

        }
        protected void btnvalidarInput(object sender, EventArgs e)
        {
   

            string nombre = txtInputregistrar.Text;
            EnviarCategoriaAlAPI(nombre);

        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<Categoria> listaDeCategorias = await ObtenerCategoriasDesdeAPI();
                listaDeCategorias.Sort((a, b) => string.Compare(a.Nombre, b.Nombre));

                foreach (var categoria in listaDeCategorias)
                {
                    TableRow row = new TableRow();
                    TableCell cellId = new TableCell();
                    TableCell cellNombre = new TableCell();
                    TableCell cellAcciones = new TableCell();

                    cellId.Text = categoria.CategoriaID.ToString();
                    cellNombre.Text = categoria.Nombre;

                    // Agrega un botón de "Acciones" con un menú desplegable
                    Button accionesButton = new Button();
                    accionesButton.Text = "Acciones";
                    accionesButton.CssClass = "acciones-btn";
                    accionesButton.Attributes.Add("onclick", $"mostrarMenuDesplegable({categoria.CategoriaID})");

                    // Crea el menú desplegable
                    var dropdownDiv = new HtmlGenericControl("div");
                    dropdownDiv.Attributes.Add("class", "dropdown-content");

                    // Agrega el botón "Modificar" al menú desplegable
                    var modificarButton = new Button();
                    modificarButton.Text = "Modificar";
                    modificarButton.Attributes.Add("onclick", $"editarCategoria({categoria.CategoriaID})");
                    dropdownDiv.Controls.Add(modificarButton);

                    // Agrega el botón "Eliminar" al menú desplegable
                    var eliminarButton = new Button();
                    eliminarButton.Text = "Eliminar";
                    eliminarButton.Attributes.Add("onclick", $"eliminarCategoria({categoria.CategoriaID})");
                    dropdownDiv.Controls.Add(eliminarButton);

                    // Agrega el menú desplegable al cell de "Acciones"
                    cellAcciones.Controls.Add(accionesButton);
                    cellAcciones.Controls.Add(dropdownDiv);

                    row.Cells.Add(cellId);
                    row.Cells.Add(cellNombre);
                    row.Cells.Add(cellAcciones);

                    tablaCategorias.Rows.Add(row);
                }

            }
        }

        private HttpStatusCode EnviarCategoriaAlAPI(string nombre)
        {
            try
            {
                string apiUrl = "http://localhost:50912/api/Categorias/AgregarCategoria";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "POST";
                request.ContentType = "application/json";
                byte[] data = Encoding.UTF8.GetBytes("{\"Nombre\":\"" + nombre + "\"}");
                request.ContentLength = data.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar la categoría al API: " + ex.Message);
                return HttpStatusCode.InternalServerError; 
            }
        }


        private async Task<List<Categoria>> ObtenerCategoriasDesdeAPI()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "http://localhost:50912/api/Categorias/ObtenerCategorias";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        List<Categoria> categorias = await response.Content.ReadAsAsync<List<Categoria>>();
                        return categorias;
                    }
                    else
                    {
                        return new List<Categoria>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<Categoria>();
            }
        }
    }
}
