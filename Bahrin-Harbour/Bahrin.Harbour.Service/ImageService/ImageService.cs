using Microsoft.AspNetCore.Http;
using QRCoder;
using Bahrin.Harbour.Helper;
using Microsoft.AspNetCore.Http.Internal;

public class ImageService : IImageService
{
    private readonly string _path = @"wwwroot/Images";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
    {
        if (imageFile == null || imageFile.Length == 0)
            throw new ArgumentException("No file provided");

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

        var fullPath = Path.Combine(_path, folderName, fileName);

        if (!Directory.Exists(Path.Combine(_path, folderName)))
        {
            Directory.CreateDirectory(Path.Combine(_path, folderName));
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }
        return fileName;
    }

    public string GenerateImageUrl(string folderName, string fileName)
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var scheme = request.Scheme;
        var host = request.Host.Value;
        var imageUrl = $"{scheme}://{host}/{_path}/{folderName}/{fileName}";

        return imageUrl;
    }

    public bool DeleteImage(string folderName, string fileName)
    {
        var fullPath = Path.Combine(_path, folderName, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);  
            return true;
        }

        return false; 
    }

    public async Task<string> UpdateImageAsync(IFormFile newImageFile, string folderName, string OldFileName)
    {
        DeleteImage(folderName, OldFileName);

        return await SaveImageAsync(newImageFile, folderName);
    }

    public async Task<string> GenerateQrCodeAsync(Guid UserId)
    {
        try
        {
            var strUserId = UserId.ToString();
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(strUserId, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                var qrFileName = Guid.NewGuid().ToString() + ".png";
                await SaveImageAsync(new FormFile(new MemoryStream(qrCodeImage), 0, qrCodeImage.Length, qrFileName, qrFileName), Constants.QrCodeImages);
                return qrFileName;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while generating the QR code.", ex);
        }
    }

}
