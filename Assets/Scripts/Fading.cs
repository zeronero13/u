using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	//public GUITexture guiTexture;
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.5f;

	public float maxAlpha = 1.00f;

	private int drawDepth = -1000;
	private int fadeDir = -1;

	public Color color=Color.black;
	public Color interpolColor= Color.black;

	private bool fading=false;

	void OnGUI(){

		//alpha += fadeDir * fadeSpeed * Time.deltaTime;
		//alpha = Mathf.Clamp01 (alpha);


		//GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

		if (fading) {
			if(fadeDir >= 0){
				if(color.a >= maxAlpha * 0.93f){
					color.a = maxAlpha;
					fading = false;
				}else{
					FadeToColor();
				}
			}else{
				if(color.a <= 0.07f){
					color.a = 0.0f;
					fading = false;
				}else{
					FadeToClear();
				}
			}
		}

		GUI.color = this.color;
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect(0,0, Screen.width, Screen.height), fadeOutTexture);
	}


	public void BeginFadeToClear(){
		enabled=true;
		fading = true;

		fadeDir = -1;

	}

	public void BeginFadeToTexture(){
		enabled=true;
		fading = true;
		
		fadeDir = +1;

	}



	void FadeToClear ()
	{
		//Debug.Log("Begin to fade to clear");
		// Lerp the colour of the texture between itself and transparent.
		color = Color.Lerp(color, Color.clear, fadeSpeed * Time.deltaTime);
		//print ("I'm running!");
	}
	

	void FadeToColor ()
	{
		//Debug.Log("Begin to fade to texture");
		// Lerp the colour of the texture between itself and black.
		//guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
		color = Color.Lerp(color, interpolColor, fadeSpeed * Time.deltaTime);

		//print ("I'm running!");
	}

	void OnLevelWasLoaded(int level) {
		//BeginFadeToTexture ();
		//Debug.Log ("Start to fade to black!");
	}
}
