﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;

public class MaxAscendingSumUnitTest
{
    [Theory]
    [InlineData(new int[] { 10, 20, 30, 5, 10, 50 }, 65)]
    [InlineData(new int[] { 10, 20, 30, 40, 50 }, 150)]
    [InlineData(new int[] { 12, 17, 15, 13, 10, 11, 12 }, 33)]
    public void Test(int[] arr, int expected)
    {
        var actual = Solution.MaxAscendingSum(arr);
        Assert.Equal(expected, actual);
    }
}