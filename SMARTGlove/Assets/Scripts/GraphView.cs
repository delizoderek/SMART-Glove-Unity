using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour {

	private const float Y_ANGLE_MIN = -80.0f;
	private const float Y_ANGLE_MAX = 80.0f;
	private const float ZOOM_DIST_MIN = 2.0f;
	private const float ZOOM_DIST_MAX = 100.0f;

	public Transform lookAt;
	public Transform camTransform;

	public float moveSensitivity;
	public float zoomSensitivity;
	public float distance = 40.0f;

	private Camera cam;

	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensivityX = 4.0f;
	private float sensivityY = 1.0f;

	private bool camMove = false;

	private void Start(){
		camTransform = transform;
		cam = Camera.main;

	}

	private void Update(){

		if (Input.GetMouseButton(0)) {
			currentX += Input.GetAxis ("Mouse X") * moveSensitivity;
			currentY += Input.GetAxis ("Mouse Y")* moveSensitivity;

			currentY = Mathf.Clamp (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
		}

		distance -= Input.GetAxis ("Mouse ScrollWheel") * zoomSensitivity;
		distance = Mathf.Clamp (distance,ZOOM_DIST_MIN,ZOOM_DIST_MAX);
	}

	private void LateUpdate(){
		Vector3 dir = new Vector3 (0, 0, -distance);
		Quaternion rotation = Quaternion.Euler (-currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt (lookAt);
	}
}
