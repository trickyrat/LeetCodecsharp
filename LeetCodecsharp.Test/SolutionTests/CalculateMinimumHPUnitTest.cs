﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests
{
    public class CalculateMinimumHPUnitTest
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                new int[][]
                {
                    new int[]{ -2, -3, 3 },
                    new int[]{ -5, -10, 1 },
                    new int[]{ 10, 30, -5 },
                },
                7
            };
            yield return new object[]
            {
                new int[][]
                {
                    new int[]{ 0 }
                },
                1
            };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void MultipleDataTest(int[][] dungeon, int expected)
        {
            var actual = Solution.CalculateMinimumHP(dungeon);
            Assert.Equal(expected, actual);
        }
    }
}