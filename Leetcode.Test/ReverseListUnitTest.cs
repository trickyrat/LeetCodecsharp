﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using Leetcode.DataStructure;

using Xunit;

namespace Leetcode.Test;

public class ReverseListUnitTest
{
    private readonly Solution _solution;
    public ReverseListUnitTest()
    {
        _solution = new Solution();
    }
    public static IEnumerable<object[]> GetNodes()
    {
        yield return new object[]
        {
            Utilities.CreateListNode(new List<int> { 1, 2, 3, 4, 5 }),
            Utilities.CreateListNode(new List<int> { 5, 4, 3, 2, 1 })
        };
        yield return new object[]
        {
            Utilities.CreateListNode(new List<int> { 1, 2 }),
            Utilities.CreateListNode(new List<int> { 2, 1 })
        };
        yield return new object[]
        {
            null,
            null
        };
    }


    [Theory]
    [MemberData(nameof(GetNodes))]
    public void Test(ListNode head, ListNode expectedNode)
    {
        
        ListNode actualNode = _solution.ReverseList(head);
        string actual = Utilities.ConvertListNodeToString(actualNode);
        string expected = Utilities.ConvertListNodeToString(expectedNode);
        Assert.Equal(expected, actual);
    }
}
