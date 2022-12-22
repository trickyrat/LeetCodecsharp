﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using LeetCode.DataStructure;

using Xunit;

namespace LeetCode.Test.SolutionTests;

public class ReverseKGroupUnitTest
{
    private readonly Solution _solution;

    public ReverseKGroupUnitTest()
    {
        _solution = new Solution();
    }

    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]
        {
            Utilities.CreateListNode(new int[]{ 1,2,3,4,5}),
            2,
            Utilities.CreateListNode(new int[]{ 2,1,4,3,5}),
        };
        yield return new object[]
        {
            Utilities.CreateListNode(new int[]{ 1,2,3,4,5}),
            3,
            Utilities.CreateListNode(new int[]{ 3,2,1,4,5}),
        };
    }


    [Theory]
    [MemberData(nameof(GetData))]
    public void MultipleDataTest(ListNode head, int k, ListNode expected)
    {
        var actual = _solution.ReverseKGroup(head, k);
        Assert.Equal(expected, actual, new ListNodeComparer());
    }
}