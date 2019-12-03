using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTest : MonoBehaviour {

	IGrid grid;
	public GameObject actor;
	AstarPathfinding astar;
	bool disable;

	public bool buildLevel;

	// Use this for initialization
	void Start () {
		grid = GetComponent<IGrid>();
		if(buildLevel){
			grid.Create(30,20);
		}
		astar = GetComponent<AstarPathfinding>();
		disable = false;
	}
	
	void Update () {
		if(Input.GetMouseButtonDown(0) && buildLevel){
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if(hit){
				if(hitInfo.transform.tag == "tile") {
					hitInfo.transform.GetComponent<Node>().walkable = false;
					hitInfo.transform.GetComponent<TileEffects>().SetObstacleMat();
				}
			}
		}
		else if(Input.GetMouseButtonDown(0) && disable == false){
			grid.Clear();
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if(hit){
				if(hitInfo.transform.tag == "tile") {
					disable = true;
					Node target = hitInfo.transform.GetComponent<Node>();
					if(!target.walkable){
						disable = false;
						return;
					}			
						
					Vector3 currentPos = actor.transform.position;
					Debug.Log(currentPos);
					Node start = grid.GetNodeFromGrid(Mathf.RoundToInt(currentPos.x),Mathf.RoundToInt(currentPos.z));
					
					Debug.Log(start + ", " + target);
					Stack<Node> path = astar.FindPath(grid,start,target);	
					grid.Clear();
					Stack<Node> pathStack = new Stack<Node>(path);
					PaintPath(pathStack);
					actor.GetComponent<Actor>().MoveOnPath(path);
					disable = false;

				}
			} 
		} 
	}

	void PaintPath(Stack<Node> path){
		for (int x = 0; x < 30; x++){
            for (int y = 0; y < 20; y++){
				if(grid.GetGrid()[x,y].walkable){
                	grid.GetGrid()[x,y].GetComponent<TileEffects>().SetDefaultMat();
				}
            }
        }

		foreach(Node openNode in astar.debugOpenList){
			openNode.GetComponent<TileEffects>().SetOpenMat();
		}

		foreach(Node closedNode in astar.debugClosedList){
			closedNode.GetComponent<TileEffects>().SetClosedMat();
		}

		while (path.Count > 0){
			Node node = path.Pop();
			node.GetComponent<TileEffects>().SetPathMat();
		}
	}
}
