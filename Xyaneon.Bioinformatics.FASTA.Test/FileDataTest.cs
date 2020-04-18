using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Data;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class FileDataTest
    {
        private const string Description1Text = "Description 1";
        private const string Description2Text = "Description 2";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequence()
        {
            _ = new FileData((Sequence)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequencesCollection()
        {
            _ = new FileData((IEnumerable<Sequence>)null);
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnTrueIfAllSequencesAreForAminoAcids()
        { 
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), new AminoAcidSequenceData("ABCD")),
                new Sequence(new Header(new Description(Description2Text)), new AminoAcidSequenceData("EFGH"))
            };
            var fileData = new FileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForAminoAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), new AminoAcidSequenceData("ABCD")),
                new Sequence(new Header(new Description(Description2Text)), new NucleicAcidSequenceData("ATGC"))
            };
            var fileData = new FileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnTrueIfAllSequencesAreForNucleicAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), new NucleicAcidSequenceData("ATGC")),
                new Sequence(new Header(new Description(Description2Text)), new NucleicAcidSequenceData("GCTA"))
            };
            var fileData = new FileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyNucleicAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForNucleicAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), new NucleicAcidSequenceData("ATGC")),
                new Sequence(new Header(new Description(Description2Text)), new AminoAcidSequenceData("ABCD"))
            };
            var fileData = new FileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyNucleicAcidSequences());
        }
    }
}
