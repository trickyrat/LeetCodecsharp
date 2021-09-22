﻿namespace Leetcodecsharp.DataStructure
{
    /// <summary>
    /// Definition for singly-linked list.
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x) => val = x;
        public ListNode(int x, ListNode next)
        {
            val = x;
            this.next = next;
        }
    }
}
