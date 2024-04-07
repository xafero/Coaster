﻿using Coaster.API.Mod;
using Coaster.API.Part;
using Coaster.Roslyn;
using Coaster.Utils;

namespace Coaster.Model.Part
{
    public sealed class CProperty : IProperty
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Visibility Visibility { get; set; }

        public PropMode Mode { get; set; }

        public void Apply(IHasMembers owner)
        {
            if (owner.IsInterface())
            {
                Mode = Mode.IfZero(PropMode.GetSet);
                return;
            }
            Mode = Mode.IfZero(owner.IsRecord() ? PropMode.GetInit : PropMode.GetSet);
            Visibility = Visibility.IfZero(Visibility.Public);
        }
    }
}