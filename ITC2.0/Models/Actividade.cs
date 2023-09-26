using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Actividade
{
    public int Id { get; set; }

    public int? IdEstudiante { get; set; }

    public string? TituloActividad { get; set; }

    public string? Descripcion { get; set; }

    public TimeSpan? Horas { get; set; }

    public TimeSpan? Terminar { get; set; }

    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
