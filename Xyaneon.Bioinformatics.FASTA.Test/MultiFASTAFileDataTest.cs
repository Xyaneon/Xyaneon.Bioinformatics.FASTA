using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Sequences;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class MultiFASTAFileDataTest
    {
        private const string Description1Text = "Description 1";
        private const string Description2Text = "Description 2";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequence()
        {
            _ = new MultiFASTAFileData((SingleFASTAFileData)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequencesCollection()
        {
            _ = new MultiFASTAFileData((IEnumerable<SingleFASTAFileData>)null);
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnTrueIfAllSequencesAreForAminoAcids()
        { 
            var sequences = new SingleFASTAFileData[] {
                new SingleFASTAFileData(new Header(new Description(Description1Text)), AminoAcidSequence.Parse("ABCD")),
                new SingleFASTAFileData(new Header(new Description(Description2Text)), AminoAcidSequence.Parse("EFGH"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForAminoAcids()
        {
            var sequences = new SingleFASTAFileData[] {
                new SingleFASTAFileData(new Header(new Description(Description1Text)), AminoAcidSequence.Parse("ABCD")),
                new SingleFASTAFileData(new Header(new Description(Description2Text)), NucleicAcidSequence.Parse("ATGC"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnTrueIfAllSequencesAreForNucleicAcids()
        {
            var sequences = new SingleFASTAFileData[] {
                new SingleFASTAFileData(new Header(new Description(Description1Text)), NucleicAcidSequence.Parse("ATGC")),
                new SingleFASTAFileData(new Header(new Description(Description2Text)), NucleicAcidSequence.Parse("GCTA"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyNucleicAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForNucleicAcids()
        {
            var sequences = new SingleFASTAFileData[] {
                new SingleFASTAFileData(new Header(new Description(Description1Text)), NucleicAcidSequence.Parse("ATGC")),
                new SingleFASTAFileData(new Header(new Description(Description2Text)), AminoAcidSequence.Parse("ABCD"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyNucleicAcidSequences());
        }
    }
}
