using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EzpeletaNetCore8.Models;

namespace EzpeletaNetCore8.Models
{
    public class EjercicioFisico
    {
        [Key]
        public int EjercicioFisicoID { get; set; }
        public int TipoEjercicioID { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public EstadoEmocional EstadoEmocionalInicio {get; set; } 
        public EstadoEmocional EstadoEmocionalFin {get; set; } 
        public string? Observaciones {get; set; }

        public virtual TipoEjercicio TipoEjercicio { get; set; }
    }

    public enum EstadoEmocional{
        Feliz = 1,
        Triste,
        Enojado,
        Ansioso,
        Estresado,
        Relajado,
        Aburrido,
        Emocionado,
        Agobiado,
        Confundido,
        Optimista,
        Pesimista,
        Motivado,
        Cansado,
        Eufórico,
        Agitado,
        Satisfecho,
        Desanimado
    }
}