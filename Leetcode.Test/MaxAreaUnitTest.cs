﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace Leetcode.Test
{
    public class MaxAreaUnitTest
    {
        [Theory]
        [InlineData(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 49)]
        [InlineData(new int[] { 1, 1 }, 1)]
        public void MultipleDataTest(int[] height, int expected)
        {
            int actual = Solution.MaxArea(height);
            Assert.Equal(expected, actual);
        }
    }
}