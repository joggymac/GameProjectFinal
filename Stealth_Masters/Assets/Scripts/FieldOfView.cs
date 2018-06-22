using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;


public class FieldOfView : NetworkBehaviour  {

	public GameObject[] Players;

	[Range(0,20)]
	public float viewRadius = 60;
	[Range(0,360)]
	public float viewAngle;

	public Color targetVisible, targetNotVisible;
	Color debugColour = new Color(255,255,255,0.2f);

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	//Other Scripts
	public LevelManager levelManager;
	LevelLoader lvlloader;
	SwatController swatController;

	public List<PlayerController_TP> playerControllers;

	public List<Footsteps> footSteps;

	//Detection 
	NavMeshAgent agent;
	public Transform target;

	private bool detected1 = false;

	public GameObject[] Spawns;
	public GameObject[] Spawns2;
	int index;
	public GameObject currentSpawn;
	public GameObject currentSpawn2;

	public GameObject Item;
	public GameObject Item2;

	//Audio
	AudioSource audioSource;
	public AudioClip detected;


	void Start(){
		
		Item = GameObject.FindGameObjectWithTag ("Item");
		Item2 = GameObject.FindGameObjectWithTag ("lvl2Item");

		Spawns = GameObject.FindGameObjectsWithTag ("Spawn");
		Spawns2 = GameObject.FindGameObjectsWithTag ("Spawn2");

		levelManager = FindObjectOfType<LevelManager>();
		audioSource = GetComponent<AudioSource>();
		agent = GetComponent<NavMeshAgent>();
		lvlloader = FindObjectOfType<LevelLoader> ();
			
	}

	void Update(){
		FindVisibleTargets ();


	}

	void FindVisibleTargets() {
		visibleTargets.Clear ();
		Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++) {
			Transform target = targetsInViewRadius [i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance (transform.position, target.position);
				RaycastHit hit;
				if (!Physics.Raycast (transform.position, dirToTarget, out hit, dstToTarget, obstacleMask)) {
					if (target.name != gameObject.name) {
						visibleTargets.Add (target);
					}
					Debug.DrawLine (transform.position, target.position, Color.green);
				} else {
					Debug.DrawLine (transform.position, hit.point, Color.red);
				}
			}
		}
		PlayerController_TP[] playerControllers = FindObjectsOfType(typeof(PlayerController_TP)) as PlayerController_TP[];
		foreach (PlayerController_TP playerController in playerControllers) {
			if (visibleTargets.Count > 0) {
				Debug.Log ("Seen");
				debugColour = targetVisible;


				playerController.detection_time--;



				if (playerController.detection_time <= 0) {
					playerController.heldItem.SetActive (false);
					RpcRespawn ();
					Footsteps[] footSteps = FindObjectsOfType (typeof(Footsteps)) as Footsteps[];
					foreach (Footsteps footStep in footSteps) {
						footStep.soundhearing = 100;


					}


					Item.SetActive (true);
					Item2.SetActive (true);

					playerController.detection_time = 30;
				}



				//playerController.detection_time = 30;
			} else if (visibleTargets.Count <= 0) {
				playerController.detection_time += Time.deltaTime;
			}


						
						
					}



						//target = GameObject.Find ("Player(Clone)").transform;
					


				}

			

		

	void RpcRespawn(){
		

		Players = GameObject.FindGameObjectsWithTag ("Player");
		audioSource.PlayOneShot (detected, 0.8f);
		foreach (GameObject Player in Players) {
			float distance = Vector3.Distance(Player.transform.position, transform.position);
			if (distance < 10) {
				if (lvlloader.lvl2 == false) {
					index = Random.Range (0, Spawns.Length);
					currentSpawn = Spawns [index];
					Player.transform.position = currentSpawn.transform.position;
				}


				if (lvlloader.lvl2 == true) {
					index = Random.Range (0, Spawns.Length);
					currentSpawn2 = Spawns2 [index];
					Player.transform.position = currentSpawn2.transform.position;
				}


			}
			
		}
	}
}
	