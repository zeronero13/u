using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class DataManager
{
	#region Public Variables

	public List<NodeEditor.EncounterNodeGraph> graphList; //TODO:kiegészíteni valamilyen complex containerrel ami többet tud mondani?

	#endregion

	#region Constructor
	public DataManager ()
	{
	}
	#endregion

	public void LoadDataFiles(){
		
	}
	#if UNITY_EDITOR
	public void SaveDataFiles(){

	}
	#endif
}


