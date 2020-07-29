//Quick Scripts by Jack Wilson, Wanderlight Games 2017.
//Thank you for purchasing this product.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using QuickScripts;

[CustomEditor(typeof(QuickRelay))]
[CanEditMultipleObjects]
public class QSEditor_QuickRelay : Editor
{
    
    //QuickPendulum _quickPendulum;
    static string helpText = "Quick Tips:" +
        "\n1. Use a Trigger Box or other Relay node to call the Run() function on this Relay node." +
        "\n\n2. Calling Run() will execute the registered Events after the delay." +
        "\n\nFor more information on how to use the Quick Relay, see the User Guide.";
    public bool showHelp;

    public override void OnInspectorGUI()
    {
        showHelp = (bool)EditorGUILayout.Toggle("Show Help", showHelp);
        if (showHelp)
        {
            EditorGUILayout.BeginFadeGroup(1);
            EditorGUILayout.HelpBox(helpText, MessageType.None);
            if (GUILayout.Button("User Guide"))
                OpenUserGuide();
            EditorGUILayout.EndFadeGroup();
        }
        DrawDefaultInspector();

        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
        }
    }

    void OpenUserGuide()
    {
        System.Diagnostics.Process.Start(Application.dataPath + "/QuickScripts/QuickScriptsUserGuide.pdf");
    }
}
