using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGridPath : MonoBehaviour {

	public Node[] path;
	
	// Update is called once per frame
	void Update () {
		if(path != null && path.Length > 1){
			RenderPath();
		}
	}

	public void SetPath(Stack<Node> path2){
		path = path2.ToArray();
	}

	void RenderPath(){
		Node start = path[0];
		Node end = path[1];
		DrawLine(start.transform.position+(Vector3.up),end.transform.position+(Vector3.up), Color.green,.2f);

		for(int i = 2; i < path.Length; i++){
			start = end;
			end = path[i];
			DrawLine(start.transform.position +(Vector3.up),end.transform.position+(Vector3.up), Color.green,.2f);
		}
	}

	public void DrawNeighborLinks(List<Node> nodes){
		foreach(Node node in nodes){
			List<Node> neighbors = node.GetComponent<GraphNeighbors>().neighborNodes;
			foreach(Node neighbor in neighbors){
				DrawLine(node.transform.position,neighbor.transform.position, Color.blue,10f);
			}
		}
	}
	void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
         {
             GameObject myLine = new GameObject();
             myLine.transform.position = start;
             myLine.AddComponent<LineRenderer>();
             LineRenderer lr = myLine.GetComponent<LineRenderer>();
             lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
             lr.startColor=color;
			 lr.startWidth = 0.1f;
			 lr.endColor=color;
			lr.endWidth = 0.1f;
             lr.SetPosition(0, start);
             lr.SetPosition(1, end);
             GameObject.Destroy(myLine, duration);
         }
}
