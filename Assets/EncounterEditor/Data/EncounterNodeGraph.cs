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
	public class EncounterNodeGraph : GraphBase
	{

		#region Main Methods

		public override void OnEnable ()
		{
			base.OnEnable ();
		}

		public override void InitGraph ()
		{
			base.InitGraph ();

		}

		public override void UpdateGraph ()
		{
			base.UpdateGraph ();
		}

		#if UNITY_EDITOR
		public override void UpdateGraphGUI (Event e, Rect viewRect, GUISkin viewSkin)
		{

			// Gráf kirajzoltatása, WorkView hivja meg saját UpdateView-ból
			base.UpdateGraphGUI (e, viewRect, viewSkin);

			//Lets look for connection mode
			if (wantsConnection) {

				if (connectionNode != null) {
					DrawConnectionToMouse (e.mousePosition);
				}

			}

			if (e.type == EventType.Layout) {
				if (selectedNode != null) {
					showProperties = true;
				}
			}

		}
		#endif

		#endregion

		#region Utility Methods
		protected override void ProcessEvents (Event e, Rect viewRect)
		{
			/*//Debug.Log("GraphProcc: processed called");
			if (viewRect.Contains (e.mousePosition)) {
				//Debug.Log(" GraphProcc: e.mousePos passed");
				if(e.type==EventType.MouseDown){
					Debug.Log(" GraphProcc: e.type mousedown");
				}

				if (e.button == 0 && e.type == EventType.MouseDown) {
					Debug.Log("GraphProcc: processed m.down");
					if (!wantsConnection) {
						DeselectAllNodes ();
					}

					foreach (NodeBase node in nodes) {
						if (node.nodeRect.Contains (e.mousePosition)) {
							Debug.Log("isSelected processed");
							selectedNode = node;
							selectedNode.isSelected = true;
							break;
						}
					}
				}
			}*/
		}

		public override void DeselectAllNodes ()
		{

			for (int i=0; i< nodes.Count; i++) {
				nodes [i].isSelected = false;
			}
			showProperties = false;
			selectedNode = null;
			wantsConnection = false;
			connectionNode = null;	
			connectionNodePort = null;
		}

		void DrawConnectionToMouse (Vector2 mousePosition)
		{
			//draw from node to mousePosition a line
			Handles.BeginGUI ();
			if (connectionNode != null) {
				Handles.Label (new Vector3 (0.0f, 0.0f, 0.0f), "Node Title: " + connectionNode.nodeTitle);
				Handles.Label (new Vector3 (0.0f, 20.0f, 0.0f), "Port Rect: " + connectionNodePort.portRect.x + " , " + connectionNodePort.portRect.y);
				Handles.Label (new Vector3 (0.0f, 40.0f, 0.0f), "Node count: " + nodes.Count );
				Handles.Label (new Vector3 (0.0f, 60.0f, 0.0f), "Connection count: " + connections.connections.Count );
			}
			Handles.Label (new Vector3 (0.0f, 80.0f, 0.0f), "[Mouse]: " + mousePosition.x + " , " + mousePosition.y);

			Handles.color = Color.white;
			Handles.DrawLine (new Vector3 ((connectionNodePort.portRect.x + connectionNodePort.portRect.width) / 2,
				(connectionNodePort.portRect.y + connectionNodePort.portRect.height) / 2,
				0f),
				new Vector3 (mousePosition.x, mousePosition.y, 0f));

			Handles.EndGUI ();
		}
		#endregion
	}
}