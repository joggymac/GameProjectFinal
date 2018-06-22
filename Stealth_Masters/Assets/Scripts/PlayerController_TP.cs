using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController_TP : NetworkBehaviour {


	public GameObject Item;
	public GameObject Item2;
	public GameObject heldItem;

	public LayerMask avoidLayer;

	public float moveSpeed = 5f;
    public float turnSpeed = 50f;
    public float runSpeed = 8f;
    public float crawlSpeed = 3f;
    Animator anim;
	Animation animate;
	public bool moving = false;

	public bool crawling = false;

	LevelLoader lvlLoader;
	public Footsteps[] footSteps;
	public FieldOfView[] fovs;
	public HUD[] huds;

	public double clock;

	public Camera cam;

	public bool heard;



	public bool targeted = false;

	//Audio
	AudioSource audio;

	public GameObject Spawn;



	public float detection_time = 30;


	 void Start()
	{


		
		Spawn = GameObject.FindWithTag("Spawn");
		cam = Camera.main;
		//heldItem = GameObject.FindGameObjectWithTag ("heldItem");
		anim = GetComponent<Animator> ();
		heldItem.SetActive (false);
		Cursor.visible = false;

		audio = GetComponent<AudioSource> ();

		lvlLoader = FindObjectOfType<LevelLoader> ();
		lvlLoader.playerControllers.Add (this);


		huds = FindObjectsOfType<HUD> ();


		foreach (FieldOfView fov in fovs) {
			fov.playerControllers.Add (this);
		}

		foreach (HUD hud in huds) {
			hud.playerControllers.Add (this);
		}

		foreach (Footsteps footStep in footSteps) {
			footStep.playerControllers.Add (this);
		}
			
	}
		
	
	void Update(){
		if (!isLocalPlayer)
			return;

		DontWall ();

	}

	void FixedUpdate()
    {
		
		if (detection_time <= 0) {

			heldItem.SetActive (false);
		} else if (detection_time >= 40) {
			detection_time = 40;
		}


        if (Input.GetKey(KeyCode.S))
        {
			moving = true;
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);
			anim.SetBool ("Idle", false);
			anim.SetBool ("Walking", true);
        }

		else if (Input.GetKeyUp(KeyCode.C))
		{

			moveSpeed = crawlSpeed;
			anim.SetBool ("Crawling", true);
			anim.SetBool ("Walking", false);
			audio.volume = 0.145f;
			crawling = true;

		}

		if (Input.GetKey (KeyCode.W)) {
			
			anim.SetBool ("Walking", true);
			anim.SetBool ("Idle", false);



		


			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				moveSpeed = runSpeed;
				anim.SetBool ("Running", true);
				anim.SetBool ("Walking", false);
				audio.volume = 0.345f;
				crawling = false;
			} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
				moveSpeed = 5f;
				anim.SetBool ("Running", false);
				audio.volume = 0.245f;
				crawling = false;
			}


      


			transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
			moving = true;

		} else {
			moving = false;
		}
			

		if (moveSpeed == 5f && audio.isPlaying == false && moving == true) {
			
			audio.loop = true;
			audio.Play ();

		}
		else if (moving == false) {
			audio.Stop ();
			anim.SetBool ("Idle", true);
			anim.SetBool("Walking", false);
			anim.SetBool("Running", false);
			anim.SetBool("Crawling", false);
			foreach (Footsteps footStep in footSteps) {
				footStep.soundhearing += Time.deltaTime;
			}
		}


        if (Input.GetKey(KeyCode.A))
        {
			moving = true;
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
            anim.SetBool("Walking", true);
			anim.SetBool ("Idle", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
			moving = true;
			anim.SetBool("Walking", true);
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
            
        }
			
			

    }


	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Item") {


			heldItem.SetActive (true);
		}
			
		if(col.gameObject.tag == "lvl2Item") {



			heldItem.SetActive (true);
		}


		}




	void DontWall(){
		Vector3 dir = transform.position;
		RaycastHit hit;
		if (Physics.Raycast (gameObject.transform.position, dir, out hit, 1, avoidLayer)) {
			transform.position = hit.point;
		}
	}

		
		


}