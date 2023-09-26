using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Supervisore
{
    public int Id { get; set; }

    public int? IdProyecto { get; set; }

    public string? Motivo { get; set; }

    public int? IdDocente { get; set; }

    public virtual Docente? IdDocenteNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
