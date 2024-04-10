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

    public class ToDoC
    {
        private readonly string _desc;
        private readonly bool _isDone;
        public ToDoC(string description, bool isDone)
        {
            _desc = description;
            _isDone = isDone;
        }

        protected virtual Type EqualityContract
        {
            get
            {
                return typeof(ToDoC);
            }
        }

        public string Description
        {
            get
            {
                return _desc;
            }

            init
            {
                _desc = value;
            }
        }

        public bool IsDone
        {
            get
            {
                return _isDone;
            }

            init
            {
                _isDone = value;
            }
        }

        public void Deconstruct(out string description, out bool isDone)
        {
            description = Description;
            isDone = IsDone;
        }

        public override string ToString() => $"{Description}|{IsDone}";
    }
}