using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Presentacione
{
    public int Id { get; set; }

    public DateTime? DiaPresentacion { get; set; }

    public string? Salon { get; set; }

    public int? IdProyecto { get; set; }

    public int? IdAdministrador { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Docente> Docentes { get; set; } = new List<Docente>();

    public virtual Administradore? IdAdministradorNavigation { get; set; }

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
