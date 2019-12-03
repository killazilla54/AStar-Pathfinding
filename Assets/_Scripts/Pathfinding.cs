using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Pathfinding : MonoBehaviour {

	MapGrid grid;

	public Material startMat;
	public Material targetMat;
	public Material openMat;
	public Material closedMat;
	public Material pathMat;

	public float time;

	private void Start()
	{
		grid = GetComponent<MapGrid> ();
	}
	//Create new Script Map Manager that will manage start and end nodes and will call this and grid's methods to build and run path.

	public void FindPath(Node start, Node target){

		Stopwatch sw = new Stopwatch();
		sw.Start();

		//Start
		List<Node> openList = new List<Node> ();
		List<Node> closedList = new List<Node> ();
		openList.Add (start);
		while (true) {
			Node current = openList [0]; //GetSmallestFCostNode();
			for (int i = 0; i < openList.Count; i++) {
				if (openList [i].fCost < current.fCost || (openList [i].fCost == current.fCost && openList [i].hCost < current.hCost)) {
					current = openList [i];
					// if(current != start && current != target){
						// current.gameObject.GetComponent<Renderer>().material = openMat;
					// }
				}
			}
			openList.Remove (current);
			closedList.Add (current);
			// if(current != start && current != target){		
				// current.gameObject.GetComponent<Renderer>().material = closedMat;
			// }
			if(current == target){
				sw.Stop();
				time = sw.ElapsedMilliseconds;
				break;
			}
			List<Node> neighbors = grid.GetNeighbors (current, false);
			foreach(Node neighbor in neighbors){

				// neighbor.hCost = grid.DiagonalDistance(neighbor,target);
				neighbor.hCost = grid.ManhattenDistance(neighbor,target);
				if (neighbor.walkable == false || closedList.Contains (neighbor)) {
					continue;
				}
				//Assuming Diagonal is allowed
				int moveCostToNeighbor = current.gCost + GetDistanceBetween (current, neighbor);
				if(moveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor)){
					neighbor.parent = current;
					neighbor.gCost = moveCostToNeighbor;
					if(!openList.Contains (neighbor)){
						openList.Add (neighbor);
						// if(neighbor != start && neighbor != target){					
						// 	neighbor.gameObject.GetComponent<Renderer>().material = openMat;
						// }
					}
				}
			}
		}
		//END
		//Return targetNode

		//Move below to the Path Manager or a Draw Graphics Script	
		DrawTileColors(openList,closedList);

		start.gameObject.GetComponent<Renderer>().material = startMat;
		target.gameObject.GetComponent<Renderer>().material = targetMat;
		bool done = false;
		Node pathNode = target;
		while (!done){
			if(pathNode != start && pathNode != target){
				pathNode.gameObject.GetComponent<Renderer>().material = pathMat;
				pathNode.gTxt.text = pathNode.gCost.ToString();
				pathNode.hTxt.text = pathNode.hCost.ToString();
				pathNode.fTxt.text = pathNode.fCost.ToString();
			}
			pathNode = pathNode.parent;
			if(pathNode == start){
				done = true;
			}
		}

	}

	int GetDistanceBetween(Node current, Node neighbor){
		if(((current.gridX - 1 == neighbor.gridX || current.gridX + 1 == neighbor.gridX) && current.gridY == neighbor.gridY) || 
		   ((current.gridY - 1 == neighbor.gridY || current.gridY + 1 == neighbor.gridY) && current.gridX == neighbor.gridX)){
			return 10;
		} else {
			return 14;
		}

	}

	void DrawTileColors(List<Node> openList, List<Node> closedList){

		//all closed = yellow
		foreach(Node node in closedList){
			node.gameObject.GetComponent<Renderer>().material = closedMat;
			UnityEngine.Debug.Log(node.fCost.ToString());
			node.gTxt.text = node.gCost.ToString();
			node.hTxt.text = node.hCost.ToString();
			node.fTxt.text = node.fCost.ToString();
		}
		foreach(Node node in openList){
			node.gameObject.GetComponent<Renderer>().material = openMat;
			node.gTxt.text = node.gCost.ToString();
			node.hTxt.text = node.hCost.ToString();
			node.fTxt.text = node.fCost.ToString();
		}
		//all open = blue
		//path = purple

	}


	public IEnumerator FindPathSlow(Node start, Node target){
		start.gameObject.GetComponent<Renderer>().material = startMat;
		target.gameObject.GetComponent<Renderer>().material = targetMat;
		Stopwatch sw = new Stopwatch();
		sw.Start();
		List<Node> openList = new List<Node> ();
		List<Node> closedList = new List<Node> ();
		openList.Add (start);
		while (true) {
			Node current = openList [0]; //GetSmallestFCostNode();
			for (int i = 0; i < openList.Count; i++) {
				if (openList [i].fCost < current.fCost || (openList [i].fCost == current.fCost && openList [i].hCost < current.hCost)) {
					current = openList [i];
					if(current != start && current != target){
						current.gameObject.GetComponent<Renderer>().material = openMat;
					}
				}
			}
			openList.Remove (current);
			closedList.Add (current);
			if(current != start && current != target){		
				current.gameObject.GetComponent<Renderer>().material = closedMat;
			}
			if(current == target){
				sw.Stop();
				time = sw.ElapsedMilliseconds;
				break;
			}
			List<Node> neighbors = grid.GetNeighbors (current, true);
			foreach(Node neighbor in neighbors){

				neighbor.hCost = grid.DiagonalDistance(neighbor,target);
				if (neighbor.walkable == false || closedList.Contains (neighbor)) {
					continue;
				}
				//Assuming Diagonal is allowed
				int moveCostToNeighbor = current.gCost + GetDistanceBetween (current, neighbor);
				if(moveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor)){
					neighbor.parent = current;
					neighbor.gCost = moveCostToNeighbor;
					if(!openList.Contains (neighbor)){
						openList.Add (neighbor);
						if(neighbor != start && neighbor != target){					
							neighbor.gameObject.GetComponent<Renderer>().material = openMat;
						}
					}
				}
			}

			yield return new WaitForSeconds(.5f);
		}

		// DrawTileColors(openList,closedList);

		start.gameObject.GetComponent<Renderer>().material = startMat;
		target.gameObject.GetComponent<Renderer>().material = targetMat;
		bool done = false;
		Node pathNode = target;
		while (!done){
			if(pathNode != start && pathNode != target){
				pathNode.gameObject.GetComponent<Renderer>().material = pathMat;
			}
			pathNode = pathNode.parent;
			if(pathNode == start){
				done = true;

			}
		}
	yield return null;
	}

}
