using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class StartNode : NodeBase
	{

	#region Variables
		//Outputs
		public NodePort flowOutputSucc;
	#endregion

	#region Constructor
	
		public StartNode ()
		{	

		}


	#endregion

	#region Main Methods
		public override void InitNode ()
		{
		
			base.InitNode ();
			nodeType = NodeType.Start;
			nodeTitle = "Start";
			nodeRect = new MyRect (50f, 50f, 60f, 50f);
			flowOutputSucc = NodePort.createPort (NodePortType.Out, NodeConnectionType.FLOW, this);
		}
	
		public override void UpdateNode (Event e, Rect viewRect)
		{
			//node gets updated, process events ...
			base.UpdateNode (e, viewRect);
		}

		public override NodeBase Next(){
			return getNextNode (flowOutputSucc);
		}

	#if UNITY_EDITOR
		public override void DrawNode (GUISkin viewSkin)
		{
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			NodePort.DrawPortBtn (flowOutputSucc, viewSkin);
			GUILayout.EndHorizontal ();
		}
	#endif
	
	#endregion
	}
}
