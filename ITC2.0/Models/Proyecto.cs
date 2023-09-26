using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? NumeroIntegrantes { get; set; }

    public DateTime? UltimaActualizacion { get; set; }

    public string? Estado { get; set; }

    public int? IdTarjeta { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

    public virtual Tarjeta? IdTarjetaNavigation { get; set; }

    public virtual ICollection<Presentacione> Presentaciones { get; set; } = new List<Presentacione>();

    public virtual ICollection<Supervisore> Supervisores { get; set; } = new List<Supervisore>();
}
