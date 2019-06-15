using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalisisMedicos.Entidades
{
    public class AnalisisDetalle
    {
        [Key]
        public int ID { get; set; }
        public int AnalisisId { get; set; }
        public int TipoId { get; set; }
        public string Resultado { get; set; }

        public AnalisisDetalle()
        {
            ID = 0;
            AnalisisId = 0;
            TipoId = 0;
            Resultado = String.Empty;
        }

        public AnalisisDetalle(int iD,int analisisId, int tipoId,string resultado)
        {
            ID = iD;
            AnalisisId = analisisId;
            TipoId = tipoId;
            Resultado=resultado;
        }
    }
}
        