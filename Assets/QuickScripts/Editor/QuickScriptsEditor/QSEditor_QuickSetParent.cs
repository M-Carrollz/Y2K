//Quick Scripts by Jack Wilson, Wanderlight Games 2017.
//Thank you for purchasing this product.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using QuickScripts;

[CustomEditor (typeof (QuickSetParent))]
[CanEditMultipleObjects]
public class QSEditor_QuickSetParent : Editor {

	[SerializeField]
	QuickSetParent _quickSetParent;
	SerializedProperty tagTransformList;

	static string helpText = "Quick Tips:\n1. Apply Quick Set Parent as a component to a standard Trigger Box (not a Quick Trigger!) and" +
		" use the tag edit button to select any tags you want to affect with SetParent. " +
		"\n\n2. NOTE: Only Transforms with a Collider, and tag will be affected by SetParent." +
		"\nIMPORTANT: BY DEFAULT IT WILL RE-PARENT THE COLLIDER'S TRANSFORM." +
		"\n\n3. If you need the SetParent to grab a Transform which is a parent of the tagged Collider, you can tell SetParent" +
		" how many steps to look up in the hierarchy. Open the Tags And Transforms dropdown and change 'Hierarchy Steps Up'." +
		"\nHierarchy Steps Up: 0 = the Transform with the Collider, 1 = its parent Transform, 2 = the parent's parent, so on." +
		"\n\n4. Do not stress if the Hierarchy Steps Up goes beyond the total levels of Hierarchy. Unity knows what to do.";
	public bool showHelp;
	public bool showTags;
	string selectedTag;

	void OnEnable() {
		_quickSetParent = (MonoBehaviour)target as QuickSetParent;
		tagTransformList = serializedObject.FindProperty ("tagsAndTransforms");
	}

	public override void OnInspectorGUI ()
	{
		showHelp = (bool)EditorGUILayout.Toggle ("Show Help", showHelp);
		if (showHelp)
		{
			EditorGUILayout.BeginFadeGroup (1);
			EditorGUILayout.HelpBox (helpText, MessageType.None);
			if (GUILayout.Button ("User Guide"))
				OpenUserGuide ();
			EditorGUILayout.EndFadeGroup ();
		}

		if (_quickSetParent.parentTransform == null)
			_quickSetParent.parentTransform = _quickSetParent.transform;

		tagTransformList.isExpanded = true;
		//EditorGUILayout.PropertyField (tagTransformList);

		showTags = (bool)EditorGUILayout.Toggle ("Edit Tags", showTags);
		if (showTags) {
			EditorGUILayout.BeginFadeGroup (1);
			selectedTag = EditorGUILayout.TagField ("Saved Tags", selectedTag);

			if (GUILayout.Button ("Add Tag"))
				_quickSetParent.ExternalAddTag (selectedTag);
			if (GUILayout.Button ("Remove Tag"))
				_quickSetParent.ExternalRemoveTag (selectedTag);
			if (GUILayout.Button ("Remove Last"))
				_quickSetParent.ExternalRemoveLast ();

			EditorGUILayout.EndFadeGroup ();
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		DrawDefaultInspector ();


		if (GUI.changed)
		{
			serializedObject.ApplyModifiedProperties ();
		}
	}

	void OpenUserGuide()
	{
		System.Diagnostics.Process.Start (Application.dataPath + "/QuickScripts/QuickScriptsUserGuide.pdf");
	}
}
