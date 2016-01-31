using UnityEngine;
using UnityEditor;
using System.Collections;

namespace NodeEditor
{
	public static class GameEditorMenus
	{
		[MenuItem("Game Editor/Launch Encounter Editor")]
		public static void InitNodeEditor ()
		{
			/**
		 * Create a Window for us
		 */
			EncounterNodeEditorWindow.InitEditorWindow ();
		
		}
	
	}
}
