using System;

namespace Equ
{
    public struct ToDoS : IEquatable<ToDoS>
    {
        private string _desc;
        private bool _isDone;
        public ToDoS(string desc, bool isDone)
        {
            _desc = desc;
            _isDone = isDone;
        }

        public string Description { get => _desc; set => _desc = value; }
        public bool IsDone { get => _isDone; set => _isDone = value; }

        public readonly bool Equals(ToDoS other) => _desc == other._desc && _isDone == other._isDone;
        public readonly override bool Equals(object obj) => obj is ToDoS other && Equals(other);
        public readonly override int GetHashCode() => HashCode.Combine(_desc, _isDone);
        public static bool operator ==(ToDoS left, ToDoS right) => left.Equals(right);
        public static bool operator !=(ToDoS left, ToDoS right) => !(left == right);
    }
}