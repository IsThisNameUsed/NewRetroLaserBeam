using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LaserBehaviour))]
public class LaserBehaviourEditor : Editor {
    private LaserBehaviour laserBehaviour;
    private bool foldState;
    private void OnEnable()
    {
        laserBehaviour = (LaserBehaviour)target;

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        foldState = EditorGUILayout.Foldout(foldState, "Debug Functions");
        if (foldState)
        {
            EditorGUI.indentLevel += 2;
            if (GUILayout.Button("Take Damage()"))
            {
                laserBehaviour.DebugTakeDamage();
            }
            EditorGUI.indentLevel -= 2;
        }
        serializedObject.ApplyModifiedProperties();


    }

}
