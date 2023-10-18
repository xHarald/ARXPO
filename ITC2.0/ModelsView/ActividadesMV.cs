using ITC2._0.Models;

namespace ITC2._0.ModelsView
{
    public class ActividadesMV
    {
        public int Codigo { get; set; }
        public String? Estudiante { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public TimeSpan? Horas { get; set; }
        public TimeSpan? Terminar { get; set; }   
    }
}
