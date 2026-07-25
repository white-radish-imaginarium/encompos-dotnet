using System;
using Newtonsoft.Json;
using Xunit;

namespace EncomposApi.Common.Tests;

/// <summary>
/// Locks the JSON wire format of <see cref="ApiError"/> and <see cref="ApiResult"/>.
/// These types are serialized straight into HTTP responses that Greensprout and the
/// POS UI consume, so their shape is observable contract rather than an implementation
/// detail. Written before the MVC decoupling refactor (CFD) to prove it changed nothing.
/// </summary>
public class ApiResultSerializationTests : TestBase
{
    private static string Serialize(object value) =>
        JsonConvert.SerializeObject(value).Replace("\r\n", "\n");

    [Fact]
    public void ApiResult_Ok_SerializesWithMessage()
    {
        Assert.Equal(
            "{\n  \"status\": 200,\n  \"ok\": true,\n  \"message\": \"hi\"\n}",
            Serialize(ApiResult.Ok("hi")));
    }

    [Fact]
    public void ApiResult_Ok_OmitsNullMessage()
    {
        Assert.Equal(
            "{\n  \"status\": 200,\n  \"ok\": true\n}",
            Serialize(ApiResult.Ok()));
    }

    [Fact]
    public void ApiResult_Created_And_Accepted_CarryTheirStatus()
    {
        Assert.Equal(201, ApiResult.Created().Status);
        Assert.Equal(202, ApiResult.Accepted().Status);
        Assert.True(ApiResult.Created().Okay);
    }

    [Fact]
    public void ApiResult_NonSuccessStatus_IsNotOkayAndTakesReasonFromApiError()
    {
        var result = new ApiResult(404);

        Assert.False(result.Okay);
        Assert.Equal("Not found.", result.Message);
    }

    [Fact]
    public void ApiError_NotFound_SerializesWithoutNullMembers()
    {
        Assert.Equal(
            "{\n  \"status\": 404,\n  \"error\": \"not_found\",\n  \"reason\": \"Not found.\"\n}",
            Serialize(ApiError.NotFound()));
    }

    [Fact]
    public void ApiError_BadRequest_UsesSuppliedReason()
    {
        Assert.Equal(
            "{\n  \"status\": 400,\n  \"error\": \"bad_request\",\n  \"reason\": \"nope\"\n}",
            Serialize(ApiError.BadRequest("nope")));
    }

    [Theory]
    [InlineData(401, "unauthorized", "Authorization required.")]
    [InlineData(403, "forbidden", "Authorization denied.")]
    [InlineData(405, "method_not_allowed", "Method not allowed.")]
    [InlineData(418, "other", "status 418")]
    public void ApiError_MapsStatusToErrorCodeAndReason(int status, string error, string reason)
    {
        var apiError = new ApiError(status);

        Assert.Equal(error, apiError.Error);
        Assert.Equal(reason, apiError.Reason);
        Assert.Null(apiError.StackTrace);
    }

    [Fact]
    public void ApiError_Details_AreSerialized()
    {
        var apiError = ApiError.BadRequest("nope");
        apiError.Details = new { field = "sku" };

        Assert.Equal(
            "{\n  \"status\": 400,\n  \"error\": \"bad_request\",\n  \"reason\": \"nope\",\n  \"details\": {\n    \"field\": \"sku\"\n  }\n}",
            Serialize(apiError));
    }

    [Fact]
    public void ApiError_Internal_HandlesNeverThrownException()
    {
        // Exception.StackTrace is null until the exception is thrown. Building an
        // error response from a constructed exception is legal for library consumers
        // and must not throw.
        var apiError = ApiError.Internal(new InvalidOperationException("not thrown"));

        Assert.Equal(500, apiError.Status);
        Assert.Equal("InvalidOperationException", apiError.Error);
        Assert.Equal("not thrown", apiError.Reason);
        Assert.Null(apiError.StackTrace);
        Assert.Equal(
            "{\n  \"status\": 500,\n  \"error\": \"InvalidOperationException\",\n  \"reason\": \"not thrown\"\n}",
            Serialize(apiError));
    }

    [Fact]
    public void ApiError_Internal_CapturesExceptionTypeAndMessage()
    {
        Exception thrown;
        try { throw new InvalidOperationException("boom"); }
        catch (Exception ex) { thrown = ex; }

        var apiError = ApiError.Internal(thrown);

        Assert.Equal(500, apiError.Status);
        Assert.Equal("InvalidOperationException", apiError.Error);
        Assert.Equal("boom", apiError.Reason);
        Assert.NotEmpty(apiError.StackTrace);
    }
}
