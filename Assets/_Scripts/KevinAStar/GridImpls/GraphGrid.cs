using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphGrid : MonoBehaviour, IGrid {

	public GameObject tilePrefab;
    public Vector2 gridWorldSize;
    Node[,] grid;
    public int gridSizeX, gridSizeY;
	//Remove above once ready ^^^^^
	Dictionary<string, Node> graphMap;
    public List<Node> gridList;


    // void Start(){
    //     grid = new Node[gridSizeX, gridSizeY];
    //     Node[] nodes = transform.GetComponentsInChildren<Node>();
    //     Debug.Log("Grid size: " + grid.Length +" - Nodes size: " + nodes.Length);
    //     for(int i = 0; i < nodes.Length; i++){
    //         grid[nodes[i].gridX, nodes[i].gridY] = nodes[i];
    //     }
    // }


    public void Create(int xSize, int ySize){
        Reset();
        gridSizeX = Mathf.RoundToInt(xSize);
        gridSizeY = Mathf.RoundToInt(ySize);
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++){
            for (int y = 0; y < gridSizeY; y++){
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, 0, y), tilePrefab.transform.rotation);
                Node node = tile.AddComponent<Node>();
                node.walkable = true;
                node.gridX = x;
                node.gridY = y;
                grid[x, y] = node;
                tile.gameObject.transform.parent = this.gameObject.transform;
                tile.gameObject.name = "Tile " + x + "," + y;
            }
        }
    }

    public void Clear()
    {
        foreach(Node current in gridList){
            current.gCost = 0;
			current.hCost = 0;
            current.parent = null;
        }
    }

    public Node[,] GetGrid(){
        return grid;
    }

    public Node GetNodeFromGrid(int x, int y){
        Vector2 targetPos = new Vector2(x,y);
        // return grid[x,y];
        Node closestNode = null;
        foreach(Node current in gridList){
            if(current.gridX == x && current.gridY == y){
                return current;
            } else if(closestNode == null){
                closestNode = current;
            }
            if(Vector2.Distance(targetPos,new Vector2(current.gridX,current.gridY))
            < Vector2.Distance(targetPos, new Vector2(closestNode.gridX,closestNode.gridY))){
                closestNode = current;
            }
        }
        return closestNode;

    }

    void Reset()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Destroy(grid[x, y].gameObject);
                grid[x, y] = null;
            }
        }
        gridSizeX = 0;
        gridSizeY = 0;
        grid = null;
    }
    public List<Node> GetNeighbors(Node node) {
        return node.GetComponent<GraphNeighbors>().GetNeighborNodes();
    }

	public int GetHCost(Node node, Node target){
		return EuclidieanDistance(node,target);
	}
	public int GetDistanceBetween(Node current, Node neighbor){
		return Mathf.RoundToInt(Vector3.Distance(current.transform.position, neighbor.transform.position)); 
	}

    private int EuclidieanDistance(Node node, Node target){
        int dx = (target.gridX - node.gridX)^2;
        int dy = (target.gridY - node.gridY)^2;
        return Mathf.RoundToInt(Mathf.Sqrt((dx+dy)));
    }
}
