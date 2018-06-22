using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMenu : MonoBehaviour {

	public GameObject Main;
	public GameObject Instructions;
	public bool Instructme;
	// Use this for initialization
	void Start () {
		Instructions.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Quit(){
		Application.Quit ();
	}

	public void Instruc()
	{
		Main = GameObject.Find ("Main");
		Main.SetActive (false);
		Instructions.SetActive (true);

	}

	public void Back(){
		Main.SetActive (true);
		Instructions.SetActive (false);
	}
}
