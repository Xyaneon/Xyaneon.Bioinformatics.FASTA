﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Xyaneon.Bioinformatics.FASTA.Sequences;
using Xyaneon.Bioinformatics.FASTA.Identifiers;
using System.Linq;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class SingleFASTAFileDataTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeader()
        {
            _ = new SingleFASTAFileData(null, NucleicAcidSequence.Parse("ATCG"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullData()
        {
            var identifier = new LocalIdentifier("value");
            var header = new Header(identifier);
            _ = new SingleFASTAFileData(header, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullString()
        {
            _ = SingleFASTAFileData.Parse((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullLinesCollection()
        {
            _ = SingleFASTAFileData.Parse((IEnumerable<string>)null);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForString()
        {
            string inputString = ">lcl|123" + Environment.NewLine + "ATCG" + Environment.NewLine + "AAAA";
            SingleFASTAFileData sequence = SingleFASTAFileData.Parse(inputString);

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
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectMissingHeaderForString()
        {
            string inputString = "ATCG" + Environment.NewLine + "AAAA";
            _ = SingleFASTAFileData.Parse(inputString);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectMissingSequenceForString()
        {
            string inputString = ">lcl|123";
            _ = SingleFASTAFileData.Parse(inputString);
        }

        [TestMethod]
        public void Parse_ShouldProduceExpectedOutputForLines()
        {
            IEnumerable<string> inputLines = new string[] {
                ">lcl|123",
                "ATCG",
                "AAAA"
            };
            SingleFASTAFileData sequence = SingleFASTAFileData.Parse(inputLines);

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
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectMissingHeaderForLines()
        {
            IEnumerable<string> inputLines = new string[] {
                "ATCG",
                "AAAA"
            };
            _ = SingleFASTAFileData.Parse(inputLines);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectMissingSequenceForLines()
        {
            IEnumerable<string> inputLines = new string[] {
                ">lcl|123"
            };
            _ = SingleFASTAFileData.Parse(inputLines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToInterleavedLines_ShouldRejectNegativeLineLength()
        {
            string inputSequence = "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSS"
                + "EMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL";

            Identifier identifier = new LocalIdentifier("123");
            Header header = new Header(identifier);
            ISequence sequence = AminoAcidSequence.Parse(inputSequence);
            var singleFASTAFileData = new SingleFASTAFileData(header, sequence);

            _ = singleFASTAFileData.ToInterleavedLines(-1).ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToInterleavedLines_ShouldRejectLineLengthOfZero()
        {
            string inputSequence = "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSS"
                + "EMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL";

            Identifier identifier = new LocalIdentifier("123");
            Header header = new Header(identifier);
            ISequence sequence = AminoAcidSequence.Parse(inputSequence);
            var singleFASTAFileData = new SingleFASTAFileData(header, sequence);

            _ = singleFASTAFileData.ToInterleavedLines(0).ToList();
        }

        [TestMethod]
        public void ToInterleavedLines_ShouldProduceExpectedOutputForDefaultLineLength()
        {
            string inputSequence = "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSS"
                + "EMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL";

            IEnumerable<string> expectedOutputLines = new string[] {
                ">lcl|123",
                "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSSEMFNEFDKRYAQGKGF",
                "ITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL"
            };

            Identifier identifier = new LocalIdentifier("123");
            Header header = new Header(identifier);
            ISequence sequence = AminoAcidSequence.Parse(inputSequence);
            var singleFASTAFileData = new SingleFASTAFileData(header, sequence);

            IEnumerable<string> actualOutputLines = singleFASTAFileData.ToInterleavedLines();

            CollectionAssert.AreEqual(expectedOutputLines.ToList(), actualOutputLines.ToList());
        }

        [TestMethod]
        public void ToInterleavedLines_ShouldProduceExpectedOutputForNonDefaultLineLength()
        {
            string inputSequence = "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSS"
                + "EMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL";

            IEnumerable<string> expectedOutputLines = new string[] {
                ">lcl|123",
                "MDSKGSSQ",
                "KGSRLLLL",
                "LVVSNLLL",
                "CQGVVSTP",
                "VCPNGPGN",
                "CQVSLRDL",
                "FDRAVMVS",
                "HYIHDLSS",
                "EMFNEFDK",
                "RYAQGKGF",
                "ITMALNSC",
                "HTSSLPTP",
                "EDKEQAQQ",
                "THHEVLMS",
                "LILGLLRS",
                "WNDPLYHL"
            };

            Identifier identifier = new LocalIdentifier("123");
            Header header = new Header(identifier);
            ISequence sequence = AminoAcidSequence.Parse(inputSequence);
            var singleFASTAFileData = new SingleFASTAFileData(header, sequence);

            IEnumerable<string> actualOutputLines = singleFASTAFileData.ToInterleavedLines(8);

            CollectionAssert.AreEqual(expectedOutputLines.ToList(), actualOutputLines.ToList());
        }

        [TestMethod]
        public void ToSequentialLines_ShouldProduceExpectedOutput()
        {
            string inputSequence = "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSS"
                + "EMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL";

            IEnumerable<string> expectedOutputLines = new string[] {
                ">lcl|123",
                "MDSKGSSQKGSRLLLLLVVSNLLLCQGVVSTPVCPNGPGNCQVSLRDLFDRAVMVSHYIHDLSSEMFNEFDKRYAQGKGFITMALNSCHTSSLPTPEDKEQAQQTHHEVLMSLILGLLRSWNDPLYHL"
            };

            Identifier identifier = new LocalIdentifier("123");
            Header header = new Header(identifier);
            ISequence sequence = AminoAcidSequence.Parse(inputSequence);
            var singleFASTAFileData = new SingleFASTAFileData(header, sequence);

            IEnumerable<string> actualOutputLines = singleFASTAFileData.ToSequentialLines();

            CollectionAssert.AreEqual(expectedOutputLines.ToList(), actualOutputLines.ToList());
        }
    }
}