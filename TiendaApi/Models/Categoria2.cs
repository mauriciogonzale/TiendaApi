using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TiendaApi.Models
{
    public partial class Categoria2
    {
        public Categoria2()
        {
            Producto2s = new HashSet<Producto2>();
        }

        public int IdCategoria { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Producto2> Producto2s { get; set; }
    }
}
