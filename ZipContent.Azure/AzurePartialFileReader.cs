using Azure;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ZipContent.Core;

namespace ZipContent.Azure
{
    public class AzurePartialFileReader : IPartialFileReader
    {
        private readonly BlobClient _blobClient;

        public AzurePartialFileReader(BlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task<long> ContentLength(CancellationToken cancellationToken = default)
        {
            var props = await _blobClient.GetPropertiesAsync(null, cancellationToken);
            return props.Value.ContentLength;
        }

        public async Task<byte[]> GetBytes(ByteRange range, CancellationToken cancellationToken = default)
        {

            var data = await _blobClient.DownloadAsync(new HttpRange(range.Start, range.End - range.Start), null, false, cancellationToken);
            return StreamToArray(data.Value.Content);

        }

        private byte[] StreamToArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}
