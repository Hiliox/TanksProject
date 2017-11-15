using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float motorForce;
	public float maxSpeed;
	public WheelCollider WheelColFR;
	public WheelCollider WheelColFL;
	public WheelCollider WheelColRR;
	public WheelCollider WheelColRL;
	public float steerForce;
	public float maxSteerSpeed;
	public float brakeForce;

	private float moveInput;
	private float steerInput;
	private Rigidbody rb;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		moveInput = Input.GetAxis("Move");
		steerInput = Input.GetAxis("Turn");
	}

	void FixedUpdate()
	{
		Move();
		Turn();
		Brake();
		LimitMovements();
	}

	void Move()
	{
		float moveForce = moveInput * motorForce;

		if (moveForce < 0) // rueckwaerts
		{
			WheelColFR.motorTorque = 0;
			WheelColFL.motorTorque = 0;
			WheelColRR.motorTorque = moveForce;
			WheelColRL.motorTorque = moveForce;

		}
		else // forwaerts oder keine richtung
		{
			WheelColFR.motorTorque = moveForce;
			WheelColFL.motorTorque = moveForce;
			WheelColRR.motorTorque = 0;
			WheelColRL.motorTorque = 0;
		}
	}

	void Turn()
	{
		float steerValue = steerInput * steerForce;

		// add/subtract steer values to wheels
		WheelColFL.motorTorque += steerValue;
		WheelColRL.motorTorque += steerValue;
		WheelColFR.motorTorque -= steerValue;
		WheelColRR.motorTorque -= steerValue;

//		Quaternion turnRotation = Quaternion.Euler(0f, steer, 0f);
//
//		rb.MoveRotation(rb.rotation * turnRotation);
	}

	void Brake()
	{
		if (moveInput == 0 && steerInput == 0)
		{
			WheelColFR.brakeTorque = brakeForce;
			WheelColFL.brakeTorque = brakeForce;
			WheelColRR.brakeTorque = brakeForce;
			WheelColRL.brakeTorque = brakeForce;
		}
		else
		{
			WheelColFR.brakeTorque =0;
			WheelColFL.brakeTorque =0;
			WheelColRR.brakeTorque =0;
			WheelColRL.brakeTorque =0;
		}
	}

	void LimitMovements()
	{
		// limit movement speed
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

		rb.maxAngularVelocity = maxSteerSpeed;
	}
}
