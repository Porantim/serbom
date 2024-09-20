using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class ContractType
{
    public sbyte Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
}
