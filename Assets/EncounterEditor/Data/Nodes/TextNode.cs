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
	public class TextNode : NodeBase {

		#region Variables
		//Outputs
		public NodePort flowOutputSucc;
		//Inputs
		public NodePort flowInput;

		//public ReaderTextData textData= new ReaderTextData();
		//public string textData = "";

		public TextData textData;
		#endregion

		#region Constructor
		
		public TextNode ()
		{
			
		}
		#endregion

		#region Main Methods
		public override void InitNode ()
		{
			
			base.InitNode ();
			nodeType = NodeType.Text;
			nodeTitle = "Text";
			nodeRect = new Rect (10f, 10f, 150f, 150f);

			textData = TextData.createAsset (this);

			flowOutputSucc = NodePort.createAsset (NodePortType.Out, NodeConnectionType.FLOW, this);
			flowInput = NodePort.createAsset (NodePortType.In, NodeConnectionType.FLOW, this);	
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

			if ((_loaded== false) && (Event.current.type == EventType.Repaint))
			{
				return;
			}

			if (Event.current.type == EventType.Layout)
			{
				_loaded = true;
			}


			GUILayout.BeginHorizontal ();
			//input: flow input port for node
			NodePort.DrawPortBtn (flowInput, viewSkin);
			GUILayout.FlexibleSpace ();
			//output, succ def., port
			NodePort.DrawPortBtn (flowOutputSucc, viewSkin);
			GUILayout.EndHorizontal ();


			GUILayout.BeginHorizontal ();
			GUILayout.BeginVertical ();
			GUILayout.Label ("Story Text:");
			textData.text=GUILayout.TextArea (textData.text);
			GUILayout.Label ("SubText:");
			textData.subText=GUILayout.TextArea (textData.subText);
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();

			
		}
		
		
		
		public override void DrawNodeProperties (GUISkin viewSkin)
		{
			if (isSelected!=true) {
				return;
			}
			//kirajzolni a tulajdonságokat
			base.DrawNodeProperties (viewSkin);

			textData.DrawProperties (viewSkin);

		}
		

		#endif
		#endregion
		
		#region Utility Methods

		#endregion
	}
}
