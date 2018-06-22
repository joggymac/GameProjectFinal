using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public GameObject PauseUI;
	private bool paused = false;
	private PlayerController_TP playerController;
	public Camera_TP Camera;
	public GameObject Main;
	public GameObject Instructions;
	public bool Instructme;


	// Use this for initialization
	void Start () {
		PauseUI = GameObject.FindWithTag ("PauseUI");
		PauseUI.SetActive (false);
		Camera = GetComponent<Camera_TP>();



		Instructions.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
			if (Input.GetButtonDown ("Pause")){
				paused = !paused;
			}
			if (paused) {
				PauseUI.SetActive (true);


			Cursor.visible = true;

			Camera.rotateCamera = false;
			}
			if (!paused) {
				PauseUI.SetActive (false);
			Cursor.visible = false;
			Camera.rotateCamera = true;
			}
		}





	public void Resume(){
		paused = false;
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Instruc()
	{
		Instructions = GameObject.Find ("Instructions");
		
		Main = GameObject.Find ("Main");
		Main.SetActive (false);
		Instructme = true;
	}
}
