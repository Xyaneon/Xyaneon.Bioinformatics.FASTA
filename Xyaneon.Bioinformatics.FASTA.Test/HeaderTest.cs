using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Xyaneon.Bioinformatics.FASTA.Test
{
    [TestClass]
    public class HeaderTest
    {
        private const string DescriptionText1 = "Description 1";
        private const string DescriptionText2 = "Description 2";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeaderItem()
        {
            _ = new Header((HeaderItem)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ShouldRejectNullHeaderItemsCollection()
        {
            _ = new Header((IEnumerable<HeaderItem>)null);
        }

        [TestMethod]
        public void ToString_ShouldProduceExpectedOutputForSingleItem()
        {
            string expected = $">{DescriptionText1}";
            var header = new Header(new List<HeaderItem>(1) { new Description(DescriptionText1) });

            Assert.AreEqual(expected, header.ToString());
        }

        [TestMethod]
        public void ToString_ShouldProduceExpectedOutputForMultipleItems()
        {
            string expected = $">{DescriptionText1}|{DescriptionText2}";
            var header = new Header(new List<HeaderItem>(2) { new Description(DescriptionText1), new Description(DescriptionText2) });

            Assert.AreEqual(expected, header.ToString());
        }
    }
}
