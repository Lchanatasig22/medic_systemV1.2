using System;
using System.Collections.Generic;

namespace medic_system.Models
{
    public partial class ConsultaImagen
    {
        public ConsultaImagen()
        {
            Consulta = new HashSet<Consultum>();
        }

        public int IdConsultaImagen { get; set; }
        public int? ImagenId { get; set; }
        public string? ObservacionImagen { get; set; }
        public int? CantidadImagen { get; set; }
        public int? EstadoImagen { get; set; }
        public int? ConsultaId { get; set; }
        public int? SecuencialConsultaImg { get; set; }

        public virtual Imagen? Imagen { get; set; }
        public virtual ICollection<Consultum> Consulta { get; set; }
    }
}
