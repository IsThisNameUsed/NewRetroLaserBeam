using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TaskManager))]
public class TaskManagerEditor : Editor
{
    TaskManager taskManager;

    private void OnEnable()
    {
        taskManager = (TaskManager)target;

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
 
        EditorGUI.indentLevel += 2;
        if (GUILayout.Button("Open task manager"))
        {
            taskManager.OpenTaskManager();
        }
        EditorGUI.indentLevel -= 2;
        
        serializedObject.ApplyModifiedProperties();
    }

}

