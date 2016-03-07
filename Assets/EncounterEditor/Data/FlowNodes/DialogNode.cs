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
	public class DialogNode : NodeBase
	{

	#region Variables
		//Outputs
		//public NodePort flowOutputSucc;
		//public NodePort flowOutputFail;
		//Inputs
		public NodePort flowInput;

		public TextData textData;
		public List<DialogOption> options;
	#endregion

	#region Constructor

		public DialogNode ()
		{

		}
	#endregion

	#region Main Methods
		public override void InitNode ()
		{
		
			base.InitNode ();
			nodeType = NodeType.Dialog;
			nodeTitle = "Dialog";
			nodeRect = new MyRect (10f, 10f, 200f, 200f);

			textData = TextData.createAsset (this);
			options = new List<DialogOption> ();
			flowInput = NodePort.createPort (NodePortType.In, NodeConnectionType.FLOW, this);	
		}
	
		public override void UpdateNode (Event e, Rect viewRect)
		{
			//node gets updated, process events ...
			base.UpdateNode (e, viewRect);
		}

		public override NodeBase Next(){
			//get default next node if possible, default is the first option, succ. output port connected node
			if (options.Count == 0) {
				return null;
			} else {
				if(getNextNode(options[0].flowOutputSucc)==null){
					return null;
				}else{
					return getNextNode(options[0].flowOutputSucc);
				}
			}
		}

		public NodeBase Next(int i, bool success=true){
			//get option[i] next node if possible
			if (options.Count < i) {

				return null;

			} else {

				if(success){
						if(getNextNode(options[i].flowOutputSucc)==null){
								return null;
						}
						return getNextNode(options[i].flowOutputSucc);
					}else{
						if(getNextNode(options[i].flowOutputFail)==null){
							return null;
						}
						return getNextNode(options[i].flowOutputFail);
				}
				

			}

		}

	#if UNITY_EDITOR
		public override void DrawNode (GUISkin viewSkin)
		{

			//do all gui component drawing here
			if (GUILayout.Button ("Add option")) {
				DialogOption newopt = DialogOption.createAsset (this);
				options.Add (newopt);
			}


			//input: flow input port for node
			GUILayout.BeginHorizontal ();
			NodePort.DrawPortBtn (flowInput, viewSkin);
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();


			DrawOptions (viewSkin);


		}


	
		public override void DrawNodeProperties (GUISkin viewSkin)
		{
			if (isSelected!=true) {
				return;
			}
			//kirajzolni a tulajdonságokat
			base.DrawNodeProperties (viewSkin);

			//draw properties of options
			textData.DrawProperties (viewSkin);
			for (int i=0; i<options.Count; ++i) {
				options[i].DrawProperties(viewSkin);
			}

		}

		public void DrawOptions (GUISkin viewSkin)
		{

			if ((options != null) && (options.Count > 0)) {
				for (int i=0; i<options.Count; i++) {
					GUILayout.BeginHorizontal ();
					GUILayout.BeginVertical (viewSkin.GetStyle ("NodeDefault"));
					options [i].DrawOption (viewSkin);
					GUILayout.EndVertical ();
					GUILayout.EndHorizontal ();
				}
			} else if (options == null) {
				options = new List<DialogOption> ();
			}

		}
	#endif
	
	#endregion

	#region Utility Methods
	#endregion
	}
}