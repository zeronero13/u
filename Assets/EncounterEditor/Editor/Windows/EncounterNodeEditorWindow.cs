using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
	
		public EncounterNodeGraph curGraph = null; // gráf amin éppen dolgozunk
	
		public float viewPrecentage = 0.75f; // tulaj. ablak mekkora % a teljes editor ablaknak
		public Vector2 scrollPos = Vector2.zero; //ScrollView pos

		public static GUISkin viewSkin;
	#endregion

	#region Main Methods
		public static void InitEditorWindow ()
		{
			curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> (); // egy példány kérése
			curWindow.titleContent.text = "Encounter Editor";
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

			EncounterNodeGraph curGraph = new EncounterNodeGraph();
			if (curGraph != null) {
			
				curGraph.graphName = name;
				curGraph.InitGraph ();
			
				EncounterNodeEditorWindow curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
			
				if (curWindow != null) {
					curWindow.curGraph = curGraph;
				}

				NodeBase nnode = curWindow.CreateNode (NodeType.Start, new Vector2(70f,70f));
				curWindow.curGraph.startNode=nnode;
				Save ();//save data file

			} else {
				EditorUtility.DisplayDialog ("Node message:", "Unable to create graph", "OK");
			}
		
		}

		public static void LoadGraph ()
		{
		
			EncounterNodeGraph curGraph = null;
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open(Application.dataPath + "/EncounterEditor/Database/data.dat",FileMode.Open);
		
			curGraph = (EncounterNodeGraph)bf.Deserialize (fs);
			fs.Close ();

			if (curGraph != null) {
			
				EncounterNodeEditorWindow curWindow = (EncounterNodeEditorWindow)EditorWindow.GetWindow<EncounterNodeEditorWindow> ();
			
				if (curWindow != null) {
					curWindow.curGraph = curGraph;
				}
			
			} else {
				EditorUtility.DisplayDialog ("Node message", "Unable load data.dat", "OK");
			}
		}
	
		public static void UnloadGraph ()
		{
			Save ();

			if (curWindow != null) {
				curWindow.curGraph = null;
			}
				
		}

		public static void Save(){
			Debug.LogWarning ("Saving data to file");

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Create ("Assets/EncounterEditor/Database/data.dat");

			bf.Serialize (fs,EncounterNodeEditorWindow.curWindow.curGraph);
			fs.Close();
		}


		public NodeBase CreateNode (NodeType nodeType, Vector2 mousePos)
		{
		
			if (curGraph != null) {
			
				NodeBase curNode = null;
				switch (nodeType) {

				case NodeType.Text:
					curNode = new TextNode ();
					curNode.nodeName = "New Text";
					break;
				case NodeType.Dialog:
					curNode = new DialogNode ();
					curNode.nodeName = "New Dialog";
					break;
				case NodeType.Branching:
					curNode = new BranchingNode ();
					curNode.nodeName = "Branching Dialog";
					break;
				case NodeType.Start:
					curNode = new StartNode ();
					curNode.nodeName = "New Start";
					break;
				case NodeType.End:
					curNode = new EndNode ();
					curNode.nodeName = "New End";
					break;
				case NodeType.Bool:
					curNode = new BoolNode ();
					curNode.nodeName = "Bool Node";
					break;
				default:
					break;
				}
			
				if (curNode != null) {

					curNode.parentGraph = curGraph;
					curGraph.nodes.Add (curNode);

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
					deleteNode.deleteAllConnection ();

					if (deleteNode != null) {
					
						curGraph.nodes.RemoveAt (nodeID);
						deleteNode= null;
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