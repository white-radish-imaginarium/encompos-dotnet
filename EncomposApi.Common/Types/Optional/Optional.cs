using System;
using System.Collections;
using System.Collections.Generic;

namespace EncomposApi.Types.Optional
{
    public interface IOptional
	{ 
		public bool IsPresent { get; }
	}

	/// <summary>
	/// For representing potentially absent properties when serializing to and from JSON.
	/// </summary>
	/// <typeparam name="T">Type of optional value.</typeparam>
	/// <remarks>
	/// Initially based on Roslyn: 
	/// https://github.com/dotnet/roslyn/blob/master/src/Compilers/Core/Portable/Optional.cs
	/// Augmented with for IEquatable<T> implementation: 
	/// https://github.com/AndreyTsvetkov/Functional.Maybe/blob/master/Functional.Maybe/Maybe.cs
	/// Taking inspiration from here: 
	/// https://github.com/nlkl/Optional/blob/master/src/Optional/Option_Maybe.cs
	/// </remarks>
	public readonly struct Optional<T> : IOptional, IEnumerable<T>, IEquatable<Optional<T>>, IComparable<Optional<T>>
	{
		/// <summary>
		/// Represents an optional value that is not present.
		/// </summary>
		public static readonly Optional<T> Absent = default;

		private readonly T _value;
		private readonly bool _hasValue;

		public Optional(T value)
		{
			_hasValue = true;
			_value = value;
		}

		public bool IsPresent => _hasValue;
		public bool IsAbsent => !_hasValue;

		public static implicit operator Optional<T>(T value) => new(value);

		public override string ToString() => _hasValue ? (_value?.ToString() ?? "null") : "absent";

		public bool Equals(Optional<T> other)
			=> _hasValue == other._hasValue
			&& EqualityComparer<T>.Default.Equals(_value, other._value);

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			return obj is Optional<T> opt && Equals(opt);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (EqualityComparer<T>.Default.GetHashCode(_value) * 397) ^ _hasValue.GetHashCode();
			}
		}

		public static bool operator ==(Optional<T> left, Optional<T> right) => left.Equals(right);

		public static bool operator !=(Optional<T> left, Optional<T> right) => !left.Equals(right);

		public bool TryGetValue(out T value)
		{
			if (_hasValue)
			{
				value = _value;
				return true;
			}
			value = default;
			return false;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
			if (_hasValue)
			{
				yield return _value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
        {
			if (_hasValue)
			{
				yield return _value;
			}
		}

        #region compare to

        public int CompareTo(Optional<T> other)
		{
			if (_hasValue && !other._hasValue) return 1;
			if (!_hasValue && other._hasValue) return -1;
			if (!_hasValue && !other._hasValue) return 0;
			return Comparer<T>.Default.Compare(_value, other._value);
		}

		/// <summary>
		/// Is not null-valued
		/// </summary>
		private bool IsNotNull => _hasValue && _value != null;

		/// <summary>
		/// Determines if an optional is less than another optional.
		/// </summary>
		/// <param name="left">The first optional to compare.</param>
		/// <param name="right">The second optional to compare.</param>
		/// <returns>A boolean indicating whether or not the left optional is less than the right optional.</returns>
		public static bool operator <(Optional<T> left, Optional<T> right) =>
			left.IsNotNull && right.IsNotNull && 
			left.CompareTo(right) < 0;

		/// <summary>
		/// Determines if an optional is less than or equal to another optional.
		/// </summary>
		/// <param name="left">The first optional to compare.</param>
		/// <param name="right">The second optional to compare.</param>
		/// <returns>A boolean indicating whether or not the left optional is less than or equal the right optional.</returns>
		public static bool operator <=(Optional<T> left, Optional<T> right) =>
			left.IsNotNull && right.IsNotNull &&
			left.CompareTo(right) <= 0;

		/// <summary>
		/// Determines if an optional is greater than another optional.
		/// </summary>
		/// <param name="left">The first optional to compare.</param>
		/// <param name="right">The second optional to compare.</param>
		/// <returns>A boolean indicating whether or not the left optional is greater than the right optional.</returns>
		public static bool operator >(Optional<T> left, Optional<T> right) =>
			left.IsNotNull && right.IsNotNull && 
			left.CompareTo(right) > 0;

		/// <summary>
		/// Determines if an optional is greater than or equal to another optional.
		/// </summary>
		/// <param name="left">The first optional to compare.</param>
		/// <param name="right">The second optional to compare.</param>
		/// <returns>A boolean indicating whether or not the left optional is greater than or equal the right optional.</returns>
		public static bool operator >=(Optional<T> left, Optional<T> right) => 
			left.IsNotNull && right.IsNotNull && 
			left.CompareTo(right) >= 0;

		#endregion

		#region standard functional methods

		/// <summary>
		/// Evaluates a specified function, based on whether a value is present or not.
		/// </summary>
		/// <param name="present">The function to evaluate if the value is present.</param>
		/// <param name="absent">The function to evaluate if the value is absent.</param>
		/// <returns>The result of the evaluated function.</returns>
		public Optional<R> Match<R>(Func<T, Optional<R>> present, Func<Optional<R>> absent)
		{
			if (!_hasValue) return absent(); 
			return present(_value);
		}

		public Optional<R> Bind<R>(Func<T, Optional<R>> present)
		{
			if (!_hasValue) return Optional<R>.Absent; 
			return present(_value);
		}

		/// <summary>
		/// Transforms the inner value in an optional.
		/// If the instance is empty, an empty optional is returned.
		/// </summary>
		/// <param name="present">The transformation function.</param>
		/// <returns>The transformed optional.</returns>
		public Optional<R> Map<R>(Func<T, R> present)
        {
			if (!_hasValue) return Optional<R>.Absent; 
			return new Optional<R>(present(_value));
		}

		/// <summary>
		/// Empties an optional if a specified predicate
		/// is not satisfied.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>The filtered optional.</returns>
		public Optional<T> Filter(Predicate<T> predicate)
		{
			if (!_hasValue || !predicate(_value)) return Absent; 
			return this;
		}

		/// <summary>
		/// Returns the existing value if present, and otherwise an alternative value.
		/// </summary>
		/// <param name="alternative">The alternative value.</param>
		/// <returns>The existing or alternative value.</returns>
		public T ValueOr(T alternative)
		{
			if (!_hasValue) return alternative; 
			return _value;
		} 

		/// <summary>
		/// Uses an alternative value, if no existing value is present.
		/// </summary>
		/// <param name="alternative">The alternative value.</param>
		/// <returns>A new optional, containing either the existing or alternative value.</returns>
		public Optional<T> Or(T alternative)
		{
			if (!_hasValue) return new Optional<T>(alternative); 
			return this;
		}

		/// <summary>
		/// Uses an alternative optional, if no existing value is present.
		/// </summary>
		/// <param name="alternativeOption">The alternative optional.</param>
		/// <returns>The alternative optional, if no value is present, otherwise itself.</returns>
		public Optional<T> Else(Optional<T> alternative)
		{
			if (!_hasValue) return alternative; 
			return this;
		}

		/// <summary>
		/// Determines if the current optional contains a specified value.
		/// </summary>
		/// <param name="value">The value to locate.</param>
		/// <returns>A boolean indicating whether or not the value was found.</returns>
		public bool Contains(T value)
		{
			if (!_hasValue) return false;
			if (_value == null) return value == null;
			return EqualityComparer<T>.Default.Equals(_value, value);
		}

		/// <summary>
		/// Determines if the current optional contains a value 
		/// satisfying a specified predicate.
		/// </summary>
		/// <param name="predicate">The predicate.</param>
		/// <returns>A boolean indicating whether or not the predicate was satisfied.</returns>
		public bool Exists(Predicate<T> predicate)
		{
			if (!_hasValue) return false; 
			return predicate(_value); 
		}

		/// <summary>
		/// Converts null to absent.
		/// </summary>
		/// <returns>The filtered optional.</returns>
		public Optional<T> NotNull()
		{
			if (!_hasValue || _value == null) return Absent;
			return this;
		}

		/// <summary>
		/// Evaluates a specified action if a value is present, aka MatchSome
		/// </summary>
		/// <param name="action">The action to evaluate if the value is present.</param>
		public void WhenPresent(Action<T> action)
		{
			if (_hasValue) action(_value);
		}

		/// <summary>
		/// Evaluates a specified action if a value is absent, aka MatchNone
		/// </summary>
		/// <param name="action">The action to evaluate if the value is present.</param>
		public void WhenAbsent(Action action)
		{
			if (!_hasValue) action();
		}

        #endregion
    }
}
