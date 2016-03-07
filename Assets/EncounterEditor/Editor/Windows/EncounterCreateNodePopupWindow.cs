using UnityEngine;
using UnityEditor;
using System.Collections;

namespace NodeEditor
{
	
	public class EncounterCreateNodePopupWindow : EditorWindow
	{
	
	#region Variables
		static EncounterCreateNodePopupWindow curPopup;
		public string name = "Enter a name...";
	#endregion
	
	#region Main Methods
		public static void InitNodePopup ()
		{
		
			curPopup = (EncounterCreateNodePopupWindow)EditorWindow.GetWindow<EncounterCreateNodePopupWindow> ();
			curPopup.titleContent.text = "Encounter Create Node Popup Window";
		}
	
		void OnGUI ()
		{
		
			GUILayout.Space (20);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (20);
		
			GUILayout.BeginVertical ();
		
			EditorGUILayout.LabelField ("Create New Graph", EditorStyles.boldLabel);
			name = EditorGUILayout.TextField ("Enter Name:", name);
		
			GUILayout.Space (10);
		
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Create Graph", GUILayout.Height (40))) {
			
				if ((!string.IsNullOrEmpty (name)) && (name != "Enter a name...")) {
					EncounterNodeEditorWindow.CreateNewGraph (name);
					curPopup.Close ();
				
				} else {
				
					EditorUtility.DisplayDialog ("Node message", "Please enter a valid graph name", "OK");
				}
			}
			if (GUILayout.Button ("Cancel", GUILayout.Height (40))) {
			
				curPopup.Close ();
			}
			GUILayout.EndHorizontal ();
		
			GUILayout.EndVertical ();
		
			GUILayout.Space (20);
			GUILayout.EndHorizontal ();
			GUILayout.Space (20);
		}
	#endregion
	
	#region Utility Methods
	#endregion
	}
}