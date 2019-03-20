using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static string VisualArray<T> (T[] array)
    {
        string visual = "";
        foreach (T element in array)
            visual += element + " : ";

        return visual;
    }

    public static class ExecutionTime
    {
        private static DateTime origin;

        public static void Set()
        {
            origin = DateTime.Now;
        }

        public static void PrintExecutionTime()
        {
            TimeSpan deltaTime = DateTime.Now.Subtract(origin);
            MonoBehaviour.print(string.Format("Durée: {0} min {1} s {2} ms", deltaTime.Minutes, deltaTime.Seconds, deltaTime.Milliseconds));
        }
    }
}
