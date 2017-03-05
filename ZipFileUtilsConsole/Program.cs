using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipFileUtilsLib;
using System.IO;

namespace ZipFileUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            var FileReader = new StreamReader(@"C:\Temp\Zip\AppSettings.json");

            var bytesArray = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                FileReader.BaseStream.CopyTo(memstream);
                bytesArray = memstream.ToArray();
            }


            string Base64EncodedFile = Convert.ToBase64String(bytesArray);

            ZippedMemoryFile ZippedFileData = ZipFileUtilsLib.ZipFileUtils.GetZippedMemoryFile("AppSettings.json", Base64EncodedFile);

            if (ZippedFileData != null && ZippedFileData.ZippedFileInMemory != null)
            {
                FileStream file = new FileStream(@"C:\Temp\Zip\" + ZippedFileData.Filename, FileMode.Create, FileAccess.Write);
                ZippedFileData.ZippedFileInMemory.WriteTo(file);
                file.Close();
            }

        }
    }
}
