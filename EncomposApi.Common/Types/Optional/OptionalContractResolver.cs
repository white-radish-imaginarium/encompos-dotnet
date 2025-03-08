using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EncomposApi.Types.Optional;

public class OptionalContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty property = base.CreateProperty(member, memberSerialization);
			property.ShouldSerialize = instance =>
			{
				if (member is PropertyInfo prop)
				{
					var value = prop.GetValue(instance);
					if (value is IOptional optional)
					{
						return optional.IsPresent;
					}
					return value != null;
				}
				return false;
			};

			return property;
		}
	}
