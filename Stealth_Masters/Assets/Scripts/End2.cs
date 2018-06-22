using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End2 : MonoBehaviour {

	PlayerController_TP playerController2;
	public List<PlayerController_TP> playerControllers;
	public List<Footsteps> footSteps;
	public string leveltoload;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	private void OnTriggerEnter(Collider other){
		PlayerController_TP[] playerControllers = FindObjectsOfType(typeof(PlayerController_TP)) as PlayerController_TP[];
		foreach (PlayerController_TP playerController in playerControllers) {
			if (playerController.heldItem.activeSelf) {
				Debug.Log ("Loading");
				Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
				foreach (Footsteps footStep in footSteps) {
					footStep.soundhearing = 0;
				}
				SceneManager.LoadScene (leveltoload);

			}

		}
	}
}
