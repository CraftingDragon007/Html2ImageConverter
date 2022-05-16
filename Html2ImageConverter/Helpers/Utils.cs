using System;

namespace Html2ImageConverter.Helpers
{
    public static class Utils
    {
        public static bool IsLocalPath(string path)
        {
            if (path.StartsWith("http"))
            {
                return false;
            }

            return new Uri(path).IsFile;
        }
    }
}
