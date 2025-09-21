namespace MoodPlaylistGenerator.Services;

public class MediaUploadService : IMediaUploadService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<MediaUploadService> _logger;
    private readonly long _maxFileSize = 10 * 1024 * 1024; // 10MB

    public MediaUploadService(IWebHostEnvironment environment, ILogger<MediaUploadService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is null or empty");

        var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", folder);
        
        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
        var filePath = Path.Combine(uploadsPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        _logger.LogInformation("File uploaded successfully: {FilePath}", filePath);
        return Path.Combine("uploads", folder, fileName).Replace("\\", "/");
    }

    public Task<bool> DeleteFileAsync(string filePath)
    {
        try
        {
            var fullPath = Path.Combine(_environment.WebRootPath, filePath.Replace("/", "\\"));
            
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                _logger.LogInformation("File deleted successfully: {FilePath}", fullPath);
                return Task.FromResult(true);
            }
            
            return Task.FromResult(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file: {FilePath}", filePath);
            return Task.FromResult(false);
        }
    }

    public bool IsValidFileType(IFormFile file, string[] allowedExtensions)
    {
        if (file == null) return false;
        
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedExtensions.Contains(extension);
    }

    public long GetMaxFileSize()
    {
        return _maxFileSize;
    }
}