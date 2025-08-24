using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.DependencyInjection;

namespace Rex.BaseService.BlobStorings
{
    /// <summary>
    /// 文件存储
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Dependency(ServiceLifetime.Singleton)]
    public class BlobStoringAppService : ApplicationService, IBlobStoringAppService
    {
        private readonly IBlobContainer<BlobBaseServiceContainer> _blobBaseContainer;

        public BlobStoringAppService(IBlobContainer<BlobBaseServiceContainer> blobBaseContainer)
        {
            _blobBaseContainer = blobBaseContainer;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string name)
        {
            await _blobBaseContainer.DeleteAsync(name);
        }

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string name)
        {
            return await _blobBaseContainer.ExistsAsync(name);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<byte[]?> GetAllBytesOrNullAsync(string name)
        {
            return await _blobBaseContainer.GetAllBytesOrNullAsync(name);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Stream?> GetOrNullAsync(string name)
        {
            return await _blobBaseContainer.GetOrNullAsync(name);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public async Task<string> SaveAsync(string name, byte[] bytes)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string extName = Path.GetExtension(name);
            string pathFile = $"{CurrentUser.Id}/{DateTime.Now.ToString("yyyyMMddHHmmss")}/{fileName}{extName}";
            await _blobBaseContainer.SaveAsync(pathFile, bytes, true);
            return pathFile;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<string> SaveAsync(string name, Stream stream)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string extName = Path.GetExtension(name);
            string pathFile = $"{CurrentUser.Id}/{DateTime.Now.ToString("yyyyMMddHHmmss")}/{fileName}{extName}";
            await _blobBaseContainer.SaveAsync(pathFile, stream, true);
            return pathFile;
        }
    }
}