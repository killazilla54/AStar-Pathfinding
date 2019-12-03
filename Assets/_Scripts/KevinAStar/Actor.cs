using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// Use this for initialization
	private Coroutine coroutine;

	public void MoveOnPath (Stack<Node> path){
		if(coroutine != null){
			StopMoveOnPath();
		}
		coroutine = StartCoroutine(MoveToPosition(path));
	}

	public void StopMoveOnPath(){
		StopCoroutine(coroutine);
		coroutine = null;
	}

	IEnumerator MoveToPosition(Stack<Node> path){
	Node start = path.Pop();
	
		while(path.Count > 0){
			Node next = path.Pop();
			Vector3 a = new Vector3(start.gridX,0,start.gridY);
			Vector3 b = new Vector3(next.gridX,0,next.gridY);
			float step = (1 / (a - b).magnitude) * Time.fixedDeltaTime * 5f;
			float t = 0;
			while (t <= 1.0f) {
				t += step; // Goes from 0 to 1, incrementing by step each time
				transform.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
				yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
			}
			transform.position = b;
			// StartCoroutine(MoveToPosition(a,b));
			start = next;
		}

		
	}
}
