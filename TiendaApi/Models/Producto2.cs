using System;
using System.Collections.Generic;

namespace TiendaApi.Models
{
    public partial class Producto2
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? IdCategoria { get; set; }

        public virtual Categoria2? objetoCategoria { get; set; }
    }
}
