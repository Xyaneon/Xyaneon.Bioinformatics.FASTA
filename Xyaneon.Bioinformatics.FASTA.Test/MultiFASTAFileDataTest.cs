using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;

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
            _ = new MultiFASTAFileData((Sequence)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullSequencesCollection()
        {
            _ = new MultiFASTAFileData((IEnumerable<Sequence>)null);
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnTrueIfAllSequencesAreForAminoAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), AminoAcidSequence.Parse("ABCD")),
                new Sequence(new Header(new Description(Description2Text)), AminoAcidSequence.Parse("EFGH"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyAminoAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForAminoAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), AminoAcidSequence.Parse("ABCD")),
                new Sequence(new Header(new Description(Description2Text)), NucleicAcidSequence.Parse("ATGC"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyAminoAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnTrueIfAllSequencesAreForNucleicAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), NucleicAcidSequence.Parse("ATGC")),
                new Sequence(new Header(new Description(Description2Text)), NucleicAcidSequence.Parse("GCTA"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsTrue(fileData.ContainsOnlyNucleicAcidSequences());
        }

        [TestMethod]
        public void ContainsOnlyNucleicAcidSequences_ShouldReturnFalseIfSomeSequencesAreNotForNucleicAcids()
        {
            var sequences = new Sequence[] {
                new Sequence(new Header(new Description(Description1Text)), NucleicAcidSequence.Parse("ATGC")),
                new Sequence(new Header(new Description(Description2Text)), AminoAcidSequence.Parse("ABCD"))
            };
            var fileData = new MultiFASTAFileData(sequences);

            Assert.IsFalse(fileData.ContainsOnlyNucleicAcidSequences());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullString()
        {
            _ = MultiFASTAFileData.Parse((string)null);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForOneSequence()
        {
            string inputString = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA");
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileData.Parse(inputString);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.SingleFASTASequences.Count);

            Sequence sequence = multiFASTAFileData.SingleFASTASequences[0];
            Assert.IsNotNull(sequence);

            Header header = sequence.Header;
            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
            Assert.IsNotNull(identifier);
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);

            NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
            Assert.AreEqual("ATCGAAAA", data.Characters);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForTwoSequences()
        {
            string inputString = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC");
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileData.Parse(inputString);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.SingleFASTASequences.Count);

            {
                Sequence sequence = multiFASTAFileData.SingleFASTASequences[0];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("123", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("ATCGAAAA", data.Characters);
            }

            {
                Sequence sequence = multiFASTAFileData.SingleFASTASequences[1];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("456", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("TTTTCCCC", data.Characters);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullLinesCollection()
        {
            _ = MultiFASTAFileData.Parse((IEnumerable<string>)null);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForLinesForOneSequence()
        {
            IEnumerable<string> lines = new List<string>() { ">lcl|123", "ATCG", "AAAA" };
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileData.Parse(lines);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.SingleFASTASequences.Count);

            Sequence sequence = multiFASTAFileData.SingleFASTASequences[0];
            Assert.IsNotNull(sequence);

            Header header = sequence.Header;
            Assert.IsNotNull(header);
            Assert.AreEqual(1, header.Items.Count);
            LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
            Assert.IsNotNull(identifier);
            Assert.AreEqual("lcl", identifier.Code);
            Assert.AreEqual("123", identifier.Value);

            NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
            Assert.AreEqual("ATCGAAAA", data.Characters);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForLinesForTwoSequences()
        {
            IEnumerable<string> lines = new List<string>() { ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC" };
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileData.Parse(lines);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.SingleFASTASequences.Count);

            {
                Sequence sequence = multiFASTAFileData.SingleFASTASequences[0];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("123", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("ATCGAAAA", data.Characters);
            }

            {
                Sequence sequence = multiFASTAFileData.SingleFASTASequences[1];
                Assert.IsNotNull(sequence);

                Header header = sequence.Header;
                Assert.IsNotNull(header);
                Assert.AreEqual(1, header.Items.Count);
                LocalIdentifier identifier = header.Items[0] as LocalIdentifier;
                Assert.IsNotNull(identifier);
                Assert.AreEqual("lcl", identifier.Code);
                Assert.AreEqual("456", identifier.Value);

                NucleicAcidSequence data = (NucleicAcidSequence)sequence.Data;
                Assert.AreEqual("TTTTCCCC", data.Characters);
            }
        }
    }
}
