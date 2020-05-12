using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.ActualSequences
{
    internal static class SequenceUtility
    {
        internal const string ArgumentOutOfRangeException_LineLengthLessThanOne = "The length of each line cannot be less than one.";

        public static IEnumerable<string> ToLines(string sequenceData, int lineLength = Constants.DefaultLineLength)
        {
            return sequenceData.SplitBy(lineLength);
        }

        public static string ToMultilineString(string sequenceData, int lineLength = Constants.DefaultLineLength)
        {
            if (lineLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(lineLength), lineLength, ArgumentOutOfRangeException_LineLengthLessThanOne);
            }

            if (lineLength == 1)
            {
                return sequenceData;
            }

            return string.Join(Environment.NewLine, ToLines(sequenceData, lineLength));
        }
    }
}
