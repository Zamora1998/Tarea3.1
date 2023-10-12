using ClasesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Tweb.Views
{
    public partial class Platillos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
            }
        }

        protected void btnGuardarPlatillo_Click(object sender, EventArgs e)
        {
            // Lógica para guardar el platillo en el servidor
        }
    }
}