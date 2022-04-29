using Xunit;
using site_services.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace site_services.tests
{
    public class CoordinateExtensionsTest
    {
        [Fact]
        public void Range_0D_ThrowsInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => CoordinateExtensions.Range());
        }

        [Fact]
        public void Range_1D_GeneratesCorrectly()
        {
            var expected = new int[]{0,1,2,3,4,5,6,7,8,9};
            var actual = CoordinateExtensions.Range(10).Select(e => e[0]);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Range_2D_3X1_GeneratesCorrectly()
        {
            int[][] expected = { new int[]{0,0}, new int[]{1,0}, new int[]{2,0}};
            var actual = CoordinateExtensions.Range(3,1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Range_2D_3X3_GeneratesCorrectly()
        {
            int[][] expected = { 
                new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 },
                new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, 2 },
                new int[] { 2, 0 }, new int[] { 2, 1 }, new int[] { 2, 2 },
            };
            var actual = CoordinateExtensions.Range(3, 3);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Range_3D__3X2X2_GeneratesCorrectly()
        {
            int[][] expected = {
                new int[] { 0, 0, 0 }, new int[] { 0, 0, 1 }, new int[] { 0, 1, 0}, new int[] { 0, 1, 1 },
                new int[] { 1, 0, 0 }, new int[] { 1, 0, 1 }, new int[] { 1, 1, 0}, new int[] { 1, 1, 1 },
                new int[] { 2, 0, 0 }, new int[] { 2, 0, 1 }, new int[] { 2, 1, 0}, new int[] { 2, 1, 1 }
            };
            var actual = CoordinateExtensions.Range(3,2,2);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Range_3D_GeneratesCorrectCount()
        {
            var actual = CoordinateExtensions.Range(3, 3, 3);
            Assert.Equal(27, actual.Length);
        }
    }
}