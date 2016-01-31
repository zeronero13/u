using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gui : MonoBehaviour {

	public GameObject fadingObject;

	public enum GameStatus{
		Gamble,
		Check,
		Combat,
		Narration
	}

	public class GambleStatusHolder{



	}
	GambleStatusHolder gamble= new GambleStatusHolder();
	GameStatus gameStatus= GameStatus.Narration;

	bool _showing;
	string _text;
	List<NodeEditor.DialogOption> options;

	// Use this for initialization
	void Start () {

		Reader.Instance.events.onStarted += onStarted;
		Reader.Instance.events.onEnded += onEnded;
		Reader.Instance.events.onTextPhase += onTextPhase;
		Reader.Instance.events.onDialogPhase += onDialogPhase;

	}

	void OnGUI(){
		if (!_showing) {
			return;
		}
		Fading fading = fadingObject.GetComponent<Fading> ();

		GUI.Box (new Rect(10,10,350,150), this._text);

		if(options==null){
			if(GUI.Button(new Rect(10,160,250,20), "Continue!")){
				Reader.Continue();
			}
		}else if(options!=null && options.Count>0){
			for(int i=0;i<options.Count;++i){
				if(GUI.Button(new Rect(10,160+20*i,250,20), options[i].optionText)){

					if(gameStatus==GameStatus.Narration){

						if(options[i].actionOp==GameActionTypeEnums.Gamble){

							fading.BeginFadeToTexture();
							//fadingObject.GetComponent<Fading>().BeginFadeToClear();
							//fadingObject.GetComponent<Fading>().BeginFadeToTexture();
							gameStatus=GameStatus.Gamble;

						}else if(options[i].actionOp==GameActionTypeEnums.Combat){
							fading.BeginFadeToTexture();

						}else if(options[i].actionOp==GameActionTypeEnums.Check){
							fading.BeginFadeToTexture();

						}else{
							/*if ==None*/
							options=null;
							Reader.Continue(i);
							return;
						}
					}else{
						/*there is a need to do stuff*/
						if(gameStatus==GameStatus.Gamble){

						}else if(gameStatus==GameStatus.Combat){
						}else if(gameStatus==GameStatus.Check){
						}

					}

				}
			}
		}

		if(gameStatus==GameStatus.Gamble){


		}else if(gameStatus==GameStatus.Combat){
		}else if(gameStatus==GameStatus.Check){
		}

	}

	public void onStarted(){
		_showing = true;
		Debug.Log("onStarted() is invoked");
	}

	public void onEnded(){
		_showing = false;
		Debug.Log("onEnded() is invoked");
	}

	public void onTextPhase(TextData data){
		//Debug.Log (data.text);
		this._text = data.text;
		Debug.Log("onText() is invoked");
		//_choices=data.choices;
	}

	public void onDialogPhase(List<NodeEditor.DialogOption> data){

		options = data;
		Debug.Log("onDialog() is invoked");
	}


}
