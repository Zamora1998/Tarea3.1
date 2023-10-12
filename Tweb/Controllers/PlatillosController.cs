using ClasesData;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweb.Models;

namespace Tweb.Controllers
{
    public class PlatillosController : ApiController
    {
        private RestauranteEntities Plati = new RestauranteEntities();

        public PlatillosController()
        {
            Plati.Configuration.LazyLoadingEnabled = false;
            Plati.Configuration.ProxyCreationEnabled = false;
        }

        [HttpPost]
        [Route("api/Platillos/RegistrarPlatillo")]
        public IHttpActionResult RegistrarPlatillo(Platilloclass platilloNuevo)
        {
            try
            {
                using (IDbConnection connection = Plati.Database.Connection) // Reemplaza con tu cadena de conexión.
                {
                    connection.Open();

                    // Verifica si el platillo ya existe en la base de datos.
                    var platilloExistente = connection.Query<Platillo>("SELECT * FROM Platillos WHERE Nombre = @Nombre", new { platilloNuevo.Nombre }).FirstOrDefault();

                    if (platilloExistente != null)
                    {
                        return Conflict();
                    }

                    // Utiliza Dapper para insertar el nuevo platillo en la base de datos.
                    var nuevoPlatillo = new Platillo
                    {
                        Nombre = platilloNuevo.Nombre,
                        Costo = platilloNuevo.Costo,
                        CategoriaID = platilloNuevo.CategoriaID,
                        IDESTADO = platilloNuevo.IDESTADO
                    };

                    // Realiza la inserción en la base de datos.
                    connection.Execute("INSERT INTO Platillos (Nombre, Costo, CategoriaID, IDESTADO) VALUES (@Nombre, @Costo, @CategoriaID, @IDESTADO)", nuevoPlatillo);

                    // Obtiene la ID del nuevo platillo insertado.
                    var nuevaId = connection.ExecuteScalar<int>("SELECT SCOPE_IDENTITY()");

                    var requestUri = Request.RequestUri;
                    var newResourceUrl = new Uri(requestUri, $"api/Platillos/RegistrarPlatillo/{nuevaId}");
                    return Created(newResourceUrl, "R");
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/Platillos/EditarPlatillo/{nombre}")]
        public IHttpActionResult EditarPlatillo(string nombre, Platillo platilloEditado)
        {
            try
            {
                using (IDbConnection connection = Plati.Database.Connection) // Reemplaza con tu cadena de conexión.
                {
                    connection.Open();

                    // Busca el platillo por nombre.
                    var platilloExistente = connection.Query<Platillo>("SELECT * FROM Platillos WHERE Nombre = @Nombre", new { Nombre = nombre }).FirstOrDefault();

                    if (platilloExistente == null)
                    {
                        return NotFound();
                    }

                    // Actualiza los datos del platillo con los nuevos valores recibidos.
                    platilloExistente.Nombre = platilloEditado.Nombre;
                    platilloExistente.Costo = platilloEditado.Costo;
                    platilloExistente.CategoriaID = platilloEditado.CategoriaID;
                    platilloExistente.IDESTADO = platilloEditado.IDESTADO;

                    // Utiliza Dapper para ejecutar la consulta de actualización.
                    connection.Execute("UPDATE Platillos SET Nombre = @Nombre, Costo = @Costo, CategoriaID = @CategoriaID, IDESTADO = @IDESTADO WHERE PlatilloID = @PlatilloID", platilloExistente);

                    return Ok("R");
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/Platillos/EliminarPlatillo/{nombre}")]
        public IHttpActionResult EliminarPlatillo(string nombre)
        {
            try
            {
                using (IDbConnection connection = Plati.Database.Connection) // Reemplaza con tu cadena de conexión.
                {
                    connection.Open();

                    // Busca el platillo por nombre.
                    var platilloExistente = connection.Query<Platillo>("SELECT * FROM Platillos WHERE Nombre = @Nombre", new { Nombre = nombre }).FirstOrDefault();

                    if (platilloExistente == null)
                    {
                        return NotFound();
                    }

                    // Utiliza Dapper para ejecutar la consulta de eliminación.
                    connection.Execute("DELETE FROM Platillos WHERE PlatilloID = @PlatilloID", new { PlatilloID = platilloExistente.PlatilloID });
                }

                return Ok("R");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
            [Route("api/Platillos/ListarPlatillos")]
            // En el controlador de Platillos
            public IHttpActionResult ListarPlatillos()
            {
                try
                {
                    var platillos = from p in Plati.Platillos
                                    join c in Plati.Categorias on p.CategoriaID equals c.CategoriaID
                                    join e in Plati.Estadoes on p.IDESTADO equals e.EstadoID
                                    select new
                                    {
                                        PlatilloID = p.PlatilloID,
                                        Nombre = p.Nombre,
                                        Costo = p.Costo,
                                        CategoriaNombre = c.Nombre,
                                        EstadoDescripcion = e.Descripcion
                                    };

                    return Ok(platillos);
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }




    }
}
