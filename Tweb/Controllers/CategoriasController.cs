using ClasesData;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public IHttpActionResult AgregarCategoria(Categoriasclass nuevaCategoria)
        {
            try
            {
                var categoriaExistente = Categ.Categorias.FirstOrDefault(c => c.Nombre == nuevaCategoria.Nombre);

                if (categoriaExistente != null)
                {
                    return Conflict();
                }
                var nuevaCategoriaEntity = new Categoria
                {
                    Nombre = nuevaCategoria.Nombre
                };

                Categ.Categorias.Add(nuevaCategoriaEntity);
                Categ.SaveChanges();
                return Created(Request.RequestUri, "R");
            }
            catch (DbException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/Categorias/EditarCategoria/{nombre}")]
        public IHttpActionResult EditarCategoria(string nombre, Categoria categoriaEditada)
        {
            try
            {
                var categoriaExistente = Categ.Categorias.FirstOrDefault(c => c.Nombre == nombre);

                if (categoriaExistente == null)
                {
                    return NotFound();
                }
                var otraCategoriaMismoNombre = Categ.Categorias.FirstOrDefault(c => c.Nombre == categoriaEditada.Nombre && c.Nombre != nombre);

                if (otraCategoriaMismoNombre != null)
                {
                    return Conflict();
                }

                categoriaExistente.Nombre = categoriaEditada.Nombre;

                Categ.SaveChanges();

                return Ok("R");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("api/Categorias/EliminarCategoria/{nombre}")]
        public IHttpActionResult EliminarCategoria(string nombre)
        {
            try
            {
                var categoriaExistente = Categ.Categorias.FirstOrDefault(c => c.Nombre == nombre);

                if (categoriaExistente == null)
                {
                    return NotFound();
                }

                Categ.Categorias.Remove(categoriaExistente);
                Categ.SaveChanges();

                return Ok("R");
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
                var todasLasCategorias = Categ.Categorias.ToList();
                return Ok(todasLasCategorias);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


    }
}
