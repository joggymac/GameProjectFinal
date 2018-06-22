using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;


public class LevelManager : NetworkBehaviour {

	public bool destroyOnDeath;
	Footsteps footSteps;
	public GameObject Spawn;
	HUD hud;

	public List<PlayerController_TP> playerControllers;



	private NetworkStartPosition[] spawnPoints;

	void Start(){
		hud = FindObjectOfType<HUD> ();
		footSteps = FindObjectOfType<Footsteps> ();
		Spawn = GameObject.FindWithTag("Spawn");


	}

	void FixedUpdate(){
			TakeDamage ();
			
		}
			





	public void TakeDamage()
	{
		if (!isServer) {
			return;
		}
		FieldOfView[] fovs = FindObjectsOfType(typeof(FieldOfView)) as FieldOfView[];
		Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");


		foreach (PlayerController_TP playerController in playerControllers) {
			if (playerController.detection_time <= 0) 
				{
					Debug.Log ("Dead");
					if (destroyOnDeath) {
						Destroy (gameObject);
					} else {
					
					}
				playerController.detection_time = 30;

					playerController.heldItem.SetActive (false);

					hud.detectionlevel = 0;
				}
			}

			
		}
}



			



		
