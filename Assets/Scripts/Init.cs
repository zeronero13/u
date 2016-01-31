using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Init : MonoBehaviour {

	void Awake(){
		//Reader.Init ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(GUI.Button(new Rect(10,250,100,20), "Start the graph!")){
			enabled=false;
			Reader.Start(0, readerCallback);


			//Reader.events.onStarted+=readerCallback;
		}


	}

	public void readerCallback(){
		enabled = true;
		Debug.Log("after finished callback from reader");
	}
}
