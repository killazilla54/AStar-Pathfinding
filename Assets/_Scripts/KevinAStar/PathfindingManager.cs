using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;


public class PathfindingManager : MonoBehaviour {


	public Node startNode;
	public Node targetNode;
	public Material startMat;
	public Material targetMat;
	public Material openMat;
	public Material closedMat;
	public Material pathMat;

	public string mode;

	public Text modeText;

	public Text timeText;

	public AstarPathfinding pathfinding;

	public IGrid mapGrid;

	void Start(){
		pathfinding = GetComponent<AstarPathfinding>();
	}

	void Update(){
		if(mode == "obstacles"){
			if(Input.GetMouseButtonDown(0)){
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
				if(hit){
					if(hitInfo.transform.tag == "tile"){
						hitInfo.transform.GetComponent<Node>().ToggleTile();
					}
				} 
			}
		} else if(mode == "entry"){
			if(Input.GetMouseButtonDown(0)){
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
		mapGrid.Create(mapSize.x, mapSize.y);
		RTSCamera cam = Camera.main.GetComponent<RTSCamera>();
		cam.minY = mapSize.minScrollY;
		cam.maxY = mapSize.maxScrollY;
		cam.panLimit = mapSize.panLimit;
	}

	public void SetObstacleMode(){
		mode = "obstacles";
	}

	public void SetEntryMode(){
		mapGrid.Clear();
		mode = "entry";
	}

	public void RunAStar(){
		float time = 0;
		mapGrid.Clear();
		Stopwatch sw = new Stopwatch();
		sw.Start();


		Stack<Node> path = pathfinding.FindPath (mapGrid,startNode,targetNode);
		sw.Stop();
        time = sw.ElapsedMilliseconds;

		timeText.text = "Path found in " + time + " ms";

		//TODO: Implement an entity.
		//Entity will receive the stack, and interally call a coroutine to move from
		//node to node in the path by Popping the next Node in the stack


		// DrawTileColors(openList,closedList);

		// startNode.gameObject.GetComponent<Renderer>().material = startMat;
		// targetNode.gameObject.GetComponent<Renderer>().material = targetMat;
		// bool done = false;
		// // Node pathNode = targetNode;
		// while (!done){
		// 	if(pathNode != startNode && pathNode != targetNode){
		// 		pathNode.gameObject.GetComponent<Renderer>().material = pathMat;
		// 		pathNode.gTxt.text = pathNode.gCost.ToString();
		// 		pathNode.hTxt.text = pathNode.hCost.ToString();
		// 		pathNode.fTxt.text = pathNode.fCost.ToString();
		// 	}
		// 	pathNode = pathNode.parent;
		// 	if(pathNode == startNode){
		// 		done = true;
		// 	}
		// }

	}

	

	// public void RunSlowAStar(){
	// 	mapGrid.Clear();
	// 	IEnumerator coroutine = pathfinding.FindPathSlow(startNode, targetNode);
	// 	StartCoroutine(coroutine);
	// }
}
