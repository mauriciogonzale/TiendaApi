using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TiendaApi.Models;
using Microsoft.AspNetCore.Cors;

namespace TiendaApi.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly TiendaApi3Context _Tiendacontext;

        public ProductoController(TiendaApi3Context _context)
        {
            _Tiendacontext = _context;
        }
        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto2> lista = new List<Producto2>();
                try
            {
                lista = _Tiendacontext.Producto2s.Include(c=>c.objetoCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje ="ok", response=lista});

            }catch (Exception ex) {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
            
        }

        [HttpGet]
        [Route("Mostrar/{IdProducto:int}")]
        public IActionResult Mostrar(int IdProducto)
        {
            Producto2 objetoProducto = _Tiendacontext.Producto2s.Find(IdProducto);

            if (objetoProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {

                objetoProducto = _Tiendacontext.Producto2s.Include(c => c.objetoCategoria).Where(p=>p.IdProducto == IdProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = objetoProducto });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = objetoProducto });
            }

        }

        [HttpPost]
        [Route("Crear")]
        public IActionResult Crear([FromBody] Producto2 objeto)
        {
            try
            {
                _Tiendacontext.Producto2s.Add(objeto);
                _Tiendacontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message});
            }
        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto2 objeto)
        {
            Producto2 objetoProducto = _Tiendacontext.Producto2s.Find(objeto.IdProducto);

            if (objetoProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                objetoProducto.Nombre = objeto.Nombre is null ? objetoProducto.Nombre : objeto.Nombre;
                objetoProducto.Descripcion = objeto.Descripcion is null ? objetoProducto.Descripcion : objeto.Descripcion;
                objetoProducto.IdCategoria = objeto.IdCategoria is null ? objetoProducto.IdCategoria : objeto.IdCategoria;


                _Tiendacontext.Producto2s.Update(objetoProducto);
                _Tiendacontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{IdProducto:int}")]
        public IActionResult Eliminar(int IdProducto)
        {
            Producto2 objetoProducto = _Tiendacontext.Producto2s.Find(IdProducto);

            if (objetoProducto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {

                _Tiendacontext.Producto2s.Remove(objetoProducto);
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
 