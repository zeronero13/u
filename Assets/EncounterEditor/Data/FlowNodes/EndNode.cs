using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class EndNode : NodeBase
	{
		
		#region Variables
		//Outputs
		public NodePort flowInput;
		#endregion
		
		#region Constructor
		
		public EndNode ()
		{	

		}
		#endregion
		
		#region Main Methods
		public override void InitNode ()
		{
			
			base.InitNode ();
			nodeType = NodeType.End;
			nodeTitle = "End";
			nodeRect = new MyRect (50f, 50f, 50f, 50f);
			flowInput = NodePort.createPort (NodePortType.In, NodeConnectionType.FLOW, this);
		}
		
		public override void UpdateNode (Event e, Rect viewRect)
		{
			//node gets updated, process events ...
			base.UpdateNode (e, viewRect);

		}
		
		#if UNITY_EDITOR
		public override void DrawNode (GUISkin viewSkin)
		{
			GUILayout.BeginHorizontal ();
			NodePort.DrawPortBtn (flowInput, viewSkin);
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();			
		}


		#endif
		
		#endregion
	}
}
