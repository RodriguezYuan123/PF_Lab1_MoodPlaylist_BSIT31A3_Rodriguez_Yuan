using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MoodPlaylistGenerator.Models
{
    public class Song
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Artist { get; set; } = string.Empty;

        public string? YouTubeUrl { get; set; }

        // Local media file properties
        public string? LocalFilePath { get; set; }
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
        public long? FileSizeBytes { get; set; }
        public MediaType? MediaType { get; set; }

        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; } = null!;
        public List<SongMood> SongMoods { get; set; } = new();
        public List<PlaylistSong> PlaylistSongs { get; set; } = new();
    }
}