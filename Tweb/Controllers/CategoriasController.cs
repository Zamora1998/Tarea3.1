using ClasesData;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweb.Models;

namespace Tweb.Controllers
{
    public class CategoriasController : ApiController
    {
        private RestauranteEntities Categ = new RestauranteEntities();

        public CategoriasController()
        {
            Categ.Configuration.LazyLoadingEnabled = false;
            Categ.Configuration.ProxyCreationEnabled = false;
        }

        [HttpPost]
        [Route("api/Categorias/AgregarCategoria")]
        public IHttpActionResult AgregarCategoria(Categoriasclasso nuevaCategoria)
        {
            try
            {
                using (IDbConnection dbConnection = Categ.Database.Connection)  // Reemplaza con tu cadena de conexión.
                {
                    dbConnection.Open();

                    // Verifica si la categoría ya existe en la base de datos.
                    var categoriaExistente = dbConnection.Query<Categoria>("SELECT * FROM Categorias WHERE Nombre = @Nombre", new { Nombre = nuevaCategoria.Nombre }).FirstOrDefault();

                    if (categoriaExistente != null)
                    {
                        return Conflict();
                    }

                    // Utiliza Dapper para insertar la nueva categoría en la base de datos.
                    var nuevaCategoriaEntity = new Categoria
                    {
                        Nombre = nuevaCategoria.Nombre
                    };

                    dbConnection.Execute("INSERT INTO Categorias (Nombre) VALUES (@Nombre)", nuevaCategoriaEntity);
                }

                return Created(Request.RequestUri, "R");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [HttpPut]
        [Route("api/Categorias/EditarCategoria")]
        public IHttpActionResult EditarCategoria([FromUri] string nombreActual, [FromBody] Categoriasclasso categoriaEditada)
        {
            try
            {
                // Verifica si la categoría actual existe.
                var categoriaExistente = Categ.Categorias.FirstOrDefault(c => c.Nombre == nombreActual);

                if (categoriaExistente == null)
                {
                    return NotFound();
                }

                var otroCategoriaMismoNombre = Categ.Categorias.FirstOrDefault(c => c.Nombre == categoriaEditada.Nombre && c.Nombre != nombreActual);

                if (otroCategoriaMismoNombre != null)
                {
                    return Conflict();
                }

                // Actualiza el nombre de la categoría con el nuevo nombre recibido en el JSON.
                categoriaExistente.Nombre = categoriaEditada.Nombre;

                using (IDbConnection dbConnection = Categ.Database.Connection)  
                {

                    dbConnection.Open();

                    // Utiliza Dapper para ejecutar la consulta de actualización.
                    dbConnection.Execute("UPDATE Categorias SET Nombre = @Nombre WHERE CategoriaID = @CategoriaID", new { Nombre = categoriaEditada.Nombre, CategoriaID = categoriaExistente.CategoriaID });
                }

                return Ok("R");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }




        [HttpDelete]
        [Route("api/Categorias/EliminarCategoria/{id}")]
        public IHttpActionResult EliminarCategoria(int id)
        {
            try
            {
                using (IDbConnection dbConnection = Categ.Database.Connection)   // Reemplaza con tu cadena de conexión.
                {
                    dbConnection.Open();

                    var categoriaExistente = dbConnection.Query<Categoria>("SELECT * FROM Categorias WHERE CategoriaID = @CategoriaID", new { CategoriaID = id }).FirstOrDefault();

                    if (categoriaExistente == null)
                    {
                        return NotFound();
                    }

                    // Utiliza Dapper para ejecutar la consulta de eliminación.
                    dbConnection.Execute("DELETE FROM Categorias WHERE CategoriaID = @CategoriaID", new { CategoriaID = id });
                }

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [HttpGet]
        [Route("api/Categorias/ObtenerCategorias")]
        public IHttpActionResult ObtenerCategorias()
        {
            try
            {
                using (IDbConnection dbConnection = Categ.Database.Connection)   // Reemplaza con tu cadena de conexión.
                {
                    dbConnection.Open();

                    // Utiliza Dapper para ejecutar la consulta de selección.
                    var todasLasCategorias = dbConnection.Query<Categoria>("SELECT * FROM Categorias").ToList();

                    return Ok(todasLasCategorias);
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



    }
}
