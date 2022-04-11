//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WaveFactory
//{
//    public static int Count => List.Count;

//    private static List<WaveSO> List
//    {
//        get
//        {
//            if (_list == null) _list = new List<WaveSO>(Resources.LoadAll<WaveSO>("Wave/Series"));
//            return _list;
//        }
//    }
//    public static WaveSO Random
//    {
//        get
//        {
//            if (_random == null) _random = Resources.Load<WaveSO>("Wave/Random");
//            return _random;
//        }
//    }

//    private static List<WaveSO> _list;
//    private static WaveSO _random;

//    public static WaveSO Get(int idx)
//    {
//        return idx < Count ? List[idx] : null;
//    }
//}
