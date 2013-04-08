using HtmlAgilityPack;
using WebResponse;

namespace LateRoomsScraper
{
    public interface IDownloadHtml
    {
        HtmlDocument GetHtmlDocument(string url);
    }

    public class DownloadHtml : IDownloadHtml
    {
        private readonly IDownloadResponses _responseDownloader;

        public DownloadHtml(IDownloadResponses responseDownloader)
        {
            _responseDownloader = responseDownloader;
        }

        public HtmlDocument GetHtmlDocument(string url)
        {
            var html = _responseDownloader.Get(url);
            
            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document;
        }
    }
}