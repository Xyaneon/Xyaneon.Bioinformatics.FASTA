using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Data;

namespace Xyaneon.Bioinformatics.FASTA.Test.Data
{
    [TestClass]
    public class NucleicAcidSequenceDataTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullString()
        {
            _ = new NucleicAcidSequenceData(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectInvalidCharacterAtStart()
        {
            _ = new NucleicAcidSequenceData("2ATCG");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectInvalidCharacterAtEnd()
        {
            _ = new NucleicAcidSequenceData("ATCG2");
        }

        [TestMethod]
        public void Constructor_ShouldConvertLowercaseSequenceToUppercase()
        {
            var nucleicAcidSequenceData = new NucleicAcidSequenceData("atcg");
            Assert.AreEqual("ATCG", nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Constructor_ShouldConvertMultilineSequenceToSingleLine()
        {
            var nucleicAcidSequenceData = new NucleicAcidSequenceData("ATG\nCAT");
            Assert.AreEqual("ATGCAT", nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Constructor_ShouldAcceptGaps()
        {
            string expectedSequence = "ATC-";
            var nucleicAcidSequenceData = new NucleicAcidSequenceData(expectedSequence);
            Assert.AreEqual(expectedSequence, nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsLineLengthOfZero()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = new NucleicAcidSequenceData(sequence);
            IEnumerable<string> actualLines = nucleicAcidSequenceData.ToLines(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsNegativeLineLength()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = new NucleicAcidSequenceData(sequence);
            IEnumerable<string> actualLines = nucleicAcidSequenceData.ToLines(-1);
        }

        [TestMethod]
        public void ToLines_ProducesExpectedOutput()
        {
            string sequence = "ATCGTA";
            var expectedLines = new string[] { "ATCG", "TA" };

            var nucleicAcidSequenceData = new NucleicAcidSequenceData(sequence);
            IEnumerable<string> actualLines = nucleicAcidSequenceData.ToLines(4);

            Assert.IsTrue(actualLines.SequenceEqual(expectedLines));
        }

        [TestMethod]
        public void ToString_ProducesExpectedOutput()
        {
            string expectedSequence = "ATC-";
            var nucleicAcidSequenceData = new NucleicAcidSequenceData(expectedSequence);
            Assert.AreEqual(expectedSequence, nucleicAcidSequenceData.ToString());
        }
    }
}
