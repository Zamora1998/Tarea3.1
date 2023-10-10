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
                    row.CssClass = "align-middle";
                    TableCell cellId = new TableCell();
                    cellId.CssClass = "text-center"; 
                    cellId.Text = categoria.CategoriaID.ToString();
                    TableCell cellNombre = new TableCell();
                    cellNombre.CssClass = "text-center";
                    cellNombre.Text = categoria.Nombre;
                    TableCell cellAcciones = new TableCell();
                    row.Cells.Add(cellId);
                    row.Cells.Add(cellNombre);
                    row.Cells.Add(cellAcciones);

                    tablaCategorias.Rows.Add(row);
                }

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
