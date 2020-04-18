using System.Collections.Generic;

namespace Xyaneon.Bioinformatics.FASTA.Utility
{
    internal static class ListUtility
    {
        public static List<T> CreateSingleElementList<T>(T item)
        {
            return new List<T>(1) { item };
        }
    }
}
