using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Tarjeta
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descripcion { get; set; }

    public string? Link { get; set; }

    public string? Extension { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaSubida { get; set; }

    public DateTime? FechaTerminado { get; set; }

    public string? EstadoTarjeta { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
}
