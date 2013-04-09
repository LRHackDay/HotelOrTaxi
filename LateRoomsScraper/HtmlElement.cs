using HtmlAgilityPack;

namespace LateRoomsScraper
{
    public interface IRetrieveElementText
    {
        string RetrieveNodeText(HtmlNode parent, string xPath);
        string RetrieveNodeAttribute(HtmlNode parent, string xPath, string attribute);
    }

    public class HtmlElement : IRetrieveElementText
    {
        public string RetrieveNodeText(HtmlNode parent, string xPath)
        {
            var node = parent.SelectSingleNode(xPath);
            return node != null ? node.InnerText.Trim() : null;
        }

        public string RetrieveNodeAttribute(HtmlNode parent, string xPath, string attribute)
        {
            var node = string.IsNullOrEmpty(xPath) ? parent : parent.SelectSingleNode(xPath);
            return node != null ? node.Attributes[attribute].Value : null;
        }
    }
}