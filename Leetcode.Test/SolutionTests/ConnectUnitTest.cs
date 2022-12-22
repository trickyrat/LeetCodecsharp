﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using LeetCode.DataStructure;

using Xunit;

namespace LeetCode.Test.SolutionTests;

public class ConnectUnitTest
{
    private readonly Solution _solution;

    public ConnectUnitTest()
    {
        _solution = new Solution();
    }
    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]
        {
            new BinaryTreeNode(),
            new BinaryTreeNode()
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void Test(BinaryTreeNode input, BinaryTreeNode expected)
    {
        var actual = _solution.Connect(input);
        //Assert.Equal(expected, actual);
    }
}
