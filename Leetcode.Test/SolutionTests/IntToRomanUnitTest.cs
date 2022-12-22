﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCode.Test.SolutionTests;

public class IntToRomanUnitTest
{
    private readonly Solution _solution;
    public IntToRomanUnitTest()
    {
        _solution = new Solution();
    }
    [Theory]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(9, "IX")]
    [InlineData(58, "LVIII")]
    [InlineData(1994, "MCMXCIV")]
    public void MultipleDataTest(int num, string expected)
    {

        var actual = _solution.IntToRoman(num);
        Assert.Equal(expected, actual);
    }

}