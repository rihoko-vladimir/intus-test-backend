using Intus.Business.Interfaces.Repositories;
using Intus.Business.Interfaces.Services;
using Intus.Infrastructure.Models.Requests;
using Intus.Infrastructure.Models.Responses;

namespace Intus.Business.Services;

public class DimensionsService : IDimensionsService
{
    private readonly IFileRepository _fileRepository;

    public DimensionsService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task PushDimensionsAsync(RectangleDimensionsRequest request)
    {
        await _fileRepository.WriteToFileAsync(request);
    }

    public async Task<RectangleDimensionsResponse> GetDimensionsAsync()
    {
        return await _fileRepository.ReadFromFileAsync();
    }
}