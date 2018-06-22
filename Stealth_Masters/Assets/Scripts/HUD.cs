using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



public class HUD : NetworkBehaviour {


	public Sprite[] DetectionSprites;

	public Sprite[] SightSprites;

	public Sprite[] StatusUIs;

	LevelManager lvlmangaer;
	public Image DetectionUI;
	public Image StatusUII;
	public List<Footsteps> footSteps;

	public int detectionlevel;

	public int statuslevel;

	public Clock clockobject;

	public Text clockText;

	public Image status;

	public List<PlayerController_TP> playerControllers;



	// Use this for initialization
	void Start () {

		clockobject = FindObjectOfType<Clock> ();
		//footSteps = GameObject.FindWithTag ("Enemy").GetComponent<Footsteps> ();

	}

	// Update is called once per frame
	void Update () {
		
		Footsteps[] footSteps = FindObjectsOfType(typeof(Footsteps)) as Footsteps[];
		foreach (Footsteps footStep in footSteps){


			if (footStep.soundhearing >= 50 && footStep.soundhearing <= 75) {
				detectionlevel = 1;

			} else if (footStep.soundhearing >= 25 && footStep.soundhearing < 50) {
				detectionlevel = 2;

			} else if (footStep.soundhearing <= 0) {
				detectionlevel = 3;

			}
				



		}
		clockobject.clock += Time.deltaTime;

		DetectionUI.sprite = DetectionSprites [detectionlevel];
		SetClockText ();
		StatusUI ();

	}

	 void SetClockText(){ 
		PlayerController_TP[] playerControllers = FindObjectsOfType(typeof(PlayerController_TP)) as PlayerController_TP[];
		foreach (PlayerController_TP playerController in playerControllers){
			if (playerController.gameObject.activeInHierarchy == true) {
				clockText.text = "Time: " + clockobject.clock.ToString ("#,##0.00");
			} if (playerController.gameObject.activeInHierarchy == false) {
				clockobject.clock = 0.00;
			}
		}


	}

	void StatusUI(){
		
		foreach (PlayerController_TP playerController in playerControllers) {
			if (playerController.crawling == true) {
				statuslevel = 1;
			} else if (playerController.moveSpeed == 5) {
				statuslevel = 2;
			}

			else {
				statuslevel = 0;
			}
		}
		StatusUII.sprite = StatusUIs [statuslevel];


	}
}
