//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(Skill))]
//public class SkillEditor : Editor
//{
//    private SerializedProperty _propertyProp;
//    private SerializedProperty _invocationProp;
//    private SerializedProperty _targetingProp;
//    private SerializedProperty _projectionProp;
//    private SerializedProperty _hitProp;

//    private void OnEnable()
//    {
//        _propertyProp = serializedObject.FindProperty("_property");
//        _invocationProp = serializedObject.FindProperty("_invocation");
//        _targetingProp = serializedObject.FindProperty("_targeting");
//        _projectionProp = serializedObject.FindProperty("_projection");
//        _hitProp = serializedObject.FindProperty("_hit");
//    }

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        GUI.enabled = false;

//        if (_propertyProp.objectReferenceValue is SkillProperty property && property != null)
//            EditorGUILayout.EnumFlagsField("Property Type", property.Type);
//        if (_invocationProp.objectReferenceValue is SkillInvocation invocation && invocation != null)
//            EditorGUILayout.EnumFlagsField("Invocation Type", invocation.Type);
//        if (_targetingProp.objectReferenceValue is SkillTargeting targeting && targeting != null)
//            EditorGUILayout.EnumFlagsField("Targeting Type", targeting.Type);
//        if (_projectionProp.objectReferenceValue is SkillProjection projection && projection != null)
//            EditorGUILayout.EnumFlagsField("Projection Type", projection.Type);
//        if (_hitProp.objectReferenceValue is SkillHit hit && hit != null)
//            EditorGUILayout.EnumFlagsField("Hit Type", hit.Type);

//        GUI.enabled = true;
//    }
//}
