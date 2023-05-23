using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static int[] GetDistinctRandomNumbers(int minVal, int maxVal, int k)
    {
        if (minVal >= maxVal) throw new Exception("minVal must be smaller than maxVal");
        if (k > maxVal - minVal) throw new Exception("k must be smaller than the length of the range");
        List<int> nums = new List<int>();
        int[] ret = new int[k];

        for (int i = minVal; i < maxVal; i++) nums.Add(i);
        for (int i = 0; i < k; i++)
        {
            UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
            int curIdx = UnityEngine.Random.Range(0, nums.Count);
            ret[i] = nums[curIdx];

            nums.RemoveAt(curIdx);
        }

        return ret;
    }
}
