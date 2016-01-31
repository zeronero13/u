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
	public abstract class GraphBase : ScriptableObject
	{

	#region Public Variables
		public string graphName = "New Encounter Graph";
		public List<NodeBase> nodes;
		public Connections connections;

		public NodeBase startNode;

		//when trying to connect nodes
		public bool wantsConnection = false;	//user wants to create a connection right now?
		public NodeBase connectionNode; 		//used to know :when drawing line from connectionNode to mouse 
		public NodePort connectionNodePort;	
		public bool showProperties = false;

		public NodeBase selectedNode;

		public GUISkin editorViewSkin;
	#endregion

	#region Main Methods

		public virtual void OnEnable ()
		{

			if (nodes == null) {

				nodes = new List<NodeBase> ();
			}

		}
	
		public virtual void InitGraph ()
		{

			connections = (Connections)ScriptableObject.CreateInstance<Connections> ();

			AssetDatabase.AddObjectToAsset (connections, this);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

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

			this.editorViewSkin = viewSkin;
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
			nodes [i].nodeRect =
			GUILayout.Window (i, nodes [i].nodeRect, DrawNode, nodes [i].nodeTitle, GUILayout.ExpandHeight (true));
		}

		protected void DrawNode (int id)
		{
			nodes [id].DrawNode (this.editorViewSkin);
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