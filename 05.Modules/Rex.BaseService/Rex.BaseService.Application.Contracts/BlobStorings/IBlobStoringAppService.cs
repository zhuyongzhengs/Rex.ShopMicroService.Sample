using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Rex.BaseService.BlobStorings
{
    /// <summary>
    /// 文件存储接口
    /// </summary>
    public interface IBlobStoringAppService : IApplicationService
    {
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<byte[]?> GetAllBytesOrNullAsync(string name);

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Stream?> GetOrNullAsync(string name);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        Task<string> SaveAsync(string name, byte[] bytes);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<string> SaveAsync(string name, Stream stream);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task DeleteAsync(string name);

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string name);
    }
}