using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EncomposApi.Types.Optional
{
    // AutoRestSchemaFilter.cs
    public class OptionalSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Optional<>))
            {
                var underlyingType = type.GetGenericArguments()[0];
                var underlyingSchema = context.SchemaGenerator.GenerateSchema(underlyingType, context.SchemaRepository);
                schema.Type = underlyingSchema.Type;
                schema.Items = underlyingSchema.Items;
            };
        }
    }
}
