using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor {
    private Player player;
    private bool foldState;
    private void OnEnable()
    {
        player = (Player)target;

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        EditorGUILayout.Separator();
        foldState = EditorGUILayout.Foldout(foldState, "Debug Functions");
        
        if (foldState)
        {
            EditorGUI.indentLevel += 2;
            if (GUILayout.Button("Take Damage()"))
            {
                player.DebugTakeDamage();
            }
            EditorGUI.indentLevel -= 2;
        }
        serializedObject.ApplyModifiedProperties();
    }

}
