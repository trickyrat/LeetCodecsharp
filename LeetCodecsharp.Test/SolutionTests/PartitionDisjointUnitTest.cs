﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;
using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;

public class PartitionDisjointUnitTest
{
    public static IEnumerable<object[]> GetData()
    {
        yield return
        [
            new[] { 5, 0, 3, 8, 6 }, 3
        ];
        yield return
        [
            new[] { 1, 1, 1, 0, 6, 12 }, 4
        ];
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void MultipleDataTest(int[] nums, int expected)
    {
        var actual = Solution.PartitionDisjoint(nums);
        Assert.Equal(expected, actual);
    }
}