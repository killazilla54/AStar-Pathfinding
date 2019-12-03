using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathManager : MonoBehaviour {


	public Node startNode;
	public Node targetNode;
	public Material startMat;
	public Material targetMat;

	public string mode;

	public Text modeText;

	public Text timeText;

	public Pathfinding pathfinding;

	public MapGrid mapGrid;

	void Update(){
		if(mode == "obstacles"){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log("Mouse down");
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if(hit){
					if(hitInfo.transform.tag == "tile"){
						hitInfo.transform.GetComponent<Node>().ToggleTile();
						// if(hitInfo.transform.GetComponent<Node>().walkable){
						// 	hitInfo.transform.GetComponent<Node>().SetAsObstacle();
						// } else {
						// 	hitInfo.transform.GetComponent<Node>().SetAsWalkable();
						// }
					}
				} else {
					Debug.Log("No hitz");
				}
			}
		} else if(mode == "entry"){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log("Mouse down");
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if(hit){
					if(hitInfo.transform.tag == "tile" && hitInfo.transform.GetComponent<Node>().walkable == true){
						startNode = hitInfo.transform.GetComponent<Node>();
						startNode.transform.GetComponent<Renderer>().material = startMat;
						mode = "target";
						// hitInfo.transform.GetComponent<Node>().walkable = !hitInfo.transform.GetComponent<Node>().walkable;
					}
				} 
			}
		} else if(mode == "target"){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log("Mouse down");
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if(hit){
					if(hitInfo.transform.tag == "tile" && hitInfo.transform.GetComponent<Node>().walkable == true){
						targetNode = hitInfo.transform.GetComponent<Node>();
						targetNode.transform.GetComponent<Renderer>().material = targetMat;
						mode = "";
						// hitInfo.transform.GetComponent<Node>().walkable = !hitInfo.transform.GetComponent<Node>().walkable;
					}
				} 
			}
		}
		modeText.text = "mode = " + mode;
		if(Input.GetKeyDown(KeyCode.P)){
			RunAStar();
		}
	}

	public void GenerateMap(MapSize mapSize){
		mapGrid.CreateGrid(mapSize.x, mapSize.y);
		RTSCamera cam = Camera.main.GetComponent<RTSCamera>();
		cam.minY = mapSize.minScrollY;
		cam.maxY = mapSize.maxScrollY;
		cam.panLimit = mapSize.panLimit;
	}

	public void SetObstacleMode(){
		mode = "obstacles";
	}

	public void SetEntryMode(){
		mapGrid.ClearBoard();
		mode = "entry";
	}

	public void RunAStar(){
		mapGrid.ClearBoard();
		pathfinding.FindPath (startNode,targetNode);
		timeText.text = "Path found in " + pathfinding.time + " ms";
	}

	public void RunSlowAStar(){
		mapGrid.ClearBoard();
		IEnumerator coroutine = pathfinding.FindPathSlow(startNode, targetNode);
		StartCoroutine(coroutine);
	}
}
