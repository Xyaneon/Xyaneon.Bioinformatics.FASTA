using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Test.Extensions;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class MultiFASTAFileReaderTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadFromStream_ShouldRejectNullStream()
        {
            _ = MultiFASTAFileReader.ReadFromStream(null);
        }

        [TestMethod]
        public void ReadFromStream_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileReader.ReadFromStream(stream);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.SingleFASTASequences.Count);

            SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[0];
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
        public void ReadFromStream_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            MultiFASTAFileData multiFASTAFileData = MultiFASTAFileReader.ReadFromStream(stream);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.SingleFASTASequences.Count);

            {
                SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[0];
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
                SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[1];
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
        public async Task ReadFromStreamAsync_ShouldRejectNullStream()
        {
            _ = await MultiFASTAFileReader.ReadFromStreamAsync(null);
        }

        [TestMethod]
        public async Task ReadFromStreamAsync_ShouldProduceExpectedOutputForOneSequence()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA").ToStream();
            MultiFASTAFileData multiFASTAFileData = await MultiFASTAFileReader.ReadFromStreamAsync(stream);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(1, multiFASTAFileData.SingleFASTASequences.Count);

            SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[0];
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
        public async Task ReadFromStreamAsync_ShouldProduceExpectedOutputForTwoSequences()
        {
            Stream stream = string.Join(Environment.NewLine, ">lcl|123", "ATCG", "AAAA", "", ">lcl|456", "TTTT", "CCCC").ToStream();
            MultiFASTAFileData multiFASTAFileData = await MultiFASTAFileReader.ReadFromStreamAsync(stream);

            Assert.IsNotNull(multiFASTAFileData);
            Assert.AreEqual(2, multiFASTAFileData.SingleFASTASequences.Count);

            {
                SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[0];
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
                SingleFASTAFileData sequence = multiFASTAFileData.SingleFASTASequences[1];
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
