using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class DescriptionTest
    {
        private const string Text = "This is a description";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullText()
        {
            _ = new Description(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_ShouldRejectTextContainingPipe()
        {
            _ = new Description("Contains | character");
        }

        [TestMethod]
        public void Constructor_ShouldCreateWithCorrectPropertyValues()
        {
            var description = new Description(Text);
            Assert.AreEqual(Text, description.Text);
        }
    }
}
