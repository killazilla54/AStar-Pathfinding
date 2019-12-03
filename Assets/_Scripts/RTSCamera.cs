
using UnityEngine;

public class RTSCamera : MonoBehaviour {

	public float panSpeed = 20f;
	public float panBoarderThinkness = 10f;
	public Vector2 panLimit;
	public float scrollSpeed = 2f;
	public float minY = 20f;
	public float maxY = 120f;

	void Update () {

		Vector3 pos = transform.position;
		float speed = (panSpeed/pos.y) * 3f; //Tinker with this...
		// Debug.Log("scaled: " + speed);

		if(Input.GetKey(KeyCode.W ) || Input.mousePosition.y >= Screen.height - panBoarderThinkness){
			pos.z += speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBoarderThinkness){
			pos.z -= speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBoarderThinkness){
			pos.x -= speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBoarderThinkness){
			pos.x += speed * Time.deltaTime;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		if ((pos.y > minY && scroll > 0) || (pos.y < maxY && scroll < 0)) { 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
			RaycastHit point; 
			Physics.Raycast(ray, out point, 25); 
			Vector3 Scrolldirection = ray.GetPoint(5);
			float step = scrollSpeed * Time.deltaTime;
			pos = Vector3.MoveTowards(transform.position, Scrolldirection, Input.GetAxis("Mouse ScrollWheel") * step);
		}
		// pos.y -= scroll * scrollSpeed * Time.deltaTime;
		pos.x = Mathf.Clamp(pos.x,-panLimit.x, panLimit.x);
		pos.y = Mathf.Clamp(pos.y, minY,maxY);
		pos.z = Mathf.Clamp(pos.z,-panLimit.y, panLimit.y);

		transform.position = pos; 
	}
}
