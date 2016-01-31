using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace NodeEditor
{
	public class DialogOption: ScriptableObject
	{
		public bool showPosition = false;
		public string status = "Open option";
		public bool _loaded = false;
		public NodeBase parentNode;

		//
		public bool optionActiveDefault;
		public bool showActiveDefault;
		public bool defaultActiveDefault;

		//Outputs
		public NodePort flowOutputSucc;
		public NodePort flowOutputFail;
		//Inputs
		public NodePort activeInput; 	//is option active? if not dont show, dont look at condition ect... overwrites def. that is set, def: true
		public NodePort showInput;		//show option even if condition not meet, but greyed it out? overwrites def. that is set, def: true
		public NodePort conditionInput;	//do condition meet to show option????

		bool foldoutText;
		public string optionText = "Continue...";

		bool foldoutConditons;
		bool foldoutActions;
		bool foldoutConsequence;

		public GameActionTypeEnums actionOp;

		public void InitOption ()
		{	
			flowOutputSucc = NodePort.createAsset (NodePortType.Out, NodeConnectionType.FLOW, parentNode);
			flowOutputFail = NodePort.createAsset (NodePortType.Out, NodeConnectionType.FLOW, parentNode);

			activeInput = NodePort.createAsset (NodePortType.In, NodeConnectionType.BOOL, parentNode);	
			showInput = NodePort.createAsset (NodePortType.In, NodeConnectionType.BOOL, parentNode);
			conditionInput = NodePort.createAsset (NodePortType.In, NodeConnectionType.BOOL, parentNode);
		}

		public void DrawOption (GUISkin viewSkin)
		{
			if (_loaded != true) {
				_loaded = true;
				return;
			}
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Option::");
			GUILayout.FlexibleSpace ();
			if (GUILayout.Button ("Del")) {
				
			}
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("'" + optionText.Substring (0, (optionText.Length > 20) ? 20 : optionText.Length) + "' ...");
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			NodePort.DrawPortBtn (activeInput,viewSkin);
			GUILayout.Label ("Active");
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("Succ: ");
			NodePort.DrawPortBtn (flowOutputSucc,viewSkin);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			NodePort.DrawPortBtn (showInput,viewSkin);
			GUILayout.Label ("Show");
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("Fail: ");
			NodePort.DrawPortBtn (flowOutputFail,viewSkin);
			GUILayout.EndHorizontal ();

			/*showPosition = EditorGUILayout.Foldout (showPosition, "'" + text.Substring (0, (text.Length > 15) ? 15 : text.Length) + "' ...");
			if (showPosition) {
				GUILayout.BeginHorizontal (GUILayout.ExpandWidth (false));

				text = GUILayout.TextArea (text, 100, GUILayout.MaxWidth (parentNode.nodeRect.width - 15), GUILayout.ExpandWidth (false));

				GUILayout.EndHorizontal ();
			}*/

			GUILayout.Space (10);
		}

		public static DialogOption createAsset (NodeBase parentNode)
		{

			DialogOption option = (DialogOption)ScriptableObject.CreateInstance<DialogOption> ();
			
			AssetDatabase.AddObjectToAsset (option, parentNode);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			option.parentNode = parentNode;
			option.InitOption ();

			return option;
		}

		#if UNITY_EDITOR
		public void DrawProperties(GUISkin viewSkin){

			GUILayout.BeginVertical ();
			foldoutText=EditorGUILayout.Foldout (foldoutText, "Option Properties");
			if (foldoutText) {		
				GUILayout.Label ("Option text:", viewSkin.GetStyle ("NodeEditorProperties"));
				optionText = GUILayout.TextField (optionText);
				//TODO
			}
			//Set Question?

			foldoutConditons=EditorGUILayout.Foldout (foldoutConditons, "Requirement");
			if (foldoutConditons) {		
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button("Add")){
					//TODO
				}
				GUILayout.EndHorizontal();

				GUILayout.BeginHorizontal();
				GUILayout.Label ("<Type>");
				GUILayout.Label ("<Value1>");
				GUILayout.Label ("<Op>");
				GUILayout.Label ("<Value2>");
				GUILayout.EndHorizontal();
				//TODO
				
			}
			foldoutActions=EditorGUILayout.Foldout (foldoutActions, "Actions");
			if (foldoutActions) {
				GUILayout.BeginHorizontal();
				actionOp = (GameActionTypeEnums)EditorGUILayout.EnumPopup("Action type:",actionOp);
				GUILayout.FlexibleSpace();
				GUILayout.FlexibleSpace();
				GUILayout.EndHorizontal();

				if(actionOp==GameActionTypeEnums.Check){

				}else if(actionOp==GameActionTypeEnums.Combat){

				}else if(actionOp==GameActionTypeEnums.Gamble){

				}
				//TODO
			}
			foldoutConsequence=EditorGUILayout.Foldout (foldoutConsequence, "Consequence");
			if (foldoutConsequence) {
				GUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();
				if(GUILayout.Button("Add")){
					//TODO
				}
				GUILayout.EndHorizontal();
				//TODO
			}



			GUILayout.EndVertical ();
			GUILayout.Space (20);
			
			EditorUtility.SetDirty (this);
		}
		#endif
	}


}
