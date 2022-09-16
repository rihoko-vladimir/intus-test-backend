using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using intus_test_backend.Configurations;
using Intus.Business.Interfaces.Repositories;
using Intus.Infrastructure.Models.Requests;
using Intus.Infrastructure.Models.Responses;

namespace Intus.Business.Repositories;

public class FileRepository : IFileRepository
{
    private readonly PathConfiguration _pathConfiguration;

    private FileRepository(PathConfiguration pathConfiguration)
    {
        _pathConfiguration = pathConfiguration;
    }
    
    public async Task WriteToFileAsync(RectangleDimensionsRequest request)
    {
        var serializedFile = JsonSerializer.Serialize(request);


        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            await File.WriteAllTextAsync(_pathConfiguration.UnixPath, serializedFile);
        }
        else
        {
            await File.WriteAllTextAsync(_pathConfiguration.DosPath, serializedFile);
        }
    }

    public async Task<RectangleDimensionsResponse> ReadFromFileAsync()
    {
        string text;
        RectangleDimensionsResponse? response;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            text = await File.ReadAllTextAsync(_pathConfiguration.UnixPath, Encoding.UTF8);
            response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);
            
            return response ?? new RectangleDimensionsResponse
            {
                X = 0,
                Y = 0,
                Width = 0,
                Height = 0
            };
        }
        text = await File.ReadAllTextAsync(_pathConfiguration.DosPath, Encoding.UTF8);
        
        response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);
        
        return response ?? new RectangleDimensionsResponse
        {
            X = 0,
            Y = 0,
            Width = 0,
            Height = 0
        };
    }
}