using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	int index = 0;
	public GameObject cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.RightArrow) && index != 2){
			cam.transform.position += Vector3.right*100;
			index++;
		} else if(Input.GetKeyDown(KeyCode.LeftArrow) && index != 0){
			cam.transform.position -= Vector3.right*100;
			index--;
		}
	}

	public void ChooseManhatten(){
		TransitionToScene("ManhattenDemo");
	}

	public void ChooseDiagonal(){
		TransitionToScene("DiagonalDemo");
	}

	public void ChooseAnyAngle(){
		TransitionToScene("AnyAngleDemo");
	}

	private void TransitionToScene(string sceneName){
		SceneManager.LoadScene(sceneName);
	}
}
