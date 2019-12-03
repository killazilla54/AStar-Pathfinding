using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	public bool walkable;
	public int gCost;
	public int hCost;
	public int fCost{
		get{
			return gCost + hCost;
		}
	}
	public int gridX;
	public int gridY;
	public Node parent;

	//Remove
	public Material obstacleMat;
	public Material defaultMat;

	//Remove these
	public TextMesh gTxt;
	public TextMesh hTxt;
	public TextMesh fTxt;

	public Node(bool _walkable, int _gridX, int _gridY){
		walkable = _walkable;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int CompareTo(Node otherNode){
		int compare = fCost.CompareTo (otherNode.fCost);
		if(compare == 0){
			compare = hCost.CompareTo (otherNode.hCost);
		}
		return -compare;//why?

	}

	//TEMP STUFF REFINE LATER.  Move to TileEffects
	public void SetAsObstacle(){
		walkable = false;
		gameObject.GetComponent<Renderer>().material = obstacleMat;
	}

	public void SetAsWalkable(){
		walkable = true;
		gameObject.GetComponent<Renderer>().material = defaultMat;
	}

	public void ToggleTile(){
		walkable = !walkable;
		Material newMat;
		if(!walkable){
			newMat = obstacleMat;
		} else {
			newMat = defaultMat;
		}
		gameObject.GetComponent<Renderer>().material = newMat;
	}
}
