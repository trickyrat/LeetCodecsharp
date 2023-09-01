﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;
public class ShortestBridgeUnitTest
{
    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]
        {
            new int[][]
            {
                new int[]{ 0, 1 },
                new int[]{ 1, 0 }
            },
            1
        };
        yield return new object[]
        {
            new int[][]
            {
                new int[]{ 0, 1, 0 },
                new int[]{ 0, 0, 0 },
                new int[]{ 0, 0, 1 }
            },
            2
        };
        yield return new object[]
        {
            new int[][]
            {
                new int[]{ 1, 1, 1, 1, 1 },
                new int[]{ 1, 0, 0, 0, 1 },
                new int[]{ 1, 0, 1, 0, 1 },
                new int[]{ 1, 0, 0, 0, 1 },
                new int[]{ 1, 1, 1, 1, 1 },
            },
            1
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void MultipleDataTest(int[][] grid, int expected)
    {
        var actual = Solution.ShortestBridge(grid);
        Assert.Equal(expected, actual);
    }

}

