using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzpeletaNetCore8.Models;

public class TipoEjercicio
{
    [Key]
    public int TipoEjercicioID { get; set; }
    public string? Descripcion { get; set; }  
    
     public bool Eliminado { get; set; }

      public virtual ICollection<EjercicioFisico> EjerciciosFisicos { get; set; } 

}

