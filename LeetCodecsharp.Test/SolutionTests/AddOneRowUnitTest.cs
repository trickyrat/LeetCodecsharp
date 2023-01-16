﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using LeetCode.DataStructure;

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;
public class AddOneRowUnitTest
{
    private readonly Solution _solution;

    public AddOneRowUnitTest()
    {
        _solution = new Solution();
    }

    public static IEnumerable<object[]> GetData()
    {
        yield return new object[]
        {
            Utilities.CreateTreeNode(new List<int?>{ 4,2,6,3,1,5}),
            1,
            2,
            Utilities.CreateTreeNode(new List<int?>{ 4,1,1,2,null,null,6,3,1,5})
        };
        yield return new object[]
        {
            Utilities.CreateTreeNode(new List<int?>{4,2,null,3,1}),
            1,
            3,
            Utilities.CreateTreeNode(new List<int?>{4,2,null,1,1,3,null,null,1 })
        };
    }

    [Theory]
    [MemberData(nameof(GetData))]
    public void MultipleDataTest(TreeNode root, int val, int depth, TreeNode expectedNode)
    {
        var actualNode = _solution.AddOneRow(root, val, depth);
        var actual = Utilities.PreorderTraversal(actualNode);
        var expected = Utilities.PreorderTraversal(expectedNode);
        Assert.Equal(expected, actual);
    }
}

