using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelLoader : NetworkBehaviour {

    private bool inExitZone;
    public string levelToLoad;
	PlayerController_TP playerController2;
	public List<PlayerController_TP> playerControllers;
	private static bool created = false;
	public GameObject Item;
	public GameObject item2;
	public GameObject[] players;

	public bool lvl2 = false;

	public GameObject level2;

	// Use this for initialization
	void Start () {
		Item = GameObject.FindGameObjectWithTag ("Item");
		Item = GameObject.FindGameObjectWithTag ("lvl2Item");
		level2 = GameObject.FindWithTag("Level2");
        inExitZone = false;


	}
	
	// Update is called once per frame
	void Update () {
		//DontDestroyPlz ();
	}
		


	void OnTriggerEnter(Collider other)
	{
		PlayerController_TP[] playerControllers = FindObjectsOfType(typeof(PlayerController_TP)) as PlayerController_TP[];
		foreach (PlayerController_TP playerController in playerControllers) {
			if (playerController.heldItem.activeSelf)
			{
				

				playerController.gameObject.transform.position = level2.transform.position;

				
				lvl2 = true;


				playerController.heldItem.SetActive (false);


			}

		}
		Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
		foreach (Footsteps footStep in footSteps) {
			footStep.soundhearing = 0;
		}


			} 



		}
	