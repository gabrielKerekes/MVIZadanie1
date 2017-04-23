namespace MVIZadanie1.Model
{
    public static class Source
    {
        private const string SourcesUrl = "http://mechatronika.cool/noviny/zdroje-informacii/";

        public static string GetSourcesHtml()
        {
            var entryContentDiv = MviWebClient.GetPageEntryContent(SourcesUrl);

            return entryContentDiv == null ? "" : entryContentDiv.InnerHtml;
        }
    }
}
