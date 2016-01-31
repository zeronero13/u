using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance=null;

	public BoardManager boardManagerScript;
	
	int level=3;
	
	void Awake(){

		if (instance == null) {
			instance = this;
		} else if(instance!=this){
			Destroy(gameObject);
		}

		DontDestroyOnLoad (gameObject);

		boardManagerScript = GetComponent<BoardManager> ();
		InitGame ();
		
	}
	
	public void InitGame(){
		
		boardManagerScript.SetupScene (level);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
