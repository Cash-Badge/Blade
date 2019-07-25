using System;
using System.IO;
using System.Linq;

namespace Acklann.Plaid.MSTest
{
    public static class TestDataDirectory
    {
        public const string FOLDER_NAME = "TestData";

        public static FileInfo GetFile(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FOLDER_NAME)).GetFiles($"*{Path.GetExtension(fileName)}", SearchOption.AllDirectories).First(file => file.Name.Equals(fileName, StringComparison.CurrentCultureIgnoreCase));
        }

        public static string GetFileContent(string fileName) => File.ReadAllText(GetFile(fileName).FullName);
    }
}