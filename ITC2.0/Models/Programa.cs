using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Programa
{
    public int Id { get; set; }

    public string? NombrePrograma { get; set; }

    public string? Descripcion { get; set; }

    public int? IdFacultad { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual Facultade? IdFacultadNavigation { get; set; }
}
