using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class History
{
    public long Id { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public int User { get; set; }

    public DateTime Date { get; set; }

    public string Action { get; set; } = null!;
}
