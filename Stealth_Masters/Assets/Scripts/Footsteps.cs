using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;



public class Footsteps : NetworkBehaviour {

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

	//Scripts
	public List<PlayerController_TP> playerControllers;
	public List<HUD> huds;

	SwatController swatController;
	LevelLoader lvlloader;
	public LevelManager levelManager;



	//Detection
	public NavMeshAgent agent;
	private int heardnoise = 0;

	private int i = 0;
	public Transform[] waypoints;
	public Transform currentWaypoint;

	//public float soundhearing = 100;

	public Transform target;
	public Transform target2;
	//Audio
	AudioSource audioSource;
	public AudioClip heard;

	//Animation
	Animator anim;

	public float soundhearing = 100;

	void Start(){

		levelManager = FindObjectOfType<LevelManager>();
		agent = GetComponent<NavMeshAgent>();
		audioSource = GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
		lvlloader = FindObjectOfType<LevelLoader> ();

	}

	void Update(){
		FindVisibleTargets ();



		var dist = Vector3.Distance(waypoints[i].position,transform.position);
		currentWaypoint = waypoints [i];

		if (dist < 5) {
			if (i < waypoints.Length - 1){ //negate targets[0], since it's already set in destination.
				i++; //change next target
				agent.destination = waypoints[i].position; //go to next target by setting it as the new destination

			}


			else if (i >= waypoints.Length - 1){
				i = -1;
				i++;
				agent.destination = waypoints[i].position;
			}
		}





				


		if (soundhearing >= 100) {
			
				
			HUD[] huds = FindObjectsOfType(typeof(HUD)) as HUD[];
			foreach (HUD hud in huds) {
				hud.detectionlevel = 0;
			}
				//Debug.Log ("HUD working");

			


		soundhearing = 100;
				

	}

		if (soundhearing > 0) {
			agent.destination = waypoints [i].position;

		}
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
		foreach (PlayerController_TP playerController in playerControllers){
			if (visibleTargets.Count > 0) {
				
				debugColour = targetVisible;
				if (playerController.moving == true) {
					if (playerController.moveSpeed == 5f) {
						soundhearing--;

						Debug.Log ("Heard");


					} else if (playerController.moveSpeed == 8f) {
						soundhearing--;
						soundhearing--;



					}
				}



				if (soundhearing <= 0) {
					TargetPlayer ();
				}
					
				if (soundhearing >= 100) {
					soundhearing = 100;
				}
					
					
				
			}
			else if (visibleTargets.Count <= 0){
				soundhearing += Time.deltaTime;
			}
			}
			

		}


	void TargetPlayer(){
		Players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject Player in Players) {
			//target2 = (playerController.gameObject.transform.position);
			//agent.destination = target2.position;
			float distance = Vector3.Distance(Player.transform.position, transform.position);
			if (distance < 10) {
				agent.destination = Player.transform.position;
			
			}





		}
		

	}
}
	
