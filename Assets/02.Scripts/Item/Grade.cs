
using UnityEngine;

public enum GradeType { Normal, Rare, Unique, Legendary }

public class Grade
{
    public static readonly int[] Percentages = { 100, 50, 20, 5 };
    public static readonly Color[] Colors = { Color.white, Color.blue, Color.magenta, Color.yellow };
}