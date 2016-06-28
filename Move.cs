using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;
	public float speed = 0.5f;
	private CharacterController controller;
	public Animator anim;
	private Vector3 moveDirection = Vector3.zero;
	private float inputH;
	private float inputV;

	public Rigidbody rbody;
	private bool run;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		run = false;
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {


		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
//			if (Input.GetButton("Jump"))
//				moveDirection.y = jumpSpeed;
		}
		speed = inputV * 50f * Time.deltaTime;
		if (run) {
			speed = 3f;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		if (Input.GetKey (KeyCode.LeftShift)) {
			run = true;
		} else {
			run = false;
		}
		if (Input.GetKey (KeyCode.Space)) {
			anim.SetBool ("jump", true);
		} else {
			anim.SetBool ("jump", false);
		
		}

		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");
		anim.SetBool ("run",run);

		anim.SetFloat ("inputH",inputH);
		anim.SetFloat ("inputV",inputV);

//		float moveX = inputH * 20f * Time.deltaTime;
//		float moveZ = inputV * 50f * Time.deltaTime;
//		if (moveZ<=0f) {
//			moveX = 0f;
//		}
//		rbody.velocity=new Vector3(moveX,0f,moveZ);
	}
}
