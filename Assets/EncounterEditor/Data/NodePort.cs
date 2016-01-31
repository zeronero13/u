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
	public class NodePort: ScriptableObject
	{
		//esetleges hiba: unity serializable viselkedes, nem szereti a nem public attr.-kat, nem public attr. nem fog serialize a disc-re
		//public bool isOccupied = false;
		//public NodeBase outputNode;
	
		public NodeBase parentNode; //which node is this output belongs to
		public NodeConnectionType connectionType; //what king of connection is it? flow, bool, int, vect2, impulse...?
		public NodePortType portType; //its an in/out port?
		public Rect portRect;

		public bool occupied;

	
		public NodePort (NodePortType _portType, NodeConnectionType _connectType, NodeBase _parentNode)
		{
			portType = _portType;
			connectionType = _connectType;
			parentNode = _parentNode;
			portRect = new Rect (0.0f, 0.0f, 0.0f, 0.0f);
		}

		#if UNITY_EDITOR
		public static NodePort createAsset (NodePortType _portType, NodeConnectionType _connectType, NodeBase _parentNode)
		{

			NodePort nport = (NodePort)ScriptableObject.CreateInstance<NodePort> ();

			nport.portType = _portType;
			nport.connectionType = _connectType;
			nport.parentNode = _parentNode;
			nport.portRect = new Rect (0.0f, 0.0f, 0.0f, 0.0f);
			
			AssetDatabase.AddObjectToAsset (nport, _parentNode);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			return nport;
		}

		public static NodePort createAsset (NodePortType _portType, NodeConnectionType _connectType, NodeBase _parentNode, Connections _conns)
		{
			
			NodePort nport = (NodePort)ScriptableObject.CreateInstance<NodePort> ();
			
			nport.portType = _portType;
			nport.connectionType = _connectType;
			nport.parentNode = _parentNode;
			nport.portRect = new Rect (0.0f, 0.0f, 0.0f, 0.0f);
			
			AssetDatabase.AddObjectToAsset (nport, _conns);
			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
			
			return nport;
		}
		#endif

		public bool isOccupied ()
		{

			return occupied;
		}
	
		public NodePortType getPortType ()
		{
			return portType;
		}
	
		public NodeConnectionType getConnectionType ()
		{
			return connectionType;
		}
	
		public NodeBase getParentNode ()
		{
			return parentNode;
		}


		public GUIStyle GetButtonStyle (GUISkin portSkin)
		{


			if (this.getConnectionType () == NodeConnectionType.FLOW) {

				if (!this.isOccupied ()) {
					return portSkin.GetStyle ("NodeFlowDefault");
				} else {
					return portSkin.GetStyle ("NodeFlowActive");
				}

			} else
			if (this.getConnectionType () == NodeConnectionType.BOOL) {
				
				if (!this.isOccupied ()) {
					return portSkin.GetStyle ("NodeBoolDefault");
				} else {
					return portSkin.GetStyle ("NodeBoolActive");
				}
				
			} else
			if (this.getConnectionType () == NodeConnectionType.IMP) {
				
				if (!this.isOccupied ()) {
					return portSkin.GetStyle ("NodeImpulseDefault");
				} else {
					return portSkin.GetStyle ("NodeImpulseActive");
				}
				
			} else {
				
				if (!this.isOccupied ()) {
					return portSkin.GetStyle ("NodeValueDefault");
				} else {
					return portSkin.GetStyle ("NodeValueActive");
				}
				
			}
			//return portSkin.GetStyle ("NodeFlowDefault");


		}

		protected static void SetPortPosition (NodeBase node, NodePort port)
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

		public static void DrawPortBtn (NodePort port, GUISkin portSkin)
		{
			if (port.portType == NodePortType.Out) {
				if (GUILayout.Button ("", port.GetButtonStyle (portSkin))) {
					
					if (port.parentNode.parentGraph != null) {
						
						SetWantsConnection (port.parentNode, port);
					}
				}
				SetPortPosition (port.parentNode, port);

			} else {

				if (GUILayout.Button ("", port.GetButtonStyle (portSkin))) {
					if (port.parentNode.parentGraph != null) {
						
						if (port.parentNode.parentGraph.wantsConnection == true) {

							port.parentNode.parentGraph.connections.AddConnection (port.parentNode.parentGraph.connectionNodePort, port);
							SetNullWantsConnection (port);

						}
						
					}
				}
				SetPortPosition (port.parentNode, port);
			}

		}

		public static void SetWantsConnection (NodeBase node, NodePort port)
		{
			
			port.parentNode.parentGraph.wantsConnection = true;
			port.parentNode.parentGraph.connectionNode = node;
			port.parentNode.parentGraph.connectionNodePort = port;
			
		}
		
		public static void SetNullWantsConnection (NodePort port)
		{
			
			port.parentNode.parentGraph.wantsConnection = false;
			port.parentNode.parentGraph.connectionNode = null;
			port.parentNode.parentGraph.connectionNodePort = null;
			
		}

	}
}
