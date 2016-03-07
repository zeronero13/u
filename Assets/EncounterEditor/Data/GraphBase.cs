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
	public abstract class GraphBase
	{

	#region Public Variables
		public string graphName = "New Encounter Graph";
		public List<NodeBase> nodes = new List<NodeBase>();
		public Connections connections= new Connections();

		public NodeBase startNode;

		//when trying to connect nodes

		[NonSerialized] public static bool wantsConnection = false;	//user wants to create a connection right now?
		[NonSerialized] public static NodeBase connectionNode = null; 		//used to know :when drawing line from connectionNode to mouse 
		[NonSerialized] public static NodePort connectionNodePort = null;	
		[NonSerialized] public static bool showProperties = false;

		[NonSerialized] public static NodeBase selectedNode = null;
		[NonSerialized] public static GUISkin editorViewSkin = null;
	#endregion

	#region Main Methods

		public virtual void OnEnable ()
		{

			if (nodes == null) {

				nodes = new List<NodeBase> ();
			}
			if (connections == null) {

				connections = new Connections ();
			}

		}
	
		public virtual void InitGraph ()
		{

			if (nodes == null) {

				nodes = new List<NodeBase> ();
			}
			if (connections == null) {

				connections = new Connections ();
			}

			if (nodes.Count > 0) {
				
				for (int i=0; i<nodes.Count; i++) {
					
					nodes [i].InitNode ();
				}
			}		
		}
	
		public virtual void UpdateGraph ()
		{

		}

	#if UNITY_EDITOR
		public virtual void UpdateGraphGUI (Event e, Rect viewRect, GUISkin viewSkin)
		{

			editorViewSkin = viewSkin;
			if (nodes.Count > 0) {
				//process events for graph
				ProcessEvents (e, viewRect);
				for (int i=0; i < nodes.Count; i++) {

					//draw node, and its components
					DrawWindows (i, e, viewRect, viewSkin);
					//update node, process events for it
					nodes [i].UpdateNode (e, viewRect);

				}
				connections.DrawInputLines ();
			}

		
		}

		protected void DrawWindows (int i, Event e, Rect viewRect, GUISkin viewSkin)
		{
			//Draw The Windows iteself, then callback DrawNode func.
			//nodes [i].nodeRect = GUI.Window (i, nodes [i].nodeRect, DrawNode, nodes [i].nodeTitle);
			nodes [i].nodeRect .setRect(
				GUILayout.Window (i, nodes[i].nodeRect.getRect(), DrawNode, nodes [i].nodeTitle, GUILayout.ExpandHeight (true)));
		}

		protected void DrawNode (int id)
		{
			nodes [id].DrawNode (editorViewSkin);
			GUI.DragWindow ();
		}
			

	#endif
	
	#endregion

	#region Utility Methode
		protected virtual void ProcessEvents(Event e, Rect viewRect)
		{

		}
		
		public virtual void DeselectAllNodes()
		{

		}
	#endregion
	}
}