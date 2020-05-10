namespace Xyaneon.Bioinformatics.FASTA.IO
{
    /// <summary>
    /// Provides constants for FASTA filename extensions.
    /// </summary>
    /// <remarks>
    /// See https://en.wikipedia.org/wiki/FASTA_format#Filename_extension for
    /// a description of these extensions.
    /// </remarks>
    public static class FilenameExtensions
    {
        /// <summary>
        /// Filename extension for FASTA files containing generic amino acid
        /// sequences.
        /// </summary>
        public static string FASTAAminoAcid = ".faa";

        /// <summary>
        /// Filename extension for FASTA files containing non-coding RNA
        /// regions of a genome using DNA nucleotides. This can include
        /// tRNA and rRNA.
        /// </summary>
        public static string FASTANonCodingRNA = ".frn";

        /// <summary>
        /// Filename extension for FASTA files containing generic nucleic acid
        /// sequences.
        /// </summary>
        public static string FASTANucleicAcid = ".fna";

        /// <summary>
        /// Filename extension for FASTA files containing coding regions of a
        /// genome.
        /// </summary>
        public static string FASTANucleotideOfGeneRegions = ".ffn";

        /// <summary>
        /// Filename extension for FASTA files containing generic nucleic acid
        /// sequences for multiple proteins.
        /// </summary>
        public static string FASTAMultipleProtein = ".mpfa";

        /// <summary>
        /// Long filename extension for generic FASTA files.
        /// </summary>
        /// <seealso cref="GenericFASTAShort"/>
        public static string GenericFASTALong = ".fasta";

        /// <summary>
        /// Short filename extension for generic FASTA files.
        /// </summary>
        /// <seealso cref="GenericFASTALong"/>
        public static string GenericFASTAShort = ".fa";
    }
}
