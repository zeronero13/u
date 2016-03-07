using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class AddNode : NodeBase
	{
		/*#region Variables
	public float nodeSum;
	public NodeOutput output;
	public NodeInput inputA;
	public NodeInput inputB;
	#endregion
	
	#region Constructor
	public AddNode ()
	{
		
		output = new NodeOutput ();
		inputA = new NodeInput ();
		inputB = new NodeInput ();
	}
	#endregion
	
	#region Main Methods
	public override void InitNode ()
	{
		
		base.InitNode ();
		nodeType = NodeType.Add;
		nodeRect = new Rect (10f, 10f, 200f, 65f);
	}
	
	public override void UpdateNode (Event e, Rect viewRect)
	{
		base.UpdateNode (e, viewRect);
	}
	
	#if UNITY_EDITOR
	public override void UpdateNodeGUI (Event e, Rect viewRect, GUISkin viewSkin)
	{
		base.UpdateNodeGUI (e, viewRect, viewSkin);

		//Output
		if (GUI.Button (new Rect (nodeRect.x + nodeRect.width, nodeRect.y + (nodeRect.height * 0.5f) - 12f, 24f, 24f), "", viewSkin.GetStyle ("NodeOutput"))) {
			
			if (parentGraph != null) {
				
				parentGraph.wantsConnection = true;
				parentGraph.connectionNode = this;
			}
		}
		//Input
		if (GUI.Button (new Rect (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f)) - 14f, 24f, 24f), "", viewSkin.GetStyle ("NodeInput"))) {

			if (parentGraph != null) {
				inputA.inputNode = parentGraph.connectionNode;
				inputA.isOccupied = inputA.inputNode != null ? true : false;

				parentGraph.wantsConnection = false;
				parentGraph.connectionNode = null;
			}		
			
		}

		if (GUI.Button (new Rect (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * 2f) - 8f, 24f, 24f), "", viewSkin.GetStyle ("NodeInput"))) {

			if (parentGraph != null) {
				inputB.inputNode = parentGraph.connectionNode;
				inputB.isOccupied = inputB.inputNode != null ? true : false;
				
				parentGraph.wantsConnection = false;
				parentGraph.connectionNode = null;
			}					
			
		}
		if (inputA.isOccupied && inputB.isOccupied) {

			FloatNode nodeA = (FloatNode)inputA.inputNode;
			FloatNode nodeB = (FloatNode)inputB.inputNode;

			if (nodeA != null && nodeB != null) {
				nodeSum = nodeA.nodeValue + nodeB.nodeValue;
			}
		} else {
			nodeSum = 0.0f;
		}

		//Draw lines
		DrawInputLines ();
	}

	public override void DrawNodeProperties ()
	{
		base.DrawNodeProperties ();
		EditorGUILayout.FloatField ("AddSum value: ", nodeSum);
	}
	#endif
	
	#endregion
	
	#region Utility Methods

	public void DrawInputLines ()
	{

		if (inputA.isOccupied && inputA.inputNode != null) {
			DrawLine (inputA, 1f);
		} else {
			inputA.isOccupied = false;
		}

		if (inputB.isOccupied && inputB.inputNode != null) {
			DrawLine (inputB, 2f);
		} else {
			inputB.isOccupied = false;
		}

	}

	void DrawLine (NodeInput curInput, float inputId)
	{
		Handles.BeginGUI ();
		Handles.color = Color.white;
		Handles.DrawLine (new Vector3 (curInput.inputNode.nodeRect.x + curInput.inputNode.nodeRect.width + 24f, curInput.inputNode.nodeRect.y + curInput.inputNode.nodeRect.height * 0.5f, 0f), new Vector3 (nodeRect.x - 24f, (nodeRect.y + (nodeRect.height * 0.33f) * inputId), 0f));
		Handles.EndGUI ();
	}
	#endregion*/
	}
}
