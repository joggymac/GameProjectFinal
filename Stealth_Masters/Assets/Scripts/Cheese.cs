using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {

	private int i = 0;


	public GameObject[] cheesePoints;
	public GameObject currentPoint;
	int index;


	// Use this for initialization
	void Start () {
		cheesePoints = GameObject.FindGameObjectsWithTag ("cheesePoint");
		index = Random.Range (0, cheesePoints.Length);
		currentPoint = cheesePoints [index];


	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = currentPoint.transform.position;

			
	}
}
