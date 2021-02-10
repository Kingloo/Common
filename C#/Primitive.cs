using System;
using System.Diagnostics.CodeAnalysis;

namespace 
{
    public abstract class Primitive<TValue, TReference> : IEquatable<Primitive<TValue, TReference>>, IComparable<Primitive<TValue, TReference>>
        where TValue : struct
        where TReference : Primitive<TValue, TReference>, new()
    {
        public TValue Value { get; protected set; } = default(TValue);

        protected Primitive(TValue value)
        {
            Value = value;
        }

        public static TReference From(TValue value)
        {
            var reference = new TReference();

            reference.Validate(value);

            reference.Value = value;

            return reference;
        }

        protected abstract void Validate(TValue value);
        public abstract int CompareTo([AllowNull] Primitive<TValue, TReference> other);

        public bool Equals([AllowNull] Primitive<TValue, TReference> other)
        {
            return !(other is null) && EqualsInternal(this, other);
        }

        public override bool Equals([AllowNull] object obj)
        {
            return obj is Primitive<TValue, TReference> other && EqualsInternal(this, other);
        }

        public static bool operator ==(Primitive<TValue, TReference> lhs, Primitive<TValue, TReference> rhs)
        {
            return EqualsInternal(lhs, rhs);
        }

        public static bool operator !=(Primitive<TValue, TReference> lhs, Primitive<TValue, TReference> rhs)
        {
            return !EqualsInternal(lhs, rhs);
        }

        private static bool EqualsInternal(Primitive<TValue, TReference> thisOne, Primitive<TValue, TReference> otherOne)
        {
            return thisOne.Value.Equals(otherOne.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString() is string s ? s : "null";
        }       
    }
}