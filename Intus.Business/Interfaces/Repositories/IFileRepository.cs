using Intus.Infrastructure.Models.Requests;
using Intus.Infrastructure.Models.Responses;

namespace Intus.Business.Interfaces.Repositories;

public interface IFileRepository
{
    public Task WriteToFileAsync(RectangleDimensionsRequest request);

    public Task<RectangleDimensionsResponse> ReadFromFileAsync();
}