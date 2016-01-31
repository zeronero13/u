using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FunctionList : MonoBehaviour
{

	List<Action> eventA = new List<Action> (); //Action<>: return is void, and no param
	List<Action<string>> eventB = new List<Action<string>> (); //Action<...>: param1, param2, ...paramN; Action=> return is void
	List<Func<string, int>> eventC = new List<Func<string, int>> (); //Func<...>: (param1,param2, ...paramN, returnX)


	// Use this for initialization
	void Start ()
	{

		//eventA += doSomethingCall;
		//eventQ += doSomethingCall;

		eventA.Add (doSomethingCallAction);
		eventB.Add (doSomethingCallStringAction);
		eventC.Add (doSomethingCallFunction);

		//foreach (var action in eventA)
		//action.Invoke ();

		//eventA [0].Invoke ();
		eventB [0].Invoke ("Action: String param");

		int ret = eventC [0].Invoke ("Func: string param, int return");
		Debug.Log ("and the return is: " + ret);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void doSomethingCallAction ()
	{
		Debug.Log ("ok");
	}

	void doSomethingCallStringAction (string s)
	{
		Debug.Log ("ok: " + s);
	}

	int doSomethingCallFunction (string s)
	{
		Debug.Log ("ok: " + s);
		return 7;
	}
}
