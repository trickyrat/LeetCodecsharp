﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using LeetCodecsharp.DataStructure;

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;
public class RotateRightUnitTest
{
    private readonly Solution _solution;

    public RotateRightUnitTest()
    {
        _solution = new Solution();
    }

    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]
        {
            Util.CreateListNode(new int[]{ 1, 2, 3, 4, 5 }),
            2,
            Util.CreateListNode(new int[]{ 4, 5, 1, 2, 3 })
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void MultipleDataTest(ListNode head, int k, ListNode expected)
    {
        var actual = _solution.RotateRight(head, k);
        Assert.Equal(expected, actual, new ListNodeComparer());
    }
}
