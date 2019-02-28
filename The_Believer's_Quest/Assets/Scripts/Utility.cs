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
}
