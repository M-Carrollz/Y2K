//Quick Scripts by Jack Wilson, Wanderlight Games 2017.
//Thank you for purchasing this product.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using QuickScripts;

[CustomEditor(typeof(QuickDoor))]
[CanEditMultipleObjects]
public class QSEditor_QuickDoor : Editor{

	[SerializeField]
	QuickDoor _quickDoor;

	public bool showHelp;
	static string helpText = "Quick Tips:\n1. If the Quick Door game object also has a Mesh Component, the door will automatically move a distance equal to the size " +
	                         "of the mesh. However you cannot change the mesh during runtime. You can override it by specifying a custom distance to move." +
	                         "\n\n2. Pivot doors will need to be a child of another game object. The Door will rotate around the transform position of the parent game object." +
	                         "\n\nFor more information on how to use the Quick Door, see the User Guide.";

//	static string pivotHeader = "Drag and drop the door's pivot game object into the field below.";
	static string slideHeader = "The Door will automatically move the distance of the mesh size. You can change this by ticking 'Custom Move Distance'.";
	static string pivotWarning = "A pivot door object must be the child of another game object.";

	// For General Purposes
	SerializedProperty doorType;	// doorEnum
	SerializedProperty moveType;	// moveEnum
	SerializedProperty coolDownTime;	// float
	SerializedProperty duration;	// float
	SerializedProperty autoClose; // bool
	SerializedProperty autoCloseDelay; // float
	SerializedProperty startOpen; // bool
	SerializedProperty twoWayStartPos; // enum
    // Events
    SerializedProperty onOpenEvents; // Events
    SerializedProperty onCloseEvents; // Events
    SerializedProperty hasEvents; // bool
    SerializedProperty drawTargetLines; // bool


    // For Sliding Doors
    SerializedProperty customMoveBool;
	SerializedProperty customMoveFloat;

	// For Pivot Doors
	SerializedProperty pivot;

	// For Audio
	SerializedProperty hasAudio;
	SerializedProperty audioSource;
	SerializedProperty extendAudio;
	SerializedProperty openStartAudioClip;
	SerializedProperty openStopAudioClip;
	SerializedProperty closeStartAudioClip;
	SerializedProperty closeStopAudioClip;

	void OnEnable()
	{
		_quickDoor = (MonoBehaviour)target as QuickDoor;
		duration = serializedObject.FindProperty ("duration");
		coolDownTime = serializedObject.FindProperty ("coolDownTime");
		doorType = serializedObject.FindProperty ("doorType");
		moveType = serializedObject.FindProperty ("moveType");
		customMoveBool = serializedObject.FindProperty ("customMoveDistance");
		customMoveFloat = serializedObject.FindProperty ("moveDistance");
		pivot = serializedObject.FindProperty ("pivotPoint");
		autoClose = serializedObject.FindProperty ("autoClose");
		autoCloseDelay = serializedObject.FindProperty ("autoCloseDelay");
		startOpen = serializedObject.FindProperty ("startOpen");
		twoWayStartPos = serializedObject.FindProperty ("twoWayStartPos");
        onOpenEvents = serializedObject.FindProperty("OnOpen");
        onCloseEvents = serializedObject.FindProperty("OnClose");
        hasEvents = serializedObject.FindProperty("hasEvents");
        drawTargetLines = serializedObject.FindProperty("drawTargetLines");

        //Audio
        hasAudio = serializedObject.FindProperty ("hasAudio");
		audioSource = serializedObject.FindProperty ("audioSource");
		openStartAudioClip = serializedObject.FindProperty ("openStartAudioClip");
		openStopAudioClip = serializedObject.FindProperty ("openStopAudioClip");
		closeStartAudioClip = serializedObject.FindProperty ("closeStartAudioClip");
		closeStopAudioClip = serializedObject.FindProperty ("closeStopAudioClip");
		extendAudio = serializedObject.FindProperty ("extendSoundFX");
	}

	override public void OnInspectorGUI()
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

		EditorGUILayout.PropertyField (doorType);
		EditorGUILayout.PropertyField (moveType);
		EditorGUILayout.Space ();

		if (doorType.enumValueIndex == 6) // If Pivot Door
		{
			// Check the door is a child of something
			if (_quickDoor.transform.parent == null)
			{	
				pivot.objectReferenceValue = null;
				EditorGUILayout.BeginFadeGroup (1);
				EditorGUILayout.HelpBox (pivotWarning, MessageType.Error);
				EditorGUILayout.EndFadeGroup ();
			}
			// If Door pivot has not been assigned but already has a parent game object, auto-assign parent transform
			if (pivot.objectReferenceValue == null && _quickDoor.transform.parent != null)
			{
				_quickDoor.pivotPoint = _quickDoor.transform.parent.gameObject;
			}
			EditorGUILayout.BeginFadeGroup (1);
			EditorGUILayout.PropertyField (pivot);
			_quickDoor.pivotAmountX = EditorGUILayout.Slider ("Pivot Amount X Axis", _quickDoor.pivotAmountX, -179.9f, 179.9f);
			_quickDoor.pivotAmountY = EditorGUILayout.Slider ("Pivot Amount Y Axis", _quickDoor.pivotAmountY, -179.9f, 179.9f);
			_quickDoor.pivotAmountZ = EditorGUILayout.Slider ("Pivot Amount Z Axis", _quickDoor.pivotAmountZ, -179.9f, 179.9f);

			EditorGUILayout.PropertyField (startOpen);
			EditorGUILayout.EndFadeGroup ();


		} else if (doorType.enumValueIndex == 7) //Two Way Door
		{
			// Check the door is a child of something
			if (_quickDoor.transform.parent == null)
			{	
				pivot.objectReferenceValue = null;
				EditorGUILayout.BeginFadeGroup (1);
				EditorGUILayout.HelpBox (pivotWarning, MessageType.Error);
				EditorGUILayout.EndFadeGroup ();
			}
			// If Door pivot has not been assigned but already has a parent game object, auto-assign parent transform
			if (pivot.objectReferenceValue == null && _quickDoor.transform.parent != null)
			{
				_quickDoor.pivotPoint = _quickDoor.transform.parent.gameObject;
			}
			EditorGUILayout.BeginFadeGroup (1);
			EditorGUILayout.PropertyField (pivot);
			_quickDoor.outerPivotAmountY = EditorGUILayout.Slider ("Outer Pivot Amount Y Axis", _quickDoor.outerPivotAmountY, 0, 179.9f);
			_quickDoor.innerPivotAmountY = EditorGUILayout.Slider ("Inner Pivot Amount X Axis", _quickDoor.innerPivotAmountY, -0.01f, -179.9f);

			EditorGUILayout.PropertyField (twoWayStartPos);
			EditorGUILayout.EndFadeGroup ();
			
		}

		else // If Sliding Door
		{
			EditorGUILayout.HelpBox (slideHeader, MessageType.None);
			EditorGUILayout.PropertyField (customMoveBool);
			if (customMoveBool.boolValue == true)
			{
				EditorGUILayout.BeginFadeGroup (1);
				EditorGUILayout.PropertyField (customMoveFloat);
				EditorGUILayout.EndFadeGroup ();
			}

			EditorGUILayout.PropertyField (startOpen);
		} 

		// Generic Properties


		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (duration);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (coolDownTime);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (autoClose);
		EditorGUILayout.Space ();
		EditorGUILayout.PropertyField (autoCloseDelay);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(hasEvents);

        // Events
        if (hasEvents.boolValue == true)
        {
            EditorGUILayout.BeginFadeGroup(1);
            EditorGUILayout.PropertyField(onOpenEvents);
            EditorGUILayout.PropertyField(onCloseEvents);
            EditorGUILayout.PropertyField(drawTargetLines);
            EditorGUILayout.EndFadeGroup();
        }
        EditorGUILayout.Space ();

        // Audio
		EditorGUILayout.PropertyField (hasAudio);
		if (hasAudio.boolValue == true)
		{
			EditorGUILayout.BeginFadeGroup (1);
			EditorGUILayout.PropertyField (audioSource);
			EditorGUILayout.PropertyField (openStartAudioClip);
			EditorGUILayout.PropertyField (openStopAudioClip);
			EditorGUILayout.PropertyField (closeStartAudioClip);
			EditorGUILayout.PropertyField (closeStopAudioClip);
			EditorGUILayout.PropertyField (extendAudio);
			EditorGUILayout.EndFadeGroup ();
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty (_quickDoor);
			serializedObject.ApplyModifiedProperties ();
		}
	}

	void OpenUserGuide()
	{
		System.Diagnostics.Process.Start (Application.dataPath + "/QuickScripts/QuickScriptsUserGuide.pdf");
	}
}
