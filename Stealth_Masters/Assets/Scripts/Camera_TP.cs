using UnityEngine;
using System.Collections;

public class Camera_TP : MonoBehaviour {

	public float mouseSensitivity = 10;
	public float xRotation;
	public float yRotation;

	public Camera camera;

	public Transform targetob;
	public float distance = 3;

	public Vector2 yMinMax = new Vector2 (-20, 80);

	public bool rotateCamera = true;

	public LayerMask avoidLayer;

	public void Start(){




	}
	// Update is called once per frame
	public void FixedUpdate () {
		
			FollowTarget ();
		if (targetob != null) {
			AvoidWalls ();
		}

	}

	public void FollowTarget(){
		if (targetob == null) {
			GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
			foreach (GameObject target in targets) {
				SetTarget (target.transform);

		}

			}
	

		MoveCamera ();

	}

	public void SetTarget(Transform newTransForm){
		targetob = newTransForm;
	}

	public void MoveCamera(){
		if (targetob != null) {
			if (rotateCamera == true) {
				transform.position = targetob.position - distance * transform.forward;
				Vector3 targetRotation = new Vector3 (yRotation, xRotation);
				transform.eulerAngles = targetRotation;


				xRotation += Input.GetAxis ("Mouse X") * mouseSensitivity;
				yRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
				yRotation = Mathf.Clamp (yRotation, yMinMax.x, yMinMax.y);

				transform.position = targetob.position - distance * transform.forward;
			}
		}

	}

	void AvoidWalls(){
		Vector3 dir = transform.position - targetob.position;

		RaycastHit hit;

		if (Physics.Raycast (targetob.position, dir, out hit, distance, avoidLayer)) {
			transform.position = hit.point;
		}
		Debug.DrawRay (targetob.position, dir, Color.green);

	}

}
