using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {

	public GameObject loadingImage; /*image GameObject will be assigned to this in the scene in editor*/

	void Awake () {
	
	}

	void Start(){
		/*After Awake() is done*/
		//The Awake function is called on all objects in the scene before any object's Start function is called.

		loadingImage.SetActive(true);
		Application.LoadLevel(1);
		/*if (Application.isLoadingLevel) {
			Debug.Log("yes its being loaded");
		}*/

	}

}
