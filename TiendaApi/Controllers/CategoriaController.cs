using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TiendaApi.Models;
using Microsoft.AspNetCore.Cors;
using System.Collections.Generic;

namespace TiendaApi.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase   
    {
        public readonly TiendaApi3Context _Tiendacontext;

        public CategoriaController(TiendaApi3Context _context)
        {
            _Tiendacontext = _context;
        }
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista() 
        {
            List<Categoria2> lista = new List<Categoria2>();
            try
            {
                lista = _Tiendacontext.Categoria2s.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }

        }

        [HttpGet]
        [Route("Mostrar/{IdCategoria:int}")]
        public IActionResult Mostrar(int IdCategoria)
        {
            Categoria2 objetoCategoria = _Tiendacontext.Categoria2s.Find(IdCategoria);

            if (objetoCategoria == null)
            {
                return BadRequest("Categoria no encontrada");
            }

            try
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = objetoCategoria });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = objetoCategoria });
            }

        }


        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Categoria2 objeto)
        {
            try
            {
                _Tiendacontext.Categoria2s.Add(objeto);
                _Tiendacontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
        

        [HttpPost]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Categoria2 objeto)
        {
            Categoria2 objetoCategoria = _Tiendacontext.Categoria2s.Find(objeto.IdCategoria);

            if (objetoCategoria == null)
            {
                return BadRequest("Categoría no encontrada");
            }

            try
            {
                objetoCategoria.Nombre = objeto.Nombre is null ? objetoCategoria.Nombre : objeto.Nombre;
                objetoCategoria.Descripcion = objeto.Descripcion is null ? objetoCategoria.Descripcion : objeto.Descripcion;

                _Tiendacontext.Categoria2s.Update(objetoCategoria);
                _Tiendacontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


        [HttpPost]
        [Route("Eliminar/{IdCategoria:int}")]
        public IActionResult Eliminar(int IdCategoria)
        {
            Categoria2 objetoCategoria = _Tiendacontext.Categoria2s.Find(IdCategoria);

            if (objetoCategoria == null)
            {
                return BadRequest("Categoria no encontrada");
            }

            if (objetoCategoria.Producto2s.Count > 0)
            {
                return BadRequest("Esta categoria no se puede eliminar porque tiene productos");
            }

            try
            {
                _Tiendacontext.Categoria2s.Remove(objetoCategoria);
                _Tiendacontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }
    }

}
