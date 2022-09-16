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

        StreamWriter fileStream;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            fileStream = File.CreateText(_pathConfiguration.UnixPath);

            await fileStream.WriteAsync(serializedFile);
        }

        fileStream = File.CreateText(_pathConfiguration.DosPath);

        await fileStream.WriteAsync(serializedFile);
    }

    public async Task<RectangleDimensionsResponse> ReadFromFileAsync()
    {
        string text;
        RectangleDimensionsResponse? response;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            if (!File.Exists(_pathConfiguration.UnixPath))
            {
                return new RectangleDimensionsResponse
                {
                    X = 32,
                    Y = 32,
                    Width = 500,
                    Height = 500
                };
            }
            text = await File.ReadAllTextAsync(_pathConfiguration.UnixPath, Encoding.UTF8);
            response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);

            return response;
        }
        
        if (!File.Exists(_pathConfiguration.DosPath))
        {
            return new RectangleDimensionsResponse
            {
                X = 32,
                Y = 32,
                Width = 500,
                Height = 500
            };
        }

        text = await File.ReadAllTextAsync(_pathConfiguration.DosPath, Encoding.UTF8);

        response = JsonSerializer.Deserialize<RectangleDimensionsResponse>(text);

        return response;
    }
}