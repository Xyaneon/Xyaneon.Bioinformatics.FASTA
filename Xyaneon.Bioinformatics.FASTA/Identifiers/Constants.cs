namespace Xyaneon.Bioinformatics.FASTA.Identifiers
{
    /// <summary>
    /// Provides constants related to FASTA identifiers.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Constants for FASTA identifier codes.
        /// </summary>
        public static class Codes
        {
            /// <summary>
            /// The code for FASTA GenInfo backbone moltype identifiers.
            /// </summary>
            public const string BackboneMolType = "bbm";

            /// <summary>
            /// The code for FASTA GenInfo backbone SeqID identifiers.
            /// </summary>
            public const string BackboneSeqID = "bbs";

            /// <summary>
            /// The code for FASTA EMBL identifiers.
            /// </summary>
            public const string EMBL = "emb";

            /// <summary>
            /// The code for FASTA GenBank identifiers.
            /// </summary>
            public const string GenBank = "gb";

            /// <summary>
            /// The code for FASTA GenInfo import ID identifiers.
            /// </summary>
            public const string ImportId = "gim";

            /// <summary>
            /// The code for FASTA local identifiers.
            /// </summary>
            public const string Local = "lcl";

            /// <summary>
            /// The code for FASTA PIR identifiers.
            /// </summary>
            public const string PIR = "pir";
        }
    }
}
