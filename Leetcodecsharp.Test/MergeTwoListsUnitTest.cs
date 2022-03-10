﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

using Leetcodecsharp.DataStructure;

using Xunit;

namespace Leetcodecsharp.Test
{
    public class MergeTwoListsUnitTest
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                Utilities.CreateListNode(new List<int> { 1, 2, 4 }),
                Utilities.CreateListNode(new List<int> { 1, 3, 4 }),
                new List<int> { 1, 1, 2, 3, 4, 4 }
            };
            yield return new object[]
             {
                  null,
                  null,
                  new List<int>()
             };
            yield return new object[]
            {
                   null,
                   Utilities.CreateListNode(new List<int> { 0 }),
                   new List<int>{ 0 }
            };
        }


        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(ListNode l1, ListNode l2, List<int> expected)
        {
            ListNode actualNode = Solution.MergeTwoLists(l1, l2);
            List<int> actual = Utilities.ConvertListNodeToList(actualNode);
            Assert.Equal(expected, actual);
        }
    }
}
