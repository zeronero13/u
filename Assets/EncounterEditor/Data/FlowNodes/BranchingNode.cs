using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;

namespace NodeEditor{

	[Serializable]
	public class BranchingNode : NodeBase {

		#region Variables
		//Outputs
		public NodePort flowOutputSucc;
		public NodePort flowOutputFail;
		//Inputs
		public NodePort flowInput;
		#endregion
		
		#region Constructor
		
		public BranchingNode ()
		{
			
		}
		#endregion

		#region Main Methods
		public override void InitNode ()
		{
			
			base.InitNode ();
			nodeType = NodeType.Branching;
			nodeTitle = "Branching";
			nodeRect = new MyRect (10f, 10f, 200f, 100f);

			flowOutputSucc = NodePort.createPort (NodePortType.Out, NodeConnectionType.FLOW, this);
			flowOutputFail = NodePort.createPort(NodePortType.Out, NodeConnectionType.FLOW, this);
			flowInput = NodePort.createPort (NodePortType.In, NodeConnectionType.FLOW, this);	
		}

		public override void UpdateNode (Event e, Rect viewRect)
		{
			//node gets updated, process events ...
			base.UpdateNode (e, viewRect);
		}

		public override NodeBase Next(){
			//get default next node if possible, default is the first option, succ. output port connected node
			//...kiértékelés????vagy nem?
			return getNextNode(flowOutputSucc);
		}

		public NodeBase Next(int i, bool success=true){
			//get option[i] next node if possible
			//...kiértékelés????vagy nem??
			return getNextNode(flowOutputSucc);
		}
		#if UNITY_EDITOR
		public override void DrawNode (GUISkin viewSkin)
		{
			
			//do all gui component drawing here


			//input: flow input port for node
			GUILayout.BeginHorizontal ();
			NodePort.DrawPortBtn (flowInput, viewSkin);
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("True: ");
			NodePort.DrawPortBtn (flowOutputSucc, viewSkin);
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.Label ("False: ");
			NodePort.DrawPortBtn (flowOutputFail, viewSkin);
			GUILayout.EndHorizontal ();

			//List<CardBase> inst=CardDeckManager.instance.cardList;
			
		}

		public override void DrawNodeProperties (GUISkin viewSkin)
		{
			if (isSelected!=true) {
				return;
			}
			//kirajzolni a tulajdonságokat
			base.DrawNodeProperties (viewSkin);

			
		}

		#endif
		#endregion

		#region Utility Methods
		#endregion
	}
}
