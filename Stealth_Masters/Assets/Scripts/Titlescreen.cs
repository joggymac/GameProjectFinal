using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titlescreen : MonoBehaviour {

	public Clock clockobject;
	public Text clockText;
	public GameObject Instructions;
	public PauseMenu pauseMenu;

	// Use this for initialization
	void Start () {
		clockobject = FindObjectOfType<Clock> ();
		Cursor.visible = true;
		Instructions = GameObject.Find ("Instructions");
		Instructions.SetActive (false);
		pauseMenu = FindObjectOfType<PauseMenu> ();



	}
	
	// Update is called once per frame
	void Update () {
		if (clockobject != null) {
			clockText.text = "Your Time: " + clockobject.clock.ToString ("#,##0.00");

		}

	}
}
