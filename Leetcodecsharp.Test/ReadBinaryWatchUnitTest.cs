﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Leetcodecsharp.Test
{
    public class ReadBinaryWatchUnitTest
    {
        [Theory]
        [InlineData(1, new string[] { "0:01", "0:02", "0:04", "0:08", "0:16", "0:32", "1:00", "2:00", "4:00", "8:00" })]
        [InlineData(9, new string[] { })]
        public void Test(int turnedOn, string[] expected)
        {
            IList<string> actual = Solution.ReadBinaryWatch(turnedOn);
            Assert.Equal(expected.ToList(), actual);
        }
    }
}