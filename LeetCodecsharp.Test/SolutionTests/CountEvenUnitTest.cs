﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;

public class CountEvenUnitTest
{
    private readonly Solution _solution;

    public CountEvenUnitTest()
    {
        _solution = new Solution();
    }

    [Theory]
    [InlineData(4, 2)]
    [InlineData(30, 14)]
    public void Test(int num, int expected)
    {
        var actual = _solution.CountEven(num);
        Assert.Equal(expected, actual);
    }
}