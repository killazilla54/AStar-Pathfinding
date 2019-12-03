using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControllerTest : MonoBehaviour {

	AstarPathfinding astar;
	IGrid graphGrid;
	public Actor actor;
	public Node start;
	public Node goal;
	private DrawGridPath draw;

	public List<Node> destinations;
	float timer;

	// Use this for initialization
	void Start () {
		timer = 0;
		astar = GetComponent<AstarPathfinding>();
		graphGrid = GetComponent<GraphGrid>();
		draw = GetComponent<DrawGridPath>();
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0){
			draw.DrawNeighborLinks(destinations);
			timer = 10;
		}
		if(Input.GetMouseButtonDown(0)){
			graphGrid.Clear();
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if(hit){
				if(hitInfo.transform.parent.tag == "tile") {
					Node target = hitInfo.transform.parent.GetComponent<Node>();
					Vector3 currentPos = actor.transform.position;
					Debug.Log(currentPos);
					Node start = graphGrid.GetNodeFromGrid(Mathf.RoundToInt(currentPos.x),Mathf.RoundToInt(currentPos.z));
					Stack<Node> path = astar.FindPath(graphGrid,start,target);	
					Stack<Node> pathStack = new Stack<Node>(path);
					draw.SetPath(path);
					actor.GetComponent<Actor>().MoveOnPath(path);
				}
			} 
		} 


	}
}
