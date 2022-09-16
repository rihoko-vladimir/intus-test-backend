using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using Intus.Business.Interfaces.Repositories;
using Intus.Infrastructure.Configurations;
using Intus.Infrastructure.Models.Requests;
using Intus.Infrastructure.Models.Responses;

namespace Intus.Business.Repositories;

public class FileRepository : IFileRepository
{
    private readonly PathConfiguration _pathConfiguration;

    public FileRepository(PathConfiguration pathConfiguration)
    {
        _pathConfiguration = pathConfiguration;
    }

    public async Task WriteToFileAsync(RectangleDimensionsRequest request)
    {
        var serializedFile = JsonSerializer.Serialize(request);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            await File.WriteAllTextAsync(_pathConfiguration.UnixPath, serializedFile);
            return;
        }

        await File.WriteAllTextAsync(_pathConfiguration.DosPath, serializedFile);
    }

    public async Task<RectangleDimensionsResponse> ReadFromFileAsync()
    {
        var defaultValue = new RectangleDimensionsResponse
        {
            X = 32,
            Y = 32,
            Width = 500,
            Height = 500
        };
        string text;
        RectangleDimensionsResponse? response;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            if (!File.Exists(_pathConfiguration.UnixPath))
            {
                return defaultValue;
            }
            text = await File.ReadAllTextAsync(_pathConfiguration.UnixPath, Encoding.UTF8);
            try
            {
                response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);

                return response;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        
        if (!File.Exists(_pathConfiguration.DosPath))
        {
            return defaultValue;
        }

        text = await File.ReadAllTextAsync(_pathConfiguration.DosPath, Encoding.UTF8);

        try
        {
            response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);

            return response;
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }
}