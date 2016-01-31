﻿using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class TextData: ScriptableObject{
	public string text="";
	public string subText="";

	public bool foldout=true;

	//change picture? bool
	//picture to load

	//change background? bool
	//background picture to load

	public static TextData createAsset(NodeEditor.NodeBase parent){

		TextData textDataAsset = (TextData)ScriptableObject.CreateInstance<TextData> ();

		AssetDatabase.AddObjectToAsset (textDataAsset, parent);
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();

		return textDataAsset;
	}

	#if UNITY_EDITOR
	public void DrawProperties(GUISkin viewSkin){




		GUILayout.BeginVertical ();
		foldout=EditorGUILayout.Foldout (foldout, "Story Text Properties");
		if (foldout) {		
			GUILayout.Label ("Story Text:", viewSkin.GetStyle ("NodeEditorProperties"));
			text = GUILayout.TextArea (text);
			GUILayout.Space (20);
			GUILayout.Label ("SubText:", viewSkin.GetStyle ("NodeEditorProperties"));
			subText = GUILayout.TextArea (subText);
		}
		GUILayout.EndVertical ();
		

		EditorUtility.SetDirty (this);

	}
	#endif
}

