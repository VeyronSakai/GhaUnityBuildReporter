// Copyright (c) 2024 VeyronSakai.
// This software is released under the MIT License.

using GhaUnityBuildReporter.Editor.UseCases;
using NUnit.Framework;

namespace GhaUnityBuildReporter.Editor.Tests
{
    public sealed class SizeFormatterTest
    {
        [TestCase(1000UL, "1000 B")]
        [TestCase(2048UL, "2.00 KB")]
        [TestCase(3145728UL, "3.00 MB")]
        [TestCase(3670016UL, "3.50 MB")]
        [TestCase(4294967296UL, "4.00 GB")]
        [TestCase(4831838208UL, "4.50 GB")]
        [TestCase(10737418240UL, "10.00 GB")]
        [TestCase(11274289152UL, "10.50 GB")]
        [TestCase(161061273600UL, "150.00 GB")]
        public void GetFormattedSizeTest(ulong size, string expected)
        {
            // Arrange
            var actual = SizeFormatter.GetFormattedSize(size);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
