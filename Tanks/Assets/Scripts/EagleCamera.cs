using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleCamera : MonoBehaviour
{
	public Transform target;
	public float minSize = 10f;
	public float maxSize = 30f;
	public float sizeTick = 0.7f;
	public float sizeSmooth = 0.3f;
	public float verticalOffset;

	private float scrollValue;
	private float desiredSize;
	private float resizeSpeedRef;
	private Vector3 offset;
	private Camera cam;

	void Start()
	{
		cam = GetComponent<Camera>();

		// set initial size of camera
		desiredSize = minSize + ((maxSize - minSize) / 3);

		// get offset to zero -> for consistent distance to tank
		offset = transform.position;
	}

	void FixedUpdate()
	{
		if (target == null)
			return;

		// get input for camera
		scrollValue = Input.GetAxisRaw("MouseScrollWheel");

		// set camera size
		setSize();

		// follow player tank
		followTarget();
	}

	void setSize()
	{
		// set new size by input
		if (scrollValue < 0f)
			desiredSize += sizeTick;
		else if (scrollValue > 0f)
			desiredSize -= sizeTick;

		// clamp value to min/max
		desiredSize = Mathf.Clamp(desiredSize, minSize, maxSize);

		// set new size
		cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, desiredSize, ref resizeSpeedRef, sizeSmooth);
	}

	void followTarget()
	{
		// get vertical offset
		Vector3 vertOffset = Vector3.zero;
		vertOffset.Set(verticalOffset, 0, verticalOffset);

		// set new position
		transform.position = target.position + offset + vertOffset;
	}
}
