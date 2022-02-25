using System;
using System.Diagnostics.CodeAnalysis;

namespace .Common
{
# pragma warning disable CA1000, CA1062
	public abstract class ValueObject<TValue, TReference> : IEquatable<ValueObject<TValue, TReference>>, IComparable<ValueObject<TValue, TReference>>
		where TValue : notnull
		where TReference : notnull, ValueObject<TValue, TReference>, new()
	{
		[NotNull] private TValue? _value = default(TValue);
		public TValue Value
		{
			get => _value;
			protected set => _value = value;
		}

		public static TReference Empty
		{
			get => From(default(TValue)!, validate: false);
		}

		protected ValueObject(TValue value)
		{
			Value = value;
		}

		public static TReference From(TValue value)
		{
			return From(value, validate: true);
		}

		private static TReference From(TValue value, bool validate)
		{
			var reference = new TReference();

			if (validate)
			{
				reference.Validate(value);
			}

			reference.Value = value;

			return reference;
		}

		protected abstract void Validate(TValue value);

		public abstract int CompareTo([AllowNull] ValueObject<TValue, TReference> other);

		public bool Equals([AllowNull] ValueObject<TValue, TReference> other)
		{
			return (other is not null) && EqualsInternal(this, other);
		}

		public override bool Equals([AllowNull] object obj)
		{
			return obj is ValueObject<TValue, TReference> other && EqualsInternal(this, other);
		}

		public static bool operator ==(ValueObject<TValue, TReference> lhs, ValueObject<TValue, TReference> rhs)
		{
			return EqualsInternal(lhs, rhs);
		}

		public static bool operator !=(ValueObject<TValue, TReference> lhs, ValueObject<TValue, TReference> rhs)
		{
			return !EqualsInternal(lhs, rhs);
		}

		public static bool operator >(ValueObject<TValue, TReference> lhs, ValueObject<TValue, TReference> rhs)
		{
			return lhs.CompareTo(rhs) > 0;
		}
		public static bool operator <(ValueObject<TValue, TReference> lhs, ValueObject<TValue, TReference> rhs)
		{
			return lhs.CompareTo(rhs) < 0;
		}

		private static bool EqualsInternal(ValueObject<TValue, TReference> thisOne, ValueObject<TValue, TReference> otherOne)
		{
			if (ReferenceEquals(thisOne, otherOne))
			{
				return true;
			}
			else
			{
				return thisOne?.Value?.Equals(otherOne.Value) ?? false;
			}
		}

		public override int GetHashCode()
		{
			return Value?.GetHashCode() ?? 0;
		}

		public override string ToString()
		{
			return Value?.ToString() ?? "null";
		}
	}
# pragma warning restore CA1000, CA1062
}
