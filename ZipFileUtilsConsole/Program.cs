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
        private static void ZipFileTest()
        {
            var FileReader = new StreamReader(@"C:\Temp\Zip\AppSettings.json");

            var bytesArray = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                FileReader.BaseStream.CopyTo(memstream);
                bytesArray = memstream.ToArray();
            }


            string Base64EncodedFile = Convert.ToBase64String(bytesArray);

            ZippedMemoryFile ZippedFileData = ZipFileUtilsLib.ZipFileUtils.ZipFileInMemory("AppSettings.json", Base64EncodedFile);

            if (ZippedFileData != null && ZippedFileData.ZippedFileInMemory != null)
            {
                FileStream file = new FileStream(@"C:\Temp\Zip\" + ZippedFileData.Filename, FileMode.Create, FileAccess.Write);
                ZippedFileData.ZippedFileInMemory.WriteTo(file);
                file.Close();
            }
        }

        public static void UnzipSingleFileTest()
        {
            StreamReader FileReader = new StreamReader(@"C:\Temp\Zip\test.zip");

            var bytesZipArray = default(byte[]);
            using (var memstream = new MemoryStream())
            {
                FileReader.BaseStream.CopyTo(memstream);
                bytesZipArray = memstream.ToArray();
            }

            string ZipBase64Contents = Convert.ToBase64String(bytesZipArray);

            UnZippedMemoryFile UnzippedFileData = ZipFileUtilsLib.ZipFileUtils.UnZipFileInMemory(@"test.zip", ZipBase64Contents);

            string Base64UnzippedEncoded = Convert.ToBase64String(UnzippedFileData.UnZippedFileInMemory.GetBuffer());
        }

        static void Main(string[] args)
        {
            /*
             * First Zip a single file
             */
            ZipFileTest();

            /*
             * Unzip Zip Archive that contains a single file
             */
            UnzipSingleFileTest();
        }
    }
}
