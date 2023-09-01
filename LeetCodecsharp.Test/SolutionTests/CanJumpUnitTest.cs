﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;
public class CanJumpUnitTest
{
    [Theory]
    [InlineData(new int[] { 2, 3, 1, 1, 4 }, true)]
    [InlineData(new int[] { 3, 2, 1, 0, 4 }, false)]
    public void MultipleDataTest(int[] nums, bool expected)
    {
        var actual = Solution.CanJump(nums);
        Assert.Equal(expected, actual);
    }
}

