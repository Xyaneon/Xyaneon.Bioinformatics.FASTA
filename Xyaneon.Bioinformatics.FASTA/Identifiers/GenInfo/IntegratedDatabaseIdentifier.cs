namespace Xyaneon.Bioinformatics.FASTA.Identifiers.GenInfo
{
    /// <summary>
    /// A GenInfo integrated database FASTA identifier.
    /// </summary>
    public sealed class IntegratedDatabaseIdentifier : Identifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegratedDatabaseIdentifier"/> class.
        /// </summary>
        /// <param name="value">The identifier value.</param>
        public IntegratedDatabaseIdentifier(int value) : base(Constants.Codes.GenInfoIntegratedDatabase)
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
