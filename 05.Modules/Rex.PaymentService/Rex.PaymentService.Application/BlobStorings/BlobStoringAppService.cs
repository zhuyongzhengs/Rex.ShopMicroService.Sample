using System.IO;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace Rex.PaymentService.BlobStorings
{
    /// <summary>
    /// 文件存储
    /// </summary>
    [RemoteService(IsEnabled = false)]
    public class BlobStoringAppService : ApplicationService, IBlobStoringAppService
    {
        private readonly IBlobContainer<BlobPaymentServiceContainer> _blobPaymentContainer;

        public BlobStoringAppService(IBlobContainer<BlobPaymentServiceContainer> blobPaymentContainer)
        {
            _blobPaymentContainer = blobPaymentContainer;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string name)
        {
            await _blobPaymentContainer.DeleteAsync(name);
        }

        /// <summary>
        /// 验证文件是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string name)
        {
            return await _blobPaymentContainer.ExistsAsync(name);
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Stream?> GetOrNullAsync(string name)
        {
            return await _blobPaymentContainer.GetOrNullAsync(name);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task SaveAsync(string name, Stream stream)
        {
            await _blobPaymentContainer.SaveAsync(name, stream, true);
        }
    }
}