using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Xyaneon.Bioinformatics.FASTA.Utility
{
    internal static class StreamUtility
    {
        public static IEnumerable<string> ReadAllLinesFromStream(Stream stream)
        {
            var lines = new List<string>();

            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (streamReader.Peek() > -1)
                {
                    lines.Add(streamReader.ReadLine());
                }
            }

            return lines;
        }

        public static async Task<IEnumerable<string>> ReadAllLinesFromStreamAsync(Stream stream, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Reading all stream lines async canceled before read.", cancellationToken);
            }

            var lines = new List<string>();

            using (StreamReader streamReader = new StreamReader(stream))
            {
                while (streamReader.Peek() > -1)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        throw new OperationCanceledException("Reading all stream lines async canceled during read.", cancellationToken);
                    }

                    lines.Add(await streamReader.ReadLineAsync());
                }
            }

            return lines;
        }
    }
}
