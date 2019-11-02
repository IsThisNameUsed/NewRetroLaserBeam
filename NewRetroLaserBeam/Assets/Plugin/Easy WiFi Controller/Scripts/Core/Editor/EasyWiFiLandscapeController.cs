using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class EasyWiFiLandscapeController
{

    static EasyWiFiLandscapeController()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChange;
    }
    static void OnHierarchyChange()
    {

        /*if (EditorApplication.currentScene.Contains("MultiplayerDynamicClientScene") ||
            EditorApplication.currentScene.Contains("ControlsKitchenSinkClientScene") ||
            EditorApplication.currentScene.Contains("DrawingClientScene") ||
            EditorApplication.currentScene.Contains("UnityUINavigationClientScene") ||
            EditorApplication.currentScene.Contains("PanTiltZoomClientScene") ||
            EditorApplication.currentScene.Contains("DualStickZoomClientScene") ||
            EditorApplication.currentScene.Contains("PrecomputedSteeringClientScene") ||
            EditorApplication.currentScene.Contains("MultiplayerControllerSelectClientScene") ||            
            EditorApplication.currentScene.Contains("SteeringWheelClientScene"))*/
        if (EditorSceneManager.GetActiveScene().name.Contains("MultiplayerDynamicClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("ControlsKitchenSinkClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("DrawingClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("UnityUINavigationClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("PanTiltZoomClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("DualStickZoomClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("PrecomputedSteeringClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("MultiplayerControllerSelectClientScene") ||
        EditorSceneManager.GetActiveScene().name.Contains("SteeringWheelClientScene"))
        {
            //we only need to execute once on our scenes
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            EditorApplication.hierarchyChanged -= OnHierarchyChange;
        }

    }
}