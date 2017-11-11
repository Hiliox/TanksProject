using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float accelSpeed = 10f;
	public float maxSpeed = 12f;
	public float accelTurnSpeed = 90f;
	public float maxTurnSpeed = 180f;

	private Rigidbody rb;
	private float moveInput;
	private float turnInput;
	private string playerMoveAxis;
	private string playerTurnAxis;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Start ()
	{
		playerMoveAxis = "Move";
		playerTurnAxis = "Turn";
	}

	void Update()
	{
		// get input
		moveInput = Input.GetAxis(playerMoveAxis);
		turnInput = Input.GetAxis(playerTurnAxis);
	}
	
	void FixedUpdate()
	{
		// Movement
		Move();
		Turn();
	}

	void Move()
	{
		rb.AddForce(transform.forward * moveInput * accelSpeed);
	}

	void Turn()
	{
		
	}
}
