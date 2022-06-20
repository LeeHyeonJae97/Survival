//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(UITweenPosition))]
//public class UITweenPositionEditor : Editor
//{
//    private SerializedProperty _start;
//    private SerializedProperty _end;
//    private bool _editStart;
//    private bool _editEnd;
//    private Vector2 _org;

//    private void OnEnable()
//    {
//        _start = serializedObject.FindProperty("_start");
//        _end = serializedObject.FindProperty("_end");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();

//        EditorGUILayout.PropertyField(_start);
//        GUI.enabled = !_editEnd;
//        _editStart = EditorGUILayout.Toggle("Start", _editStart);
//        GUI.enabled = true;

//        EditorGUILayout.PropertyField(_end);
//        GUI.enabled = !_editStart;
//        _editEnd = EditorGUILayout.Toggle("End", _editEnd);
//        GUI.enabled = true;

//        Vector2 pos = ((RectTransform)target).anchoredPosition;
//        ((RectTransform)target).

//        if (_editStart)
//        {
//            _start.vector2Value = pos;
//        }
//        else if (_editEnd)
//        {
//            pos = (())
//        }

//        serializedObject.ApplyModifiedProperties();
//    }

//    private void OnSceneGUI()
//    {

//    }
//}
