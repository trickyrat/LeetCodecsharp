﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace LeetCodecsharp;

public class KthLargest
{
    public PriorityQueue<int, int> PQ { get; set; }
    public int K { get; set; }
    public KthLargest(int k, int[] nums)
    {
        PQ = new PriorityQueue<int, int>();
        K = k;
        foreach (int num in nums)
        {
            Add(num);
        }
    }

    public int Add(int val)
    {
        PQ.Enqueue(val, val);
        if (PQ.Count > K)
        {
            PQ.Dequeue();
        }
        return PQ.Peek();
    }
}
