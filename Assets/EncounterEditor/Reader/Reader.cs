using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using NodeEditor;
/*public class ReaderCallback: Action{

}*/

public delegate void ReaderCallback();


public class ReaderEvents{

	//atomic actions; pl. Reader.events.onStarted+= callbackDoText; public void callbackDoText(){...code...};
	public Action onStarted;
	public Action onEnded;
	public Action<TextData> onTextPhase;
	public Action<List<DialogOption>> onDialogPhase;

}

public sealed class Reader {

	private void dummyMethod(){}
	private void dummyMethodTextPhase(TextData data){}
	private void dummyMethodDialogPhase(List<DialogOption> data){}

	#region Singleton 
	private static readonly Reader instance = new Reader();
	static Reader()
	{

	}
	
	private Reader()
	{
		events.onStarted += dummyMethod;
		events.onEnded += dummyMethod;
		events.onTextPhase += dummyMethodTextPhase;
		events.onDialogPhase += dummyMethodDialogPhase;
	}
	
	public static Reader Instance
	{
		get
		{
			return instance;
		}
	}
	#endregion

	#region Variables
	public ReaderEvents events= new ReaderEvents(); //ha nincs semmi events-ben akkor invoke esetén dobhat null exceptiont!
	private EncounterNodeGraph curGraph;
	private NodeBase curNode;
	private ReaderCallback callback;
	#endregion
	//public static void Start(int i, Action callback){}//start id i storyboard, load in graph...
	public static void Start(int i, ReaderCallback _callback){

		instance.curNode = null;

		instance.callback = _callback;
		LoadGraph("default.asset");

		//events.onTextPhase (new ReaderTextData("we have a text phase!"));

		instance.curNode = instance.curGraph.startNode;

		if (instance.curNode == null)
			Debug.LogError ("Error curNode instance is null");
		if (instance.curGraph == null)
			Debug.LogError ("Error curGraph is null");
		if (instance.curGraph.startNode == null)
			Debug.LogError ("Error startNode is null");
		if (instance.events == null)
			Debug.LogError ("Error events is null");
		if (instance.events.onStarted == null)
			Debug.LogError ("Error events.onStarted is null");
		Continue ();


		//After everything is done return and do a callback;
		//callback ();
	}
	//(dialog id, callbackfunction) dialog id to load/start, callback function called when dialog has ended

	//...public void onTextPhase(DialoguerTextData data){_text=data.text;_choices=data.choices;}

	//public static void ContinueDialoge(); //move forward at the next text node, in def. first option is selected as path
	//public static void ContinueDialoge(int i); move forward at the selected index
	private static void LoadGraph(string path){

		Reader.Instance.curGraph = (EncounterNodeGraph)UnityEditor.AssetDatabase.LoadAssetAtPath ( "Assets/EncounterEditor/Database/"+path, typeof(EncounterNodeGraph));
		if (Reader.Instance.curGraph != null) {

		} else {
			Debug.LogError ( "Unable load current path!");
		}
	}

	// Continue the current dialogue after a Regular or Branched Text node
	public static void Continue(int i=0, bool success=true){
		i = Math.Abs (i);

		if ((instance.curNode == null)) {

			Debug.LogError ("Error: curNode NullException");
			return;

		} 
		
		//invokeEvents ();

		if (instance.curNode.nodeType == NodeType.Start) {

			instance.events.onStarted();
			instance.curNode=instance.curNode.Next();
			invokeEvents();
			//Reader.Continue();
			return;

		} else if (instance.curNode.nodeType == NodeType.Text) {

			instance.curNode=instance.curNode.Next();
			invokeEvents();
			return;

		} else if (instance.curNode.nodeType == NodeType.Dialog) {

			instance.curNode = ((DialogNode)instance.curNode).Next (i, success);
			invokeEvents();
			return;

		} else if (instance.curNode.nodeType == NodeType.End) {

			instance.curNode=instance.curNode.Next();
			invokeEvents();
			return;

		} else {
			//esetek amikor auto. tovább kellene lépni, pl. flow.conditional.branching

			//TODO: events for them
			instance.curNode=instance.curNode.Next();
			invokeEvents();
			Reader.Continue();
			return;
		}


		//invoke()
	}

	private static void invokeEvents(){
		//invoke event

		if (instance.curNode.nodeType == NodeType.Start) {

			Reader.Instance.events.onStarted ();
			/*Nem lehetséges, skip*/
					
		} else if (instance.curNode.nodeType == NodeType.Text) {
			
			TextNode node = instance.curNode as TextNode;
			instance.events.onTextPhase (node.textData);
			
		} else if (instance.curNode.nodeType == NodeType.Dialog) {
			
			//TODO event kiváltás?, adataok küldése callbacken
			//új event megfelelő modon kezelni az adatokat
			DialogNode node = instance.curNode as DialogNode;
			if (null == node) {
				Debug.LogError ("Error: current node reference is null");
			}
			instance.events.onTextPhase (node.textData);
			instance.events.onDialogPhase (node.options);
			
		} else if (instance.curNode.nodeType == NodeType.End) {

			instance.curNode = null;
			instance.events.onEnded ();
			if (instance.callback != null) {
				instance.callback ();
			}
			
		} else {
			//egyebek pl. flow.conditional.branching? metadata stb
			//TODO
		}

	}



}
