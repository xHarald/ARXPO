using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Administradore
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<Presentacione> Presentaciones { get; set; } = new List<Presentacione>();
}
