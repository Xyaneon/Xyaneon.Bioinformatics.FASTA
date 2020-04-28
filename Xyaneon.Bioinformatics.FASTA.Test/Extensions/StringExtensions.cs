using System.IO;

namespace Xyaneon.Bioinformatics.FASTA.Test.Extensions
{
    internal static class StringExtensions
    {
        public static Stream ToStream(this string str)
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);

            writer.Write(str);
            writer.Flush();
            memoryStream.Position = 0;

            return memoryStream;
        }
    }
}
