using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace NodeEditor
{
	[Serializable]
	public class EncounterNodePropertyView : ViewBase
	{

	#region Public Variables
		public bool showProperties = false;
		public bool _loaded=false;
	#endregion
	
	#region Protected Variables
	#endregion
	
	#region Constructor
		public EncounterNodePropertyView ():base("Property View")
		{
		}
	#endregion

	#region Main Methods
		public override void UpdateView (Rect editorRect, Rect percentageRect, Event e, EncounterNodeGraph curGraph, GUISkin viewSkin)
		{
			base.UpdateView (editorRect, percentageRect, e, curGraph, viewSkin);
			GUI.Box (viewRect, viewTitle, viewSkin.GetStyle ("ViewBg"));
		
			GUILayout.BeginArea (viewRect);
			GUILayout.Space (60);
			GUILayout.BeginHorizontal ();
			GUILayout.Space (30);
			/*if (curGraph != null) {
			if (!curGraph.showProperties) {
				EditorGUILayout.LabelField ("None");
			} else {
				curGraph.selectedNode.DrawNodeProperties ();
			}
		}*/


			GUILayout.EndHorizontal ();
			if (curGraph != null) {
				if (/*curGraph*/EncounterNodeGraph.showProperties == true && _loaded==true) {
					EncounterNodeGraph.selectedNode.DrawNodeProperties(viewSkin);
				}
			}
			if (Event.current.type == EventType.Repaint) {
				_loaded=true;
			}
			GUILayout.EndArea ();
		
			ProcessEvents (e);
		
		}
	
		public override void ProcessEvents (Event e)
		{
			base.ProcessEvents (e);
		
			if (viewRect.Contains (e.mousePosition)) {
			
				if (e.button == 0) {
				
					if (e.type == EventType.MouseDown) {
					
					
					}
					if (e.type == EventType.MouseDrag) {
					
					
					}
					if (e.type == EventType.MouseUp) {
					
					
					}
				} else if (e.button == 1) {
				
					if (e.type == EventType.MouseDown) {
					
					
					}
				}
			}
		}
	#endregion
	
	#region Utility Methods
	#endregion
	}
}