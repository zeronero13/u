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
	public class Connections : ScriptableObject
	{

		[Serializable]
		public class Connection
		{

			public NodePort output;

			public NodePort input;

			public Connection (NodePort output, NodePort input)
			{
				this.output = output;
				this.input = input;
			}

			public bool isEqualByRef (NodePort output, NodePort input)
			{
				// If parameter is null return false:
				if ((object)output == null) {
					return false;
				}
				if ((object)input == null) {
					return false;
				}

				if ((object.ReferenceEquals (this.output, output)) && (object.ReferenceEquals (this.input, input))) {
					return true;
				}
				return false;
			}

		}

		public Connections ()
		{
			connections = new List<Connection> ();
		}

		public Connection getConnectionForPort(NodePort port){


			if (port.portType == NodePortType.Out) {
				foreach (Connection conn in connections) {
					if (object.ReferenceEquals (conn.output, port)) {
						return conn;
					}
				}
			}else if(port.portType == NodePortType.In){
				foreach (Connection conn in connections) {
					if (object.ReferenceEquals (conn.input, port)) {
						return conn;
					}
				}

			}
			return null;
		}

		public NodeBase getNextNodeForward(NodePort port){
			//FLOW, IMPULSE
			return port.parentNode.parentGraph.connections.getConnectionForPort(port).input.parentNode;
		}

		public NodeBase getNextNodeBackward(NodePort port){
			//VALUES
			return port.parentNode.parentGraph.connections.getConnectionForPort(port).output.parentNode;
		}

		public List<Connection> connections = new List<Connection> ();

		//equals by value
		public bool isContained (NodePort output, NodePort input)
		{
			// If parameter is null return false:
			if ((object)output == null) {
				return false;
				//should throw error
			}
			if ((object)input == null) {
				return false;
				//should throw error
			}			
			// Return true if we find matching connection

			foreach (Connection conn in connections) {
				if (conn.isEqualByRef (output, input)) {
					return true;
				}
			}

			return false;
		}

		public bool isConnectionCanBeMade (NodePort output, NodePort input)
		{
		
			if (output == null) {
				return false;
			}
			if (input == null) {
				return false;
			}
			if (input == output) {
				return false;
			}
			if (input.getParentNode () == output.getParentNode ()) {
				return false;
			}
			if (input.getConnectionType () != output.getConnectionType ()) {
				return false;
			}
			if (input.getPortType () == output.getPortType ()) {
				return false;
			}
			if (isContained (output, input)) {
				return false;
			}
		
			return true;
		}

		public bool isPortOccupied (NodePort port)
		{
			foreach (Connection conn in connections) {
				if ((object.ReferenceEquals (conn.output, port)) || (object.ReferenceEquals (conn.input, port))) {
					return true;
				}
			}
			return false;
		}

		public void deleteAllConnectionRelatedOutput (NodePort output)
		{
			//delete all connection where this port exist as output
			NodePort compInput;

			for (int i = connections.Count - 1; i >= 0; i--) {
				if (object.ReferenceEquals (connections [i].output, output)) {

					compInput = connections [i].input;


					connections.Remove (connections [i]);
					//look at connections where conn.input was mod and set occupied
					//better take a look at those which had work on them
					if (isPortOccupied (compInput)) {
						compInput.occupied = true;
					} else {
						compInput.occupied = false;
					}
				}
			}
			output.occupied = false;

		}

		public void deleteAllConnectionRelatedInput (NodePort input)
		{
			NodePort compOutput;
			//delete all connection where this port exist as input
			for (int i = connections.Count - 1; i >= 0; i--) {
				if (object.ReferenceEquals (connections [i].input, input)) {

					compOutput = connections [i].output;

					connections.Remove (connections [i]);

					if (isPortOccupied (compOutput)) {
						compOutput.occupied = true;
					} else {
						compOutput.occupied = false;
					}
				}
			}
			input.occupied = false;
			
		}

		public void AddConnection (NodePort output, NodePort input)
		{
			//obj == null akkor return;
			if (((object)output == null) || ((object)input == null)) {
				return;
			}
			//same objects? by reference
			if (object.ReferenceEquals (output, input)) {
				return;
			}
			if (output.parentNode.parentGraph.wantsConnection != true) {
				return;
			}
			if (output.getPortType () != NodePortType.Out) {
				return;
			}
			if (input.getPortType () != NodePortType.In) {
				return;
			}

			if (isConnectionCanBeMade (output, input)) {
				if (output.getConnectionType () == NodeConnectionType.FLOW) {
					
					//Flow outputs only have 1 output line
					
					//delete all connection (should be only 1) where this port exist
					deleteAllConnectionRelatedOutput (output);

					//add port
					//connections.Add (new Connection (output, input));////

					Connection newConn = new Connection (output, input);
					connections.Add (newConn);

					output.occupied = true;
					input.occupied = true;
					
					return;
				} else if ((input.getConnectionType () != NodeConnectionType.FLOW) && (input.getConnectionType () != NodeConnectionType.IMP)) {
					//értékek esetén, tehát nem flow vagy impulse esetén, az inputba csak egy bejövő lehetséges

					//delete all connection (should be only 1) where this port exist
					deleteAllConnectionRelatedInput (input);

					//add port
					Connection newConn = new Connection (output, input);
					connections.Add (newConn);

					output.occupied = true;
					input.occupied = true;

				} else {
					Connection newConn = new Connection (output, input);
					connections.Add (newConn);

					output.occupied = true;
					input.occupied = true;
				}
			}


			return;
		}

		public void DrawInputLines ()
		{
			if (connections.Count == 0) {
				return;
			}
			Vector3 startPos;
			Vector3 endPos;
			Vector3 startTang;
			Vector3 endTang;
			Color color = Color.black;
			Color shadColor = new Color (0, 0, 0, .06f);

			foreach (Connection conn in connections) {

				startPos = new Vector3 (conn.output.portRect.x + 15, conn.output.portRect.y, 0);
				endPos = new Vector3 (conn.input.portRect.x - 15, conn.input.portRect.y, 0);

				startTang = new Vector3 (conn.output.portRect.x + Vector3.right.x * 50, conn.output.portRect.y + Vector3.right.y * 50, 0);
				endTang = new Vector3 (conn.input.portRect.x + Vector3.left.x * 50, conn.input.portRect.y + Vector3.left.y * 50, 0);

				color = Color.black;

				if (conn.output.connectionType == NodeConnectionType.FLOW) {
					color = Color.white;
				} else if (conn.output.connectionType == NodeConnectionType.IMP) {
					color = Color.yellow;
				} else if (conn.output.connectionType == NodeConnectionType.BOOL) {
					color = Color.blue;
				} else {
					color = Color.green;
				}

				for (int i = 0; i < 3; i++) {
					Handles.DrawBezier (startPos, endPos, startTang, endTang, shadColor, null, (i + 1) * 5);
				}
				Handles.DrawBezier (startPos, endPos, startTang, endTang, color, null, 3);

			}

		}
	}
}
