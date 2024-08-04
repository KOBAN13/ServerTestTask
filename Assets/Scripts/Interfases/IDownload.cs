using Cysharp.Threading.Tasks;

namespace Interfases
{
    public interface IDownload
    {
        void DownloadFileFromServer(AnyBundle bundle);
    }
}