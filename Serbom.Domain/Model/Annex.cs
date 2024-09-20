using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class Annex
{
    public int Id { get; set; }

    public int Contract { get; set; }

    public int? Amendment { get; set; }

    public string Name { get; set; } = null!;

    public bool Deleted { get; set; }

    public byte[] Content { get; set; } = null!;
}
