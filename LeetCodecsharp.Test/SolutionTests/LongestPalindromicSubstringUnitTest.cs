﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;

public class LongestPalindromicSubstringUnitTest
{
    private readonly Solution _solution;
    public LongestPalindromicSubstringUnitTest()
    {
        _solution = new Solution();
    }
    [Theory]
    [InlineData("babad", new string[] { "bab", "aba" })]
    [InlineData("cbbd", new string[] { "bb" })]
    public void Test(string input, string[] expected)
    {

        var actual = _solution.LongestPalindrome(input);
        Assert.Contains(actual, expected);
    }
}