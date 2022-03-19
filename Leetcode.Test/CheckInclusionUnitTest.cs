﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace Leetcode.Test
{
    public class CheckInclusionUnitTest
    {
        [Theory]
        [InlineData("ab", "eidbaooo", true)]
        [InlineData("ab", "eidboaoo", false)]
        public void Test_Should_Return_True(string s1, string s2, bool expected)
        {
            bool actual = Solution.CheckInclusion(s1, s2);
            Assert.Equal(expected, actual);
        }
    }
}
