using Intus.Infrastructure.Models.Requests;
using Intus.Infrastructure.Models.Responses;

namespace Intus.Business.Interfaces.Services;

public interface IDimensionsService
{
    public Task PushDimensionsAsync(RectangleDimensionsRequest request);

    public Task<RectangleDimensionsResponse> GetDimensionsAsync();
}