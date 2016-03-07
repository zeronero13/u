using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace NodeEditor
{
	[Serializable]
	public abstract class NodeBase
	{
	#region Public Variables
		public bool isSelected = false;	//is node selected?
		//public Rect nodeRect;			//node position
		public MyRect nodeRect;
		public bool hasInputs = false;
		public string nodeTitle = "Node title";
		public string nodeName = "Node name";
		public NodeType nodeType;
		public GraphBase parentGraph; 

		[NonSerialized] public bool _loaded=false;
	#endregion

	#region Protected Variables
		//[NonSerialized]
		[NonSerialized] protected GUISkin nodeSkin;
	#endregion


		#region Constructor

		#endregion
	#region SubClasses


	#endregion

	#region Main Methods
		public virtual void InitNode ()
		{
			_loaded = false;
		
		}
		public virtual void UpdateNode (Event e, Rect viewRect)
		{
		
			ProcessEvents (e, viewRect);
		}


		public virtual NodeBase Next(){
			return null;
		}

		protected NodeBase getNextNode(NodePort port){
			if (port.connectionType == NodeConnectionType.FLOW) {
				return port.parentNode.parentGraph.connections.getNextNodeForward (port);
			}else if(port.connectionType == NodeConnectionType.IMP){
				return port.parentNode.parentGraph.connections.getNextNodeForward (port);
			}else{
				return port.parentNode.parentGraph.connections.getNextNodeBackward (port);
			}

		}

		public void deleteAllConnection (){
			/*delete all connections that are connected to this node*/
			deleteAllConnection (this);
		}

		public static void deleteAllConnection (NodeBase node){
			/*delete all connections that are connected to this node*/
			node.parentGraph.connections.deleteAllNodeConnections (node);
		}

	#if UNITY_EDITOR
		public virtual void DrawNode (GUISkin viewSkin)
		{
			//Draw all the stuff here
			//DrawInputLines should be called after all custom code
		}
	
		public virtual void DrawNodeProperties (GUISkin viewSkin)
		{
			//Draw properties of node to properties tab
		}
	#endif
	
	#endregion
	
	#region Untility Methods


		protected virtual void SetPortPosition (NodeBase node, NodePort port)
		{
			if (Event.current.type == EventType.Repaint) {
				//ha nem repaint event után van akkor getLastRect(); 0,0,1,1 fog adni!!!!!!!!
				Rect lastRect = GUILayoutUtility.GetLastRect ();
				port.portRect.x = ((node.nodeRect.x + lastRect.x) * 2 + lastRect.width) / 2;
				port.portRect.y = ((node.nodeRect.y + lastRect.y) * 2 + lastRect.height) / 2;
				port.portRect.width = node.nodeRect.x + lastRect.x + lastRect.width;
				port.portRect.height = node.nodeRect.y + lastRect.y + lastRect.height;
			}
		}

		public virtual void NodeDeleted (NodeBase node)
		{
			//if node deleted from editor, clean up afterwards
			//editor calls all nodes with this func and they can clean up if there was a transition from or to that node
		}
	
		public virtual NodeBase ClickedOnInput (Vector2 pos)
		{
			//click happened inside the window	
			return null;
		}

		void ProcessEvents (Event e, Rect viewRect)
		{
		}
	#endregion
	}
}