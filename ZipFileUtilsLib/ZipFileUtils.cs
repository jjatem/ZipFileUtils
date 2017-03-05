using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ZipFileUtilsLib
{
    public class ZippedMemoryFile
    {
        public string Filename { get; set; }
        public MemoryStream ZippedFileInMemory { get; set; }
    }

    public class ZipFileUtils
    {
        public static ZippedMemoryFile GetZippedMemoryFile(string FileName, string Base64EncodedContents)
        {
            ZippedMemoryFile rv = new ZippedMemoryFile();

            try
            {
                /*
                 * Convert Base64 File Contents to Byte Array
                 */
                byte[] Base64DecodedFile = Convert.FromBase64String(Base64EncodedContents);

                /*
                 * Compress/Zip file and return it in a MemoryStream
                 */
                var compressedFileOutStream = new MemoryStream();

                using (var MyCompressor = new ZipArchive(compressedFileOutStream, ZipArchiveMode.Create, true))
                {
                    var compressedFile = MyCompressor.CreateEntry(FileName, CompressionLevel.Optimal);
                    using (var entryStream = compressedFile.Open())
                    {
                        using (var FileToCompressStream = new MemoryStream(Base64DecodedFile))
                        {
                            FileToCompressStream.CopyTo(entryStream);
                        }

                        rv.Filename = FileName + ".zip";
                        rv.ZippedFileInMemory = compressedFileOutStream;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error has ocurred Compressing Base64 encoded file with filename {FileName}. Error Message was: {ex.Message} ");
            }

            return rv;
        }
    }
}
