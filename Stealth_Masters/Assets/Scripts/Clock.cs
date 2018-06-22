using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour {

	public double clock;

	// Use this for initialization
	void Start () {
		clock = 0.00;
	}

	void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
