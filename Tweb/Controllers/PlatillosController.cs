﻿using ClasesData;
using System;
using System.Collections.Generic;
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
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == platilloNuevo.Nombre);

                if (platilloExistente != null)
                {
                    return Conflict();
                }

                var nuevoPlatillo = new Platillo
                {
                    Nombre = platilloNuevo.Nombre,
                    Costo = platilloNuevo.Costo,
                    CategoriaID = platilloNuevo.CategoriaID,
                    IDESTADO = platilloNuevo.IDESTADO
                };

                Plati.Platillos.Add(nuevoPlatillo);
                Plati.SaveChanges();

                var requestUri = Request.RequestUri;
                var newResourceUrl = new Uri(requestUri, $"api/Platillos/RegistrarPlatillo/{nuevoPlatillo.PlatilloID}");
                return Created(newResourceUrl, "R");
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
                // Buscar el platillo por nombre
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                if (platilloExistente == null)
                {
                    return NotFound();
                }

                platilloExistente.Nombre = platilloEditado.Nombre;
                platilloExistente.Costo = platilloEditado.Costo;
                platilloExistente.CategoriaID = platilloEditado.CategoriaID;
                platilloExistente.IDESTADO = platilloEditado.IDESTADO;

                Plati.SaveChanges();

                return Ok("R");
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
                var platilloExistente = Plati.Platillos.FirstOrDefault(p => p.Nombre == nombre);

                if (platilloExistente == null)
                {
                    return NotFound();
                }

                Plati.Platillos.Remove(platilloExistente);
                Plati.SaveChanges();

                return Ok("R");
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("api/Platillos/ListarPlatillos")]
        public IHttpActionResult ListarPlatillos()
        {
            try
            {
                var platillos = Plati.Platillos.ToList();

                if (platillos.Count == 0)
                {
                    return NotFound();
                }

                return Ok(platillos);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
