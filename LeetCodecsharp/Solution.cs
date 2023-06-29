﻿// Licensed to the Trickyrat under one or more agreements.
// The Trickyrat licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeetCodecsharp.DataStructure;

namespace LeetCodecsharp;

/// <summary>
/// LeetCode Solution Class
/// </summary>
public class Solution
{
    /// <summary>
    /// 1. Two Sum
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] TwoSum(int[] nums, int target)
    {
        var res = new int[2];
        var dic = new Dictionary<int, int>();

        for (var i = 0; i < nums.Length; i++)
        {
            if (dic.TryGetValue(target - nums[i], out var value))
            {
                res[1] = i;
                res[0] = value;
                break;
            }

            if (!dic.ContainsKey(nums[i]))
            {
                dic.Add(nums[i], i);
            }
        }

        return res;
    }

    /// <summary>
    /// 2. Add Two Numbers
    /// </summary>
    /// <param name="l1"></param>
    /// <param name="l2"></param>
    /// <returns></returns>
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        // 116ms
        var dummyHead = new ListNode(0);
        if (l1 == null && l2 == null)
        {
            return dummyHead;
        }

        var carry = 0;
        var curr = dummyHead;
        while (l1 != null || l2 != null)
        {
            var num1 = l1?.val ?? 0;
            var num2 = l2?.val ?? 0;
            var sum = num1 + num2 + carry;
            curr.next = new ListNode(sum % 10);
            curr = curr.next;
            carry = sum / 10;
            l1 = l1?.next;
            l2 = l2?.next;
        }

        if (carry != 0)
        {
            curr.next = new ListNode(carry);
        }

        return dummyHead.next;

        // 148ms
        // ListNode r = new ListNode(-1);
        // ListNode n = r;
        // int carry = 0;
        // while (carry > 0 || l1 != null || l2 != null)
        // {
        //     int v = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        //     carry = v / 10;
        //     n = n.next = new ListNode(v % 10);
        //     l1 = l1?.next;
        //     l2 = l2?.next;
        // }
        // return r.next;
    }

    /// <summary>
    /// 3. Longest Substring Without Repeating Characters
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int LengthOfLongestSubstring(string s)
    {
        // 96 ms int[]
        // int n = s.Length, ans = 0;
        // int[] index = new int[128];
        // for (int j = 0, i = 0; j < n; j++)
        // {
        //     i = Math.Max(index[s[j]], i);
        //     ans = Math.Max(ans, j - i + 1);
        //     index[s[j]] = j + 1;
        // }
        // return ans;

        // 100 ms HashSet<char>
        // int len = s.Length;
        // HashSet<char> set = new HashSet<char>();
        // int ans = 0, i = 0, j = 0;
        // while (i < len && j < len)
        // {
        //     if (!set.Contains(s[j]))
        //     {
        //         set.Add(s[j++]);
        //         ans = Math.Max(ans, j - i);
        //     }
        //     else set.Remove(s[i++]);
        // }
        // return ans;

        // 100ms Dictionary<char, int>
        int n = s.Length, ans = 0;
        var dic = new Dictionary<char, int>();
        for (int j = 0, i = 0; j < n; j++)
        {
            if (dic.ContainsKey(s[j]))
            {
                i = Math.Max(dic[s[j]], i);
                dic[s[j]] = j + 1;
            }
            else
            {
                dic.Add(s[j], j + 1);
            }

            ans = Math.Max(ans, j - i + 1);
        }

        return ans;
    }

    /// <summary>
    /// 4. Median of Two Sorted Arrays
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int m = nums1.Length, n = nums2.Length;
        if (m > n)
        {
            (nums1, nums2) = (nums2, nums1);
            (m, n) = (n, m);
        }

        // use binary search
        int min = 0, max = m, halfLen = (m + n + 1) / 2;
        while (min <= max)
        {
            var i = min + (max - min) / 2;
            var j = halfLen - i;
            if (i < max && nums2[j - 1] > nums1[i])
            {
                min++;
            }
            else if (i > min && nums1[i - 1] > nums2[j])
            {
                max--;
            }
            else
            {
                int maxLeft;
                if (i == 0)
                {
                    maxLeft = nums2[j - 1];
                }
                else if (j == 0)
                {
                    maxLeft = nums1[i - 1];
                }
                else
                {
                    maxLeft = Math.Max(nums1[i - 1], nums2[j - 1]);
                }

                if ((m + n) % 2 == 1)
                {
                    return maxLeft;
                }

                int minRight;
                if (i == m)
                {
                    minRight = nums2[j];
                }
                else if (j == n)
                {
                    minRight = nums1[i];
                }
                else
                {
                    minRight = Math.Min(nums2[j], nums1[i]);
                }

                return (maxLeft + minRight) / 2.0;
            }
        }

        return 0.0;
    }

    /// <summary>
    /// 5. Longest Palindromic Substring
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string LongestPalindrome(string s)
    {
        var T = PreProcess(s);
        var n = T.Length;
        var P = new int[n];
        int C = 0, R = 0;
        for (var i = 1; i < n - 1; i++)
        {
            var mirror = 2 * C - i;
            P[i] = (R > 1) ? Math.Min(R - i, P[mirror]) : 0;
            while (T[i + 1 + P[i]] == T[i - 1 - P[i]])
            {
                P[i]++;
            }

            if (i + P[i] <= R)
            {
                continue;
            }

            C = i;
            R = i + P[i];
        }

        // Find the maximum element in P
        var maxLen = 0;
        var centerIndex = 0;
        for (var i = 1; i < n - 1; i++)
        {
            if (P[i] <= maxLen)
            {
                continue;
            }

            maxLen = P[i];
            centerIndex = i;
        }

        return s.Substring((centerIndex - 1 - maxLen) / 2, maxLen);

        string PreProcess(string input)
        {
            var len = input.Length;
            if (len == 0)
            {
                return "^$";
            }

            var ret = "^";
            for (var i = 0; i < len; i++)
            {
                ret += string.Concat("#", input.AsSpan(i, 1));
            }

            ret += "#$";
            return ret;
        }
    }

    /// <summary>
    /// <para>6. ZigZag Conversion</para>
    /// </summary>
    /// <param name="s"></param>
    /// <param name="numRows"></param>
    /// <returns></returns>
    public string Convert(string s, int numRows)
    {
        if (numRows <= 1)
        {
            return s;
        }

        var res = new StringBuilder();
        for (var i = 0; i < numRows; i++)
        {
            for (var j = 0; j < s.Length; j += 2 * numRows - 2)
            {
                var index = i + j;
                if (index < s.Length)
                {
                    res.Append(s[index]);
                }

                if (i == 0 || i == numRows - 1)
                {
                    continue;
                }

                index = j + 2 * numRows - 2 - i;
                if (index < s.Length)
                {
                    res.Append(s[index]);
                }
            }
        }

        return res.ToString();
    }

    /// <summary>
    /// 7. Reverse Integer
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int Reverse(int x)
    {
        var rev = 0;
        while (x != 0)
        {
            var pop = x % 10;
            x /= 10;
            switch (rev)
            {
                case > int.MaxValue / 10:
                case int.MaxValue / 10 when pop > 7:
                case < int.MinValue / 10:
                case int.MinValue / 10 when pop < -8:
                    return 0;
                default:
                    rev = rev * 10 + pop;
                    break;
            }
        }

        return rev;
    }

    /// <summary>
    /// 8. String to Integer(atoi)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public int Atoi(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return 0;
        }

        var sign = 1;
        var bas = 0;
        var i = 0;
        while (str[i] == ' ')
        {
            i++;
        }

        if (str[i] == '-' || str[i] == '+')
        {
            sign = str[i++] == '-' ? -1 : 1;
        }

        while (i < str.Length && str[i] >= '0' && str[i] <= '9')
        {
            if (bas > int.MaxValue / 10 || (bas == int.MaxValue / 10 && str[i] - '0' > 7))
            {
                return (sign == 1) ? int.MaxValue : int.MinValue;
            }

            bas = 10 * bas + (str[i++] - '0');
        }

        return bas * sign;
    }

    /// <summary>
    /// 9. Palindrome Number
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public bool IsPalindrome(int x)
    {
        if (x < 0 || (x != 0 && x % 10 == 0))
        {
            return false;
        }

        var res = 0;
        while (res < x)
        {
            res = res * 10 + x % 10;
            x /= 10;
        }

        return (x == res || x == res / 10);
        // if (x < 10) return true;
        // int n = 0, temp = x;
        // while (temp / 10 != 0)
        // {
        //     n += temp % 10;
        //     n *= 10;
        //     temp /= 10;
        // }
        // n += temp % 10;
        // return (n == x);
    }

    /// <summary>
    /// 10. Regular Expression Matching
    /// </summary>
    /// <param name="text">string input</param>
    /// <param name="pattern">match pattern</param>
    /// <returns></returns>
    public bool IsMatch(string text, string pattern)
    {
        var dp = new bool[text.Length + 1, pattern.Length + 1];
        dp[text.Length, pattern.Length] = true;
        for (var i = text.Length; i >= 0; i--)
        {
            for (var j = pattern.Length - 1; j >= 0; j--)
            {
                var firstMatch = i < text.Length && (pattern[j] == text[i] || pattern[j] == '.');
                if (j + 1 < pattern.Length && pattern[j + 1] == '*')
                {
                    dp[i, j] = dp[i, j + 2] || firstMatch && dp[i + 1, j];
                }
                else
                {
                    dp[i, j] = firstMatch && dp[i + 1, j + 1];
                }
            }
        }

        return dp[0, 0];
    }

    /// <summary>
    /// 11. Container With Most Water
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public int MaxArea(int[] height)
    {
        var maxArea = 0;
        int left = 0, right = height.Length - 1;
        while (left < right)
        {
            maxArea = Math.Max(maxArea, Math.Min(height[left], height[right]) * (right - left));
            if (height[left] < height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return maxArea;
    }

    /// <summary>
    /// 12. Integer to Roman
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string IntToRoman(int num)
    {
        string[] M = { "", "M", "MM", "MMM" };
        string[] C = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] X = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] I = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
        return M[num / 1000] + C[(num % 1000) / 100] + X[(num % 100) / 10] + I[num % 10];
    }

    /// <summary>
    /// 13. Roman to Integer
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int RomanToInt(string s)
    {
        var dic = new Dictionary<char, int>
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };
        var value = 0;
        var prev = s[0];
        foreach (var curr in s)
        {
            value += dic[curr];
            if (dic[prev] < dic[curr])
            {
                value -= dic[prev] * 2;
            }

            prev = curr;
        }

        return value;
    }

    /// <summary>
    /// 14. Longest Common Prefix
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0)
        {
            return "";
        }

        var pre = strs[0];
        for (var i = 1; i < strs.Length; i++)
        {
            while (strs[i].IndexOf(pre, StringComparison.Ordinal) != 0)
            {
                pre = pre[..^1];
            }
        }

        return pre;
    }

    /// <summary>
    /// 15. Three Sum
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        var res = new List<IList<int>>();
        //for (int i = 0; i < nums.Length - 2; i++)
        //{
        //    if (i == 0 || (i > 0 && nums[i] != nums[i - 1]))
        //    {
        //        int lo = i + 1, hi = nums.Length - 1, sum = 0 - nums[i];
        //        // two sum
        //        while (lo < hi)
        //        {
        //            if (nums[lo] + nums[hi] == sum)
        //            {
        //                res.Add(
        //                    new List<int>
        //                    {
        //                        nums[i],
        //                        nums[lo],
        //                        nums[hi]
        //                    });

        //                while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
        //                while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
        //                lo++;
        //                hi--;
        //            }
        //            else if (nums[lo] + nums[hi] < sum)
        //            {
        //                while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
        //                lo++;
        //            }
        //            else
        //            {
        //                while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
        //                hi--;
        //            }
        //        }
        //    }
        //}

        var len = nums.Length;
        for (var i = 0; i < len; i++)
        {
            var target = -nums[i];
            var left = i + 1;
            var right = len - 1;
            if (target < 0)
            {
                break;
            }

            while (left < right)
            {
                var sum = nums[left] + nums[right];
                if (sum < target)
                {
                    left++;
                }
                else if (sum > target)
                {
                    right--;
                }
                else
                {
                    var tmp = new List<int> { nums[i], nums[left], nums[right] };
                    res.Add(tmp);
                    while (left < right && nums[left] < tmp[1])
                    {
                        left++;
                    }

                    while (left < right && nums[right] == tmp[2])
                    {
                        right--;
                    }
                }
            }

            while (i + 1 < len && nums[i + 1] == nums[i])
            {
                i++;
            }
        }

        return res;
    }

    /// <summary>
    /// 16. Three Sum Closest
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int ThreeSumClosest(int[] nums, int target)
    {
        if (nums.Length < 3)
        {
            return 0;
        }

        var closest = nums[0] + nums[1] + nums[2];
        var len = nums.Length;
        Array.Sort(nums);
        for (var first = 0; first < len - 2; first++)
        {
            if (first > 0 && nums[first] == nums[first - 1])
            {
                continue;
            }

            var second = first + 1;
            var third = len - 1;
            while (second < third)
            {
                var currentSum = nums[first] + nums[second] + nums[third];
                if (currentSum == target)
                {
                    return currentSum;
                }

                if (Math.Abs(target - currentSum) < Math.Abs(target - closest))
                {
                    closest = currentSum;
                }

                if (currentSum > target)
                {
                    third--;
                }
                else
                {
                    second++;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// 17. Letter Combinations of a Phone Number
    /// </summary>
    /// <param name="digits">input digits</param>
    /// <returns></returns>
    public IList<string> LetterCombinations(string digits)
    {
        string[] map = { "0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
        if (string.IsNullOrEmpty(digits))
        {
            return new List<string>();
        }

        var ans = new Queue<string>();
        ans.Enqueue("");
        for (var i = 0; i < digits.Length; i++)
        {
            var x = digits[i] - '0';
            while (ans.Peek().Length == i)
            {
                var t = ans.Dequeue();
                foreach (var c in map[x])
                {
                    ans.Enqueue(t + c);
                }
            }
        }

        return ans.ToList();
    }

    /// <summary>
    /// 18. 4Sum
    /// </summary>
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        IList<IList<int>> res = new List<IList<int>>();
        var n = nums.Length;
        if (n < 4)
        {
            return res;
        }

        Array.Sort(nums);
        for (var i = 0; i < n - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            if (nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3] > target)
            {
                break;
            }

            if (nums[i] + nums[n - 3] + nums[n - 2] + nums[n - 1] < target)
            {
                continue;
            }

            for (var j = i + 1; j < n - 2; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1])
                {
                    continue;
                }

                if (nums[i] + nums[j] + nums[j + 1] + nums[j + 2] > target)
                {
                    break;
                }

                if (nums[i] + nums[j] + nums[n - 2] + nums[n - 1] < target)
                {
                    continue;
                }

                int left = j + 1, right = n - 1;
                while (left < right)
                {
                    var sum = nums[left] + nums[right] + nums[i] + nums[j];
                    if (sum < target)
                    {
                        left++;
                    }
                    else if (sum > target)
                    {
                        right--;
                    }
                    else
                    {
                        res.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                        do
                        {
                            left++;
                        } while (nums[left] == nums[left - 1] && left < right);

                        do
                        {
                            right--;
                        } while (nums[right] == nums[right + 1] && left < right);
                    }
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 19. Remove Nth Node From End of List
    /// </summary>
    /// <param name="head"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        var dummy = new ListNode(0, head);
        var first = dummy;
        var second = dummy;
        for (var i = 0; i <= n; i++)
        {
            first = first.next;
        }

        while (first is not null)
        {
            first = first.next;
            second = second.next;
        }

        second.next = second.next.next;
        return dummy.next;
    }

    /// <summary>
    /// 20. Valid Parentheses
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool IsValid(string s)
    {
        var stack = new Stack<char>();
        foreach (var item in s)
        {
            switch (item)
            {
                case '(':
                    stack.Push(')');
                    break;
                case '{':
                    stack.Push('}');
                    break;
                case '[':
                    stack.Push(']');
                    break;
                default:
                    {
                        if (stack.Count == 0 || stack.Pop() != item)
                        {
                            return false;
                        }

                        break;
                    }
            }
        }

        return stack.Count == 0;
    }

    /// <summary>
    /// 21. Merge Two Sorted Lists
    /// </summary>
    /// <param name="l1">first list</param>
    /// <param name="l2">second list</param>
    /// <returns>merged list</returns>
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        var dummyHead = new ListNode(0);
        var head = dummyHead;
        while (l1 != null && l2 != null)
        {
            if (l1.val <= l2.val)
            {
                head.next = l1;
                l1 = l1.next;
            }
            else
            {
                head.next = l2;
                l2 = l2.next;
            }

            head = head.next;
        }

        head.next = l1 ?? l2;
        return dummyHead.next;

        //if (l1 == null) { return l2; }
        //if (l2 == null) { return l1; }
        //ListNode ans = null;
        //if (l1.val < l2.val)
        //{
        //    ans = l1;
        //    ans.next = MergeTwoLists(l1.next, l2);
        //}
        //else
        //{
        //    ans = l2;
        //    ans.next = MergeTwoLists(l1, l2.next);
        //}
        //return ans;
    }

    /// <summary>
    /// 22. Generate Parentheses
    /// </summary>
    public IList<string> GenerateParenthesis(int n)
    {
        IList<string> ans = new List<string>();
        // if (n == 0)
        //     ans.Add("");
        // else
        // {
        //     for (int c = 0; c < n; c++)
        //         foreach (string left in GenerateParenthesis(c))
        //             foreach (string right in GenerateParenthesis(n - 1 - c))
        //                 ans.Add("(" + left + ")" + right);
        // }
        // return ans;
        Backtrack(ans, "", 0, 0, n);
        return ans;

        void Backtrack(IList<string> list, string str, int open, int close, int n)
        {
            if (str.Length == n * 2)
            {
                list.Add(str);
                return;
            }

            if (open < n)
            {
                Backtrack(list, str + "(", open + 1, close, n);
            }

            if (close < open)
            {
                Backtrack(list, str + ")", open, close + 1, n);
            }
        }
    }

    /// <summary>
    /// 23. Merge K Sorted Lists
    /// </summary>
    /// <param name="lists">the array of lists</param>
    /// <returns>merged list</returns>
    public ListNode MergeKLists(ListNode[] lists)
    {
        var len = lists.Length;
        var interval = 1;
        while (interval < len)
        {
            for (var i = 0; i < len - interval; i += interval * 2)
            {
                lists[i] = MergeTwoLists(lists[i], lists[i + interval]);
            }

            interval *= 2;
        }

        return len > 0 ? lists[0] : null;
    }

    /// <summary>
    /// 24. Swap Node in Pairs
    /// </summary>
    public ListNode SwapPairs(ListNode head)
    {
        var dummy = new ListNode(0)
        {
            next = head
        };
        var curr = dummy;
        while (curr.next?.next != null)
        {
            var a = curr.next;
            var b = curr.next.next;
            a.next = b.next;
            curr.next = b;
            curr.next.next = a;
            curr = curr.next.next;
        }

        return dummy.next;
    }

    /// <summary>
    /// 25. Reverse Nodes in k-Group
    /// </summary>
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        // Non-recursive
        var n = 0;
        for (var i = head; i != null;)
        {
            n++;
            i = i.next;
        }

        var dummy = new ListNode(0)
        {
            next = head
        };
        for (ListNode prev = dummy, tail = head; n >= k; n -= k)
        {
            for (var i = 1; i < k; i++)
            {
                var next = tail.next.next;
                tail.next.next = prev.next;
                prev.next = tail.next;
                tail.next = next;
            }

            prev = tail;
            tail = tail.next;
        }

        return dummy.next;

        // Recursive
        // ListNode curr = head;
        // int count = 0;
        // while(curr!=null && count != k)
        // {
        //     curr = curr.next;
        //     count++;
        // }
        // if(count == k)
        // {
        //     curr = ReverseKGroup(curr, k);
        //     while(count-- > 0)
        //     {
        //         ListNode tmp = head.next;
        //         head.next = curr;
        //         curr = head;
        //         head = tmp;
        //     }
        //     head = curr;
        // }
        // return head;
    }

    /// <summary>
    /// 26. Remove Duplicates from Sorted Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int RemoveDuplicates(int[] nums)
    {
        var i = nums.Length > 0 ? 1 : 0;
        foreach (var n in nums)
        {
            if (n > nums[i - 1])
            {
                nums[i++] = n;
            }
        }

        return i;
    }

    /// <summary>
    /// 27. Remove Element
    /// </summary>
    public int RemoveElement(int[] nums, int val)
    {
        var len = nums.Length;
        var found = 0;
        for (var i = 0; i < len; i++)
        {
            if (found > 0)
            {
                nums[i - found] = nums[i];
            }

            if (nums[i] == val)
            {
                found++;
            }
        }

        return len - found;
    }

    /// <summary>
    /// 28. Implement strStr()
    /// </summary>
    /// <param name="haystack"></param>
    /// <param name="needle"></param>
    /// <returns></returns>
    public int StrStr(string haystack, string needle)
    {
        int m = haystack.Length, n = needle.Length;
        if (n < 1)
        {
            return 0;
        }

        var lps = KMPProcess(needle);
        for (int i = 0, j = 0; i < m;)
        {
            if (haystack[i] == needle[j])
            {
                i++;
                j++;
            }

            if (j == n)
            {
                return i - j;
            }

            if (i >= m || haystack[i] == needle[j])
            {
                continue;
            }

            if (j > 0)
            {
                j = lps[j - 1];
            }
            else
            {
                i++;
            }
        }

        return -1;

        List<int> KMPProcess(string s)
        {
            var n = needle.Length;
            var lps = new List<int>();
            for (var i = 0; i < n; i++)
            {
                lps.Add(0);
            }

            for (int i = 1, len = 0; i < n;)
            {
                if (s[i] == s[len])
                {
                    lps[i] = ++len;
                    i++;
                }
                else if (len > 0)
                {
                    len = lps[len - 1];
                }
                else
                {
                    lps[i++] = 0;
                }
            }

            return lps;
        }
    }

    /// <summary>
    /// 29. Divide Two Integers
    /// </summary>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public int Divide(int dividend, int divisor)
    {
        if (divisor == 0 || (dividend == int.MinValue && divisor == -1))
        {
            return int.MaxValue;
        }

        var sign = dividend < 0 ^ divisor < 0 ? -1 : 1;
        var dvd = Math.Abs((long)dividend);
        var dvs = Math.Abs((long)divisor);
        var res = 0;
        while (dvd >= dvs)
        {
            var tmp = dvs;
            var multiple = 1;
            while (dvd >= (tmp << 1))
            {
                tmp <<= 1;
                multiple <<= 1;
            }

            dvd -= tmp;
            res += multiple;
        }

        return sign * res;
    }

    /// <summary>
    /// 30. Substring with Concatenation of All Words
    /// </summary>
    public IList<int> FindSubstring(string s, string[] words)
    {
        IList<int> ret = new List<int>();
        if (s.Length == 0 || words.Length == 0)
        {
            return ret;
        }

        int n = s.Length, size = words.Length, len = words[0].Length;
        var map = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (map.ContainsKey(word))
            {
                map[word]++;
            }
            else
            {
                map.Add(word, 1);
            }
        }

        for (var i = 0; i < len; i++)
        {
            int left = i, count = 0;
            var window = new Dictionary<string, int>();
            for (var j = i; j + len - 1 < n; j += len)
            {
                var tmp = s.Substring(j, len);
                if (!map.ContainsKey(tmp))
                {
                    window.Clear();
                    count = 0;
                    left = j + len;
                }
                else
                {
                    if (window.ContainsKey(tmp))
                    {
                        window[tmp]++;
                    }
                    else
                    {
                        window.Add(tmp, 1);
                    }

                    count++;
                    while (left + len - 1 < n && window[tmp] > map[tmp])
                    {
                        window[s.Substring(left, len)]--;
                        count--;
                        left += len;
                    }

                    if (count != size)
                    {
                        continue;
                    }

                    ret.Add(left);
                    window[s.Substring(left, len)]--;
                    count--;
                    left += len;
                }
            }
        }

        return ret;
    }

    /// <summary>
    /// 31. Next Permutation
    /// </summary>
    public void NextPermutation(int[] nums)
    {
        var i = nums.Length - 2;
        while (i >= 0 && nums[i + 1] <= nums[i])
        {
            i--;
        }

        if (i >= 0)
        {
            var j = nums.Length - 1;
            while (j >= 0 && nums[j] <= nums[i])
            {
                j--;
            }

            Util.Swap(ref nums[i], ref nums[j]);
        }

        Reverse(nums, i + 1);

        void Reverse(int[] data, int start)
        {
            int i = start, j = data.Length - 1;
            while (i < j)
            {
                Util.Swap(ref data[i], ref data[j]);
                i++;
                j--;
            }
        }
    }

    /// <summary>
    /// 32. Longest Valid Parentheses
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int LongestValidParentheses(string s)
    {
        //int maxans = 0;
        //int len = s.Length;
        //int[] dp = new int[len];
        //for (int i = 1; i < len; i++)
        //{
        //    if (s[i] == ')')
        //    {
        //        if (s[i - 1] == '(')
        //        {
        //            dp[i] = (i >= 2 ? dp[i - 2] : 0) + 2;
        //        }
        //        else if (i - dp[i - 1] > 0 && s[i - dp[i - 1] - 1] == '(')
        //        {
        //            dp[i] = dp[i - 1] + ((i - dp[i - 1]) >= 2 ? dp[i - dp[i - 1] - 2] : 0) + 2;
        //        }
        //        maxans = Math.Max(maxans, dp[i]);
        //    }
        //}
        //return maxans;
        var len = s.Length;
        int left = 0, right = 0, maxLen = 0;
        for (var i = 0; i < len; i++)
        {
            if (s[i] == '(')
            {
                left++;
            }
            else
            {
                right++;
            }

            if (left == right)
            {
                maxLen = Math.Max(maxLen, 2 * right);
            }
            else if (right > left)
            {
                left = right = 0;
            }
        }

        left = right = 0;
        for (var i = len - 1; i >= 0; i--)
        {
            if (s[i] == '(')
            {
                left++;
            }
            else
            {
                right++;
            }

            if (left == right)
            {
                maxLen = Math.Max(maxLen, 2 * left);
            }
            else if (left > right)
            {
                left = right = 0;
            }
        }

        return maxLen;
    }

    /// <summary>
    /// 33. Search in Rotated Sorted Array
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int Search(int[] nums, int target)
    {
        var n = nums.Length;
        switch (n)
        {
            case 0:
                return -1;
            case 1:
                return nums[0] == target ? 0 : -1;
        }

        int l = 0, r = n - 1;
        while (l <= r)
        {
            var mid = (l + r) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[0] <= nums[mid])
            {
                if (nums[0] <= target && target < nums[mid])
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid + 1;
                }
            }
            else
            {
                if (nums[mid] < target && target <= nums[n - 1])
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid - 1;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// 34. Find First and Last Position of Element in Sorted Array
    /// </summary>
    public int[] SearchRange(int[] nums, int target)
    {
        var missingResult = new int[] { -1, -1 };
        //int[] res = new int[] { 0, 0 };
        //if (nums.Length < 1)
        //{
        //    return missingResult;
        //}
        //int lo = 0, hi = nums.Length - 1;
        //while (lo < hi)
        //{
        //    int mid = lo + (hi - lo) / 2;
        //    if (nums[mid] >= target)
        //    {
        //        hi = mid;
        //    }
        //    else
        //    {
        //        lo = mid + 1;
        //    }
        //}
        //int first = nums[lo] == target ? lo : -1;
        //if (first == -1)
        //{
        //    return missingResult;
        //}
        //lo = first;
        //hi = nums.Length - 1;
        //while (lo < hi)
        //{
        //    int mid = lo + (hi - lo + 1) / 2;
        //    if (nums[mid] <= target)
        //    {
        //        lo = mid;
        //    }
        //    else
        //    {
        //        hi = mid - 1;
        //    }
        //}
        //res[0] = first;
        //res[1] = lo;
        //return res;
        var leftIndex = BinarySearch(nums, target, true);
        var rightIndex = BinarySearch(nums, target, false) - 1;
        if (leftIndex <= rightIndex && rightIndex < nums.Length && nums[leftIndex] == nums[rightIndex])
        {
            return new[] { leftIndex, rightIndex };
        }

        return missingResult;

        int BinarySearch(int[] nums, int target, bool lower)
        {
            int l = 0, r = nums.Length - 1, ans = nums.Length;
            while (l <= r)
            {
                var mid = l + (r - l) / 2;
                if (nums[mid] > target || (lower && nums[mid] >= target))
                {
                    r = mid - 1;
                    ans = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }

            return ans;
        }
    }

    /// <summary>
    /// 35. Search Insert Position
    /// </summary>
    public int SearchInsert(int[] nums, int target)
    {
        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }

        return nums[left] < target ? left + 1 : left;
    }

    /// <summary>
    /// 36. Valid Sudoku
    /// </summary>
    public bool IsValidSudoku(char[][] board)
    {
        // Dictionary<int, int>[] rows = new Dictionary<int, int>[9]; 
        // Dictionary<int, int>[] columns = new Dictionary<int, int>[9]; 
        // Dictionary<int, int>[] boxes = new Dictionary<int, int>[9]; 

        // for (int i = 0; i < 9; i++)
        // {
        //     rows[i] = new Dictionary<int, int>();
        //     columns[i] = new Dictionary<int, int>();
        //     boxes[i] = new Dictionary<int, int>();
        // }
        // for (int i = 0; i < 9; i++)
        // {
        //     for(int j = 0; j < 9; j++)
        //     {
        //         char num = board[i][j];
        //         if(num != '.')
        //         {
        //             int n = (int)num;
        //             int box_index = (i / 3) * 3 + j / 3;
        //             rows[i][n] = rows[i].GetValueOrDefault(n, 0) + 1;
        //             columns[j][n] = columns[j].GetValueOrDefault(n, 0) + 1;
        //             boxes[box_index][n] = boxes[box_index].GetValueOrDefault(n, 0) + 1;
        //             if (rows[i][n] > 1 || columns[j][n] > 1 || boxes[box_index][n] > 1)
        //                 return false;
        //         }
        //     }
        // }
        // return true;

        var seen = new HashSet<string>();
        for (var i = 0; i < 9; i++)
        {
            for (var j = 0; j < 9; j++)
            {
                var num = board[i][j];
                if (num == '.')
                {
                    continue;
                }

                if (!seen.Add(num + " in row " + i) ||
                    !seen.Add(num + " in column " + j) ||
                    !seen.Add(num + " in block " + i / 3 + "-" + j / 3))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 37. Sudoku Solver 
    /// </summary>
    public void SolveSudoku(char[][] board)
    {
        bool DoSolve(char[][] dataBoard, int row, int col)
        {
            for (var i = row; i < 9; i++, col = 0)
            {
                for (var j = col; j < 9; j++)
                {
                    if (dataBoard[i][j] != '.')
                    {
                        continue;
                    }

                    for (var num = '1'; num <= '9'; num++)
                    {
                        if (!IsValid(dataBoard, i, j, num))
                        {
                            continue;
                        }

                        dataBoard[i][j] = num;
                        if (DoSolve(dataBoard, i, j + 1))
                        {
                            return true;
                        }

                        dataBoard[i][j] = '.';
                    }

                    return false;
                }
            }

            return true;
        }

        bool IsValid(char[][] dataBoard, int row, int col, char num)
        {
            int blkRow = (row / 3) * 3, blkCol = (col / 3) * 3;
            for (var i = 0; i < 9; i++)
            {
                if (dataBoard[i][col] == num || dataBoard[row][i] == num ||
                    dataBoard[blkRow + i / 3][blkCol + i % 3] == num)
                {
                    return false;
                }
            }

            return true;
        }

        DoSolve(board, 0, 0);
    }

    /// <summary>
    /// 38. Count and Say
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public string CountAndSay(int n)
    {
        if (n < 0)
        {
            return "-1";
        }

        var result = "1";
        for (var i = 1; i < n; ++i)
        {
            result = Build(result);
        }

        return result;

        string Build(string s)
        {
            var sb = new StringBuilder();
            var p = 0;
            while (p < s.Length)
            {
                var val = s[p];
                var count = 0;
                while (p < s.Length && s[p] == val)
                {
                    p++;
                    count++;
                }

                sb.Append(count);
                sb.Append(val);
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 39. Combination Sum
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        IList<IList<int>> ans = new List<IList<int>>();
        var combine = new List<int>();
        Dfs(candidates, target, ans, combine, 0);
        return ans;

        void Dfs(int[] data, int target, IList<IList<int>> ans, List<int> combine, int idx)
        {
            if (idx == data.Length)
            {
                return;
            }

            if (target == 0)
            {
                ans.Add(new List<int>(combine));
                return;
            }

            Dfs(data, target, ans, combine, idx + 1);
            if (target - data[idx] < 0)
            {
                return;
            }

            combine.Add(data[idx]);
            Dfs(data, target - data[idx], ans, combine, idx);
            combine.RemoveAt(combine.Count - 1);
        }
    }

    /// <summary>
    /// 40. Combination Sum II
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        var freq = new List<int[]>();
        IList<IList<int>> ans = new List<IList<int>>();
        var sequence = new List<int>();

        Array.Sort(candidates);
        foreach (var num in candidates)
        {
            var size = freq.Count;
            if (freq.Count == 0 || num != freq[size - 1][0])
            {
                freq.Add(new int[] { num, 1 });
            }
            else
            {
                ++freq[size - 1][1];
            }
        }

        DFS(0, target);
        return ans;

        void DFS(int pos, int rest)
        {
            if (rest == 0)
            {
                ans.Add(new List<int>(sequence));
                return;
            }

            if (pos == freq.Count || rest < freq[pos][0])
            {
                return;
            }

            DFS(pos + 1, rest);
            var most = Math.Min(rest / freq[pos][0], freq[pos][1]);
            for (var i = 1; i <= most; i++)
            {
                sequence.Add(freq[pos][0]);
                DFS(pos + 1, rest - i * freq[pos][0]);
            }

            for (var i = 1; i <= most; i++)
            {
                sequence.RemoveAt(sequence.Count - 1);
            }
        }
    }

    /// <summary>
    /// 41. First Missing Positive
    /// </summary>
    public int FirstMissingPositive(int[] nums)
    {
        var len = nums.Length;
        for (var i = 0; i < len; i++)
        {
            while (nums[i] > 0 && nums[i] <= len && nums[nums[i] - 1] != nums[i])
            {
                Util.Swap(ref nums[i], ref nums[nums[i] - 1]);
            }
        }

        for (var i = 0; i < len; i++)
        {
            if (nums[i] != i + 1)
            {
                return i + 1;
            }
        }

        return len + 1;
    }

    /// <summary>
    /// 42. Trap Rain Water
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public int Trap(int[] height)
    {
        if (height == null)
        {
            return 0;
        }

        int l = 0, r = height.Length - 1;
        int lMax = 0, rMax = 0;
        var ans = 0;
        while (l < r)
        {
            if (l >= r)
            {
                continue;
            }

            if (height[l] < height[r])
            {
                if (height[l] >= lMax)
                {
                    lMax = height[l];
                }
                else
                {
                    ans += (lMax - height[l]);
                }

                l++;
            }
            else
            {
                if (height[r] >= rMax)
                {
                    rMax = height[r];
                }
                else
                {
                    ans += (rMax - height[r]);
                }

                r--;
            }
        }

        return ans;
    }

    /// <summary>
    /// 43. Multiply String
    /// </summary>
    public string Multiply(string num1, string num2)
    {
        int m = num1.Length, n = num2.Length;
        var pos = new int[m + n];
        for (var i = m - 1; i >= 0; i--)
        {
            for (var j = n - 1; j >= 0; j--)
            {
                var mul = (num1[i] - '0') * (num2[j] - '0');
                int p1 = i + j, p2 = i + j + 1;
                var sum = mul + pos[p2];
                pos[p1] += sum / 10;
                pos[p2] = (sum) % 10;
            }
        }

        var sb = new StringBuilder();
        foreach (var item in pos)
        {
            if (!(sb.Length == 0 && item == 0))
            {
                sb.Append(item);
            }
        }

        return sb.Length == 0 ? "0" : sb.ToString();
    }

    /// <summary>
    /// 45. Jump Game II
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int Jump(int[] nums)
    {
        var n = nums.Length;
        if (n < 2)
        {
            return 0;
        }

        var level = 0;
        var currentMax = 0;
        var i = 0;
        var nextMax = 0;
        while (currentMax - i + 1 > 0)
        {
            level++;
            for (; i <= currentMax; i++)
            {
                nextMax = Math.Max(nextMax, nums[i] + i);
                if (nextMax >= n - 1)
                {
                    return level;
                }
            }

            currentMax = nextMax;
        }

        return 0;

        // with greedy 
        //int jumps = 0, currEnd = 0, currFarthest = 0;
        //for (int i = 0; i < nums.Length - 1; i++)
        //{
        //    currFarthest = Math.Max(currFarthest, i + nums[i]); ;
        //    if (i == currEnd)
        //    {
        //        jumps++;
        //        currEnd = currFarthest;
        //        if (currEnd >= nums.Length - 1)
        //            break;
        //    }
        //}
        //return jumps;
    }

    /// <summary>
    /// 46. Permutations
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<IList<int>> Permute(int[] nums)
    {
        IList<IList<int>> res = new List<IList<int>>();
        var q = new Queue<IList<int>>();
        q.Enqueue(new List<int>());
        foreach (var t in nums)
        {
            var size = q.Count;
            while (size-- > 0)
            {
                var list = q.Dequeue();
                for (var j = 0; j <= list.Count; j++)
                {
                    var tmp = new List<int>(list);
                    tmp.Insert(j, t);
                    q.Enqueue(tmp);
                }
            }
        }

        while (q.Count > 0)
        {
            res.Add(q.Dequeue());
        }

        return res.Reverse().ToList();
    }

    /// <summary>
    /// 48. Rotate Image
    /// </summary>
    /// <param name="nums"></param>
    public void Rotate(int[][] nums)
    {
        var n = nums.Length;
        for (var i = 0; i < n / 2; i++)
        {
            for (var j = 0; j < n; j++)
            {
                (nums[i][j], nums[n - i - 1][j]) = (nums[n - i - 1][j], nums[i][j]);
            }
        }

        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < i; j++)
            {
                (nums[i][j], nums[j][i]) = (nums[j][i], nums[i][j]);
            }
        }
    }

    /// <summary>
    /// 50. Power
    /// </summary>
    /// <param name="x">base</param>
    /// <param name="n">power</param>
    /// <returns></returns>
    public double MyPow(double x, int n)
    {
        double QuickMul(double x, long N)
        {
            var ans = 1.0d;
            var x_contribute = x;
            while (N > 0)
            {
                if (N % 2 == 1)
                {
                    ans *= x_contribute;
                }

                x_contribute *= x_contribute;
                N /= 2;
            }

            return ans;
        }

        long N = n;
        return N >= 0 ? QuickMul(x, N) : 1.0 / QuickMul(x, -N);
    }

    /// <summary>
    /// 53. Maximum Subarray
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxSubArray(int[] nums)
    {
        int pre = 0, maxAns = nums[0];
        foreach (var num in nums)
        {
            pre = Math.Max(pre + num, num);
            maxAns = Math.Max(maxAns, pre);
        }

        return maxAns;
    }

    /// <summary>
    /// 54. Spiral Matrix
    /// </summary>
    public IList<int> SpiralOrder(int[][] matrix)
    {
        if (matrix.Length == 0)
        {
            return new List<int>() { 0 };
        }

        int startRow = 0, startColumn = 0;
        int height = matrix.Length, width = matrix[0].Length;
        IList<int> result = new List<int>();
        while (true)
        {
            if (height == 0 || width == 0) // can also use if(index == height * width) 
            {
                break;
            }

            for (var col = startColumn; col < startColumn + width; col++)
            {
                result.Add(matrix[startRow][col]);
            }

            startRow++;
            height--;
            if (height == 0)
            {
                break;
            }

            for (var row = startRow; row < startRow + height; row++)
            {
                result.Add(matrix[row][startColumn + width - 1]);
            }

            width--;
            if (width == 0)
            {
                break;
            }

            for (var col = startColumn + width - 1; col >= startColumn; col--)
            {
                result.Add(matrix[startRow + height - 1][col]);
            }

            height--;
            if (height == 0)
            {
                break;
            }

            for (var row = startRow + height - 1; row >= startRow; row--)
            {
                result.Add(matrix[row][startColumn]);
            }

            startColumn++;
            width--;
        }

        return result;
    }

    /// <summary>
    /// 55. Jump Game
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public bool CanJump(int[] nums)
    {
        var lastPos = nums.Length - 1;
        for (var i = nums.Length - 1; i >= 0; i--)
        {
            if (i + nums[i] >= lastPos)
            {
                lastPos = i;
            }
        }

        return lastPos == 0;
    }

    /// <summary>
    /// 56. Merge Intervals
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public int[][] Merge(int[][] intervals)
    {
        if (intervals.Length == 0)
        {
            return Array.Empty<int[]>();
        }

        Array.Sort(intervals, (l, r) => l[0] - r[0]);
        var merged = new List<int[]>();
        foreach (var t in intervals)
        {
            var n = merged.Count;
            int l = t[0], r = t[1];
            if (n == 0 || merged[n - 1][1] < l)
            {
                merged.Add(new[] { l, r });
            }
            else
            {
                merged[n - 1][1] = Math.Max(merged[n - 1][1], r);
            }
        }

        return merged.ToArray();
    }

    /// <summary>
    /// 58. Length Of Last Word
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int LengthOfLastWord(string s)
    {
        // s = s.Trim();
        // int lastIndex = s.LastIndexOf(' ') + 1;
        // return s.Length - lastIndex;
        int len = 0, tail = s.Length - 1;
        while (tail >= 0 && s[tail] == ' ')
        {
            tail--;
        }

        while (tail >= 0 && s[tail] != ' ')
        {
            len++;
            tail--;
        }

        return len;
    }

    /// <summary>
    /// 59. Spiral Matrix II
    /// </summary>
    public int[][] GenerateMatrix(int n)
    {
        if (n == 0)
        {
            return new[] { new[] { 0 } };
        }

        int startRow = 0, startColumn = 0;
        int index = 1, height = n, width = n;
        var matrix = new int[n][];
        for (var r = 0; r < n; r++)
        {
            matrix[r] = new int[n];
        }

        while (true)
        {
            if (width == 0)
            {
                break;
            }

            for (var col = startColumn; col < startColumn + width; col++)
            {
                matrix[startRow][col] = index++;
            }

            startRow++;
            height--;
            if (height == 0)
            {
                break;
            }

            for (var row = startRow; row < startRow + height; row++)
            {
                matrix[row][startColumn + width - 1] = index++;
            }

            width--;
            if (width == 0)
            {
                break;
            }

            for (var col = startColumn + width - 1; col >= startColumn; col--)
            {
                matrix[startRow + height - 1][col] = index++;
            }

            height--;
            if (height == 0)
            {
                break;
            }

            for (var row = startRow + height - 1; row >= startRow; row--)
            {
                matrix[row][startColumn] = index++;
            }

            startColumn++;
            width--;
        }

        return matrix;
    }

    /// <summary>
    /// 61. Rotate List
    /// </summary>
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null || k == 0 || head.next == null)
        {
            return head;
        }

        var oldTail = head;
        int n;
        for (n = 1; oldTail.next != null; n++)
        {
            oldTail = oldTail.next;
        }

        oldTail.next = head;
        var newTail = head;
        for (var i = 0; i < n - k % n - 1; i++)
        {
            newTail = newTail.next;
        }

        var newHead = newTail.next;
        newTail.next = null;
        return newHead;
    }

    /// <summary>
    /// 62. Unique Paths
    /// </summary>
    public int UniquePaths(int m, int n)
    {
        var dp = new int[n];
        Array.Fill(dp, 1);
        for (var i = 1; i < m; i++)
        {
            for (var j = 1; j < n; j++)
            {
                dp[j] += dp[j - 1];
            }
        }

        return dp[n - 1];
    }

    /// <summary>
    /// 63. Unique Paths II
    /// </summary>
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        // if(obstacleGrid[0][0] == 1)
        //     return 0;
        // int height = obstacleGrid.Length;
        // int width = obstacleGrid[0].Length;
        // obstacleGrid[0][0] = 1;
        // for(int row = 1; row < height; row++)
        //     obstacleGrid[row][0] = (obstacleGrid[row][0] == 0 && obstacleGrid[row - 1][0] == 1) ? 1 : 0;
        // for(int col = 1; col < width; col++)
        //     obstacleGrid[0][col] = (obstacleGrid[0][col] == 0 && obstacleGrid[0][col - 1] == 1) ? 1 : 0;
        // for(int row = 1; row < height; row++)
        //     for(int col = 1; col < width; col++)
        //         if(obstacleGrid[row][col] == 0)
        //             obstacleGrid[row][col] = obstacleGrid[row - 1][col] + obstacleGrid[row][col - 1];
        //         else
        //             obstacleGrid[row][col] = 0;
        // return obstacleGrid[height - 1][width - 1];

        var width = obstacleGrid[0].Length;
        var dp = new int[width];
        dp[0] = 1;
        foreach (var row in obstacleGrid)
        {
            for (var j = 0; j < width; j++)
            {
                if (row[j] == 1)
                {
                    dp[j] = 0;
                }
                else if (j > 0)
                {
                    dp[j] += dp[j - 1];
                }
            }
        }

        return dp[width - 1];
    }

    /// <summary>
    /// 64. Minimum Path Sum
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int MinPathSum(int[][] grid)
    {
        var width = grid[0].Length;
        var height = grid.Length;
        var dp = new int[height][];
        for (var i = 0; i < height; i++)
        {
            dp[i] = new int[width];
        }

        dp[0][0] = grid[0][0];
        for (var i = 1; i < width; i++)
        {
            dp[0][i] = dp[0][i - 1] + grid[0][i];
        }

        for (var i = 1; i < height; i++)
        {
            dp[i][0] = dp[i - 1][0] + grid[i][0];
        }

        for (var i = 1; i < height; i++)
        {
            for (var j = 1; j < width; j++)
            {
                dp[i][j] = Math.Min(dp[i - 1][j], dp[i][j - 1]) + grid[i][j];
            }
        }

        return dp[height - 1][width - 1];
    }

    /// <summary>
    /// 66. Plus One 
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public int[] PlusOne(int[] digits)
    {
        var n = digits.Length;
        for (var i = n - 1; i >= 0; i--)
        {
            if (digits[i] < 9)
            {
                digits[i]++;
                return digits;
            }

            digits[i] = 0;
        }

        var newNumber = new int[n + 1];
        newNumber[0] = 1;
        return newNumber;
    }

    /// <summary>
    /// 67. Add Binary
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public string AddBinary(string a, string b)
    {
        var s = "";
        int c = 0, i = a.Length - 1, j = b.Length - 1;
        while (i >= 0 || j >= 0 || c == 1)
        {
            c += i >= 0 ? a[i--] - '0' : 0;
            c += j >= 0 ? b[j--] - '0' : 0;
            s = System.Convert.ToChar(c % 2 + '0') + s;
            c /= 2;
        }

        return s;
    }

    /// <summary>
    /// 68. Text Justification
    /// </summary>
    /// <param name="words"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        IList<string> ans = new List<string>();
        int right = 0, n = words.Length;
        while (true)
        {
            var left = right;
            var sumLen = 0;
            while (right < n && sumLen + words[right].Length + right - left <= maxWidth)
            {
                sumLen += words[right++].Length;
            }

            if (right == n)
            {
                var sb = Join(words, left, n, " ");
                sb.Append(Blank(maxWidth - sb.Length));
                ans.Add(sb.ToString());
                return ans;
            }

            var numWords = right - left;
            var numSpaces = maxWidth - sumLen;
            if (numWords == 1)
            {
                var sb = new StringBuilder(words[left]);
                sb.Append(Blank(numSpaces));
                ans.Add(sb.ToString());
                continue;
            }

            var avgSpaces = numSpaces / (numWords - 1);
            var extraSpaces = numSpaces % (numWords - 1);
            var curr = new StringBuilder();
            curr.Append(Join(words, left, left + extraSpaces + 1, Blank(avgSpaces + 1)));
            curr.Append(Blank(avgSpaces));
            curr.Append(Join(words, left + extraSpaces + 1, right, Blank(avgSpaces)));
            ans.Add(curr.ToString());
        }

        string Blank(int n)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < n; i++)
            {
                sb.Append(' ');
            }

            return sb.ToString();
        }

        StringBuilder Join(string[] words, int left, int right, string seperator)
        {
            var sb = new StringBuilder(words[left]);
            for (var i = left + 1; i < right; i++)
            {
                sb.Append(seperator);
                sb.Append(words[i]);
            }

            return sb;
        }
    }

    /// <summary>
    /// 69. Sqrt(x)
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public int Sqrt(int x)
    {
        if (x == 0)
        {
            return 0;
        }

        int left = 1, right = x;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (mid == x / mid)
            {
                return mid;
            }
            else if (mid < x / mid)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return right;
    }

    /// <summary>
    /// 70. Climb Stairs
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int ClimbStairs(int n)
    {
        // 44ms
        //if (n == 1) return 1;
        //int[] dp = new int[n + 1];
        //dp[1] = 1;
        //dp[2] = 2;
        //for (int i = 3; i <= n; i++)
        //  dp[i] = dp[i - 1] + dp[i - 2];
        //return dp[n];

        // 56ms
        int a = 1, b = 1;
        while (n-- > 0)
        {
            a = (b += a) - a;
        }

        return a;
    }

    /// <summary>
    /// 71. Simplify Path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string SimplifyPath(string path)
    {
        var stack = new Stack<string>();
        var skip = new HashSet<string> { "", ".", ".." };
        foreach (var dir in path.Split('/'))
        {
            if (dir == ".." && stack.Count > 0)
            {
                stack.Pop();
            }
            else if (!skip.Contains(dir))
            {
                stack.Push(dir);
            }
        }

        if (stack.Count == 0)
        {
            return "/";
        }

        var sb = new StringBuilder();
        while (stack.Count > 0)
        {
            sb.Insert(0, "/" + stack.Pop());
        }

        return sb.ToString();
    }

    /// <summary>
    /// 73. Set Zeroes
    /// </summary>
    /// <param name="matrix"></param>
    public void SetZeroes(int[][] matrix)
    {
        int col0 = 1, rows = matrix.Length, cols = matrix[0].Length;
        // top-down
        for (var i = 0; i < rows; i++)
        {
            // first column
            if (matrix[i][0] == 0)
            {
                col0 = 0;
            }

            for (var j = 1; j < cols; j++)
            {
                // if the current cell is "0" set i row and j col as "0"
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = matrix[0][j] = 0;
                }
            }
        }

        // bottom-up
        for (var i = rows - 1; i >= 0; i--)
        {
            for (var j = cols - 1; j >= 1; j--)
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                {
                    matrix[i][j] = 0;
                }
            }

            if (col0 == 0)
            {
                matrix[i][0] = 0;
            }
        }
    }

    /// <summary>
    /// 74. Search a 2D Matrix
    /// </summary>
    public bool SearchMatrix(int[][] matrix, int target)
    {
        var row = matrix.Length;
        if (row == 0)
        {
            return false;
        }

        var col = matrix[0].Length;
        int low = 0, high = row * col - 1;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            var r = mid / col;
            var c = mid % col;
            if (matrix[r][c] > target)
            {
                high = mid - 1;
            }
            else if (matrix[r][c] < target)
            {
                low = mid + 1;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 77. Combinations
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public IList<IList<int>> Combine(int n, int k)
    {
        var temp = new List<int>();
        IList<IList<int>> ans = new List<IList<int>>();
        for (var i = 1; i <= k; ++i)
        {
            temp.Add(i);
        }

        temp.Add(n + 1);
        var j = 0;
        while (j < k)
        {
            ans.Add(new List<int>(temp.GetRange(0, k)));
            j = 0;
            while (j < k && temp[j] + 1 == temp[j + 1])
            {
                temp[j] = j + 1;
                ++j;
            }

            ++temp[j];
        }

        return ans;
    }

    /// <summary>
    /// 80. Remove Duplicates from Sorted Array II
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int RemoveDuplicatesV2(int[] nums)
    {
        var len = nums.Length;
        if (len <= 2)
        {
            return len;
        }

        int slow = 2, fast = 2;
        while (fast < len)
        {
            if (nums[slow - 2] != nums[fast])
            {
                nums[slow] = nums[fast];
                ++slow;
            }

            ++fast;
        }

        return slow;
    }

    /// <summary>
    /// 83. Remove Duplicates from Sorted List
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode DeleteDuplicates(ListNode head)
    {
        var current = head;
        while (current?.next != null)
        {
            if (current.val == current.next.val)
            {
                current.next = current.next.next;
            }
            else
            {
                current = current.next;
            }
        }

        return head;
    }

    /// <summary>
    /// 88. Merge Sorted Array
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="m"></param>
    /// <param name="num2"></param>
    /// <param name="n"></param>
    public void Merge(int[] num1, int m, int[] num2, int n)
    {
        int p = m - 1, q = n - 1, i = m + n - 1;
        while (q >= 0)
        {
            if (p < 0 || num2[q] >= num1[p])
            {
                num1[i--] = num2[q--];
            }
            else
            {
                num1[i--] = num1[p--];
            }
        }
    }

    /// <summary>
    /// 94. Binary Tree Inorder Traversal
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> InorderTraversal(TreeNode root)
    {
        IList<int> ret = new List<int>();
        if (root == null)
        {
            return ret;
        }
        // recursively
        // InorderHelper(ret, root);

        // iteratively
        var stack = new Stack<TreeNode>();
        var curr = root;
        while (curr != null || stack.Count > 0)
        {
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.left;
            }

            curr = stack.Pop();
            ret.Add(curr.val);
            curr = curr.right;
        }

        return ret;

        // void InorderHelper(IList<int> data, TreeNode node)
        // {
        //     if (node == null)
        //     {
        //         return;
        //     }
        //     InorderHelper(data, node.left);
        //     data.Add(node.val);
        //     InorderHelper(data, node.right);
        // }
    }

    /// <summary>
    /// 98. Validate Binary Search Tree
    /// </summary>
    public bool IsValidBST(TreeNode root)
    {
        var stack = new Stack<TreeNode>();
        var inorder = -double.MaxValue;
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }

            root = stack.Pop();
            if (root.val <= inorder)
            {
                return false;
            }

            inorder = root.val;
            root = root.right;
        }

        return true;
    }

    /// <summary>
    /// 100. Same Tree
    /// </summary>
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        if (p == null || q == null)
        {
            return p == q;
        }

        return p.val == q.val && IsSameTree(p.left, q.right) && IsSameTree(q.left, q.right);
    }

    /// <summary>
    /// 101. Symmetric Tree
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public bool IsSymmetric(TreeNode root)
    {
        // 116ms by recursion
        // return IsMirror(root, root);

        // 104ms by iteration
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var t1 = queue.Dequeue();
            var t2 = queue.Dequeue();
            if (t1 == null & t2 == null)
            {
                continue;
            }

            if (t1 == null || t2 == null)
            {
                return false;
            }

            if (t1.val != t2.val)
            {
                return false;
            }

            queue.Enqueue(t1.left);
            queue.Enqueue(t2.right);
            queue.Enqueue(t1.right);
            queue.Enqueue(t2.left);
        }

        return true;

        // bool IsMirror(TreeNode l1, TreeNode l2)
        // {
        //     if (l1 == null && l2 == null)
        //     {
        //         return true;
        //     }
        //
        //     if (l1 == null || l2 == null)
        //     {
        //         return false;
        //     }
        //
        //     return (l1.val == l2.val)
        //            && IsMirror(l1.left, l2.right)
        //            && IsMirror(l1.right, l2.left);
        // }
    }

    /// <summary>
    /// 104. Maximum Depth of Binary Tree
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int MaxDepth(TreeNode root)
    {
        // DFS 112ms
        // return root == null ? 0 : 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
        // BFS 112ms
        if (root == null)
        {
            return 0;
        }

        var res = 0;
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            res++;
            for (int i = 0, n = queue.Count; i < n; i++)
            {
                var p = queue.Peek();
                queue.Dequeue();
                if (p.left != null)
                {
                    queue.Enqueue(p.left);
                }

                if (p.right != null)
                {
                    queue.Enqueue(p.right);
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 107. Binary Tree Level Order Traversal II
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        IList<IList<int>> res = new List<IList<int>>();
        if (root == null)
        {
            return res;
        }

        var que = new Queue<TreeNode>();
        que.Enqueue(root);
        while (true)
        {
            var nodeCount = que.Count;
            if (nodeCount == 0)
            {
                break;
            }

            var subList = new List<int>();
            while (nodeCount > 0)
            {
                var dataNode = que.Dequeue();
                subList.Add(dataNode.val);
                if (dataNode.left != null)
                {
                    que.Enqueue(dataNode.left);
                }

                if (dataNode.right != null)
                {
                    que.Enqueue(dataNode.right);
                }

                nodeCount--;
            }

            res.Insert(0, subList);
        }

        return res;
    }

    /// <summary>
    /// 108. Convert Sorted Array To Binary Search Tree
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public TreeNode SortedArrayToBST(int[] nums)
    {
        //if (nums == null || nums.Length == 0) return null;
        //int mid = nums.Length / 2;
        //TreeNode root = new TreeNode(nums[mid]);
        //root.left = SubProcess(nums.Where((num, index) => index < mid).ToArray());
        //root.right = SubProcess(nums.Where((num, index) => index > mid).ToArray());
        //return root;
        return SubProcess(nums, 0, nums.Length - 1);

        TreeNode SubProcess(int[] data, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            var mid = start + (end - start) / 2;
            var root = new TreeNode(data[mid])
            {
                left = SubProcess(data, start, mid - 1),
                right = SubProcess(data, mid + 1, end)
            };
            return root;
        }
    }

    /// <summary>
    /// 109. Convert Sorted List to Binary Search Tree
    /// </summary>
    public TreeNode SortedListToBST(ListNode head)
    {
        var size = FindSize(head);
        var dummyHead = head;
        //Solution.head = head;
        return ConvertListToBST(0, size - 1);

        int FindSize(ListNode node)
        {
            var ptr = node;
            var c = 0;
            while (ptr != null)
            {
                ptr = ptr.next;
                c += 1;
            }

            return c;
        }

        TreeNode ConvertListToBST(int l, int r)
        {
            if (l > r)
            {
                return null;
            }

            var mid = (l + r) / 2;
            var left = ConvertListToBST(l, mid - 1);
            var node = new TreeNode(head.val)
            {
                left = left
            };
            head = head.next;
            node.right = ConvertListToBST(mid + 1, r);
            return node;
        }
    }

    /// <summary>
    /// 110. Balanced Binary Tree
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public bool IsBalanced(TreeNode root)
    {
        return DFS(root) != -1;

        int DFS(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            var leftHeight = DFS(node.left);
            if (leftHeight == -1)
            {
                return -1;
            }

            var rightHeight = DFS(node.right);
            if (rightHeight == -1)
            {
                return -1;
            }

            if (Math.Abs(leftHeight - rightHeight) > 1)
            {
                return -1;
            }

            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }

    /// <summary>
    /// 111. Minimum Depth of Binary Tree
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int MinDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }

        int l = MinDepth(root.left), r = MinDepth(root.right);
        return 1 + (l < r && l > 0 || r < 1 ? l : r);
    }

    /// <summary>
    /// 112. Path Sum
    /// </summary>
    /// <param name="root"></param>
    /// <param name="sum"></param>
    /// <returns></returns>
    public bool HasPathSum(TreeNode root, int sum)
    {
        if (root == null)
        {
            return false;
        }

        if (root.val == sum && root.left == null && root.right == null)
        {
            return true;
        }

        return HasPathSum(root.left, sum - root.val) || HasPathSum(root.right, sum - root.val);
    }

    /// <summary>
    /// 113. Path Sum II
    /// </summary>
    /// <param name="root"></param>
    /// <param name="targetSum"></param>
    /// <returns></returns>
    public IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        IList<IList<int>> res = new List<IList<int>>();
        IList<int> path = new List<int>();
        Dfs(root, targetSum);
        return res;

        void Dfs(TreeNode node, int sum)
        {
            if (node is null)
            {
                return;
            }

            path.Add(node.val);
            sum -= node.val;
            if (node.left is null && node.right is null && sum == 0)
            {
                res.Add(new List<int>(path));
            }

            Dfs(node.left, sum);
            Dfs(node.right, sum);
            path.RemoveAt(path.Count - 1);
        }
    }

    /// <summary>
    /// 116. Populating Next Right Pointers in Each Node
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public BinaryTreeNode Connect(BinaryTreeNode root)
    {
        if (root == null)
        {
            return null;
        }

        var leftmost = root;
        while (leftmost.Left != null)
        {
            var head = leftmost;
            while (head != null)
            {
                head.Left.Next = head.Right;
                if (head.Next != null)
                {
                    head.Right.Next = head.Next.Left;
                }

                head = head.Next;
            }

            leftmost = leftmost.Left;
        }

        return root;
    }

    /// <summary>
    /// 118. Pascal's triangle
    /// </summary>
    /// <param name="numRows"></param>
    /// <returns></returns>
    public IList<IList<int>> Generate(int numRows)
    {
        IList<IList<int>> triangle = new List<IList<int>>();
        //if (numRows == 0) return triangle;
        //triangle.Add(new List<int>());
        //triangle[0].Add(1);
        //for(int rowNum = 1; rowNum < numRows; rowNum++)
        //{
        //    List<int> row = new List<int>();
        //    IList<int> prevRow = triangle[rowNum - 1];
        //    row.Add(1);
        //    for (int j = 1; j < rowNum; j++) row.Add(prevRow[j - 1] + prevRow[j]);
        //    row.Add(1);
        //    triangle.Add(row);
        //}
        for (var i = 0; i < numRows; ++i)
        {
            IList<int> row = new List<int>();
            for (var r = 1; r <= i + 1; r++)
            {
                row.Add(1);
            }

            triangle.Add(row);
            for (var j = 1; j < i; ++j)
            {
                triangle[i][j] = triangle[i - 1][j - 1] + triangle[i - 1][j];
            }
        }

        return triangle;
    }

    /// <summary>
    /// 120. Triangle
    /// </summary>
    /// <param name="triangle"></param>
    /// <returns></returns>
    public int MinimumTotal(IList<IList<int>> triangle)
    {
        var n = triangle.Count;
        var f = new int[n];
        f[0] = triangle[0][0];
        for (var i = 1; i < n; ++i)
        {
            f[i] = f[i - 1] + triangle[i][i];
            for (var j = i - 1; j > 0; --j)
            {
                f[j] = Math.Min(f[j - 1], f[j]) + triangle[i][j];
            }

            f[0] += triangle[i][0];
        }

        return f.Min();
    }

    /// <summary>
    /// 121. Best Time to Buy and Sell Stock
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public int MaxProfit(int[] prices)
    {
        // 108 ms
        //int total = 0;
        //for (int i = 1; i < prices.Length; i++)
        //    if (prices[i] > prices[i - 1])
        //        total += prices[i] - prices[i - 1];
        //return total;
        var len = prices.Length;
        if (len < 1)
        {
            return 0;
        }

        var full = new int[len];
        var empty = new int[len];
        empty[0] = 0;
        full[0] = prices[0] * -1;
        for (var i = 1; i < len; i++)
        {
            empty[i] = Math.Max(empty[i - 1], full[i - 1] + prices[i]);
            full[i] = Math.Max(full[i - 1], empty[i - 1] - prices[i]);
        }

        return Math.Max(full[len - 1], empty[len - 1]);
    }

    /// <summary>
    /// 125. Valid Palindrome
    /// </summary>
    public bool IsPalindrome(string s)
    {
        for (int i = 0, j = s.Length - 1; i < j;)
        {
            if (!char.IsLetterOrDigit(s[i])) // skip space from head
            {
                i++;
            }
            else if (!char.IsLetterOrDigit(s[j])) // skip space from tail
            {
                j--;
            }
            else if (char.ToLower(s[i++]) != char.ToLower(s[j--]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 136. Single Number
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SingleNumber(int[] nums)
    {
        return nums.Aggregate(0, (current, item) => current ^ item);
    }

    /// <summary>
    /// 137. Single Number II
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public int SingleNumberII(int[] A)
    {
        int ones = 0, twos = 0;
        foreach (var t in A)
        {
            ones = (ones ^ t) & ~twos;
            twos = (twos ^ t) & ~ones;
        }

        return ones;
    }

    /// <summary>
    /// 141. Linked List Cycle
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public bool HasCycle(ListNode head)
    {
        if (head?.next == null)
        {
            return false;
        }

        var slow = head;
        var fast = head.next;
        while (slow != fast)
        {
            if (fast?.next == null)
            {
                return false;
            }

            slow = slow.next;
            fast = fast.next.next;
        }

        return true;
    }

    /// <summary>
    /// 144. Binary Tree Preorder Traversal
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PreorderTraversal(TreeNode root)
    {
        IList<int> res = new List<int>();
        var stack = new Stack<TreeNode>();
        while (root != null)
        {
            res.Add(root.val);
            if (root.right != null)
            {
                stack.Push(root.right);
            }

            root = root.left;
            if (root == null && stack.Count > 0)
            {
                root = stack.Pop();
            }
        }

        return res;
    }

    /// <summary>
    /// 148. Sort List
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode SortList(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }

        ListNode prev = null, left = head, right = head;
        while (right?.next != null)
        {
            prev = left;
            left = left.next;
            right = right.next.next;
        }

        prev.next = null;

        var l1 = SortList(head);
        var l2 = SortList(left);

        return MergeTwoLists(l1, l2);
    }

    /// <summary>
    /// 153. Find Minimum in Rotated Sorted Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindMin(int[] nums)
    {
        if (nums.Length == 1)
        {
            return nums[0];
        }

        int l = 0, r = nums.Length - 1;
        if (nums[r] > nums[0])
        {
            return nums[0];
        }

        while (r >= l)
        {
            var mid = l + (r - l) / 2;
            if (nums[mid] > nums[mid + 1])
            {
                return nums[mid + 1];
            }

            if (nums[mid - 1] > nums[mid])
            {
                return nums[mid];
            }

            if (nums[mid] > nums[0])
            {
                l = mid + 1;
            }
            else
            {
                r = mid - 1;
            }
        }

        return -1;
    }

    /// <summary>
    /// 162. Find Peak Element
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindPeakElement(int[] nums)
    {
        var len = nums.Length;
        int l = 0, r = len - 1, ans = -1;
        while (l <= r)
        {
            var mid = l + (r - l) / 2;
            if (Compare(nums, mid - 1, mid) < 0 &&
                Compare(nums, mid, mid + 1) > 0)
            {
                ans = mid;
                break;
            }

            if (Compare(nums, mid, mid + 1) < 0)
            {
                l = mid + 1;
            }
            else
            {
                r = mid - 1;
            }
        }

        return ans;

        int[] Get(int[] data, int index)
        {
            if (index == -1 || index == data.Length)
            {
                return new[] { 0, 0 };
            }

            return new[] { 1, data[index] };
        }

        int Compare(int[] data, int index1, int index2)
        {
            var num1 = Get(data, index1);
            var num2 = Get(data, index2);
            if (num1[0] != data[0])
            {
                return num1[0] > num2[0] ? 1 : -1;
            }

            if (num1[1] == num2[1])
            {
                return 0;
            }

            return num1[1] > num2[1] ? 1 : -1;
        }
    }

    /// <summary>
    /// 167. Two Sum II - Input array is sorted
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int[] TwoSumII(int[] numbers, int target)
    {
        int left = 0, right = numbers.Length - 1;
        while (left < right)
        {
            var sum = numbers[left] + numbers[right];
            if (sum == target)
            {
                return new int[] { left + 1, right + 1 };
            }
            else if (sum > target)
            {
                right--;
            }
            else
            {
                left++;
            }
        }

        return new[] { -1, -1 };
    }

    /// <summary>
    /// 168. Excel Sheet Column Title
    /// </summary>
    /// <param name="columnNumber"></param>
    /// <returns></returns>
    public string ConvertToTitle(int columnNumber)
    {
        var sb = new StringBuilder();
        while (columnNumber != 0)
        {
            columnNumber--;
            sb.Append((char)(columnNumber % 26 + 'A'));
            columnNumber /= 26;
        }

        var columnTitle = new StringBuilder();
        for (var i = sb.Length - 1; i >= 0; i--)
        {
            columnTitle.Append(sb[i]);
        }

        return columnTitle.ToString();
    }

    /// <summary>
    /// 169. Majority Element
    /// </summary>
    public int MajorityElement(int[] nums)
    {
        var count = 0;
        var candidate = 0;
        foreach (var num in nums)
        {
            if (count == 0)
            {
                candidate = num;
            }

            count += num == candidate ? 1 : -1;
        }

        return candidate;
    }

    /// <summary>
    /// 171. Excel Sheet Column Number
    /// </summary>
    /// <param name="columnTitle"></param>
    /// <returns></returns>
    public int TitleToNumber(string columnTitle)
    {
        var number = 0;
        var multiple = 1;
        for (var i = columnTitle.Length - 1; i >= 0; --i)
        {
            var k = columnTitle[i] - 'A' + 1;
            number += k * multiple;
            multiple *= 26;
        }

        return number;
    }

    /// <summary>
    /// 172. Factorial Trailing Zeroes
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int TrailingZeroes(int n)
    {
        return n == 0 ? 0 : n / 5 + TrailingZeroes(n / 5);
    }

    /// <summary>
    /// 174. Dungeon Game
    /// </summary>
    /// <param name="dungeon">map</param>
    /// <returns></returns>
    public int CalculateMinimumHP(int[][] dungeon)
    {
        var m = dungeon.Length;
        var n = dungeon[0].Length;
        if (m == 0 || n == 0)
        {
            return 0;
        }

        var health = new int[m][];
        for (var i = 0; i < m; ++i)
        {
            health[i] = new int[n];
        }

        health[m - 1][n - 1] = Math.Max(1 - dungeon[m - 1][n - 1], 1);
        for (var i = m - 2; i >= 0; i--)
        {
            health[i][n - 1] = Math.Max(health[i + 1][n - 1] - dungeon[i][n - 1], 1);
        }

        for (var j = n - 2; j >= 0; j--)
        {
            health[m - 1][j] = Math.Max(health[m - 1][j + 1] - dungeon[m - 1][j], 1);
        }

        for (var i = m - 2; i >= 0; i--)
        {
            for (var j = n - 2; j >= 0; j--)
            {
                var down = Math.Max(health[i + 1][j] - dungeon[i][j], 1);
                var right = Math.Max(health[i][j + 1] - dungeon[i][j], 1);
                health[i][j] = Math.Min(right, down);
            }
        }

        return health[0][0];
    }

    /// <summary>
    /// 189. Rotate Array
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public void Rotate(int[] nums, int k)
    {
        k %= nums.Length;
        Reverse(nums, 0, nums.Length - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, nums.Length - 1);

        void Reverse(int[] data, int start, int end)
        {
            while (start < end)
            {
                (data[start], data[end]) = (data[end], data[start]);
                start++;
                end--;
            }
        }
    }

    /// <summary>
    /// 190. Reverse Bits
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public uint ReverseBits(uint n)
    {
        n = (n >> 16) | (n << 16);
        n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
        n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
        n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
        n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
        return n;
        //if (n == 0) return 0;
        //uint res = 0;
        //for (int i = 0; i < 32; i++)
        //{
        //    res <<= 1;
        //    if ((n & 1) == 1) res++;
        //    n >>= 1;
        //}
        //return res;
    }

    /// <summary>
    /// 191. Number of 1 Bits
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int HammingWeight(uint n)
    {
        var res = 0;
        while (n != 0)
        {
            n &= (n - 1);
            res++;
        }

        return res;
    }

    /// <summary>
    /// 198. House Robber
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int Rob(int[] nums)
    {
        //int a = 0;
        //int b = 0;
        //for (int i = 0; i < nums.Length; i++)
        //{
        //    if (i % 2 == 0)
        //        a = Math.Max(a + nums[i], b);
        //    else
        //        b = Math.Max(a, b + nums[i]);
        //}
        //return Math.Max(a, b);

        var rob = 0;
        var notRob = 0;
        foreach (var item in nums)
        {
            var current = notRob + item;
            notRob = Math.Max(notRob, rob);
            rob = current;
        }

        return Math.Max(notRob, rob);
    }

    /// <summary>
    /// 202. Happy Number
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public bool IsHappy(int n)
    {
        int slow = n, fast = n;
        while (slow != 1)
        {
            slow = DigitSquareSum(slow);
            fast = DigitSquareSum(DigitSquareSum(fast));
            if (slow != 1 && slow == fast)
            {
                return false;
            }
        }

        return true;

        int DigitSquareSum(int num)
        {
            var sum = 0;
            while (num > 0)
            {
                var tmp = num % 10;
                sum += tmp * tmp;
                num /= 10;
            }

            return sum;
        }
    }

    /// <summary>
    /// 204. Count Primes
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountPrimes(int n)
    {
        if (n < 3)
        {
            return 0;
        }

        var f = new bool[n];
        var count = n / 2;
        for (var i = 3; i * i < n; i += 2)
        {
            if (f[i])
            {
                continue;
            }

            for (var j = i * i; j < n; j += 2 * i)
            {
                if (f[j])
                {
                    continue;
                }

                --count;
                f[j] = true;
            }
        }

        return count;
    }

    /// <summary>
    /// 205. Isomorphic Strings
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsIsomorphic(string s, string t)
    {
        var m1 = new int[256];
        var m2 = new int[256];
        var n = s.Length;
        for (var i = 0; i < 256; i++)
        {
            m1[i] = m2[i] = -1;
        }

        for (var i = 0; i < n; i++)
        {
            if (m1[s[i]] != m2[t[i]])
            {
                return false;
            }

            m1[s[i]] = m2[t[i]] = i;
        }

        return true;
    }

    /// <summary>
    /// 206. Reverse Linked List
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode ReverseList(ListNode head)
    {
        // Iteratively Time: O(n) Space O(1)
        ListNode prev = null;
        var curr = head;
        while (curr is not null)
        {
            var next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
        }

        return prev;

        // Recursively Time: O(n) Space O(n)
        //if (head is null || head.next is null)
        //{
        //    return head;
        //}
        //ListNode newHead = ReverseList(head.next);
        //head.next.next = head;
        //head.next = null;
        //return newHead;
    }

    /// <summary>
    /// 212. Word Search II
    /// </summary>
    /// <param name="board"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public IList<string> FindWords(char[][] board, string[] words)
    {
        void Dfs(char[][] dataBoard, int i, int j, TrieNode p, IList<string> list)
        {
            var c = dataBoard[i][j];
            var m = dataBoard.Length;
            var n = dataBoard[0].Length;
            if (c == '#' || p.Get(c) == null)
            {
                return;
            }

            p = p.Get(c);
            if (p.Word != null)
            {
                list.Add(p.Word);
                p.Word = null;
            }

            dataBoard[i][j] = '#';
            if (i > 0)
            {
                Dfs(dataBoard, i - 1, j, p, list);
            }

            if (j > 0)
            {
                Dfs(dataBoard, i, j - 1, p, list);
            }

            if (i < m - 1)
            {
                Dfs(dataBoard, i + 1, j, p, list);
            }

            if (j < n - 1)
            {
                Dfs(dataBoard, i, j + 1, p, list);
            }

            dataBoard[i][j] = c;
        }

        TrieNode BuildTrie(string[] wordData)
        {
            var node = new TrieNode();
            foreach (var item in wordData)
            {
                var p = node;
                foreach (var ch in item)
                {
                    if (p.Get(ch) == null)
                    {
                        p.Links[ch - 'a'] = new TrieNode();
                    }

                    p = p.Get(ch);
                }

                p.Word = item;
            }

            return node;
        }

        IList<string> res = new List<string>();
        var root = BuildTrie(words);
        int m = board.Length, n = board[0].Length;
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                Dfs(board, i, j, root, res);
            }
        }

        return res;
    }

    /// <summary>
    /// 213. House Robber II
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int RobII(int[] nums)
    {
        var n = nums.Length;
        if (n < 2)
        {
            return n == 1 ? nums[0] : 0;
        }

        return Math.Max(Robber(nums, 0, n - 2), Robber(nums, 1, n - 1));

        int Robber(int[] data, int l, int r)
        {
            int pre = 0, cur = 0;
            for (var i = l; i <= r; i++)
            {
                var temp = Math.Max(pre + data[i], cur);
                pre = cur;
                cur = temp;
            }

            return cur;
        }
    }

    /// <summary>
    /// 217. Contains Duplicate
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public bool ContainsDuplicate(int[] nums)
    {
        ISet<int> set = new HashSet<int>();
        foreach (var item in nums)
        {
            if (!set.Add(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 231. Power of Two
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public bool IsPowerOfTwo(int n)
    {
        return n > 0 && (n & (n - 1)) == 0;
    }

    /// <summary>
    /// 234.Palindrome Linked List
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public bool IsPalindrome(ListNode head)
    {
        if (head == null)
        {
            return true;
        }

        var firstHalfEnd = EndOfFirstHalf(head);
        var secondHalfStart = ReverseList(firstHalfEnd.next);
        var p1 = head;
        var p2 = secondHalfStart;
        while (p2 != null)
        {
            if (p1.val != p2.val)
            {
                return false;
            }

            p1 = p1.next;
            p2 = p2.next;
        }

        firstHalfEnd.next = ReverseList(secondHalfStart);
        return true;

        ListNode ReverseList(ListNode node)
        {
            ListNode prev = null;
            var curr = node;
            while (curr != null)
            {
                var tmp = curr.next;
                curr.next = prev;
                prev = curr;
                curr = tmp;
            }

            return prev;
        }

        ListNode EndOfFirstHalf(ListNode node)
        {
            var fast = node;
            var slow = node;
            while (fast.next?.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }

            return slow;
        }
    }

    /// <summary>
    /// 242. Valid Anagram
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length)
        {
            return false;
        }

        var tables = new int[26];
        foreach (var c in s)
        {
            tables[c - 'a']++;
        }

        foreach (var c in t)
        {
            tables[c - 'a']--;
        }

        return tables.All(e => e == 0);
    }

    /// <summary>
    /// 258. Add Digits
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int AddDigits(int num)
    {
        return (num - 1) % 9 + 1;
    }

    /// <summary>
    /// 260. Single Number III
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int[] SingleNumberIII(int[] nums)
    {
        var xorSum = nums.Aggregate(0, (current, num) => current ^ num);
        var lsb = (xorSum == int.MinValue ? xorSum : xorSum & (-xorSum));
        int type1 = 0, type2 = 0;
        foreach (var num in nums)
        {
            if ((num & lsb) != 0)
            {
                type1 ^= num;
            }
            else
            {
                type2 ^= num;
            }
        }

        return new[] { type1, type2 };
    }

    /// <summary>
    /// 268. Missing Number
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MissingNumber(int[] nums)
    {
        var len = nums.Length;
        var sum = len * (len + 1) / 2;
        var actual = nums.Sum();
        return sum - actual;
    }

    /// <summary>
    /// 283. Move Zeroes
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public void MoveZeroes(int[] nums)
    {
        //int j = 0, len = nums.Length;
        //for (int i = 0; i < len; i++)
        //    if (nums[i] != 0)
        //        nums[j++] = nums[i];
        //for (; j < len; j++)
        //    nums[j] = 0;

        int len = nums.Length, left = 0, right = 0;
        while (right < len)
        {
            if (nums[right] != 0)
            {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
            }

            right++;
        }
    }

    /// <summary>
    /// 287. Find the Duplicate Number
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindDuplicate(int[] nums)
    {
        int slow = 0, fast = 0;
        do
        {
            slow = nums[slow];
            fast = nums[nums[fast]];
        } while (slow != fast);

        slow = 0;
        while (slow != fast)
        {
            slow = nums[slow];
            fast = nums[fast];
        }

        return slow;
    }

    /// <summary>
    /// 337. House Robber III 
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int RobIII(TreeNode root)
    {
        var res = SubRob(root);
        return Math.Max(res[0], res[1]);

        int[] SubRob(TreeNode node)
        {
            if (node == null)
            {
                return new int[2];
            }

            var left = SubRob(node.left);
            var right = SubRob(node.right);
            var ret = new int[2];
            ret[0] = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);
            ret[1] = node.val + left[0] + right[0];
            return ret;
        }
    }

    /// <summary>
    /// 342. Power of Four
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public bool IsPowerOfFour(int num)
    {
        return num > 0 && (num & (num - 1)) == 0 && (num - 1) % 3 == 0;
    }

    /// <summary>
    /// 344. Reverse String
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public void ReverseString(char[] s)
    {
        var len = s.Length;
        for (int left = 0, right = len - 1; left < right; ++left, --right)
        {
            Util.Swap(s, left, right);
        }
    }

    /// <summary>
    /// 357. Count Numbers with Unique Digits
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int CountNumbersWithUniqueDigits(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        if (n == 1)
        {
            return 10;
        }

        int res = 10, cur = 9;
        for (var i = 0; i < n - 1; i++)
        {
            cur *= 9 - i;
            res += cur;
        }

        return res;
    }

    /// <summary>
    /// 371. Sum of Two Integers
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int GetSum(int a, int b)
    {
        while (b != 0)
        {
            var carry = (a & b) << 1;
            a ^= b;
            b = carry;
        }

        return a;
    }

    /// <summary>
    /// 374. Guess Number Higher or Lower
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int GuessNumber(int n)
    {
        int left = 1, right = n;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var res = Guess(mid);
            switch (res)
            {
                case 0:
                    return mid;
                case < 0:
                    right = mid - 1;
                    break;
                default:
                    left = mid + 1;
                    break;
            }
        }

        return -1;

        int Guess(int num)
        {
            var random = new Random();
            var target = random.Next(1, int.MaxValue);
            if (num > target)
            {
                return 1;
            }
            else if (num < target)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    /// <summary>
    /// 386. Lexicographical Numbers
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<int> LexicalOrder(int n)
    {
        IList<int> list = new List<int>(n);
        var num = 1;
        for (var i = 0; i < n; i++)
        {
            list.Add(num);
            if (num * 10 <= n)
            {
                num *= 10;
            }
            else
            {
                while (num % 10 == 9 || num + 1 > n)
                {
                    num /= 10;
                }

                num++;
            }
        }

        return list;
    }

    /// <summary>
    /// 393. UTF-8 Validation
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool ValidUtf8(int[] data)
    {
        const int mask1 = 1 << 7;
        const int mask2 = (1 << 7) + (1 << 6);
        var m = data.Length;
        var index = 0;
        while (index < m)
        {
            var num = data[index];
            var n = GetBytes(num);
            if (n < 0 || index + n > m)
            {
                return false;
            }

            for (var i = 1; i < n; i++)
            {
                if (!IsValid((data[index + i])))
                {
                    return false;
                }
            }

            index += n;
        }

        return true;

        int GetBytes(int num)
        {
            if ((num & mask1) == 0)
            {
                return 1;
            }

            var n = 0;
            var mask = mask1;
            while ((num & mask) != 0)
            {
                n++;
                if (n > 4)
                {
                    return -1;
                }

                mask >>= 1;
            }

            return n >= 2 ? n : -1;
        }

        bool IsValid(int num)
        {
            return (num & mask2) == mask1;
        }
    }

    /// <summary>
    /// 396. Rotate Function
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxRotateFunction(int[] nums)
    {
        var n = nums.Length;
        if (n <= 1)
        {
            return 0;
        }

        int f = 0, numSum = nums.Sum();
        for (var i = 0; i < n; i++)
        {
            f += i * nums[i];
        }

        var ans = f;
        for (var i = n - 1; i > 0; i--)
        {
            f += numSum - n * nums[i];
            ans = Math.Max(ans, f);
        }

        return ans;
    }

    /// <summary>
    /// 401. Binary Watch
    /// </summary>
    public IList<string> ReadBinaryWatch(int turnedOn)
    {
        IList<string> ans = new List<string>();
        for (var i = 0; i < 1024; ++i)
        {
            int h = i >> 6, m = i & 63;
            if (h < 12 && m < 60 && BitCount(i) == turnedOn)
            {
                ans.Add(h + ":" + (m < 10 ? "0" : "") + m);
            }
        }

        return ans;

        int BitCount(int i)
        {
            i = i - ((i >> 1) & 0x55555555);
            i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
            i = (i + (i >> 4)) & 0x0f0f0f0f;
            i = i + (i >> 8);
            i = i + (i >> 16);
            return i & 0x3f;
        }
    }

    /// <summary>
    /// 412. Fizz Buzz
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> FizzBuzz(int n)
    {
        IList<string> answer = new List<string>();
        for (var i = 1; i <= n; i++)
        {
            var sb = new StringBuilder();
            if (i % 3 == 0)
            {
                sb.Append("Fizz");
            }

            if (i % 5 == 0)
            {
                sb.Append("Buzz");
            }

            if (sb.Length == 0)
            {
                sb.Append(i);
            }

            answer.Add(sb.ToString());
        }

        return answer;
    }

    /// <summary>
    /// 415. Add Strings
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public string AddStrings(string num1, string num2)
    {
        int i = num1.Length - 1, j = num2.Length - 1, carry = 0;
        var ans = new StringBuilder();
        while (i >= 0 || j >= 0 || carry != 0)
        {
            var x = i >= 0 ? num1[i] - '0' : 0;
            var y = j >= 0 ? num2[j] - '0' : 0;
            var result = x + y + carry;
            ans.Append(result % 10);
            carry = result / 10;
            i--;
            j--;
        }

        return new string(ans.ToString().Reverse().ToArray());
    }

    /// <summary>
    /// 429. N-ary Tree Level Order Traversal
    /// </summary>
    public IList<IList<int>> LevelOrder(Node root)
    {
        IList<IList<int>> res = new List<IList<int>>();
        if (root == null)
        {
            return res;
        }

        var queue = new Queue<Node>();
        queue.Enqueue(root);
        while (queue.Any())
        {
            var size = queue.Count;
            IList<int> tmp = new List<int>();
            for (var i = 0; i < size; i++)
            {
                var curr = queue.Peek();
                tmp.Add(curr.val);
                foreach (var child in curr.children)
                {
                    queue.Enqueue(child);
                }

                queue.Dequeue();
            }

            res.Add(tmp);
        }

        return res;
    }

    /// <summary>
    /// 434. Number of Segments in a String
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int CountSegments(string s)
    {
        return s.Where((t, i) => (i == 0 || s[i - 1] == ' ') && t != ' ').Count();
    }

    /// <summary>
    /// 450. Delete Node in a BST
    /// </summary>
    public TreeNode DeleteNode(TreeNode root, int key)
    {
        if (root == null)
        {
            return null;
        }

        if (root.val > key)
        {
            root.left = DeleteNode(root.left, key);
        }
        else if (root.val < key)
        {
            root.right = DeleteNode(root.right, key);
        }
        else
        {
            if (root.left == null)
            {
                return root.right;
            }

            if (root.right == null)
            {
                return root.left;
            }

            var minNode = FindMin(root.right);
            root.val = minNode.val;
            root.right = DeleteNode(root.right, root.val);
        }

        return root;

        TreeNode FindMin(TreeNode node)
        {
            while (node.left != null)
            {
                node = node.left;
            }

            return node;
        }
    }

    /// <summary>
    /// 453. Minimum Moves to Equal Array Elements
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinMoves(int[] nums)
    {
        var min = nums.Min();
        var res = 0;
        foreach (var num in nums)
        {
            res += num - min;
        }
        return res;
    }

    /// <summary>
    /// 459. Repeated Substring Pattern
    /// </summary>
    /// <param name="s">input string</param>
    /// <returns></returns>
    public bool RepeatedSubstring(string s)
    {
        var n = s.Length;
        var sb = new StringBuilder();
        for (var i = 1; i <= n / 2; i++)
        {
            if (n % i != 0)
            {
                continue;
            }

            sb.Clear();
            var sub = s[..i];
            while (sb.Length < n)
            {
                sb.Append(sub);
            }

            if (sb.ToString().Equals(s))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 462. Minimum Moves to Equal Array Elements II
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinMove2(int[] nums)
    {
        Array.Sort(nums);
        var n = nums.Length;
        var mid = nums[n / 2];
        return nums.Sum(x => Math.Abs(x - mid));
    }

    /// <summary>
    /// 463. IsLand Perimeter
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int IsLandPerimeter(int[,] grid)
    {
        var island = 0;
        var neighbor = 0;
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid.GetLength(1) - 1; j++)
            {
                if (grid[i, j] != 1)
                {
                    continue;
                }

                island++;
                if (i < grid.Length - 1 && grid[i, j + 1] == 1)
                {
                    neighbor++;
                }

                if (j < grid.GetLength(1) - 1 && grid[i, j] == 1)
                {
                    neighbor++;
                }
            }
        }

        return island * 4 - neighbor * 2;
    }

    /// <summary>
    /// 467. Unique Substrings in Wraparound String
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public int FindSubstringInWraparoundString(string p)
    {
        var dp = new int[26];
        var k = 0;
        for (var i = 0; i < p.Length; ++i)
        {
            if (i > 0 && (p[i] - p[i - 1] + 26) % 26 == 1)
            {
                ++k;
            }
            else
            {
                k = 1;
            }

            dp[p[i] - 'a'] = Math.Max(dp[p[i] - 'a'], k);
        }

        return dp.Sum();
    }

    /// <summary>
    /// 468. Valid IP Address
    /// </summary>
    /// <param name="queryIP"></param>
    /// <returns></returns>
    public string ValidIPAddress(string queryIP)
    {
        if (queryIP.Count(c => c == '.') == 3)
        {
            return ValidIPv4(queryIP);
        }
        else if (queryIP.Count(c => c == ':') == 7)
        {
            return ValidIPv6(queryIP);
        }
        else
        {
            return "Neither";
        }

        string ValidIPv4(string IP)
        {
            var chunks = IP.Split('.');
            foreach (var chunk in chunks)
            {
                if (chunk.Length is 0 or > 3)
                {
                    return "Neither";
                }

                if (chunk[0] == '0' && chunk.Length != 1)
                {
                    return "Neither";
                }

                if (chunk.Any(c => !char.IsNumber(c)))
                {
                    return "Neither";
                }

                if (System.Convert.ToInt32(chunk) > 255)
                {
                    return "Neither";
                }
            }

            return "IPv4";
        }

        string ValidIPv6(string IP)
        {
            var chunks = IP.Split(':');
            const string hexDigits = "0123456789abcdefABCDEF";
            foreach (var chunk in chunks)
            {
                if (chunk.Length is 0 or > 4)
                {
                    return "Neither";
                }

                if (chunk.Any(c => !hexDigits.Contains(c)))
                {
                    return "Neither";
                }
            }

            return "IPv6";
        }
    }

    /// <summary>
    /// 477.Total Hamming Distance
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int TotalHammingDistance(int[] nums)
    {
        var size = nums.Length;
        var res = 0;
        for (var i = 0; i < 30; i++)
        {
            var tmp = nums.Sum(num => (num >> i) & 1);
            res += tmp * (size - tmp);
        }

        return res;
    }

    /// <summary>
    /// 498. Diagonal Traverse
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public int[] FindDiagonalOrder(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0)
        {
            return Array.Empty<int>();
        }

        var N = matrix.Length;
        var M = matrix[0].Length;
        int row = 0, col = 0;
        var direction = 1;
        var res = new int[N * M];
        var r = 0;
        while (row < N && col < M)
        {
            res[r++] = matrix[row][col];
            var newRow = row + (direction == 1 ? -1 : 1);
            var newCol = col + (direction == 1 ? 1 : -1);
            if (newRow < 0 || newRow == N || newCol < 0 || newCol == M)
            {
                if (direction == 1)
                {
                    row += (col == M - 1 ? 1 : 0);
                    col += (col < M - 1 ? 1 : 0);
                }
                else
                {
                    col += (row == N - 1 ? 1 : 0);
                    row += (row < N - 1 ? 1 : 0);
                }

                direction = 1 - direction;
            }
            else
            {
                row = newRow;
                col = newCol;
            }
        }

        return res;
    }

    /// <summary>
    /// 504. Base 7
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public string ConvertToBase7(int num)
    {
        if (num == 0)
        {
            return "0";
        }

        var negative = num < 0;
        num = Math.Abs(num);
        var digits = new StringBuilder();
        while (num > 0)
        {
            digits.Append(num % 7);
            num /= 7;
        }

        if (negative)
        {
            digits.Append('-');
        }

        var res = digits.ToString();
        return string.Create(res.Length, res, (chars, state) =>
        {
            state.AsSpan().CopyTo(chars);
            chars.Reverse();
        });
    }

    /// <summary>
    /// 509. Fibonacci Number
    /// </summary>
    public int Fib(int N)
    {
        if (N < 2)
        {
            return N;
        }

        var f0 = 0;
        var f1 = 1;
        var res = 0;
        for (var i = 1; i < N; i++)
        {
            res = f0 + f1;
            f0 = f1;
            f1 = res;
        }

        return res;
    }

    /// <summary>
    /// 521. Longest Uncommon Subsequence I
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int FindLUSLength(string a, string b)
    {
        return a == b ? -1 : Math.Max(a.Length, b.Length);
    }

    /// <summary>
    /// 537. Complex Number Multiplication
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public string ComplexNumberMultiply(string num1, string num2)
    {
        var complex1 = num1.Split('+', 'i');
        var complex2 = num2.Split('+', 'i');
        var real1 = int.Parse(complex1[0]);
        var real2 = int.Parse(complex2[0]);
        var imag1 = int.Parse(complex1[1]);
        var imag2 = int.Parse(complex2[1]);
        return $"{real1 * real2 - imag1 * imag2}+{real1 * imag2 + imag1 * real2}i";
    }

    /// <summary>
    /// 540. Single Element in a Sorted Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SingleNonDuplicate(int[] nums)
    {
        int low = 0, high = nums.Length - 1;
        while (low < high)
        {
            var mid = (high - low) / 2 + low;
            if (nums[mid] == nums[mid ^ 1])
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return nums[low];
    }

    /// <summary>
    /// 542. 01 Matrix
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public int[][] UpdateMatrix(int[][] mat)
    {
        int m = mat.Length, n = mat[0].Length;
        var dist = new int[m][];
        for (var i = 0; i < m; i++)
        {
            dist[i] = new int[n];
            Array.Fill(dist[i], int.MaxValue / 2);
        }

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (mat[i][j] == 0)
                {
                    dist[i][j] = 0;
                }
            }
        }

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i - 1 >= 0)
                {
                    dist[i][j] = Math.Min(dist[i][j], dist[i - 1][j] + 1);
                }

                if (j - 1 >= 0)
                {
                    dist[i][j] = Math.Min(dist[i][j], dist[i][j - 1] + 1);
                }
            }
        }

        for (var i = m - 1; i >= 0; i--)
        {
            for (var j = n - 1; j >= 0; j--)
            {
                if (i + 1 < m)
                {
                    dist[i][j] = Math.Min(dist[i][j], dist[i + 1][j] + 1);
                }

                if (j + 1 < n)
                {
                    dist[i][j] = Math.Min(dist[i][j], dist[i][j + 1] + 1);
                }
            }
        }

        return dist;
    }

    /// <summary>
    /// 553. Optimal Division
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public string OptimalDivision(int[] nums)
    {
        var n = nums.Length;
        switch (n)
        {
            case 1:
                return nums[0].ToString();
            case 2:
                return $"{nums[0]}/{nums[1]}";
        }

        var res = new StringBuilder();
        res.Append(nums[0]);
        res.Append("/(");
        res.Append(nums[1]);
        for (var i = 2; i < n; i++)
        {
            res.Append('/');
            res.Append(nums[i]);
        }

        res.Append(')');
        return res.ToString();
    }

    /// <summary>
    /// 557. Reverse Words in a String III
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string ReverseWords(string s)
    {
        var sb = new StringBuilder();
        var len = s.Length;
        var i = 0;
        while (i < len)
        {
            var start = i;
            while (i < len && s[i] != ' ')
            {
                i++;
            }

            for (var p = start; p < i; p++)
            {
                sb.Append(s[start + i - 1 - p]);
            }

            while (i < len && s[i] == ' ')
            {
                i++;
                sb.Append(' ');
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// 567. Permutation in String
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public bool CheckInclusion(string s1, string s2)
    {
        int n = s1.Length, m = s2.Length;
        if (n > m)
        {
            return false;
        }

        var cnt = new int[26];
        for (var i = 0; i < n; i++)
        {
            --cnt[s1[i] - 'a'];
            ++cnt[s2[i] - 'a'];
        }

        var diff = cnt.Count(item => item != 0);
        if (diff == 0)
        {
            return true;
        }

        for (var i = n; i < m; i++)
        {
            int x = s2[i] - 'a', y = s2[i - n] - 'a';
            if (x == y)
            {
                continue;
            }

            if (cnt[x] == 0)
            {
                ++diff;
            }

            ++cnt[x];
            if (cnt[x] == 0)
            {
                --diff;
            }

            if (cnt[y] == 0)
            {
                ++diff;
            }

            --cnt[y];
            if (cnt[y] == 0)
            {
                --diff;
            }

            if (diff == 0)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 589. N-ary Tree Preorder Traversal
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PreOrder(Node root)
    {
        IList<int> ans = new List<int>();
        Dfs(root);
        return ans;

        void Dfs(Node node)
        {
            if (node is null)
            {
                return;
            }

            ans.Add(node.val);
            foreach (var ch in node.children)
            {
                Dfs(ch);
            }
        }
    }

    /// <summary>
    /// 590. N-ary Tree Postorder Traversal
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<int> PostOrder(Node root)
    {
        IList<int> ans = new List<int>();
        Dfs(root);
        return ans;

        void Dfs(Node node)
        {
            if (node is null)
            {
                return;
            }

            foreach (var ch in node.children)
            {
                Dfs(ch);
            }

            ans.Add(node.val);
        }
    }

    /// <summary>
    /// 599. Minimum Index Sum of Two Lists
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public string[] FindRestaurant(string[] list1, string[] list2)
    {
        var index = new Dictionary<string, int>();
        for (var i = 0; i < list1.Length; i++)
        {
            index.Add(list1[i], i);
        }

        IList<string> ret = new List<string>();
        var indexSum = int.MaxValue;
        for (var i = 0; i < list2.Length; i++)
        {
            if (!index.ContainsKey(list2[i]))
            {
                continue;
            }

            var j = index[list2[i]];
            if (i + j < indexSum)
            {
                ret.Clear();
                ret.Add(list2[i]);
                indexSum = i + j;
            }
            else if (i + j == indexSum)
            {
                ret.Add(list2[i]);
            }
        }

        return ret.ToArray();
    }

    /// <summary>
    /// 617. Merge Two Binary Trees
    /// </summary>
    public TreeNode MergeTrees(TreeNode t1, TreeNode t2)
    {
        // Recursive
        // if(t1 == null)
        //     return t2;
        // if(t2 == null)
        //     return t1;
        // t1.val += t2.val;
        // t1.left = MergeTrees(t1.left, t2.left);
        // t1.right = MergeTrees(t1.right, t2.right);
        // return t1;
        // Iterative
        if (t1 == null)
        {
            return t2;
        }

        var stack = new Stack<TreeNode[]>();
        stack.Push(new[] { t1, t2 });
        while (stack.Count > 0)
        {
            var t = stack.Pop();
            if (t[0] == null || t[1] == null)
            {
                continue;
            }

            t[0].val += t[1].val;
            if (t[0].left == null)
            {
                t[0].left = t[1].left;
            }
            else
            {
                stack.Push(new[] { t[0].left, t[1].left });
            }

            if (t[0].right == null)
            {
                t[0].right = t[1].right;
            }
            else
            {
                stack.Push(new[] { t[0].right, t[1].right });
            }
        }

        return t1;
    }

    /// <summary>
    /// 623. Add One Row to Tree
    /// </summary>
    /// <param name="root"></param>
    /// <param name="val"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public TreeNode AddOneRow(TreeNode root, int val, int depth)
    {
        if (root is null)
        {
            return null;
        }

        if (depth == 1)
        {
            return new TreeNode(val, root, null);
        }

        if (depth == 2)
        {
            root.left = new TreeNode(val, root.left, null);
            root.right = new TreeNode(val, null, root.right);
        }
        else
        {
            root.left = AddOneRow(root.left, val, depth - 1);
            root.right = AddOneRow(root.right, val, depth - 1);
        }

        return root;
    }

    /// <summary>
    /// 636. Exclusive Time of Functions
    /// </summary>
    /// <param name="n"></param>
    /// <param name="logs"></param>
    /// <returns></returns>
    public int[] ExclusiveTime(int n, IList<string> logs)
    {
        var stack = new Stack<int[]>();
        var res = new int[n];
        const string startCommand = "start";
        foreach (var log in logs)
        {
            var data = log.Split(':');
            var index = int.Parse(data[0]);
            var timestamp = int.Parse(data[2]);
            if (data[1] == startCommand)
            {
                if (stack.Count > 0)
                {
                    res[stack.Peek()[0]] += timestamp - stack.Peek()[1];
                }

                stack.Push(new int[] { index, timestamp });
            }
            else
            {
                var pair = stack.Pop();
                res[pair[0]] += timestamp - pair[1] + 1;
                if (stack.Count > 0)
                {
                    stack.Peek()[1] = timestamp + 1;
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 646. Maximum Length of Pair Chain
    /// </summary>
    /// <param name="pairs"></param>
    /// <returns></returns>
    public int FindLongestChain(int[][] pairs)
    {
        Array.Sort(pairs, (a, b) => a[1] - b[1]);
        int curr = int.MinValue, res = 0;
        foreach (var pair in pairs)
        {
            if (curr < pair[0])
            {
                curr = pair[1];
                res++;
            }
        }

        return res;
    }

    /// <summary>
    /// 652. Find Duplicate Subtrees
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public IList<TreeNode> FindDuplicateSubtrees(TreeNode root)
    {
        var seen = new Dictionary<string, (TreeNode, int)>();
        var repeat = new HashSet<TreeNode>();
        var index = 0;

        int DFS(TreeNode node)
        {
            if (node is null)
            {
                return 0;
            }

            (int, int, int) triple = (node.val, DFS(node.left), DFS(node.right));
            var key = triple.ToString();
            if (seen.ContainsKey(key))
            {
                var pair = seen[key];
                repeat.Add(pair.Item1);
                return pair.Item2;
            }
            else
            {
                seen.Add(key, (node, ++index));
                return index;
            }
        }

        DFS(root);
        return new List<TreeNode>(repeat);
    }

    /// <summary>
    /// 653. Two Sum IV
    /// </summary>
    /// <param name="root"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public bool FindTarget(TreeNode root, int k)
    {
        TreeNode GetLeft(Stack<TreeNode> stack)
        {
            var root = stack.Pop();
            var node = root.right;
            while (node != null)
            {
                stack.Push(node);
                node = node.left;
            }

            return root;
        }

        TreeNode GetRight(Stack<TreeNode> stack)
        {
            var root = stack.Pop();
            var node = root.left;
            while (node != null)
            {
                stack.Push(node);
                node = node.right;
            }

            return root;
        }

        TreeNode left = root, right = root;
        var leftStack = new Stack<TreeNode>();
        var rightStack = new Stack<TreeNode>();
        leftStack.Push(left);
        while (left.left != null)
        {
            leftStack.Push(left.left);
            left = left.left;
        }

        rightStack.Push(right);
        while (right.right != null)
        {
            rightStack.Push(right.right);
            right = right.right;
        }

        while (left != right)
        {
            if (left.val + right.val == k)
            {
                return true;
            }

            if (left.val + right.val < k)
            {
                left = GetLeft(leftStack);
            }
            else
            {
                right = GetRight(rightStack);
            }
        }

        return false;
    }

    /// <summary>
    /// 654. Maximum Binary Tree
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public TreeNode ConstructMaximumBinaryTree(int[] nums)
    {
        var n = nums.Length;
        var stack = new List<int>();
        var trees = new TreeNode[n];
        for (var i = 0; i < n; i++)
        {
            trees[i] = new TreeNode(nums[i]);
            while (stack.Count > 0 && nums[i] > nums[stack[stack.Count - 1]])
            {
                trees[i].left = trees[stack[stack.Count - 1]];
                stack.RemoveAt(stack.Count - 1);
            }

            if (stack.Count > 0)
            {
                trees[stack[stack.Count - 1]].right = trees[i];
            }

            stack.Add(i);
        }

        return trees[stack[0]];
    }

    /// <summary>
    /// 658. Find K Closest Elements
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="k"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public IList<int> FindClosestElements(int[] arr, int k, int x)
    {
        int BinarySearch(int[] nums, int target)
        {
            int low = 0, high = nums.Length - 1;
            while (low < high)
            {
                var mid = low + (high - low) / 2;
                if (nums[mid] >= target)
                {
                    high = mid;
                }
                else
                {
                    low = mid + 1;
                }
            }

            return low;
        }

        var right = BinarySearch(arr, x);
        var left = right - 1;
        var n = arr.Length;
        while (k-- > 0)
        {
            if (left < 0)
            {
                right++;
            }
            else if (right >= n || x - arr[left] <= arr[right] - x)
            {
                left--;
            }
            else
            {
                right++;
            }
        }

        return arr[(left + 1)..right];
    }

    /// <summary>
    /// 662. Maximum Width of Binary Tree
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int WidthOfBinaryTree(TreeNode root)
    {
        var levelMin = new Dictionary<int, int>();

        int DFS(TreeNode node, int depth, int index)
        {
            if (node is null)
            {
                return 0;
            }

            levelMin.TryAdd(depth, index);
            return Math.Max(index - levelMin[depth] + 1,
                Math.Max(
                    DFS(node.left, depth + 1, index * 2),
                    DFS(node.right, depth + 1, index * 2 + 1)));
        }

        return DFS(root, 1, 1);
    }

    /// <summary>
    /// 665. Non-decreasing Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public bool CheckPossibility(int[] nums)
    {
        int n = nums.Length, count = 0;
        for (var i = 0; i < n - 1; i++)
        {
            int x = nums[i], y = nums[i + 1];
            if (x > y)
            {
                count++;
                if (count > 1)
                {
                    return false;
                }

                if (i > 0 && y < nums[i - 1])
                {
                    nums[i + 1] = x;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 667. Beautiful Arrangement II
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int[] ConstructArray(int n, int k)
    {
        var res = new int[n];
        var index = 0;
        for (var i = 1; i < n - k; ++i)
        {
            res[index++] = i;
        }

        for (int i = n - k, j = n; i <= j; ++i, --j)
        {
            res[index++] = i;
            if (i != j)
            {
                res[index++] = j;
            }
        }

        return res;
    }

    /// <summary>
    /// 669. Trim a Binary Search Tree
    /// </summary>
    public TreeNode TrimBST(TreeNode root, int low, int high)
    {
        // recursively
        //if (root == null)
        //{
        //    return null;
        //}

        //if (root.val > high)
        //{
        //    return TrimBST(root.left, low, high);
        //}

        //if (root.val < low)
        //{
        //    return TrimBST(root.right, low, high);
        //}

        //root.left = TrimBST(root.left, low, high);
        //root.right = TrimBST(root.right, low, high);
        //return root;

        // iteratively
        while (root is not null && (root.val < low || root.val > high))
        {
            if (root.val < low)
            {
                root = root.right;
            }
            else
            {
                root = root.left;
            }
        }

        if (root is null)
        {
            return null;
        }

        for (var node = root; node.left is not null;)
        {
            if (node.left.val < low)
            {
                node.left = node.left.right;
            }
            else
            {
                node = node.left;
            }
        }

        for (var node = root; node.right is not null;)
        {
            if (node.right.val > high)
            {
                node.right = node.right.left;
            }
            else
            {
                node = node.right;
            }
        }

        return root;
    }

    /// <summary>
    /// 670. Maximum Swap
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int MaximumSwap(int num)
    {
        var chars = num.ToString().ToCharArray();
        var n = chars.Length;
        int maxIndex = n - 1, index1 = -1, index2 = -1;
        for (var i = n - 1; i >= 0; i--)
        {
            if (chars[i] > chars[maxIndex])
            {
                maxIndex = i;
            }
            else if (chars[i] < chars[maxIndex])
            {
                index1 = i;
                index2 = maxIndex;
            }
        }

        if (index1 >= 0)
        {
            (chars[index1], chars[index2]) = (chars[index2], chars[index1]);
            return int.Parse(new string(chars));
        }

        return num;
    }

    /// <summary>
    /// 672. Bulb Switcher II
    /// </summary>
    /// <param name="n"></param>
    /// <param name="presses"></param>
    /// <returns></returns>
    public int FlipLights(int n, int presses)
    {
        var seen = new HashSet<int>();
        for (var i = 0; i < 1 << 4; i++)
        {
            var pressArray = new int[4];
            for (var j = 0; j < 4; j++)
            {
                pressArray[j] = i >> j & 1;
            }

            var sum = pressArray.Sum();
            if (sum % 2 == presses % 2 && sum <= presses)
            {
                var status = pressArray[0] ^ pressArray[1] ^ pressArray[3];
                if (n >= 2)
                {
                    status |= (pressArray[0] ^ pressArray[1]) << 1;
                }

                if (n >= 3)
                {
                    status |= (pressArray[0] ^ pressArray[2]) << 2;
                }

                if (n >= 4)
                {
                    status |= status << 3;
                }

                seen.Add(status);
            }
        }

        return seen.Count;
    }

    /// <summary>
    /// 679. 24 Game
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public bool JudgePoint24(int[] nums)
    {
        var A = nums.Select(item => System.Convert.ToDouble(item)).ToList();
        return Solve(A);

        bool Solve(List<double> data)
        {
            switch (data.Count)
            {
                case 0:
                    return false;
                case 1:
                    return Math.Abs(data[0] - 24) < 1e-6;
            }

            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < data.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var nums2 = data.Where((_, k) => k != i && k != j).ToList();
                    for (var k = 0; k < 4; k++)
                    {
                        switch (k)
                        {
                            case < 2 when j > i:
                                continue;
                            case 0:
                                nums2.Add(data[i] + data[j]);
                                break;
                            case 1:
                                nums2.Add(data[i] * data[j]);
                                break;
                            case 2:
                                nums2.Add(data[i] - data[j]);
                                break;
                            case 3 when data[j] != 0:
                                nums2.Add(data[i] / data[j]);
                                break;
                            case 3:
                                continue;
                        }

                        if (Solve(nums2))
                        {
                            return true;
                        }

                        nums2.Remove(nums2.Count - 1);
                    }
                }
            }

            return false;
        }
    }

    /// <summary>
    /// 682. Baseball Game
    /// </summary>
    /// <param name="ops"></param>
    /// <returns></returns>
    public int CalPoints(string[] ops)
    {
        var ret = 0;
        var points = new List<int>();
        foreach (var op in ops)
        {
            var n = points.Count;
            switch (op[0])
            {
                case '+':
                    ret += points[n - 1] + points[n - 2];
                    points.Add(points[n - 1] + points[n - 2]);
                    break;
                case 'D':
                    ret += 2 * points[n - 1];
                    points.Add(2 * points[n - 1]);
                    break;
                case 'C':
                    ret -= points[n - 1];
                    points.RemoveAt(n - 1);
                    break;
                default:
                    ret += int.Parse(op);
                    points.Add(int.Parse(op));
                    break;
            }
        }

        return ret;
    }

    /// <summary>
    /// 687. Longest Univalue Path
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int LongestUnivaluePath(TreeNode root)
    {
        var res = 0;

        int DFS(TreeNode node)
        {
            if (node is null)
            {
                return 0;
            }

            int left = DFS(node.left), right = DFS(node.right);
            int left1 = 0, right1 = 0;
            if (node.left is not null && node.left.val == node.val)
            {
                left1 = left + 1;
            }

            if (node.right is not null && node.right.val == node.val)
            {
                right1 = right + 1;
            }

            res = Math.Max(res, left1 + right1);
            return Math.Max(left1, right1);
        }

        DFS(root);
        return res;
    }

    /// <summary>
    /// 695. Max Area of Island
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int MaxAreaOfIsland(int[][] grid)
    {
        var ans = 0;
        var rowLength = grid.Length;
        var colLength = grid[0].Length;
        var di = new int[] { 0, 0, 1, -1 };
        var dj = new int[] { 1, -1, 0, 0 };
        for (var row = 0; row < rowLength; row++)
        {
            for (var col = 0; col < colLength; col++)
            {
                var curr = 0;
                var queueRow = new Queue<int>();
                var queueCol = new Queue<int>();
                queueRow.Enqueue(row);
                queueCol.Enqueue(col);
                while (queueRow.Count != 0)
                {
                    int currRow = queueRow.Dequeue(), currCol = queueCol.Dequeue();
                    if (currRow < 0 || currCol < 0 || currRow == rowLength || currCol == colLength ||
                        grid[currRow][currCol] != 1)
                    {
                        continue;
                    }

                    ++curr;
                    grid[currRow][currCol] = 0;
                    for (var index = 0; index != 4; ++index)
                    {
                        int nextRow = currRow + di[index], nextCol = currCol + dj[index];
                        queueRow.Enqueue(nextRow);
                        queueCol.Enqueue(nextCol);
                    }
                }

                ans = Math.Max(ans, curr);
            }
        }

        return ans;
    }

    /// <summary>
    /// 698. Partition to K Equal Sum Subsets
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public bool CanPartitionKSubsets(int[] nums, int k)
    {
        var sum = nums.Sum();
        if (sum % k != 0)
        {
            return false;
        }

        var average = sum / k;
        Array.Sort(nums);
        var len = nums.Length;
        if (nums[len - 1] > average)
        {
            return false;
        }

        var dp = new bool[1 << len];
        var currSum = new int[1 << len];
        dp[0] = true;
        for (var i = 0; i < 1 << len; i++)
        {
            if (!dp[i])
            {
                continue;
            }

            for (var j = 0; j < len; j++)
            {
                if (currSum[i] + nums[j] > average)
                {
                    break;
                }

                if (((i >> j) & 1) == 0)
                {
                    var next = i | (1 << j);
                    if (!dp[next])
                    {
                        currSum[next] = (currSum[i] + nums[j]) % average;
                        dp[next] = true;
                    }
                }
            }
        }

        return dp[(1 << len) - 1];
    }

    /// <summary>
    /// 700. Search in a Binary Search Tree
    /// </summary>
    public TreeNode SearchBST(TreeNode root, int val)
    {
        while (root != null && root.val != val)
        {
            root = val < root.val ? root.left : root.right;
        }

        return root;
    }

    /// <summary>
    /// 701. Insert into a Binary Search Tree
    /// </summary>
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        // recursive
        // if(root == null)
        //     return new TreeNode(val);
        // if(val > root.val)
        //     root.right = InsertIntoBST(root.right, val);
        // else
        //     root.left = InsertIntoBST(root.left, val);
        // return root;
        // iterative
        if (root == null)
        {
            return new TreeNode(val);
        }

        var currentNode = root;
        while (true)
        {
            if (currentNode.val >= val)
            {
                if (currentNode.left != null)
                {
                    currentNode = currentNode.left;
                }
                else
                {
                    currentNode.left = new TreeNode(val);
                    break;
                }
            }
            else
            {
                if (currentNode.right != null)
                {
                    currentNode = currentNode.right;
                }
                else
                {
                    currentNode.right = new TreeNode(val);
                    break;
                }
            }
        }

        return root;
    }

    /// <summary>
    /// 704. Binary Search
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int BinarySearch(int[] nums, int target)
    {
        // version 1
        //int left = 0;
        //int right = nums.Length - 1;
        //while (left <= right)
        //{
        //    int mid = left + (right - left) / 2;
        //    if (nums[mid] > target)
        //    {
        //        right = mid - 1;
        //    }
        //    else if (nums[mid] < target)
        //    {
        //        left = mid + 1;
        //    }
        //    else
        //    {
        //        return mid;
        //    }
        //}
        //return -1;

        var left = 0;
        var right = nums.Length;
        while (left < right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] > target)
            {
                right = mid;
            }
            else if (nums[mid] < target)
            {
                left = mid;
            }
            else
            {
                return mid;
            }
        }

        return -1;
    }

    /// <summary>
    /// 709. To Lower Case
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string ToLowerCase(string str)
    {
        var sb = new StringBuilder();
        foreach (var r in str.Select(c => (char)(c | 32)))
        {
            sb.Append(r);
        }

        return sb.ToString();

        // LINQ
        // return string.Concat(str.Select(c => c >= 'A' && c <= 'Z' ? (char)(c + 32) : c));
    }

    /// <summary>
    /// 720. Longest Word in Dictionary
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public string LongestWord(string[] words)
    {
        var trie = new Trie();
        foreach (var word in words)
        {
            trie.Insert(word);
        }

        var longest = string.Empty;
        foreach (var word in words)
        {
            if (trie.Search(word))
            {
                if (word.Length > longest.Length || (word.Length == longest.Length && word.CompareTo(longest) < 0))
                {
                    longest = word;
                }
            }
        }

        return longest;
    }

    /// <summary>
    /// 724. Find Pivot Index
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int PivotIndex(int[] nums)
    {
        var total = nums.Sum();
        var sum = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            if (2 * sum + nums[i] == total)
            {
                return i;
            }

            sum += nums[i];
        }

        return -1;
    }

    /// <summary>
    /// 728. Self Dividing Numbers
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public IList<int> SelfDividingNumbers(int left, int right)
    {
        bool IsSelfDividing(int num)
        {
            var tmp = num;
            while (tmp > 0)
            {
                var digit = tmp % 10;
                if (digit == 0 || num % digit != 0)
                {
                    return false;
                }

                tmp /= 10;
            }

            return true;
        }

        IList<int> ans = new List<int>();
        for (var i = left; i <= right; i++)
        {
            if (IsSelfDividing(i))
            {
                ans.Add(i);
            }
        }

        return ans;
    }

    /// <summary>
    /// 733. Flood Fill
    /// </summary>
    /// <param name="image"></param>
    /// <param name="sr"></param>
    /// <param name="sc"></param>
    /// <param name="newColor"></param>
    /// <returns></returns>
    public int[][] FloodFill(int[][] image, int sr, int sc, int newColor)
    {
        int[] dx = { 1, 0, 0, -1 };
        int[] dy = { 0, 1, -1, 0 };
        var currentColor = image[sr][sc];
        if (currentColor == newColor)
        {
            return image;
        }

        int n = image.Length, m = image[0].Length;
        var queue = new Queue<int[]>();
        queue.Enqueue(new[] { sr, sc });
        image[sr][sc] = newColor;
        while (queue.Any())
        {
            var pair = queue.Dequeue();
            int x = pair[0], y = pair[1];
            for (var i = 0; i < 4; i++)
            {
                int mx = x + dx[i], my = y + dy[i];
                if (mx < 0 || mx >= n || my < 0 || my >= m || image[mx][my] != currentColor)
                {
                    continue;
                }

                queue.Enqueue(new[] { mx, my });
                image[mx][my] = newColor;
            }
        }

        return image;
    }

    /// <summary>
    /// 739. Daily Temperatures
    /// </summary>
    public int[] DailyTemperatures(int[] T)
    {
        var stack = new Stack<int>();
        var len = T.Length;
        var ans = new int[len];
        for (var i = 0; i < len; i++)
        {
            while (stack.Count != 0 && T[i] > T[stack.Peek()])
            {
                var t = stack.Peek();
                stack.Pop();
                ans[t] = i - t;
            }

            stack.Push(i);
        }

        return ans;
    }

    /// <summary>
    /// 769. Max Chunks To Make Sorted
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public int MaxChunksToSorted(int[] arr)
    {
        int m = 0, res = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            m = Math.Max(m, arr[i]);
            if (m == i)
            {
                res++;
            }
        }

        return res++;
    }

    /// <summary>
    /// 777. Swap Adjacent in LR String
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public bool CanTransform(string start, string end)
    {
        var n = start.Length;
        int i = 0, j = 0;
        while (i < n && j < n)
        {
            while (i < n && start[i] == 'X')
            {
                i++;
            }

            while (j < n && end[j] == 'X')
            {
                j++;
            }

            if (i < n && j < n)
            {
                var c = start[i];
                if (c != end[j])
                {
                    return false;
                }

                if ((c == 'L' && i < j) || (c == 'R' && i > j))
                {
                    return false;
                }

                i++;
                j++;
            }
        }

        while (i < n)
        {
            if (start[i] != 'X')
            {
                return false;
            }

            i++;
        }

        while (j < n)
        {
            if (end[j] != 'X')
            {
                return false;
            }

            j++;
        }

        return true;
    }

    /// <summary>
    /// 779. K-th Symbol in Grammar
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int KthGrammar(int n, int k)
    {
        k--;
        var res = 0;
        while (k > 0)
        {
            k &= k - 1;
            res ^= 1;
        }
        return res;
    }

    /// <summary>
    /// 784. Letter Case Permutation
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public List<string> LetterCasePermutation(string s)
    {
        var ans = new List<StringBuilder> { new StringBuilder() };
        foreach (var c in s)
        {
            var n = ans.Count;
            if (char.IsLetter(c))
            {
                for (var i = 0; i < n; ++i)
                {
                    ans.Add(new StringBuilder(ans[i].ToString()));
                    ans[i].Append(char.ToLower(c));
                    ans[n + i].Append(char.ToUpper(c));
                }
            }
            else
            {
                for (var i = 0; i < n; i++)
                {
                    ans[i].Append(c);
                }
            }
        }

        return ans.Select(sb => sb.ToString()).ToList();
    }

    /// <summary>
    /// 793. Preimage Size of Factorial Zeroes Function
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    public int PreimageSizeFZF(int k)
    {
        long Zeta(long x)
        {
            long res = 0;
            while (x != 0)
            {
                res += x / 5;
                x /= 5;
            }

            return res;
        }

        long Nx(int k)
        {
            long left = 0, right = 5L * k;
            while (left <= right)
            {
                var mid = (left + right) / 2;
                if (Zeta(mid) < k)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return right + 1;
        }

        return (int)(Nx(k + 1) - Nx(k));
    }

    /// <summary>
    /// 796. Rotate String
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public bool RotateString(string A, string B)
    {
        return A.Length == B.Length && (A + A).Contains(B);
    }

    /// <summary>
    /// 801. Minimum Swaps To Make Sequences Increasing
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public int MinSwap(int[] nums1, int[] nums2)
    {
        var n = nums1.Length;
        int a = 0, b = 1;
        for (var i = 1; i < n; i++)
        {
            int at = a, bt = b;
            a = b = n;
            if (nums1[i] > nums1[i - 1] && nums2[i] > nums2[i - 1])
            {
                a = Math.Min(a, at);
                b = Math.Min(b, bt + 1);
            }
            if (nums1[i] > nums2[i - 1] && nums2[i] > nums1[i - 1])
            {
                a = Math.Min(a, bt);
                b = Math.Min(b, at + 1);
            }
        }

        return Math.Min(a, b);
    }

    /// <summary>
    /// 804. Unique Morse Code Words
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public int UniqueMorseRepresentations(string[] words)
    {
        var morse = new string[]
        {
            ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---",
            ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.."
        };
        var seen = new HashSet<string>();
        foreach (var word in words)
        {
            var code = new StringBuilder();
            foreach (var c in word)
            {
                code.Append(morse[c - 'a']);
            }

            seen.Add(code.ToString());
        }

        return seen.Count;
    }

    /// <summary>
    /// 806. Number of Lines To Write String
    /// </summary>
    /// <param name="widths"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public int[] NumberOfLines(int[] widths, string s)
    {
        const int maxWidth = 100;
        int lines = 1, width = 0;
        for (var i = 0; i < s.Length; i++)
        {
            var need = widths[s[i] - 'a'];
            width += need;
            if (width > maxWidth)
            {
                lines++;
                width = need;
            }
        }

        return new int[] { lines, width };
    }

    /// <summary>
    /// 807. Max Increase to Keep City Skyline
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int MaxIncreaseKeepingSkyline(int[][] grid)
    {
        var n = grid.Length;
        var rowMax = new int[n];
        var colMax = new int[n];
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                rowMax[i] = Math.Max(rowMax[i], grid[i][j]);
                colMax[j] = Math.Max(colMax[j], grid[i][j]);
            }
        }

        var ans = 0;
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                ans += Math.Min(rowMax[i], colMax[j]) - grid[i][j];
            }
        }

        return ans;
    }

    /// <summary>
    /// 811. Subdomain Visit Count
    /// </summary>
    /// <param name="cpdomains"></param>
    /// <returns></returns>
    public IList<string> SubdomainVisit(string[] cpdomains)
    {
        IList<string> res = new List<string>();
        var counts = new Dictionary<string, int>();
        foreach (var cpdomain in cpdomains)
        {
            var space = cpdomain.IndexOf(' ');
            var count = int.Parse(cpdomain.Substring(0, space));
            var domain = cpdomain.Substring(space + 1);
            if (!counts.ContainsKey(domain))
            {
                counts.Add(domain, 0);
            }

            counts[domain] += count;
            for (var i = 0; i < domain.Length; i++)
            {
                if (domain[i] == '.')
                {
                    var subdomain = domain.Substring(i + 1);
                    if (!counts.ContainsKey(subdomain))
                    {
                        counts.Add(subdomain, 0);
                    }

                    counts[subdomain] += count;
                }
            }
        }

        foreach (var pair in counts)
        {
            res.Add(pair.Value + " " + pair.Key);
        }

        return res;
    }

    /// <summary>
    /// 817. Linked List Components
    /// </summary>
    /// <param name="head"></param>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int NumComponents(ListNode head, int[] nums)
    {
        ISet<int> numsSet = new HashSet<int>();
        foreach (var num in nums)
        {
            numsSet.Add(num);
        }

        var inSet = false;
        var res = 0;
        while (head is not null)
        {
            if (numsSet.Contains(head.val))
            {
                if (!inSet)
                {
                    inSet = true;
                    res++;
                }
            }
            else
            {
                inSet = false;
            }

            head = head.next;
        }

        return res;
    }

    /// <summary>
    /// 819. Most Common Word
    /// </summary>
    /// <param name="paragraph"></param>
    /// <param name="banned"></param>
    /// <returns></returns>
    public string MostCommonWord(string paragraph, string[] banned)
    {
        var bannedSet = new HashSet<string>();
        foreach (var word in banned)
        {
            bannedSet.Add(word);
        }

        var maxFrequency = 0;
        var frequencies = new Dictionary<string, int>();
        var sb = new StringBuilder();
        var len = paragraph.Length;
        for (var i = 0; i <= len; i++)
        {
            if (i < len && char.IsLetter(paragraph[i]))
            {
                sb.Append(char.ToLower(paragraph[i]));
            }
            else if (sb.Length > 0)
            {
                var word = sb.ToString();
                if (!bannedSet.Contains(word))
                {
                    if (!frequencies.ContainsKey(word))
                    {
                        frequencies.Add(word, 1);
                    }
                    else
                    {
                        frequencies[word]++;
                    }

                    maxFrequency = Math.Max(maxFrequency, frequencies[word]);
                }

                sb.Clear();
            }
        }

        var mostCommon = "";
        foreach ((var word, var frequency) in frequencies)
        {
            if (frequency == maxFrequency)
            {
                mostCommon = word;
                break;
            }
        }

        return mostCommon;
    }

    /// <summary>
    /// 821. Shortest Distance to a Character
    /// </summary>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public int[] ShortestToChar(string s, char c)
    {
        var n = s.Length;
        var ans = new int[n];
        var prev = int.MinValue / 2;
        for (var i = 0; i < n; i++)
        {
            if (s[i] == c)
            {
                prev = i;
            }

            ans[i] = i - prev;
        }

        prev = int.MaxValue / 2;
        for (var i = n - 1; i >= 0; i--)
        {
            if (s[i] == c)
            {
                prev = i;
            }

            ans[i] = Math.Min(ans[i], prev - i);
        }

        return ans;
    }

    /// <summary>
    /// 824. Goat Latin
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public string ToGoatLatin(string sentence)
    {
        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        if (sentence.Length == 0)
        {
            return string.Empty;
        }

        var sb = new StringBuilder("a");
        var words = sentence.Split(' ');
        for (var i = 0; i < words.Length; i++)
        {
            if (vowels.Contains(words[i][0]))
            {
                words[i] = words[i] + "ma" + sb.ToString();
            }
            else
            {
                words[i] = words[i][1..^0] + words[i][0] + "ma" + sb.ToString();
            }

            sb.Append('a');
        }

        return string.Join(" ", words);
    }

    /// <summary>
    /// 828. Count Unique Characters of All Substrings of a Given String
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int UniqueLetterString(string s)
    {
        var index = new Dictionary<char, IList<int>>();
        for (var i = 0; i < s.Length; i++)
        {
            var c = s[i];
            if (!index.ContainsKey(c))
            {
                index.Add(c, new List<int> { -1 });
            }

            index[c].Add(i);
        }

        var res = 0;
        foreach (var pair in index)
        {
            var list = pair.Value;
            list.Add(s.Length);
            for (var i = 1; i < list.Count - 1; i++)
            {
                res += (list[i] - list[i - 1]) * (list[i + 1] - list[i]);
            }
        }

        return res;
    }

    /// <summary>
    /// 838. Push Dominoes
    /// </summary>
    /// <param name="dominoes"></param>
    /// <returns></returns>
    public string PushDominoes(string dominoes)
    {
        var s = dominoes.ToCharArray();
        int n = s.Length, i = 0;
        var left = 'L';
        while (i < n)
        {
            var j = i;
            while (j < n && s[j] == '.')
            {
                j++;
            }

            var right = j < n ? s[j] : 'R';
            if (left == right)
            {
                while (i < j)
                {
                    s[i++] = right;
                }
            }
            else if (left == 'R' && right == 'L')
            {
                var k = j - 1;
                while (i < k)
                {
                    s[i++] = 'R';
                    s[k--] = 'L';
                }
            }

            left = right;
            i = j + 1;
        }

        return new string(s);
    }

    /// <summary>
    /// 844. Backspace String Compare 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool BackspaceCompare(string s, string t)
    {
        int i = s.Length - 1, j = t.Length - 1;
        int skipS = 0, skipT = 0;
        while (i >= 0 || j >= 0)
        {
            while (i >= 0)
            {
                if (s[i] == '#')
                {
                    skipS++;
                    i--;
                }
                else if (skipS > 0)
                {
                    skipS--;
                    i--;
                }
                else
                {
                    break;
                }
            }

            while (j >= 0)
            {
                if (t[j] == '#')
                {
                    skipT++;
                    j--;
                }
                else if (skipT > 0)
                {
                    skipT--;
                    j--;
                }
                else
                {
                    break;
                }
            }

            if (i >= 0 && j >= 0)
            {
                if (s[i] != t[j])
                {
                    return false;
                }
            }
            else
            {
                if (i >= 0 || j >= 0)
                {
                    return false;
                }
            }

            i--;
            j--;
        }

        return true;
    }

    /// <summary>
    /// 846. Hand of Straights
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="groupSize"></param>
    /// <returns></returns>
    public bool IsNStraightHand(int[] hand, int groupSize)
    {
        var n = hand.Length;
        if (n % groupSize != 0)
        {
            return false;
        }

        Array.Sort(hand);
        var count = new Dictionary<int, int>();
        foreach (var x in hand)
        {
            if (!count.ContainsKey(x))
            {
                count.Add(x, 0);
            }

            count[x]++;
        }

        foreach (var x in hand)
        {
            if (!count.ContainsKey(x))
            {
                continue;
            }

            for (var j = 0; j < groupSize; j++)
            {
                var num = x + j;
                if (!count.ContainsKey(num))
                {
                    return false;
                }

                count[num]--;
                if (count[num] == 0)
                {
                    count.Remove(num);
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 852. Peak Index in a Mountain Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int PeakIndexInMountainArray(int[] nums)
    {
        int l = 0, r = nums.Length - 1;
        while (l < r)
        {
            var mid = (l + r) / 2;
            if (nums[mid] < nums[mid + 1])
            {
                l = mid + 1;
            }
            else
            {
                r = mid;
            }
        }

        return l;
    }

    /// <summary>
    /// 856. Score of Parentheses
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int ScoreOfParentheses(string s)
    {
        int bal = 0, n = s.Length, res = 0;
        for (var i = 0; i < n; i++)
        {
            bal += (s[i] == '(' ? 1 : -1);
            if (s[i] == ')' && s[i - 1] == '(')
            {
                res += 1 << bal;
            }
        }

        return res;
    }

    /// <summary>
    /// 857. Minimum Cost to Hire K Workers
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="wage"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public double MinCostToHireWorkers(int[] quality, int[] wage, int k)
    {
        var n = quality.Length;
        var hire = new int[n];
        for (var i = 0; i < n; i++)
        {
            hire[i] = i;
        }

        Array.Sort(hire, (a, b) =>
        {
            return quality[b] * wage[a] - quality[a] * wage[b];
        });
        var res = 1e9;
        var totalQuality = 0.0d;
        var queue = new PriorityQueue<int, int>();
        for (var i = 0; i < k - 1; i++)
        {
            totalQuality += quality[hire[i]];
            queue.Enqueue(quality[hire[i]], -quality[hire[i]]);
        }

        for (var i = k - 1; i < n; i++)
        {
            var index = hire[i];
            totalQuality += quality[index];
            queue.Enqueue(quality[index], -quality[index]);
            var totalCost = ((double)wage[index] / quality[index]) * totalQuality;
            res = Math.Min(res, totalCost);
            totalQuality -= queue.Dequeue();
        }

        return res;
    }

    /// <summary>
    /// 870. Advantage Shuffle
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public int[] AdvantageCount(int[] nums1, int[] nums2)
    {
        var n = nums1.Length;
        var index1 = new int[n];
        var index2 = new int[n];
        for (var i = 0; i < n; i++)
        {
            index1[i] = i;
            index2[i] = i;
        }
        Array.Sort(index1, (i, j) => nums1[i] - nums1[j]);
        Array.Sort(index2, (i, j) => nums2[i] - nums2[j]);

        var res = new int[n];
        int left = 0, right = n - 1;
        for (var i = 0; i < n; i++)
        {
            if (nums1[index1[i]] > nums2[index2[left]])
            {
                res[index2[left]] = nums1[index1[i]];
                left++;
            }
            else
            {
                res[index2[right]] = nums1[index1[i]];
                right--;
            }
        }

        return res;
    }

    /// <summary>
    /// 876. Middle of the Linked List
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode MiddleNode(ListNode head)
    {
        var slow = head;
        var fast = head;
        while (fast?.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        return slow;
    }

    /// <summary>
    /// 883. Projection Area of 3D Shapes 
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int ProjectionArea(int[][] grid)
    {
        var ans = 0;
        for (var x = 0; x < grid.Length; x++)
        {
            var row = 0;
            var column = 0;
            for (var y = 0; y < grid[x].Length; y++)
            {
                if (grid[x][y] != 0)
                {
                    ans++;
                }

                row = Math.Max(row, grid[x][y]);
                column = Math.Max(column, grid[y][x]);
            }

            ans += row + column;
        }

        return ans;
    }

    /// <summary>
    /// 884. Uncommon Words from Two Sentences.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public string[] UncommonFromSentences(string s1, string s2)
    {
        var dic = new Dictionary<string, int>();
        foreach (var word in s1.Split(' '))
        {
            if (dic.ContainsKey(word))
            {
                dic[word]++;
            }
            else
            {
                dic[word] = 1;
            }
        }

        foreach (var word in s2.Split(' '))
        {
            if (dic.ContainsKey(word))
            {
                dic[word]++;
            }
            else
            {
                dic[word] = 1;
            }
        }

        return (from word in dic where word.Value == 1 select word.Key).ToArray();
    }

    /// <summary>
    /// 886. Possible Bipartition
    /// </summary>
    /// <param name="n"></param>
    /// <param name="dislikes"></param>
    /// <returns></returns>
    public bool PossibleBipartition(int n, int[][] dislikes)
    {
        bool DFS(int curr, int nowColor, int[] color, IList<int>[] group)
        {
            color[curr] = nowColor;
            foreach (var next in group[curr])
            {
                if (color[next] != 0 && color[next] == color[curr])
                {
                    return false;
                }
                if (color[next] == 0 && !DFS(next, 3 ^ nowColor, color, group))
                {
                    return false;
                }
            }

            return true;
        }

        var color = new int[n + 1];
        var group = new IList<int>[n + 1];
        for (var i = 0; i <= n; i++)
        {
            group[i] = new List<int>();
        }

        foreach (var p in dislikes)
        {
            group[p[0]].Add(p[1]);
            group[p[1]].Add(p[0]);
        }

        for (var i = 1; i <= n; i++)
        {
            if (color[i] == 0 && !DFS(i, 1, color, group))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 892. Surface Area of 3D Shapes
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int SurfaceArea(int[][] grid)
    {
        int res = 0, n = grid.Length;
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (grid[i][j] > 0)
                {
                    res += grid[i][j] * 4 + 2;
                }

                if (i > 0)
                {
                    res -= Math.Min(grid[i][j], grid[i - 1][j]) * 2;
                }

                if (j > 0)
                {
                    res -= Math.Min(grid[i][j], grid[i][j - 1]) * 2;
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 893. Groups of Special-Equivalent Strings
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public int NumSpecialEquivGroups(string[] A)
    {
        var seen = new HashSet<string>();
        foreach (var s in A)
        {
            var count = new int[52];
            for (var i = 0; i < s.Length; i++)
            {
                count[s[i] - 'a' + 26 * (i % 2)]++;
            }

            seen.Add(string.Join(",", count));
        }

        return seen.Count;
    }

    /// <summary>
    /// 896. Monotonic Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns><c>true</c> if and only if the given array A is monotonic; otherwise, <c>false</c></returns>
    public bool IsMonotonic(int[] nums)
    {
        var store = 0;
        for (var i = 0; i < nums.Length - 1; i++)
        {
            var c = nums[i].CompareTo(nums[i + 1]);
            if (c == 0)
            {
                continue;
            }

            if (c != store && store != 0)
            {
                return false;
            }

            store = c;
        }

        return true;
    }

    /// <summary>
    /// 904. Fruit Into Baskets
    /// </summary>
    /// <param name="fruit"></param>
    /// <returns></returns>
    public int TotalFruit(int[] fruits)
    {
        var n = fruits.Length;
        var counter = new Dictionary<int, int>();
        int left = 0, res = 0;
        for (var right = 0; right < n; ++right)
        {
            counter.TryAdd(fruits[right], 0);
            ++counter[fruits[right]];
            while (counter.Count > 2)
            {
                --counter[fruits[left]];
                if (counter[fruits[left]] == 0)
                {
                    counter.Remove(fruits[left]);
                }
                ++left;
            }
            res = Math.Max(res, right - left + 1);
        }
        return res;
    }

    /// <summary>
    /// 905. Sort Array By Parity
    /// </summary>
    public int[] SortArrayByParity(int[] nums)
    {
        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            while (left < right && nums[left] % 2 == 0)
            {
                left++;
            }

            while (left < right && nums[right] % 2 == 1)
            {
                right--;
            }

            if (left < right)
            {
                (nums[left], nums[right]) = (nums[right], nums[left]);
                left++;
                right--;
            }
        }

        return nums;
    }

    /// <summary>
    /// 908. Smallest Range I
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int SmallestRangeI(int[] nums, int k)
    {
        var min = nums.Min();
        var max = nums.Max();
        return max - min <= 2 * k ? 0 : max - min - 2 * k;
    }

    /// <summary>
    /// 915. Partition Array into Disjoint Intervals
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int PartitionDisjoint(int[] nums)
    {
        var n = nums.Length;
        int leftMax = nums[0], leftPos = 0, curr = nums[0];
        for (var i = 1; i < n - 1; i++)
        {
            curr = Math.Max(curr, nums[i]);
            if (nums[i] < leftMax)
            {
                leftMax = curr;
                leftPos = i;
            }
        }
        return leftPos + 1;
    }

    /// <summary>
    /// 917. Reverse Only Letters
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string ReverseOnlyLetters(string s)
    {
        var letters = s.ToCharArray();
        int l = 0, r = s.Length - 1;
        while (true)
        {
            while (l < r && !char.IsLetter(s[l]))
            {
                l++;
            }

            while (r > l && !char.IsLetter(s[r]))
            {
                r--;
            }

            if (l >= r)
            {
                break;
            }

            //char tmp = letters[l];
            //letters[l] = letters[r];
            //letters[r] = tmp;
            Util.Swap(letters, l, r);
            l++;
            r--;
        }

        return new string(letters);
    }

    /// <summary>
    /// 921. Minimum Add to Make Parentheses Valid
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int MinAddToMakeValid(string s)
    {
        int res = 0, leftCount = 0;
        var n = s.Length;
        for (var i = 0; i < n; i++)
        {
            var c = s[i];
            if (c == '(')
            {
                leftCount++;
            }
            else
            {
                if (leftCount > 0)
                {
                    leftCount--;
                }
                else
                {
                    res++;
                }
            }
        }

        res += leftCount;
        return res;
    }

    /// <summary>
    /// 927. Three Equal Parts
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public int[] ThreeEqualParts(int[] arr)
    {
        var sum = arr.Sum();
        if (sum % 3 != 0)
        {
            return new int[] { -1, -1 };
        }

        if (sum == 0)
        {
            return new int[] { 0, 2 };
        }

        var partial = sum / 3;
        int first = 0, second = 0, third = 0, curr = 0;
        for (var i = 0; i < arr.Length; i++)
        {
            if (arr[i] == 1)
            {
                if (curr == 0)
                {
                    first = i;
                }
                else if (curr == partial)
                {
                    second = i;
                }
                else if (curr == 2 * partial)
                {
                    third = i;
                }

                curr++;
            }
        }

        var len = arr.Length - third;
        if (first + len <= second && second + len <= third)
        {
            var i = 0;
            while (third + i < arr.Length)
            {
                if (arr[first + i] != arr[second + i] || arr[first + i] != arr[third + i])
                {
                    return new int[] { -1, -1 };
                }

                i++;
            }

            return new int[] { first + len - 1, second + len };
        }

        return new[] { -1, -1 };
    }

    /// <summary>
    /// 934. Shortest Bridge
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int ShortestBridge(int[][] grid)
    {
        void DFS(int x, int y, int[][] grid, Queue<(int, int)> queue)
        {
            if (x < 0 || y < 0 || x >= grid.Length || y >= grid[0].Length || grid[x][y] != 1)
            {
                return;
            }
            queue.Enqueue((x, y));
            grid[x][y] = -1;
            DFS(x - 1, y, grid, queue);
            DFS(x + 1, y, grid, queue);
            DFS(x, y - 1, grid, queue);
            DFS(x, y + 1, grid, queue);
        }

        var n = grid.Length;
        int[][] dirs = { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (grid[i][j] == 1)
                {
                    var queue = new Queue<(int, int)>();
                    DFS(i, j, grid, queue);
                    var step = 0;
                    while (queue.Count > 0)
                    {
                        var size = queue.Count;
                        for (var k = 0; k < size; k++)
                        {
                            var cell = queue.Dequeue();
                            int x = cell.Item1, y = cell.Item2;
                            for (var d = 0; d < 4; d++)
                            {
                                var nx = x + dirs[d][0];
                                var ny = y + dirs[d][1];
                                if (nx >= 0 && ny >= 0 && nx < n && ny < n)
                                {
                                    if (grid[nx][ny] == 0)
                                    {
                                        queue.Enqueue((nx, ny));
                                        grid[nx][ny] = -1;
                                    }
                                    else if (grid[nx][ny] == 1)
                                    {
                                        return step;
                                    }
                                }
                            }
                        }
                        step++;
                    }
                }
            }
        }
        return 0;
    }

    /// <summary>
    /// 938. Range Sum of BST
    /// </summary>
    /// <param name="root"></param>
    /// <param name="L"></param>
    /// <param name="R"></param>
    /// <returns></returns>
    public int RangeSumBST(TreeNode root, int L, int R)
    {
        // Recursive
        //int sum = 0;
        //if (root == null) return sum;
        //if (root.val > L)
        //    sum += RangeSumBST(root.left, L, R);
        //if (root.val < R)
        //    sum += RangeSumBST(root.right, L, R);
        //if (root.val >= L && root.val <= R)
        //    sum += root.val;
        //return sum;

        // Iterative
        var sum = 0;
        var stack = new Stack<TreeNode>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            var currentNode = stack.Pop();
            if (currentNode == null)
            {
                continue;
            }

            if (L <= currentNode.val && currentNode.val <= R)
            {
                sum += currentNode.val;
            }

            if (L < currentNode.val)
            {
                stack.Push(currentNode.left);
            }

            if (currentNode.val < R)
            {
                stack.Push(currentNode.right);
            }
        }

        return sum;
    }

    public int DistinctSubseqII(string s)
    {
        var mod = 1000000007;
        var alphas = new int[26];
        int n = s.Length, res = 0;
        for (var i = 0; i < n; i++)
        {
            var index = s[i] - 'a';
            var prev = alphas[index];
            alphas[index] = (res + 1) % mod;
            res = ((res + alphas[index] - prev) % mod + mod) % mod;
        }

        return res;
    }

    /// <summary>
    /// 942. DI String Match
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int[] DIStringMatch(string s)
    {
        int n = s.Length, lo = 0, hi = n;
        var perm = new int[n + 1];
        for (var i = 0; i < n; i++)
        {
            perm[i] = s[i] == 'I' ? lo++ : hi--;
        }

        perm[n] = lo;
        return perm;
    }

    /// <summary>
    /// 944. Delete Columns to Make Sorted
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public int MinDeletionSize(string[] strs)
    {
        var row = strs.Length;
        var col = strs[0].Length;
        var ans = 0;
        for (var j = 0; j < col; j++)
        {
            for (var i = 1; i < row; i++)
            {
                if (strs[i - 1][j] > strs[i][j])
                {
                    ans++;
                    break;
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// 946. Validate Stack Sequences
    /// </summary>
    /// <param name="pushed"></param>
    /// <param name="popped"></param>
    /// <returns></returns>
    public bool ValidateStackSequences(int[] pushed, int[] popped)
    {
        var stack = new Stack<int>();
        var n = pushed.Length;
        for (int i = 0, j = 0; i < n; ++i)
        {
            stack.Push(pushed[i]);
            while (stack.Count > 0 && stack.Peek() == popped[j])
            {
                stack.Pop();
                ++j;
            }
        }

        return stack.Count == 0;
    }

    /// <summary>
    /// 953. Verifying an Alien Dictionary
    /// </summary>
    /// <param name="words"></param>
    /// <param name="order"></param>
    /// <returns></returns>
    public bool IsAlienSorted(string[] words, string order)
    {
        var index = new int[26];
        for (var i = 0; i < order.Length; ++i)
        {
            index[order[i] - 'a'] = i;
        }

        for (var i = 1; i < words.Length; ++i)
        {
            var valid = false;
            for (var j = 0; j < words[i - 1].Length && j < words[i].Length; ++j)
            {
                var prev = index[words[i - 1][j] - 'a'];
                var curr = index[words[i][j] - 'a'];
                if (prev < curr)
                {
                    valid = true;
                    break;
                }
                else if (prev > curr)
                {
                    return false;
                }
            }

            if (!valid)
            {
                if (words[i - 1].Length > words[i].Length)
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// 961. N-Repeated Element in Size 2N Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int RepeatedNTimes(int[] nums)
    {
        ISet<int> found = new HashSet<int>();
        foreach (var num in nums)
        {
            if (!found.Add(num))
            {
                return num;
            }
        }

        return -1;
    }

    /// <summary>
    /// 976. Largest Perimeter Triangle
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int LargestPerimeter(int[] nums)
    {
        Array.Sort(nums);
        for (var i = nums.Length - 1; i >= 2; --i)
        {
            if (nums[i - 2] + nums[i - 1] > nums[i])
            {
                return nums[i - 2] + nums[i - 1] + nums[i];
            }
        }

        return 0;
    }

    /// <summary>
    /// 977. Squares of a Sorted Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int[] SortedSquares(int[] nums)
    {
        var len = nums.Length;
        var ans = new int[len];
        for (int i = 0, j = len - 1, pos = len - 1; i <= j;)
        {
            if (nums[i] * nums[i] > nums[j] * nums[j])
            {
                ans[pos] = nums[i] * nums[i];
                ++i;
            }
            else
            {
                ans[pos] = nums[j] * nums[j];
                --j;
            }

            --pos;
        }

        return ans;
    }

    /// <summary>
    /// 994. Rotting Oranges
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int OrangeRotting(int[][] grid)
    {
        var dr = new int[] { -1, 0, 1, 0 };
        var dc = new int[] { 0, -1, 0, 1 };
        int rowLength = grid.Length, colLength = grid[0].Length;
        var queue = new Queue<int>();
        var depth = new Dictionary<int, int>();
        for (var row = 0; row < rowLength; ++row)
        {
            for (var col = 0; col < colLength; ++col)
            {
                if (grid[row][col] != 2)
                {
                    continue;
                }

                var code = row * colLength + col;
                queue.Enqueue(code);
                depth.Add(code, 0);
            }
        }

        var ans = 0;
        while (queue.Count != 0)
        {
            var code = queue.Dequeue();
            int row = code / colLength, col = code % colLength;
            for (var k = 0; k < 4; k++)
            {
                var nr = row + dr[k];
                var nc = col + dc[k];
                if (0 > nr || nr >= rowLength || 0 > nc || nc >= colLength || grid[nr][nc] != 1)
                {
                    continue;
                }

                grid[nr][nc] = 2;
                var nCode = nr * colLength + nc;
                queue.Enqueue(nCode);
                depth.Add(nCode, depth[code] + 1);
                ans = depth[nCode];
            }
        }

        if (grid.SelectMany(row => row).Any(v => v == 1))
        {
            return -1;
        }

        return ans;
    }

    /// <summary>
    /// 998. Maximum Binary Tree II
    /// </summary>
    /// <param name="root"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public TreeNode InsertIntoMaxTree(TreeNode root, int val)
    {
        TreeNode parent = null;
        var curr = root;
        while (curr != null)
        {
            if (val > curr.val)
            {
                if (parent is null)
                {
                    return new TreeNode(val, root, null);
                }

                var node = new TreeNode(val, curr, null);
                parent.right = node;
                return root;
            }
            else
            {
                parent = curr;
                curr = curr.right;
            }
        }

        parent.right = new TreeNode(val);
        return root;
    }

    /// <summary>
    /// 1022. Sum of Root To Leaf Binary Numbers
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public int SumRootToLeaf(TreeNode root)
    {
        int DFS(TreeNode node, int val)
        {
            if (node is null)
            {
                return 0;
            }

            val = (val << 1) | node.val;
            if (node.left is null && node.right is null)
            {
                return val;
            }

            return DFS(node.left, val) + DFS(node.right, val);
        }

        return DFS(root, 0);
    }

    /// <summary>
    /// 1025. Divisor Game
    /// </summary>
    /// <param name="N"></param>
    /// <returns></returns>
    public bool DivisorGame(int N)
    {
        return N % 2 == 0;
    }

    /// <summary>
    /// 1046. Last Stone Weight
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns>
    public int LastStoneWeight(List<int> stones)
    {
        var pq = new PriorityQueue<int>();
        foreach (var stone in stones)
        {
            pq.Push(stone);
        }

        while (pq.Count > 1)
        {
            var s1 = pq.Top();
            pq.Pop();
            var s2 = pq.Top();
            pq.Pop();
            if (s1 > s2)
            {
                pq.Push(s1 - s2);
            }
        }

        return pq.IsEmpty() ? 0 : pq.Top();
    }

    /// <summary>
    /// 1091. Shortest Path in Binary Matrix
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public int ShortestPathBinaryMatrix(int[][] grid)
    {
        if (grid[0][0] == 1)
        {
            return -1;
        }

        var n = grid.Length;
        var dist = new int[n][];
        for (var i = 0; i < n; i++)
        {
            dist[i] = new int[n];
            Array.Fill(dist[i], int.MaxValue);
        }

        var queue = new Queue<(int, int)>();
        queue.Enqueue((0, 0));
        dist[0][0] = 1;
        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            if (x == n - 1 && y == n - 1)
            {
                return dist[x][y];
            }

            for (var dx = -1; dx < 2; dx++)
            {
                for (var dy = -1; dy < 2; dy++)
                {
                    if (x + dx < 0 || x + dx >= n || y + dy < 0 || y + dy >= n)
                    {
                        continue;
                    }

                    if (grid[x + dx][y + dy] == 1 || dist[x + dx][y + dy] <= dist[x][y] + 1)
                    {
                        continue;
                    }

                    dist[x + dx][y + dy] = dist[x][y] + 1;
                    queue.Enqueue((x + dx, y + dy));
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// 1235. Maximum Profit in Job Scheduling
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="profit"></param>
    /// <returns></returns>
    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int BinarySearch(int[][] jobs, int right, int target)
        {
            var left = 0;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                if (jobs[mid][1] > target)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return left;
        }

        var n = startTime.Length;
        var jobs = new int[n][];
        for (var i = 0; i < n; i++)
        {
            jobs[i] = new int[] { startTime[i], endTime[i], profit[i] };
        }

        Array.Sort(jobs, (a, b) => a[1] - b[1]);
        var dp = new int[n + 1];
        for (var i = 1; i <= n; i++)
        {
            var k = BinarySearch(jobs, i - 1, jobs[i - 1][0]);
            dp[i] = Math.Max(dp[i - 1], dp[k] + jobs[i - 1][2]);
        }

        return dp[n];
    }

    /// <summary>
    /// 1249
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string MinRemoveToMakeValid(string s)
    {
        var sb = new StringBuilder();
        var openSeen = 0;
        var balance = 0;
        foreach (var c in s)
        {
            switch (c)
            {
                case '(':
                    openSeen++;
                    balance++;
                    break;
                case ')' when balance == 0:
                    continue;
                case ')':
                    balance--;
                    break;
            }

            sb.Append(c);
        }

        var res = new StringBuilder();
        var openToKeep = openSeen - balance;
        for (var i = 0; i < sb.Length; i++)
        {
            var c = sb[i];
            if (c == '(')
            {
                openToKeep--;
                if (openToKeep < 0)
                {
                    continue;
                }
            }

            res.Append(c);
        }

        return res.ToString();
    }

    /// <summary>
    /// 1260. Shift 2D Grid
    /// </summary>
    public IList<IList<int>> ShiftGrid(int[][] grid, int k)
    {
        int n = grid.Length, m = grid[0].Length;
        // use array 
        IList<IList<int>> res = new int[n][];
        for (var r = 0; r < n; r++)
        {
            res[r] = new int[m];
        }

        // use list
        // IList<IList<int>> res = new IList<List<int>>();
        // for (int i = 0; i < n; i++)
        // {
        //     List<int> tmp = new List<int>();
        //     for (int j = 0; j < m; j++)    
        //         tmp.Add(0);
        //     res.Add(tmp);
        // }

        k %= m * n;
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < m; j++)
            {
                var index = (i * m + j + k) % (m * n);
                int x = index / m, y = index % m;
                res[x][y] = grid[i][j];
            }
        }

        return res;
    }

    /// <summary>
    /// 1305. All Elements in Two Binary Search Trees
    /// </summary>
    /// <param name="root1"></param>
    /// <param name="root2"></param>
    /// <returns></returns>
    public IList<int> GetAllElements(TreeNode root1, TreeNode root2)
    {
        void Inorder(TreeNode node, IList<int> res)
        {
            if (node != null)
            {
                Inorder(node.left, res);
                res.Add((node.val));
                Inorder(node.right, res);
            }
        }

        IList<int> nums1 = new List<int>();
        IList<int> nums2 = new List<int>();
        Inorder(root1, nums1);
        Inorder(root2, nums2);
        IList<int> merged = new List<int>();
        int p1 = 0, p2 = 0;
        while (true)
        {
            if (p1 == nums1.Count)
            {
                while (p2 < nums2.Count)
                {
                    merged.Add(nums2[p2++]);
                }

                break;
            }

            if (p2 == nums1.Count)
            {
                while (p1 < nums1.Count)
                {
                    merged.Add(nums1[p1++]);
                }

                break;
            }

            merged.Add(nums1[p1] < nums2[p2] ? nums1[p1++] : nums2[p2++]);
        }

        return merged;
    }

    /// <summary>
    /// 1374. Generate a String With Characters That Have Odd Counts
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public string GenerateTheString(int n)
    {
        if (n % 2 == 0)
        {
            return new string('a', n - 1) + "b";
        }

        return new string('a', n);
    }

    /// <summary>
    /// 1380. Lucky Numbers in a Matrix
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public IList<int> LuckyNumbers(int[][] matrix)
    {
        int m = matrix.Length, n = matrix[0].Length;
        var minRow = new int[m];
        Array.Fill(minRow, int.MaxValue);
        var maxCol = new int[n];
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                minRow[i] = Math.Min(minRow[i], matrix[i][j]);
                maxCol[j] = Math.Max(maxCol[j], matrix[i][j]);
            }
        }

        IList<int> res = new List<int>();
        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (matrix[i][j] == minRow[i] && matrix[i][j] == maxCol[j])
                {
                    res.Add(matrix[i][j]);
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 1403. Minimum Subsequence in Non-Increasing Order
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public IList<int> MinSubsequence(int[] nums)
    {
        Array.Sort(nums);
        var total = nums.Sum();
        IList<int> ans = new List<int>();
        var curr = 0;
        for (var i = nums.Length - 1; i >= 0; --i)
        {
            curr += nums[i];
            ans.Add(nums[i]);
            if (total - curr < curr)
            {
                break;
            }
        }

        return ans;
    }

    /// <summary>
    /// 1408. String Matching in an Array
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public IList<string> StringMatching(string[] words)
    {
        IList<string> res = new List<string>();
        for (var i = 0; i < words.Length; i++)
        {
            for (var j = 0; j < words.Length; j++)
            {
                if (i != j && words[j].Contains(words[i]))
                {
                    res.Add(words[i]);
                    break;
                }
            }
        }

        return res;
    }

    /// <summary>
    /// 1417. Reformat The String
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string Reformat(string s)
    {
        var sumDigit = 0;
        foreach (var c in s)
        {
            if (char.IsDigit(c))
            {
                sumDigit++;
            }
        }

        var sumAlpha = s.Length - sumDigit;
        if (Math.Abs(sumDigit - sumAlpha) > 1)
        {
            return string.Empty;
        }

        var flag = sumDigit > sumAlpha;
        var arr = s.ToCharArray();
        for (int i = 0, j = 1; i < arr.Length; i += 2)
        {
            if (char.IsDigit(arr[i]) != flag)
            {
                while (char.IsDigit(arr[j]) != flag)
                {
                    j += 2;
                }

                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }

        return new string(arr);
    }

    /// <summary>
    /// 1441. Build an Array With Stack Operations
    /// </summary>
    /// <param name="target"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> BuildArray(int[] target, int n)
    {
        IList<string> res = new List<string>();
        var prev = 0;
        foreach (var number in target)
        {
            for (var i = 0; i < number - prev - 1; i++)
            {
                res.Add("Push");
                res.Add("Pop");
            }
            res.Add("Push");
            prev = number;
        }

        return res;
    }

    /// <summary>
    /// 1447. Simplified Fractions
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public IList<string> SimplifiedFractions(int n)
    {
        IList<string> res = new List<string>();
        for (var denominator = 2; denominator <= n; ++denominator)
        {
            for (var numerator = 1; numerator < denominator; ++numerator)
            {
                if (Gcd(numerator, denominator) == 1)
                {
                    res.Add(numerator + "/" + denominator);
                }
            }
        }

        return res;

        int Gcd(int a, int b)
        {
            return b != 0 ? Gcd(b, a % b) : a;
        }
    }

    /// <summary>
    /// 1455. Check If a Word Occurs As a Prefix of Any Word in a Sentence
    /// </summary>
    /// <param name="sentence"></param>
    /// <param name="searchWord"></param>
    /// <returns></returns>
    public int IsPrefixOfWord(string sentence, string searchWord)
    {
        bool IsPrefix(string input, int start, int end, string target)
        {
            for (var i = 0; i < target.Length; i++)
            {
                if (start + i >= end || input[start + i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }

        int n = sentence.Length, index = 1, start = 0, end = 0;
        while (start < n)
        {
            while (end < n && sentence[end] != ' ')
            {
                end++;
            }

            if (IsPrefix(sentence, start, end, searchWord))
            {
                return index;
            }

            index++;
            end++;
            start = end;
        }

        return -1;
    }

    /// <summary>
    /// 1470. Shuffle the Array
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public int[] Shuffle(int[] nums, int n)
    {
        var res = new int[n * 2];
        for (var i = 0; i < n; ++i)
        {
            res[2 * i] = nums[i];
            res[2 * i + 1] = nums[i + n];
        }

        return res;
    }

    /// <summary>
    /// 1475. Final Prices With a Special Discount in a Shop
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public int[] FinalPrices(int[] prices)
    {
        var n = prices.Length;
        var res = new int[n];
        var stack = new Stack<int>();
        for (var i = n - 1; i >= 0; --i)
        {
            while (stack.Count > 0 && stack.Peek() > prices[i])
            {
                stack.Pop();
            }

            res[i] = stack.Count == 0 ? prices[i] : prices[i] - stack.Peek();
            stack.Push(prices[i]);
        }

        return res;
    }

    /// <summary>
    /// 1486. XOR Operation in an Array.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    public int XorOperation(int n, int start)
    {
        // (sumXor(s - 1) ^ sumXor(s + n - 1)) * 2 + e; s = start/2, e = (0 || 1)
        var s = start >> 1;
        var e = n & start & 1;
        var res = SumXor(s - 1) ^ SumXor(s + n - 1);
        return res << 1 | e;

        int SumXor(int num)
        {
            return (num % 4) switch
            {
                0 => num,
                1 => 1,
                2 => num + 1,
                _ => 0
            };
        }
    }

    /// <summary>
    /// 1491. Average Salary Excluding the Minimum and Maximum Salary
    /// </summary>
    /// <param name="salary"></param>
    /// <returns></returns>
    public double Average(int[] salary)
    {
        int min = int.MaxValue, max = int.MinValue;
        var sum = 0;
        foreach (var item in salary)
        {
            sum += item;
            max = Math.Max(max, item);
            min = Math.Min(min, item);
        }

        return (sum - max - min) / (salary.Length - 2);
    }

    /// <summary>
    /// 1523. Count Odd Numbers in an Interval Range
    /// </summary>
    /// <param name="low"></param>
    /// <param name="high"></param>
    /// <returns></returns>
    public int CountOdds(int low, int high)
    {
        int PreSum(int num)
        {
            return (num + 1) >> 1;
        }

        return PreSum(high) - PreSum(low - 1);
    }

    /// <summary>
    /// 1582. Special Positions in a Binary Matrix
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public int NumSpecial(int[][] mat)
    {
        int row = mat.Length, col = mat[0].Length;
        for (var i = 0; i < row; i++)
        {
            var count = 0;
            for (var j = 0; j < col; j++)
            {
                if (mat[i][j] == 1)
                {
                    count++;
                }
            }

            if (i == 0)
            {
                count--;
            }

            if (count > 0)
            {
                for (var j = 0; j < col; j++)
                {
                    if (mat[i][j] == 1)
                    {
                        mat[0][j] += count;
                    }
                }
            }
        }

        var sum = 0;
        foreach (var item in mat[0])
        {
            if (item == 1)
            {
                sum++;
            }
        }

        return sum;
    }

    /// <summary>
    /// 1592. Rearrange Spaces Between Words
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string ReorderSpaces(string text)
    {
        var n = text.Length;
        var whitespceCount = n;
        var words = text.Trim().Split(" ");
        var wordCount = 0;
        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                whitespceCount -= word.Length;
                wordCount++;
            }
        }

        var sb = new StringBuilder();
        if (words.Length == 1)
        {
            sb.Append(words[0]);
            for (var i = 0; i < whitespceCount; ++i)
            {
                sb.Append(' ');
            }

            return sb.ToString();
        }

        var spacePerCount = whitespceCount / (wordCount - 1);
        var restSpaceCount = whitespceCount % (wordCount - 1);
        for (var i = 0; i < words.Length; i++)
        {
            if (words[i].Length == 0)
            {
                continue;
            }

            if (sb.Length > 0)
            {
                for (var j = 0; j < spacePerCount; ++j)
                {
                    sb.Append(' ');
                }
            }

            sb.Append(words[i]);
        }

        for (var i = 0; i < restSpaceCount; ++i)
        {
            sb.Append(' ');
        }

        return sb.ToString();
    }

    /// <summary>
    /// 1598. Crawler Log Folder
    /// </summary>
    /// <param name="logs"></param>
    /// <returns></returns>
    public int MinOperations(string[] logs)
    {
        var depth = 0;
        foreach (var log in logs)
        {
            if (log == "./")
            {
                continue;
            }
            else if (log == "../")
            {
                if (depth > 0)
                {
                    depth--;
                }
            }
            else
            {
                depth++;
            }
        }

        return depth;
    }

    /// <summary>
    /// 1608. Special Array With X Elements Greater Than or Equal X
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int SpecialArray(int[] nums)
    {
        Array.Sort(nums, (a, b) => b - a);
        var n = nums.Length;
        for (var i = 1; i <= n; i++)
        {
            if (nums[i - 1] >= i && (i == n || nums[i] < i))
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 1619. Mean of Array After Removing Some Elements
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public double TrimMean(int[] arr)
    {
        var n = arr.Length;
        Array.Sort(arr);
        var sum = 0;
        for (var i = n / 20; i < 19 * n / 20; i++)
        {
            sum += arr[i];
        }

        return sum / (0.9 * n);
    }

    /// <summary>
    /// 1624. Largest Substring Between Two Equal Characters
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int MaxLengthBetweenEqualCharacters(string s)
    {
        var n = s.Length;
        var dic = new Dictionary<char, int>();
        var res = -1;
        for (var i = 0; i < n; i++)
        {
            if (!dic.ContainsKey(s[i]))
            {
                dic.Add(s[i], i);
            }
            else
            {
                res = Math.Max(res, i - dic[s[i]] - 1);
            }
        }

        return res;
    }

    /// <summary>
    /// 1636. Sort Array by Increasing Frequency
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int[] FrequencySort(int[] nums)
    {
        var count = new Dictionary<int, int>();
        foreach (var num in nums)
        {
            count.TryAdd(num, 0);
            count[num]++;
        }

        Array.Sort(nums, (a, b) =>
        {
            int count1 = count[a], count2 = count[b];
            return count1 != count2 ? count1 - count2 : b - a;
        });
        return nums;
    }

    /// <summary>
    /// 1658. Minimum Operations to Reduce X to Zero
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public int MinOperations(int[] nums, int x)
    {
        var n = nums.Length;
        var sum = nums.Sum();

        if (sum < x)
        {
            return -1;
        }

        var right = 0;
        int leftSum = 0, rightSum = sum;
        var res = n + 1;

        for (var left = -1; left < n; ++left)
        {
            if (left != -1)
            {
                leftSum += nums[left];
            }
            while (right < n && leftSum + rightSum > x)
            {
                rightSum -= nums[right];
                ++right;
            }
            if (leftSum + rightSum == x)
            {
                res = Math.Min(res, (left + 1) + (n - right));
            }
        }

        return res > n ? -1 : res;
    }

    /// <summary>
    /// 1672. Richest Customer Wealth
    /// </summary>
    /// <param name="accounts"></param>
    /// <returns></returns>
    public int MaximumWealth(int[][] accounts)
    {
        return accounts.Max(x => x.Sum());
    }

    /// <summary>
    /// 1694. Reformat Phone Number
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public string ReformatNumber(string number)
    {
        var sb = new StringBuilder();
        foreach (var ch in number)
        {
            if (char.IsDigit(ch))
            {
                sb.Append(ch);
            }
        }

        var digits = sb.ToString();
        var n = digits.Length;
        var pt = 0;
        var res = new StringBuilder();
        while (n > 0)
        {
            if (n > 4)
            {
                res.Append(digits.Substring(pt, 3) + '-');
                pt += 3;
                n -= 3;
            }
            else
            {
                if (n == 4)
                {
                    res.Append(digits.Substring(pt, 2) + "-" + digits.Substring(pt + 2, 2));
                }
                else
                {
                    res.Append(digits.Substring(pt, n));
                }

                break;
            }
        }

        return res.ToString();
    }

    /// <summary>
    /// 1700. Number of Students Unable to Eat Lunch
    /// </summary>
    /// <param name="students"></param>
    /// <param name="sandwiches"></param>
    /// <returns></returns>
    public int CountStudents(int[] students, int[] sandwiches)
    {
        var square = students.Sum();
        var circular = students.Length - square;
        for (var i = 0; i < sandwiches.Length; i++)
        {
            if (sandwiches[i] == 0 && circular > 0)
            {
                circular--;
            }
            else if (sandwiches[i] == 1 && square > 0)
            {
                square--;
            }
            else
            {
                break;
            }
        }
        return square + circular;
    }

    /// <summary>
    /// 1750. Minimum Length of String After Deleting Similar Ends
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int MinimumLength(string s)
    {
        var n = s.Length;
        int left = 0, right = n - 1;
        while (left < right && s[left] == s[right])
        {
            var c = s[left];
            while (left <= right && s[left] == c)
            {
                left++;
            }
            while (left <= right && s[right] == c)
            {
                right--;
            }
        }
        return right - left + 1;
    }

    /// <summary>
    /// 1768. Merge Strings Alternately
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns></returns>
    public string MergeAlternately(string word1, string word2)
    {
        int m = word1.Length, n = word2.Length;
        int i = 0, j = 0;
        var sb = new StringBuilder();
        while (i < m || j < n)
        {
            if (i < m)
            {
                sb.Append(word1[i++]);
            }

            if (j < n)
            {
                sb.Append(word2[j++]);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// 1779. Find Nearest Point That Has the Same X or Y Coordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="points"></param>
    /// <returns></returns>
    public int NearestValidPoint(int x, int y, int[][] points)
    {
        var minDistance = int.MaxValue;
        var ans = -1;
        for (var i = 0; i < points.Length; i++)
        {
            if (points[i][0] == x || points[i][1] == y)
            {
                var distance = Math.Abs(points[i][0] - x) + Math.Abs(points[i][1] - y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    ans = i;
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// 1784. Check if Binary String Has at Most One Segment of Ones
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool CheckOnesSegment(string s) => !s.Contains("01");

    /// <summary>
    /// 1790. Check if One String Swap Can Make Strings Equal
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public bool AreAlmostEqual(string s1, string s2)
    {
        var n = s1.Length;
        IList<int> diff = new List<int>();
        for (var i = 0; i < n; i++)
        {
            if (s1[i] != s2[i])
            {
                if (diff.Count >= 2)
                {
                    return false;
                }
                diff.Add(i);
            }
        }

        if (diff.Count == 0)
        {
            return true;
        }

        if (diff.Count != 2)
        {
            return false;
        }

        return s1[diff[0]] == s2[diff[1]] && s1[diff[1]] == s2[diff[0]];
    }

    /// <summary>
    /// 1791. Find Center of Star Graph
    /// </summary>
    /// <param name="edges"></param>
    /// <returns></returns>
    public int FindCenter(int[][] edges)
    {
        return edges[0][0] == edges[1][0] || edges[0][0] == edges[1][1] ? edges[0][0] : edges[0][1];
    }

    /// <summary>
    /// 1800. Maximum Ascending Subarray Sum
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaxAscendingSum(int[] nums)
    {
        var res = 0;
        var i = 0;
        var n = nums.Length;
        while (i < n)
        {
            var currSum = nums[i++];
            while (i < n && nums[i] > nums[i - 1])
            {
                currSum += nums[i++];
            }

            res = Math.Max(res, currSum);
        }

        return res;
    }

    /// <summary>
    /// 1802. Maximum Value at a Given Index in a Bounded Array
    /// </summary>
    /// <param name="n"></param>
    /// <param name="index"></param>
    /// <param name="maxSum"></param>
    /// <returns></returns>
    public int MaxValue(int n, int index, int maxSum)
    {
        double left = index;
        double right = n - index - 1;
        if (left > right)
        {
            var temp = left;
            left = right;
            right = temp;
        }

        var upper = ((left + 1) * (left + 1) - 3 * (left + 1)) / 2 + left + 1 + (left + 1) + ((left + 1) * (left + 1) - 3 * (left + 1)) / 2 + right + 1;
        if (upper >= maxSum)
        {
            double a = 1;
            double b = -2;
            var c = left + right + 2 - maxSum;
            return (int)Math.Floor((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
        }

        upper = (2 * (right + 1) - left - 1) * left / 2 + (right + 1) + ((right + 1) * (right + 1) - 3 * (right + 1)) / 2 + right + 1;
        if (upper >= maxSum)
        {
            var a = 1.0 / 2;
            var b = left + 1 - 3.0 / 2;
            var c = right + 1 + (-left - 1) * left / 2 - maxSum;
            return (int)Math.Floor((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
        }
        else
        {
            var a = left + right + 1; ;
            var b = (-left * left - left - right * right - right) / 2 - maxSum;
            return (int)Math.Floor(-b / a);
        }
    }

    /// <summary>
    /// 1823. Find the Winner of the Circular Game
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int FindTheWinner(int n, int k)
    {
        var winner = 1;
        for (var i = 2; i <= n; i++)
        {
            winner = (k + winner - 1) % i + 1;
        }

        return winner;
    }

    /// <summary>
    /// 1827. Minimum Operations to Make the Array Increasing
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MinOperations(int[] nums)
    {
        var prev = nums[0] - 1;
        var res = 0;
        foreach (var num in nums)
        {
            prev = Math.Max(prev + 1, num);
            res += prev - num;
        }

        return res;
    }

    /// <summary>
    /// 1945. Sum of Digits of String After Convert
    /// </summary>
    /// <param name="s"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int GetLucky(string s, int k)
    {
        var sb = new StringBuilder();
        foreach (var ch in s)
        {
            sb.Append(ch - 'a' + 1);
        }

        var digits = sb.ToString();
        for (var i = 1; i <= k && digits.Length > 1; i++)
        {
            var sum = 0;
            foreach (var ch in digits)
            {
                sum += ch - '0';
            }

            digits = sum.ToString();
        }

        return int.Parse(digits);
    }

    /// <summary>
    /// 1984. Minimum Difference Between Highest and Lowest of K Scores
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int MinimumDifference(int[] nums, int k)
    {
        var n = nums.Length;
        Array.Sort(nums);
        var res = int.MaxValue;
        for (var i = 0; i + k - 1 < n; ++i)
        {
            res = Math.Min(res, nums[i + k - 1] - nums[i]);
        }

        return res;
    }

    /// <summary>
    /// 1991. Find the Middle Index in Array
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int FindMiddleIndex(int[] nums)
    {
        var total = nums.Sum();
        var sum = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            if ((2 * sum + nums[i]) == total)
            {
                return i;
            }

            sum += nums[i];
        }

        return -1;
    }

    /// <summary>
    /// 1995. Count Special Quadruplets 
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int CountQuadruplets(int[] nums)
    {
        var n = nums.Length;
        var ans = 0;
        var cnt = new Dictionary<int, int>();
        for (var b = n - 3; b >= 1; --b)
        {
            for (var d = b + 2; d < n; ++d)
            {
                var difference = nums[d] - nums[b + 1];
                if (!cnt.ContainsKey(difference))
                {
                    cnt.Add(difference, 1);
                }
                else
                {
                    ++cnt[difference];
                }
            }

            for (var a = 0; a < b; ++a)
            {
                var sum = nums[a] + nums[b];
                if (cnt.ContainsKey(sum))
                {
                    ans += cnt[sum];
                }
            }
        }

        return ans;
    }

    /// <summary>
    /// 2006. Count Number of Pairs With Absolute Difference K
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int CountKDifference(int[] nums, int k)
    {
        int ans = 0, n = nums.Length;
        var cnt = new Dictionary<int, int>();
        for (var j = 0; j < n; j++)
        {
            ans += (cnt.ContainsKey(nums[j] - k) ? cnt[nums[j] - k] : 0)
                   + (cnt.ContainsKey(nums[j] + k) ? cnt[nums[j] + k] : 0);
            if (!cnt.ContainsKey(nums[j]))
            {
                cnt.Add(nums[j], 0);
            }

            ++cnt[nums[j]];
        }

        return ans;
    }

    /// <summary>
    /// 2011. Final Value of Variable After Performing Operations
    /// </summary>
    /// <param name="operations"></param>
    /// <returns></returns>
    public int FinalValueAfterOperations(string[] operations)
    {
        return operations.Select(op => op[1] == '+' ? 1 : -1).Sum();
    }

    /// <summary>
    /// 2016. Maximum Difference Between Increasing Elements
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int MaximumDifference(int[] nums)
    {
        var n = nums.Length;
        int ans = -1, preMin = nums[0];
        for (var i = 1; i < n; i++)
        {
            if (nums[i] > preMin)
            {
                ans = Math.Max(ans, nums[i] - preMin);
            }
            else
            {
                preMin = nums[i];
            }
        }

        return ans;
    }

    /// <summary>
    /// 2027. Minimum Moves to Convert String
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int MinimumMoves(string s)
    {
        var res = 0;
        var count = -1;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == 'X' && i > count)
            {
                res++;
                count = i + 2;
            }
        }
        return res;
    }

    /// <summary>
    /// 2032. Two Out of Three
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="nums3"></param>
    /// <returns></returns>
    public IList<int> TwoOutOfThree(int[] nums1, int[] nums2, int[] nums3)
    {
        Dictionary<int, int> dic = new();
        foreach (var num in nums1)
        {
            dic.TryAdd(num, 1);
        }

        foreach (var num in nums2)
        {
            dic.TryAdd(num, 0);
            dic[num] |= 2;
        }

        foreach (var num in nums3)
        {
            dic.TryAdd(num, 0);
            dic[num] |= 4;
        }

        IList<int> res = new List<int>();
        foreach (var pair in dic)
        {
            int k = pair.Key, v = pair.Value;
            if ((v & (v - 1)) != 0)
            {
                res.Add(k);
            }
        }

        return res;
    }

    /// <summary>
    /// 2037. Minimum Number of Moves to Seat Everyone
    /// </summary>
    /// <param name="seats"></param>
    /// <param name="students"></param>
    /// <returns></returns>
    public int MinMovesToSeat(int[] seats, int[] students)
    {
        Array.Sort(seats);
        Array.Sort(students);
        return seats.Select((t, i) => Math.Abs(t - students[i])).Sum();
    }

    /// <summary>
    /// 2042. Check if Numbers Are Ascending in a Sentence
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public bool AreNumberAscending(string s)
    {
        int prev = 0, pos = 0;
        while (pos < s.Length)
        {
            if (char.IsDigit(s[pos]))
            {
                var curr = 0;
                while (pos < s.Length && char.IsDigit(s[pos]))
                {
                    curr = curr * 10 + s[pos] - '0';
                    pos++;
                }
                if (curr <= prev)
                {
                    return false;
                }
                prev = curr;
            }
            else
            {
                pos++;
            }
        }
        return true;
    }

    /// <summary>
    /// 2044. Count Number of Maximum Bitwise-OR Subsets
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public int CountMaxOrSubsets(int[] nums)
    {
        int maxOr = 0, cnt = 0;
        Dfs(0, 0);
        return cnt;

        void Dfs(int pos, int orVal)
        {
            if (pos == nums.Length)
            {
                if (orVal > maxOr)
                {
                    maxOr = orVal;
                    cnt = 1;
                }
                else if (orVal == maxOr)
                {
                    cnt++;
                }

                return;
            }

            Dfs(pos + 1, orVal | nums[pos]);
            Dfs(pos + 1, orVal);
        }
    }

    /// <summary>
    /// 2055. Plates Between Candles
    /// </summary>
    /// <param name="s"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public int[] PlatesBetweenCandles(string s, int[][] queries)
    {
        var n = s.Length;
        var preSum = new int[n];
        var sum = 0;
        for (var i = 0; i < n; i++)
        {
            if (s[i] == '*')
            {
                sum++;
            }

            preSum[i] = sum;
        }

        var left = new int[n];
        for (int i = 0, l = -1; i < n; i++)
        {
            if (s[i] == '|')
            {
                l = i;
            }

            left[i] = l;
        }

        var right = new int[n];
        for (int i = n - 1, r = -1; i >= 0; i--)
        {
            if (s[i] == '|')
            {
                r = i;
            }

            right[i] = r;
        }

        var ans = new int[queries.Length];
        for (var i = 0; i < queries.Length; i++)
        {
            var query = queries[i];
            int x = right[query[0]], y = left[query[1]];
            ans[i] = x == -1 || y == -1 || x >= y ? 0 : preSum[y] - preSum[x];
        }

        return ans;
    }

    /// <summary>
    /// 2180. Count Integers With Even Digit Sum
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int CountEven(int num)
    {
        int y = num / 10, x = num % 10;
        int res = y * 5, ySum = 0;
        while (y > 0)
        {
            ySum += y % 10;
            y /= 10;
        }

        if (ySum % 2 == 0)
        {
            res += x / 2 + 1;
        }
        else
        {
            res += (x + 1) / 2;
        }

        return res - 1;
    }

    public int PrefixCount(string[] words, string pref)
    {
        return words.Where(x => x.StartsWith(pref)).Count();
    }

    /// <summary>
    /// 2351. First Letter to Appear Twice
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public char RepeatedCharacter(string s)
    {
        var map = new int[26];
        foreach (var c in s)
        {
            var index = c - 'a';
            if (map[index] > 0)
            {
                return c;
            }
            map[index]++;
        }

        return ' ';
    }

    /// <summary>
    /// 6078. Rearrange Characters to Make Target String
    /// </summary>
    /// <param name="s"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int RearrageCharacters(string s, string target)
    {
        var sDic = new Dictionary<char, int>();
        var targetDic = new Dictionary<char, int>();
        foreach (var ch in s)
        {
            if (sDic.ContainsKey(ch))
            {
                sDic[ch]++;
            }
            else
            {
                sDic.Add(ch, 1);
            }
        }

        foreach (var ch in target)
        {
            if (targetDic.ContainsKey(ch))
            {
                targetDic[ch]++;
            }
            else
            {
                targetDic.Add(ch, 1);
            }
        }

        var ans = int.MaxValue;
        foreach (var ch in target)
        {
            var sValue = 0;
            sDic.TryGetValue(ch, out sValue);
            ans = Math.Min(ans, sValue / targetDic[ch]);
        }

        return ans;
    }
}