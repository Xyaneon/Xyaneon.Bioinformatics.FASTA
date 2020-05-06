using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Xyaneon.Bioinformatics.FASTA.Utility
{
    internal static class StreamUtility
    {
        public static IEnumerable<string> ReadAllLines(Stream stream)
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

        public static async Task<IEnumerable<string>> ReadAllLinesAsync(Stream stream, CancellationToken cancellationToken = default)
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

        public static void WriteAllLines(Stream stream, IEnumerable<string> lines)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                foreach (string line in lines)
                {
                    streamWriter.WriteLine(line);
                }
            }
        }

        public static async Task WriteAllLinesAsync(Stream stream, IEnumerable<string> lines, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Writing all stream lines async canceled before write.", cancellationToken);
            }

            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException("Writing all stream lines async canceled during write.", cancellationToken);
                }

                foreach (string line in lines)
                {
                    await streamWriter.WriteLineAsync(line);
                }
            }
        }
    }
}
