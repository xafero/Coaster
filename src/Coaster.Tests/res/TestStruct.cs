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

    record PersonCustom
    {
        public PersonCustom(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }

    public readonly record struct ToDoStruct(string Description, bool IsDone);
    public record ToDoTwo(string Description, bool IsDone);
    public record ToDoDef(string Description, bool IsDone, string Category = "Default");
    public record ToDo(string Description)
    {
        public ToDo(string description, bool isDone) : this(description)
        {
            IsDone = isDone;
        }

        public bool IsDone { get; init; }
    }
}