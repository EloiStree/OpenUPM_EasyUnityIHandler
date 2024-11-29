using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CursorMono_SphereCastingToUnityInterface))]
public class CursorEditorMono_SphereCastingToUnityInterface : Editor
{
    

    public override void OnInspectorGUI()
    {
        CursorMono_SphereCastingToUnityInterface cursor = (CursorMono_SphereCastingToUnityInterface)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Press"))
        {
            cursor.Press();
        }
        if (GUILayout.Button("Click"))
        {
            cursor.Click();
        }
        if (GUILayout.Button("Release"))
        {
            cursor.Release();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Left"))
        {
            cursor.ScrollHorizontalLeft();
        }
        if (GUILayout.Button("Right"))
        {
            cursor.ScrollHorizontalRight();
        }
        if (GUILayout.Button("Up"))
        {
            cursor.ScrollVerticalUp();
        }
        if (GUILayout.Button("Down"))
        {
            cursor.ScrollVerticalDown();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }

}
