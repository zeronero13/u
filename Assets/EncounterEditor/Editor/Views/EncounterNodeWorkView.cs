using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace NodeEditor
{
	public class EncounterNodeWorkView : ViewBase
	{
		/**
	 * Munkaterület
	 */


	#region Variables
	
		//lokális feladatokhoz
		private int deleteNodeID = 0;
		private Vector2 mousePos;
	#endregion
	
	#region Constructor
		public EncounterNodeWorkView ():base("Encounter Work View")
		{
		}
	#endregion

	#region Main Methods
		public override void UpdateView (Rect editorRect, Rect percentageRect, Event e, EncounterNodeGraph curGraph, GUISkin viewSkin)
		{
			//Update, és Kirajzoltatás
			base.UpdateView (editorRect, percentageRect, e, curGraph, viewSkin);
			//Draw Workspace
			//pl. viewRect 0f,0f,W:=w*0.75f,H:=h*1f (a (0,0) == 'root' ablakjának pozicioja a monitoron)

			if (viewTitle==null)Debug.LogError("viewTitle is null");
			if (viewSkin==null)Debug.LogError("viewSkin is null");
			GUI.Box (viewRect, viewTitle, viewSkin.GetStyle ("ViewBg"));
		
			//DrawGrid
			NodeUtils.DrawGrid (viewRect, 80f, 0.15f, Color.white);
			NodeUtils.DrawGrid (viewRect, 20f, 0.05f, Color.white);

			/*
		//DrawBox
		//scrollPos = GUI.BeginScrollView (viewRect, scrollPos, new Rect (0, 0, 1000, 1000));
		GUILayout.BeginArea (viewRect);

		//DrawGraph, and all the other stuff
		if (curGraph != null) {
			curGraph.UpdateGraphGUI (e, viewRect, viewSkin);
		}
		
		GUILayout.EndArea ();
		//GUI.EndScrollView ();
		*/
		
			ProcessEvents (e);
		}

		public Rect getViewRect ()
		{
			return viewRect;
		}

		public override void ProcessEvents (Event e)
		{
			base.ProcessEvents (e);
		
			if (viewRect.Contains (e.mousePosition)) {
			
				if (e.button == 0) {
				
					if (e.type == EventType.MouseDown) {
					
						if(curGraph!=null){
							for(int i=0;i < curGraph.nodes.Count; ++i){

								if (e.button == 0 && e.type == EventType.MouseDown) {
									curGraph.nodes[i].isSelected=false;

									if (curGraph.nodes[i].nodeRect.Contains (e.mousePosition)) {
										EncounterNodeGraph.selectedNode= curGraph.nodes[i];
										EncounterNodeGraph.selectedNode.isSelected=true;
										EncounterNodeEditorWindow.getInstance().propertyView._loaded=false;
										break;
									}
								}
							}
						}
						
					}
					if (e.type == EventType.MouseDrag) {

					}
					if (e.type == EventType.MouseUp) {	
					}
					if (e.type == EventType.ScrollWheel) {			
					}
				
				} else if (e.button == 1) {
				
					if (e.type == EventType.MouseDown) {
						mousePos = e.mousePosition;
						bool overNode = false;
						deleteNodeID = 0;
						if (curGraph != null) {
						
							if (curGraph.nodes.Count > 0) {
							
								for (int i=0; i<curGraph.nodes.Count; i++) {
								
									if (curGraph.nodes [i].nodeRect.Contains (mousePos) && (curGraph.nodes[i].nodeType!=NodeType.Start)) {
										deleteNodeID = i;
										overNode = true;
									}
								}
							}
						}
					
						if (!overNode) {
							ProcessContextMenu (e, 0);
						} else {
							ProcessContextMenu (e, 1);
						}
					}
				}
			}
		}
	#endregion

	#region Utility Methods
		void ProcessContextMenu (Event e, int contextID)
		{
			GenericMenu menu = new GenericMenu ();
			if (contextID == 0) {
				menu.AddItem (new GUIContent ("Create Graph"), false, ContextCallback, "0");
				menu.AddItem (new GUIContent ("Load Graph"), false, ContextCallback, "load");
			
				if (curGraph != null) {
					menu.AddSeparator ("");
					menu.AddItem (new GUIContent ("Unload Graph"), false, ContextCallback, "unload");
					menu.AddSeparator ("");
					menu.AddItem (new GUIContent ("Flow node/Text node"), false, ContextCallback, "text node");
					menu.AddItem (new GUIContent ("Flow node/Dialog node"), false, ContextCallback, "dialog node");
					menu.AddItem (new GUIContent ("Flow node/Branching node"), false, ContextCallback, "branching node");
					menu.AddItem (new GUIContent ("Flow node/End node"), false, ContextCallback, "end node");
					menu.AddSeparator ("");
					menu.AddItem (new GUIContent ("Logic nodes/Bool node"), false, ContextCallbackLogic, "boolNode");

				}
			} else {
			
				if (curGraph != null) {
				
					menu.AddItem (new GUIContent ("Delete node"), false, ContextCallback, "delete");
				}
			
			}
			menu.ShowAsContext ();
			e.Use ();
		}
	
		void ContextCallback (object obj)
		{

			switch (obj.ToString ()) {
			case "0":
				EncounterCreateNodePopupWindow.InitNodePopup ();
				break;
			case "load":
				EncounterNodeEditorWindow.LoadGraph ();
				break;
			case "unload":
				EncounterNodeEditorWindow.UnloadGraph ();
				break;
			case "text node":
				EncounterNodeEditorWindow.getInstance ().CreateNode (NodeType.Text, mousePos);
				break;
			case "dialog node":
				EncounterNodeEditorWindow.getInstance ().CreateNode (NodeType.Dialog, mousePos);
				break;
			case "branching node":
				EncounterNodeEditorWindow.getInstance ().CreateNode (NodeType.Branching, mousePos);
				break;
			case "end node":
				EncounterNodeEditorWindow.getInstance ().CreateNode (NodeType.End, mousePos);
				break;
			case "delete":
				EncounterNodeEditorWindow.getInstance ().DeleteNode (deleteNodeID);
				break;
			default:
			
				break;
			}
		}

		void ContextCallbackLogic (object obj)
		{

			switch (obj.ToString ()) {
			case "boolNode":
				EncounterNodeEditorWindow.getInstance ().CreateNode (NodeType.Bool, mousePos);
				break;
			default:

				break;
			}
		}

	#endregion
	}
}