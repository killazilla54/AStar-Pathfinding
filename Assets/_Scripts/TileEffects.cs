using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffects : MonoBehaviour {
	Renderer rend;
	 private Color startcolor;
	 private Color white = Color.white;
	 private Color path = Color.red;
	 private Color open = Color.cyan;
	 private Color closed = Color.yellow;
	 public Color hoverPath;

	void Start(){
		rend = GetComponent<Renderer>();
		if(!GetComponent<Node>().walkable){
			SetObstacleMat();
		} else {
			SetDefaultMat();
		}
	}
	void OnMouseEnter() {
		startcolor = GetComponent<Renderer>().material.color;
		if(GetComponent<Node>().walkable){
			GetComponent<Renderer>().material.color = Color.green;
		}
	}
	void OnMouseExit() {
		GetComponent<Renderer>().material.color = startcolor;
	}

	public void SetDefaultMat(){
		rend.material.color = white;
	}
	public void SetOpenMat(){
		rend.material.color = open;
	}
	public void SetClosedMat(){
		rend.material.color = closed;
	}
	public void SetPathMat(){
		startcolor = path;//This is because startColor is not path when click happens so reverts to white
		rend.material.color = path;
	}
	public void SetObstacleMat(){
		startcolor = Color.black;
		rend.material.color = Color.black;
	}
}
