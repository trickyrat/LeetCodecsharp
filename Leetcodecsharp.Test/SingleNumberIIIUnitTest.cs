﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace Leetcodecsharp.Test
{
    public class SingleNumberIIIUnitTest
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 1, 3, 2, 5 }, new int[] { 3, 5 })]
        [InlineData(new int[] { -1, 0 }, new int[] { -1, 0 })]
        [InlineData(new int[] { 0, 1 }, new int[] { 1, 0 })]
        public void Test(int[] nums, int[] expected)
        {
            int[] actual = Solution.SingleNumberIII(nums);
            Assert.Equal(expected, actual);
        }
    }
}