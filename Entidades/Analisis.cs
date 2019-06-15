using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedicos.Entidades
{
   public class Analisis
    {
        [Key]
        public int AnalisisId { get; set; }
        public DateTime Fecha{ get; set; }
        public int UsuarioId { get; set; }
        public int TipoAnalisisId { get; set; }
        public List<AnalisisDetalle> AnalisisDetalles;
        
        public Analisis()
        {
            AnalisisId = 0;
            Fecha = DateTime.Now;
            UsuarioId = 0;
            AnalisisDetalles = new List<AnalisisDetalle>();
            TipoAnalisisId = 0;

            
        }
    }
}
