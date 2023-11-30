using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? TipoIdentificacion { get; set; }

    public int? Identificacion { get; set; }

    public int? IdPrograma { get; set; }

    public int? IdProyecto { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();

    public virtual Programa? IdProgramaNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
