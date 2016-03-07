using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class IntNode: NodeEditor.NodeBase
	{
		#region Variables
		//Outputs
		public NodePort flowOutputSucc;
		#endregion

		#region Constructor
		public IntNode ()
		{
		}
		#endregion

		#region Main Methods
		public override void InitNode ()
		{

			base.InitNode ();
			nodeType = NodeType.Bool;
			nodeTitle = "Integer";
			nodeRect = new MyRect (50f, 50f, 100f, 80f);
			flowOutputSucc = NodePort.createPort (NodePortType.Out, NodeConnectionType.NUM, this);
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
			Integer,
			Randomization
		}

		private InputType inputType;
		private string inputValue;
		private string randomFrom;
		private string randomTo;


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
			if (inputType == InputType.Integer) {
				GUILayout.Label ("Input value:");
				inputValue = EditorGUILayout.TextField  (inputValue);
			} else {
				randomFrom = EditorGUILayout.TextField ("From:",randomFrom);
				randomTo = EditorGUILayout.TextField ("To:",randomTo);
			}
			GUILayout.EndHorizontal ();

		}
		#endif

		private int calculateRandom(){

			int rFrom = 0;
			int rTo = 0;

			int.TryParse (this.randomFrom, out rFrom);
			int.TryParse (this.randomTo, out rTo);

			int randomFrom = (int)(rFrom);
			int randomTo = (int)(rTo);

			int selected = UnityEngine.Random.Range (randomFrom, randomTo+1);

			float selectedValue = selected / 10;

			inputValue = selectedValue.ToString ();

			return (int)selectedValue;
		}

		#endregion
	}
}

