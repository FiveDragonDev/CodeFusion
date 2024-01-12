namespace CodeFusion
{
    public static class FileHelper
    {
        public static FileStream CreateFileWithDirectories(string path)
        {
            string? directoryPath = Path.GetDirectoryName(path);

            if (directoryPath != null && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return File.Create(path);
        }
    }
}
