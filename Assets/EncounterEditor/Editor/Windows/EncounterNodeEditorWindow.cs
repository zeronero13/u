using UnityEngine;
using UnityEditor;
using System.Collections;

namespace NodeEditor
{
	public class EncounterNodeEditorWindow : EditorWindow
	{
		/**
	 * GameEditorMenus -> Encounter Editor -> EncounterNodeEditorWindow.InitEditorWindow();
	 */

	#region Variables
		/**
	 *Static Hogy csak egy legyen belole? ~ Singleton ?  
	 */
		public static EncounterNodeEditorWindow curWindow; /* static var megkapja saját maga egy példányát és nyilvántartja*/
	
		public EncounterNodePropertyView propertyView;
		public EncounterNodeWorkView workView; //tényleges munk.terület ahol a node-ok megjelennek
	
		public EncounterNodeGraph curGraph = null; // gráf amiben tároljuk a node-okat
	
		public float viewPrecentage = 0.75f; // tulaj. ablak mekkora % a teljes editor ablaknak
		public Vector2 scrollPos = Vector2.zero; //ScrollView pos
		public static GUISkin viewSkin;
	#endregion

	#region Main Methods
		public static void InitEditorWindow ()
		{
			curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> (); // egy példány kérése
			curWindow.title = "Encounter Editor";
			LoadEditorSkin ();
			CreateViews ();
		}
	
		void OnEnable ()
		{
			/**
		 * on window launches
		 */
		
		}
	
		void OnDestory ()
		{
		
		
		}
	
		void Update ()
		{
		
		
		}
	
		void OnGUI ()
		{
			//Check for null views
			if (propertyView == null || workView == null) {
				CreateViews ();
				return;	
			}
			//Get and process current events
			Event e = Event.current;
			ProcessEvents (e);

			//Update views, és kirajzoltatás
			workView.UpdateView (position, new Rect (0f, 0f, viewPrecentage, 1f), e, curGraph, viewSkin);
			propertyView.UpdateView (new Rect (position.width, position.y, position.width, position.height),
		                         new Rect (viewPrecentage, 0f, 1f - viewPrecentage, 1f), e, curGraph, viewSkin);

			//ide kihozva a nodok kirajzoltatása
			//DrawBox
			scrollPos = GUI.BeginScrollView (workView.getViewRect (), scrollPos, new Rect (0, 0, 1000, 1000));

			BeginWindows ();
			//DrawGraph, and all the other stuff
			if (curGraph != null) {
				curGraph.UpdateGraphGUI (e, workView.getViewRect (), workView.getEditorSkin ());

			}
			EndWindows ();
			GUI.EndScrollView ();
			//...
			Repaint ();
		
		}
	#endregion

	#region Utility Methods
		public static EncounterNodeEditorWindow getInstance ()
		{
			return curWindow;
		}

		public EncounterNodeGraph getCurrentNodeGraph ()
		{
			return curGraph;
		}

		static void CreateViews ()
		{
		
			if (curWindow != null) {
				curWindow.propertyView = new EncounterNodePropertyView ();
				curWindow.workView = new EncounterNodeWorkView ();
			} else {
				curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
			
			}
		}

		void ProcessEvents (Event e)
		{
		
		
		}

		/**
	 * 
	 * 
	 */

		public static void CreateNewGraph (string name)
		{
		
			EncounterNodeGraph curGraph = (EncounterNodeGraph)ScriptableObject.CreateInstance<EncounterNodeGraph> ();
			if (curGraph != null) {
			
				curGraph.graphName = name;
				//curGraph.InitGraph ();
			
				//AssetDatabase.CreateAsset (curGraph, "Assets/NodeEditor/Database/" + name + ".asset");
				AssetDatabase.CreateAsset (curGraph, "Assets/EncounterEditor/Database/" + name + ".asset");
				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();

				curGraph.InitGraph ();
			
				EncounterNodeEditorWindow curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
			
				if (curWindow != null) {
					curWindow.curGraph = curGraph;
				}

				curWindow.curGraph.startNode=curWindow.CreateNode (NodeType.Start, new Vector2(70f,70f));

				AssetDatabase.SaveAssets ();
				AssetDatabase.Refresh ();

			} else {
				EditorUtility.DisplayDialog ("Node message:", "Unable to create graph", "OK");
			}
		
		}

		public static void LoadGraph ()
		{
		
			EncounterNodeGraph curGraph = null;
			//string graphPath = EditorUtility.OpenFilePanel ("Load Graph", Application.dataPath + "/NodeEditor/Database/", "");
			string graphPath = EditorUtility.OpenFilePanel ("Load Graph", Application.dataPath + "/EncounterEditor/Database/", "");

			if (graphPath.Length < 1) {
				return; 
			}

			int appPathLen = Application.dataPath.Length;
			string finalPath = graphPath.Substring (appPathLen - 6);
		
			curGraph = (EncounterNodeGraph)AssetDatabase.LoadAssetAtPath (finalPath, typeof(EncounterNodeGraph));
			//Debug.Log (finalPath);
			if (curGraph != null) {
			
				EncounterNodeEditorWindow curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
			
				if (curWindow != null) {
					curWindow.curGraph = curGraph;
					//curWindow.workView.setGraph (curGraph);
				}
				//load in connection between nodes
				/*string connectionPath = "Assets/EncounterEditor/Database/" + curGraph.graphName + "_Connections.asset";
				Connections curConn = (Connections)AssetDatabase.LoadAssetAtPath (connectionPath, typeof(Connections));
				if (curConn != null) {
					curGraph.connections = curConn;
				}*/

			
			} else {
				EditorUtility.DisplayDialog ("Node message", "Unable load current path!", "OK");
			}
		}
	
		public static void UnloadGraph ()
		{
			EncounterNodeEditorWindow curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
		
			if (curWindow != null) {
				curWindow.curGraph = null;
			}
		}


		public NodeBase CreateNode (NodeType nodeType, Vector2 mousePos)
		{
		
			if (curGraph != null) {
			
				NodeBase curNode = null;
				switch (nodeType) {

				case NodeType.Text:
					curNode = (TextNode)ScriptableObject.CreateInstance<TextNode> ();
					curNode.nodeName = "New Text";
					break;
				case NodeType.Dialog:
					curNode = (DialogNode)ScriptableObject.CreateInstance<DialogNode> ();
					curNode.nodeName = "New Dialog";
					break;
				case NodeType.Branching:
					curNode = (BranchingNode)ScriptableObject.CreateInstance<BranchingNode> ();
					curNode.nodeName = "Branching Dialog";
					break;
				case NodeType.Start:
					curNode = (StartNode)ScriptableObject.CreateInstance<StartNode> ();
					curNode.nodeName = "New Start";
					break;
				case NodeType.End:
					curNode = (EndNode)ScriptableObject.CreateInstance<EndNode> ();
					curNode.nodeName = "New End";
					break;
				default:
					break;
				}
			
				if (curNode != null) {

					curNode.parentGraph = curGraph;
					curGraph.nodes.Add (curNode);
				
					AssetDatabase.AddObjectToAsset (curNode, curGraph);
					AssetDatabase.SaveAssets ();
					AssetDatabase.Refresh ();

					curNode.InitNode ();
					curNode.nodeRect.x = mousePos.x;
					curNode.nodeRect.y = mousePos.y;

				}
				return curNode;
			}
			return null;
		}

		public void DeleteNode (int nodeID)
		{
		
			if (curGraph != null) {
			
				if (curGraph.nodes.Count >= nodeID) {
				
					NodeBase deleteNode = curGraph.nodes [nodeID];
					if (deleteNode != null) {
					
						curGraph.nodes.RemoveAt (nodeID);
						GameObject.DestroyImmediate (deleteNode, true);
					
						AssetDatabase.SaveAssets ();
						AssetDatabase.Refresh ();
					}
				}
			}
		}

		protected static void LoadEditorSkin ()
		{
			viewSkin = (GUISkin)Resources.Load ("GUISkins/EditorSkins/NodeEditorSkin");
		}
	
		public GUISkin getEditorSkin ()
		{
			return viewSkin;
		}
	#endregion
	}
}