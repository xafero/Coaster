using System;
using System.Collections.Generic;
using System.Text;

namespace Sample
{
    public readonly struct Marvel
    {
        public string CharacterName { get; }

        public Marvel(string name)
        {
            CharacterName = name;
        }
    }

    public record class PersonMut
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public sealed record StudentMut : PersonMut
    {
        public int Id { get; set; }
    }

    public record class PersonImm
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public sealed record StudentImm : PersonImm
    {
        public int Id { get; init; }
    }
}