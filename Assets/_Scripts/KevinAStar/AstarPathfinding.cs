using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarPathfinding : MonoBehaviour {

    public bool debugEnabled;
    public List<Node> debugOpenList;
    public List<Node> debugClosedList;

    public Stack<Node> FindPath(IGrid grid, Node start, Node target) {
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        start.gCost = 0;
        start.hCost = grid.GetHCost(start, target);
        openList.Add(start);
        while(openList.Count > 0 ){
            Node current = GetSmallestFCost(openList);
            
            if (current == target) { //Target Node found
                if(debugEnabled){ //Debug added for demo to allow outside access of open & closed lists
                    debugOpenList = openList;
                    debugClosedList = closedList;
                }
                return ConstructPath(current);
            }
            //Move the node we are evaluating from the open list to closed
            openList.Remove(current);
            closedList.Add(current);
            
            List<Node> neighbors = grid.GetNeighbors(current); //Get neighbors based on implementations requirements
            foreach (Node neighbor in neighbors){
				neighbor.hCost = grid.GetHCost(neighbor,target); //H cost determined by the implementation's hueristic choice
				int moveCostToNeighbor = current.gCost + grid.GetDistanceBetween (current, neighbor);
				if (closedList.Contains (neighbor)) {
					continue;
				}
				if(moveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor)){
					neighbor.parent = current;
					neighbor.gCost = moveCostToNeighbor;
					if(!openList.Contains (neighbor)){
						openList.Add (neighbor);
					}
				}
            }
        }
        //If open list exhausted without finding target node, no path exists
        return null;
    }

    private Node GetSmallestFCost(List<Node> openList){
        Node min = openList[0];
        for(int i = 1; i < openList.Count; i++){ //skip 1st, since its already min
            if(openList[i].fCost < min.fCost){
                min = openList[i];
            }
        }
        return min;
    }

    private Stack<Node> ConstructPath(Node node){
        Stack<Node> path = new Stack<Node>();
        path.Push(node);
        while(node.parent != null){
            node = node.parent;
            path.Push(node);
        }
        return path;
    }
}
