using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Xyaneon.Bioinformatics.FASTA.Utility
{
    internal static class FileUtility
    {
        public static async Task WriteAllLinesAsync(string path, IEnumerable<string> lines, CancellationToken cancellationToken = default)
        {
            using (FileStream fileStream = File.OpenWrite(path))
            {
                await StreamUtility.WriteAllLinesAsync(fileStream, lines, cancellationToken);
            }
        }
    }
}
