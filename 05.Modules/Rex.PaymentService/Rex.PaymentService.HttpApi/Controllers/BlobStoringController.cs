using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Rex.PaymentService.BlobStorings;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Rex.PaymentService.Controllers
{
    /// <summary>
    /// 文件存储 控制器
    /// </summary>
    [Route("api/payment/blobStoring")]
    public class BlobStoringController : PaymentServiceController
    {
        private readonly IBlobStoringAppService _blobStoringAppService;
        private readonly IConfiguration _configuration;

        public BlobStoringController(IBlobStoringAppService blobStoringAppService, IConfiguration configuration)
        {
            _blobStoringAppService = blobStoringAppService;
            _configuration = configuration;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetOrNullAsync([FromRoute] string name)
        {
            string filePath = WebUtility.UrlDecode(name);
            string fileName = $"{Path.GetFileNameWithoutExtension(filePath)}{Path.GetExtension(filePath)}";

            Stream? stream = await _blobStoringAppService.GetOrNullAsync(filePath);
            if (stream == null) return NotFound($"文件 {fileName} 未找到。");

            // MIME类型
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string? contentType))
                contentType = "application/octet-stream";

            return File(stream, contentType, fileName);
        }

        /// <summary>
        /// 证书上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("certificate/upload")]
        [Authorize]
        public async Task<IActionResult> CertificateUploadAsync(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                await _blobStoringAppService.SaveAsync(file.FileName, stream);
                var blobStoringProvider = _configuration.GetSection("BlobStoringProvider");
                return Ok(new
                {
                    CertificateBlobContainerName = blobStoringProvider.GetValue<string>("BucketName"),
                    CertificateBlobName = file.FileName
                });
            }
        }
    }
}