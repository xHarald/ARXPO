using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Correo { get; set; }

    public string? Contraseña { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Administradore> Administradores { get; set; } = new List<Administradore>();

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
