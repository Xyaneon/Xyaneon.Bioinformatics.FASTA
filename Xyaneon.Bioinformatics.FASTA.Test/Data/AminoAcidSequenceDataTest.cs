using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xyaneon.Bioinformatics.FASTA.Data;

namespace Xyaneon.Bioinformatics.FASTA.Test.Data
{
    [TestClass]
    public class AminoAcidSequenceDataTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullString()
        {
            _ = new AminoAcidSequenceData(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectInvalidCharacterAtStart()
        {
            _ = new AminoAcidSequenceData("2ABC");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectInvalidCharacterAtEnd()
        {
            _ = new AminoAcidSequenceData("ABC2");
        }

        [TestMethod]
        public void Constructor_ShouldConvertLowercaseSequenceToUppercase()
        {
            var aminoAcidSequenceData = new AminoAcidSequenceData("abc");
            Assert.AreEqual("ABC", aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Constructor_ShouldConvertMultilineSequenceToSingleLine()
        {
            var aminoAcidSequenceData = new AminoAcidSequenceData("ABC\nDEF");
            Assert.AreEqual("ABCDEF", aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Constructor_ShouldAcceptGaps()
        {
            string expectedSequence = "ABC-";
            var aminoAcidSequenceData = new AminoAcidSequenceData(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        public void Constructor_ShouldAcceptTranslationStop()
        {
            string expectedSequence = "ABC*";
            var aminoAcidSequenceData = new AminoAcidSequenceData(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.Characters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsLineLengthOfZero()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = new AminoAcidSequenceData(sequence);
            IEnumerable<string> actualLines = aminoAcidSequenceData.ToLines(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ToLines_RejectsNegativeLineLength()
        {
            string sequence = "ABCDEF";

            var aminoAcidSequenceData = new AminoAcidSequenceData(sequence);
            IEnumerable<string> actualLines = aminoAcidSequenceData.ToLines(-1);
        }

        [TestMethod]
        public void ToLines_ProducesExpectedOutput()
        {
            string sequence = "ABCDEF";
            var expectedLines = new string[] { "ABCD", "EF" };

            var aminoAcidSequenceData = new AminoAcidSequenceData(sequence);
            IEnumerable<string> actualLines = aminoAcidSequenceData.ToLines(4);

            Assert.IsTrue(actualLines.SequenceEqual(expectedLines));
        }

        [TestMethod]
        public void ToString_ProducesExpectedOutput()
        {
            string expectedSequence = "ABC-";
            var aminoAcidSequenceData = new AminoAcidSequenceData(expectedSequence);
            Assert.AreEqual(expectedSequence, aminoAcidSequenceData.ToString());
        }
    }
}
