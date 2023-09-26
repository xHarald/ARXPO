using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Docente
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public int? Identificacion { get; set; }

    public int? IdPresentacion { get; set; }

    public virtual Presentacione? IdPresentacionNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Supervisore> Supervisores { get; set; } = new List<Supervisore>();
}
