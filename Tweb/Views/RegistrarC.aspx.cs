using System;
using System.IO;
using System.Net;
using System.Text;

namespace Tweb.Views
{
    public partial class RegistrarC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nombre = txtCategoria.Text;
            EnviarCategoriaAlAPI(nombre);
        }
        private void EnviarCategoriaAlAPI(string nombre)
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
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    Console.WriteLine("La categoría se envió con éxito al API.");
                    lblMensaje.Text = "La categoría se envió con éxito al API.";
                }
                else
                {
                    Console.WriteLine("Hubo un error al enviar la categoría al API.");
                    lblMensaje.Text = "Hubo un error al enviar la categoría al API.";
                }

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string responseContent = reader.ReadToEnd();
                    Console.WriteLine("Respuesta del API: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar la categoría al API: " + ex.Message);
                lblMensaje.Text = "Error al enviar la categoría al API: " + ex.Message;
            }
        }
    }
}
