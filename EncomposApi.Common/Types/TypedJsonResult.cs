using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EncomposApi.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EncomposApi.Types;

public record TypedJsonResult<T>
{
    private readonly static Lazy<JsonSerializer> _serializer =
        new(() => JsonUtilities.CreateSerializer());

    public TypedJsonResult(JToken json, HttpStatusCode status = HttpStatusCode.OK)
    {
        Json = json;
        Status = (int)status;
    }

    public JToken Json { get; init; }

    public T ToObject() => Json == null ? default : Json.ToObject<T>(_serializer.Value);

    public int Status { get; set; } = 200;

    public static implicit operator JsonResult(TypedJsonResult<T> result)
    {
        return new JsonResult(result.Json)
        {
            StatusCode = result.Status
        };
    }
}
