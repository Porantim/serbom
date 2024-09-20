using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public bool? Active { get; set; }

    public string Salt { get; set; } = null!;
}
