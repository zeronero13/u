using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class BoolNode: NodeEditor.NodeBase
	{
		#region Variables
		//Outputs
		public NodePort flowOutputSucc;
		#endregion

		#region Constructor
		public BoolNode (){}

		#endregion

		#region Main Methods
		public override void InitNode ()
		{

			base.InitNode ();
			nodeType = NodeType.Bool;
			nodeTitle = "Boolean";
			nodeRect = new MyRect (50f, 50f, 100f, 80f);
			flowOutputSucc = NodePort.createPort (NodePortType.Out, NodeConnectionType.BOOL, this);
		}

		public override void UpdateNode (Event e, Rect viewRect)
		{
			//node gets updated, process events ...
			base.UpdateNode (e, viewRect);
		}

		public override NodeBase Next(){
			return getNextNode (flowOutputSucc);
		}

		public enum InputType
		{
			Boolean,
			Randomization
		}

		public enum InputValue
		{
			FALSE,
			TRUE
		}

		private InputType inputType;
		private InputValue inputValue;

		#if UNITY_EDITOR
		public override void DrawNode (GUISkin viewSkin)
		{
			GUILayout.BeginHorizontal (GUILayout.MaxWidth(this.nodeRect.width));
			GUILayout.FlexibleSpace ();
			NodePort.DrawPortBtn (flowOutputSucc, viewSkin);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Input Type:");
			inputType = (InputType)EditorGUILayout.EnumPopup (inputType);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			if (inputType == InputType.Boolean) {
				GUILayout.Label ("Input value:");
				inputValue = (InputValue)EditorGUILayout.EnumPopup (inputValue);
			}
			GUILayout.EndHorizontal ();

		}
		#endif

		#endregion
	}
}

