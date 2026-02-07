using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Rex.BaseService.BlobStorings;
using System.IO;
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

        public BlobStoringController(IBlobStoringAppService blobStoringAppService)
        {
            _blobStoringAppService = blobStoringAppService;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOrNullAsync([FromQuery] string filePath)
        {
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
                fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{RoutePath}?filePath={uPath}";
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
                fileUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{RoutePath}?filePath={uPath}";
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
    }
}