﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using Xunit;

namespace LeetCodecsharp.Test.SolutionTests;

public class UniqueMorseRepresentationsUnitTest

{
    [Theory]
    [InlineData(new string[] { "gin", "zen", "gig", "msg" }, 2)]
    [InlineData(new string[] { "a" }, 1)]
    public void MultipleDataTest(string[] words, int expected)
    {
        var actual = Solution.UniqueMorseRepresentations(words);
        Assert.Equal(expected, actual);
    }
}