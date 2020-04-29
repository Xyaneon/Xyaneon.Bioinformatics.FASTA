namespace Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo
{
    /// <summary>
    /// A GenInfo backbone SeqID FASTA identifier.
    /// </summary>
    public sealed class BackboneSeqIdIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackboneSeqIdIdentifier"/> class.
        /// </summary>
        /// <param name="value">The identifier value.</param>
        public BackboneSeqIdIdentifier(int value) : base(Constants.Codes.BackboneSeqID)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value of this identifier.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Returns the string representation of this identifier, as it would
        /// appear in the FASTA file.
        /// </summary>
        /// <returns>The string representation of this identifier.</returns>
        public override string ToString()
        {
            return $"{Code}|{Value}";
        }
    }
}
