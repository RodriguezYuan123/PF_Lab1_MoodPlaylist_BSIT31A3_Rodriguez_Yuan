namespace MoodPlaylistGenerator.Services;

public interface IMediaUploadService
{
    Task<string> UploadFileAsync(IFormFile file, string folder);
    Task<bool> DeleteFileAsync(string filePath);
    bool IsValidFileType(IFormFile file, string[] allowedExtensions);
    long GetMaxFileSize();
}