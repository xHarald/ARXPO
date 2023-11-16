using System;
using System.Collections.Generic;

namespace ITC2._0.Models;

public partial class Facultade
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public string? TelefonoContacto { get; set; }

    public string? Correo { get; set; }

    public bool Estado { get; set; } = true;

    public virtual ICollection<Programa> Programas { get; set; } = new List<Programa>();
}
