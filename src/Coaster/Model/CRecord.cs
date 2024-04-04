﻿using System.Collections.Generic;
using Coaster.API;

namespace Coaster.Model
{
    public sealed class CRecord : CMember, IHasInterfaces, IHasVisibility, IHasParameters
    {
        public ISet<string> Interfaces { get; set; } = new HashSet<string>();

        public Visibility Visibility { get; set; } = Visibility.Public;

        public List<CParam> Params { get; set; } = new();
    }
}