using Microsoft.AspNetCore.Http;

public interface IImageService
{
    string GenerateImageUrl(string folderName, string fileName);
    Task<string> SaveImageAsync(IFormFile imageFile, string folderName);
    bool DeleteImage(string folderName, string fileName);
    Task<string> UpdateImageAsync(IFormFile newImageFile, string folderName, string OldFileName);
    Task<string> GenerateQrCodeAsync(Guid UserId);
}