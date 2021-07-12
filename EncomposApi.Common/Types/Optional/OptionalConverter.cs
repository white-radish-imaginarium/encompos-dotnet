using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;

namespace EncomposApi.Types.Optional
{
	// based on: https://github.com/AndreyTsvetkov/Functional.Maybe/blob/master/Functional.Maybe.Json/MaybeConverter.cs
	public class OptionalConverter : JsonConverter
	{
		public OptionalConverter()
		{
			_converters = new ConcurrentDictionary<Type, TypeConverter>();
		}

		private static readonly Type OpenOptionalType = typeof(Optional<>);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) =>
			GetConverter(value.GetType()).WriteJson(writer, value, serializer);

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			GetConverter(objectType).ReadJson(reader, serializer);

		public override bool CanConvert(Type objectType) =>
			objectType.IsGenericType
			&& objectType.GetGenericTypeDefinition() == OpenOptionalType;

		private TypeConverter GetConverter(Type closedOptionalType)
		{
			var underlyingType = closedOptionalType.GetGenericArguments()[0];

			if (!_converters.TryGetValue(underlyingType, out TypeConverter converter))
			{
				var converterType = typeof(OptionalConverter<>).MakeGenericType(underlyingType);
				_converters.TryAdd(underlyingType, new TypeConverter(converterType));
				converter = _converters[underlyingType];
			}

			return converter;
		}

		private readonly ConcurrentDictionary<Type, TypeConverter> _converters;

		private class TypeConverter
		{
			private readonly Action<JsonWriter, object, JsonSerializer> _writeJsonDelegate;
			private readonly Func<JsonReader, JsonSerializer, object> _readJsonDelegate;

			public TypeConverter(Type converterType)
			{
				var converter = Activator.CreateInstance(converterType);

				_writeJsonDelegate = (Action<JsonWriter, object, JsonSerializer>)Delegate.CreateDelegate(
					typeof(Action<JsonWriter, object, JsonSerializer>),
					converter,
					converterType.GetMethod(nameof(OptionalConverter.WriteJson)));

				_readJsonDelegate = (Func<JsonReader, JsonSerializer, object>)Delegate.CreateDelegate(
					typeof(Func<JsonReader, JsonSerializer, object>),
					converter,
					converterType.GetMethod(nameof(OptionalConverter.ReadJson)));
			}

			public void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				_writeJsonDelegate(writer, value, serializer);
			}

			public object ReadJson(JsonReader reader, JsonSerializer serializer)
			{
				return _readJsonDelegate(reader, serializer);
			}
		}
	}

	internal class OptionalConverter<T>
	{
		public void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var option = (Optional<T>)value;
			if (option.TryGetValue(out T optionValue))
			{
				serializer.Serialize(writer, optionValue);
				return;
			}
			throw new InvalidOperationException("Attempted to serialize absent property");
		}

		public object ReadJson(JsonReader reader, JsonSerializer serializer)
		{
			T value;
			if (reader.TokenType == JsonToken.Null)
			{
				// can we set T to null?
				value = default;
				if (value == null) return new Optional<T>(value);

				// otherwise coalsce to Absent
				return Optional<T>.Absent;
			}

			value = serializer.Deserialize<T>(reader);
			return new Optional<T>(value);
		}
	}
}
