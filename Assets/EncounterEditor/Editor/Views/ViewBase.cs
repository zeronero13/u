using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;

namespace NodeEditor
{
	[Serializable]
	public class ViewBase
	{
	
	#region Public Variables
		public string viewTitle;
		public Rect viewRect;
	#endregion
	
	#region Protected Variables
		protected GUISkin viewSkin;
		protected EncounterNodeGraph curGraph;
	#endregion
	
	#region Constructors
		public ViewBase (string title)
		{
			this.viewTitle = title;
			LoadEditorSkin ();
		}
	#endregion
	
	#region Main Methods
		public virtual void UpdateView (Rect editorRect, Rect percentageRect, Event e, EncounterNodeGraph curGraph, GUISkin viewSkin)
		{

			this.viewSkin = viewSkin;

			if (this.viewSkin == null) {
			
				LoadEditorSkin ();
				return;
			}
			//Set the current Graph
			//this.curGraph = EncounterNodeEditorWindow.getInstance().getCurrentNodeGraph();
			this.curGraph = curGraph;
		
			//Set title of our current graph name
			if (curGraph != null) {
				viewTitle = curGraph.graphName;
			} else {
				viewTitle = "No Graph";
			}
		
			//Update View rectangle
			viewRect = new Rect (editorRect.x * percentageRect.x,
		                     editorRect.y * percentageRect.y,
		                     editorRect.width * percentageRect.width,
		                     editorRect.height * percentageRect.height
			);
		
		}
	
		public virtual void ProcessEvents (Event e)
		{
		}
	#endregion
	
	#region Utility Methods
		protected void LoadEditorSkin ()
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