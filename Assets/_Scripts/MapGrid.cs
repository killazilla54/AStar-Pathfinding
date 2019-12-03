using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGrid : MonoBehaviour {

	public GameObject tilePrefab;
	public Vector2 gridWorldSize;
	public Material obstacleMat;
	public Material defaultMat;
	Node [,] grid;
	int gridSizeX, gridSizeY;

	void Awake(){
		
		// CreateGrid ();
	}

	public void CreateGrid (int xSize, int ySize) {
		Reset();
		gridSizeX = Mathf.RoundToInt (xSize);
		gridSizeY = Mathf.RoundToInt (ySize);
		grid = new Node [gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				GameObject tile = Instantiate (tilePrefab, new Vector3 (-(gridSizeX/2) + x, 0, -(gridSizeY/2) +y), tilePrefab.transform.rotation);
				Node node = tile.AddComponent<Node> ();
				node.walkable = true;
				node.gridX = x;
				node.gridY = y;
				grid [x, y] = node;
				node.gTxt = tile.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
				node.hTxt = tile.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
				node.fTxt = tile.gameObject.transform.GetChild(2).gameObject.GetComponent<TextMesh>();
				// node.fTxt = tilePrefab.gameObject.AddComponent<TextMesh>();
				tile.gameObject.transform.parent = this.gameObject.transform;
				tile.gameObject.name = "Tile " + x + "," + y;
				node.obstacleMat = obstacleMat;
				node.defaultMat = defaultMat;
			}
		}
	}

	public void ClearBoard(){
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				if(grid[x,y].walkable){
					grid[x,y].gameObject.GetComponent<Renderer>().material = defaultMat;
				}
			}
		}
	}

	void Reset(){
		for(int x = 0; x < gridSizeX; x++){
			for(int y = 0; y < gridSizeY; y++){
				Destroy(grid[x,y].gameObject);
				grid[x,y] = null;
			}
		}
		gridSizeX = 0;
		gridSizeY = 0;
		grid = null;
	}

	public List<Node> GetNeighbors (Node node, bool allDir) {
		List<Node> neighbors = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if ((x == 0 && y == 0) || (!allDir && Mathf.Abs(x) == Mathf.Abs(y))) {
					continue;
				}
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY  ) {
					
					neighbors.Add (grid [checkX, checkY]);
				}
			}
		}
		return neighbors;
	}

	public int ManhattenDistance(Node node, Node target){
		int D = 10;
		int dX = Mathf.Abs(node.gridX - target.gridX);
		int dY = Mathf.Abs(node.gridY - target.gridY);
		return D * (dX + dY);
	}

	public int DiagonalDistance(Node node, Node target){
		int D = 10; //Cardinal Direction Cost
		int D2 = 14; //Diagonal Cost
		int dX = Mathf.Abs(node.gridX - target.gridX);
		int dY = Mathf.Abs(node.gridY - target.gridY);
		return D * (dX + dY) + (D2 - 2 * D) * Mathf.Min(dX,dY);
	}
}

