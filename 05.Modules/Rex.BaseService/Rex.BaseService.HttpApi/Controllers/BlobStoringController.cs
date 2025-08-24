using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Rex.BaseService.BlobStorings;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Rex.BaseService.Controllers
{
    /// <summary>
    /// 文件存储 控制器
    /// </summary>
    [Route(RoutePath)]
    public class BlobStoringController : BaseServiceController
    {
        public const string RoutePath = "api/base/blobStoring";
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
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            string fileUrl = string.Empty;
            using (var stream = file.OpenReadStream())
            {
                string uPath = await _blobStoringAppService.SaveAsync(file.FileName, stream);
                var blobStoringProvider = _configuration.GetSection("BlobStoringProvider");
                string? url = blobStoringProvider.GetValue<string>("Url")?.TrimEnd('/');
                if (url.IsNullOrWhiteSpace())
                {
                    url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    fileUrl = $"{url}/{RoutePath}/{uPath}";
                }
                else
                {
                    fileUrl = $"{url}/{uPath}";
                }
            }
            return Ok(fileUrl);
        }

        /// <summary>
        /// 编辑器(Wangeditor)上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("wangeditor/upload")]
        [Authorize]
        public async Task<IActionResult> WangeditorUploadAsync(IFormFile file)
        {
            string fileUrl = string.Empty;
            using (var stream = file.OpenReadStream())
            {
                string uPath = await _blobStoringAppService.SaveAsync(file.FileName, stream);
                var blobStoringProvider = _configuration.GetSection("BlobStoringProvider");
                string? url = blobStoringProvider.GetValue<string>("Url")?.TrimEnd('/');
                if (url.IsNullOrWhiteSpace())
                {
                    url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                    fileUrl = $"{url}/{RoutePath}/{uPath}";
                }
                else
                {
                    fileUrl = $"{url}/{uPath}";
                }
            }
            return Ok(new
            {
                errno = 0,
                data = new
                {
                    url = fileUrl
                }
            });
        }

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("exists/{name}")]
        [Authorize]
        public async Task<IActionResult> ExistsAsync([FromRoute] string name)
        {
            string fileName = WebUtility.UrlDecode(name);

            var blobStoringProvider = _configuration.GetSection("BlobStoringProvider");
            string? url = blobStoringProvider.GetValue<string>("Url")?.TrimEnd('/');
            string fileUrl = string.Empty;
            if (url.IsNullOrWhiteSpace())
            {
                url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                fileUrl = $"{url}/{RoutePath}";
            }
            else
            {
                fileUrl = url;
            }
            fileName = fileName.Replace(fileUrl, "").TrimStart('/');

            bool result = await _blobStoringAppService.ExistsAsync(fileName);
            return Ok(result);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("delete/{name}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] string name)
        {
            string fileName = WebUtility.UrlDecode(name);

            var blobStoringProvider = _configuration.GetSection("BlobStoringProvider");
            string? url = blobStoringProvider.GetValue<string>("Url")?.TrimEnd('/');
            string fileUrl = string.Empty;
            if (url.IsNullOrWhiteSpace())
            {
                url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
                fileUrl = $"{url}/{RoutePath}";
            }
            else
            {
                fileUrl = url;
            }
            fileName = fileName.Replace(fileUrl, "").TrimStart('/');

            await _blobStoringAppService.DeleteAsync(fileName);
            return NoContent();
        }
    }
}