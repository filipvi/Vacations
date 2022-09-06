namespace Vacations.Utilities.Navigation
{
    public static class NavigationFilenameExtractor
    {
        public static string ExtractFilename(string href)
        {
            var filename = System.IO.Path.GetFileNameWithoutExtension(href);

            if (filename.Contains("?"))
            {
                var temp = filename.Split('?');
                filename = temp[0];
            }

            return filename;
        }
    }
}
