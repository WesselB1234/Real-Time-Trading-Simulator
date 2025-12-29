namespace RealTimeStockSimulator.Models.Helpers
{
    public static class ImageEncoder
    {
        public static byte[] GetBytesFromImagePath(string path)
        {
            return File.ReadAllBytes(path);
        }

        public static string GetBase64StringFromImageBytes(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
