using ITC2._0.Models;

namespace ITC2._0.ModelsView
{
    public class ProgramasMV
    {
        public int Id { get; set; }

        public string? NombrePrograma { get; set; }

        public string? Descripcion { get; set; }

        public int? IdFacultad { get; set; }

        public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        public virtual Facultade? IdFacultadNavigation { get; set; }
    }
}
