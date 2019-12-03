using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNeighbors : MonoBehaviour {

	public List<Node> neighborNodes;	

	void Start(){
		Node node = GetComponent<Node>();
		node.gridX = Mathf.RoundToInt(transform.position.x);
		node.gridY = Mathf.RoundToInt(transform.position.z);


	}

	public List<Node> GetNeighborNodes(){
		return neighborNodes;
	}
}
