using ClasesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using System.Data;
using System.Data.Entity;

namespace Tweb.Controllers
{
    public class EstadoController : ApiController
    {
        private RestauranteEntities Estado = new RestauranteEntities();

        public EstadoController()
        {
            Estado.Configuration.LazyLoadingEnabled = false;
            Estado.Configuration.ProxyCreationEnabled = false;
        }

        [HttpGet]
        [Route("api/Estados/ObtenerEstados")]
        public IHttpActionResult ObtenerEstados()
        {
            try
            {
                using (IDbConnection dbConnection = Estado.Database.Connection)
                {
                    dbConnection.Open();
                    var todosLosEstados = dbConnection.Query<Estado>("SELECT * FROM Estado").ToList();
                    return Ok(todosLosEstados);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
