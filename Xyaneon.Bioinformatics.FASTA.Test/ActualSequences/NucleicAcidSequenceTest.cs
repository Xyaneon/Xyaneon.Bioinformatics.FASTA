using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.ActualSequences;

namespace Xyaneon.Bioinformatics.FASTA.Test.ActualSequences
{
    [TestClass]
    public class NucleicAcidSequenceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parse_ShouldRejectNullString()
        {
            _ = NucleicAcidSequence.Parse(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectInvalidCharacterAtStart()
        {
            _ = NucleicAcidSequence.Parse("2ATCG");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_ShouldRejectInvalidCharacterAtEnd()
        {
            _ = NucleicAcidSequence.Parse("ATCG2");
        }

        [TestMethod]
        public void Parse_ShouldConvertLowercaseSequenceToUppercase()
        {
            var nucleicAcidSequenceData = NucleicAcidSequence.Parse("atcg");
            Assert.AreEqual("ATCG", nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldConvertMultilineSequenceToSingleLine()
        {
            var nucleicAcidSequenceData = NucleicAcidSequence.Parse("ATG\nCAT");
            Assert.AreEqual("ATGCAT", nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Parse_ShouldAcceptGaps()
        {
            string expectedSequence = "ATC-";
            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(expectedSequence);
            Assert.AreEqual(expectedSequence, nucleicAcidSequenceData.Characters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsLineLengthOfZero()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            _ = nucleicAcidSequenceData.ToLines(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsNegativeLineLength()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            _ = nucleicAcidSequenceData.ToLines(-1);
        }

        [TestMethod]
        public void ToLines_ProducesExpectedOutput()
        {
            string sequence = "ATCGTA";
            var expectedLines = new string[] { "ATCG", "TA" };

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            IEnumerable<string> actualLines = nucleicAcidSequenceData.ToLines(4);

            Assert.IsTrue(actualLines.SequenceEqual(expectedLines));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToMultilineString_RejectsLineLengthOfZero()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            _ = nucleicAcidSequenceData.ToMultilineString(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToMultilineString_RejectsNegativeLineLength()
        {
            string sequence = "ATCGTA";

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            _ = nucleicAcidSequenceData.ToMultilineString(-1);
        }

        [TestMethod]
        public void ToMultilineString_ProducesExpectedOutput()
        {
            string sequence = "ATCGTA";
            string expectedLines = $"ATCG{Environment.NewLine}TA";

            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(sequence);
            string actualLines = nucleicAcidSequenceData.ToMultilineString(4);

            Assert.AreEqual(expectedLines, actualLines);
        }

        [TestMethod]
        public void ToString_ProducesExpectedOutput()
        {
            string expectedSequence = "ATC-";
            var nucleicAcidSequenceData = NucleicAcidSequence.Parse(expectedSequence);
            Assert.AreEqual(expectedSequence, nucleicAcidSequenceData.ToString());
        }
    }
}
