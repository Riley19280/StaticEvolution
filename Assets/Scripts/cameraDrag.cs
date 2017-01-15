using UnityEngine;
using System.Collections;

public class cameraDrag : MonoBehaviour {
	public float dragSpeed = 50;
	private Vector3 dragOrigin;
	
	
	void Update()
	{
		drag ();
		zoom();
		
		
	}

	void drag ()
	{
		if (Input.GetMouseButtonDown (0)) {
			dragOrigin = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton (0))
			return;
		Vector3 pos = Camera.main.ScreenToViewportPoint (Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3 (pos.x * (1/Camera.main.fieldOfView)*dragSpeed, pos.y * (1/Camera.main.fieldOfView)*dragSpeed, 0);
		transform.Translate (move, Space.World);
	}

	void zoom ()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Camera.main.fieldOfView-=2;

		}
		
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Camera.main.fieldOfView+=2;
		}


	}
}


