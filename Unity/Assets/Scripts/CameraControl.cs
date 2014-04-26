using System;
using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	
	public Vector3 Center;

	[Range(0, 1000)]
	public float Distance = 500.0f;
	[Range(0, Mathf.PI * 2)]
	public float AngleHorizontal = Mathf.PI / 4;
	[Range(0, Mathf.PI / 2)]
	public float AngleVertical = Mathf.PI / 4;
	public bool AngleVerticalAllowLimits = false;
	[Range(-Mathf.PI / 2, Mathf.PI / 2)]
	public float AngleVerticalMinimumLimit = 0;
	[Range(-Mathf.PI / 2, Mathf.PI / 2)]
	public float AngleVerticalMaximumLimit = Mathf.PI / 2;
	private float field_AngleVerticalDistanceFromLimits = 0.0001f;

	public KeyCode KeyUp = KeyCode.W;
	public KeyCode KeyDown = KeyCode.S;
	public KeyCode KeyLeft = KeyCode.A;
	public KeyCode KeyRight = KeyCode.D;
	public KeyCode KeyZoomIn = KeyCode.E;
	public KeyCode KeyZoomOut = KeyCode.Q;

	public float AngleHorizontalSpeed = Mathf.PI * 2;
	public float AngleVerticalSpeed = Mathf.PI / 2;

	void Start ()
	{
	}
	
	void LateUpdate ()
	{
		if (AngleVerticalMinimumLimit > AngleVerticalMaximumLimit)
		{
			float temp = (AngleVerticalMinimumLimit + AngleVerticalMaximumLimit) / 2;
			AngleVerticalMinimumLimit = temp;
			AngleVerticalMaximumLimit = temp;
		}

		if (Input.GetKey(KeyLeft))
		{
			AngleHorizontal -= Time.deltaTime * AngleHorizontalSpeed;
		}
		if (Input.GetKey(KeyRight))
		{
			AngleHorizontal += Time.deltaTime * AngleHorizontalSpeed;
		}
		if (Input.GetKey(KeyUp))
		{
			AngleVertical += Time.deltaTime * AngleVerticalSpeed;
		}
		if (Input.GetKey(KeyDown))
		{
			AngleVertical -= Time.deltaTime * AngleVerticalSpeed;
		}
		if (Input.GetKey(KeyZoomIn) || Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			Distance *= 0.9f;
		}
		if (Input.GetKey(KeyZoomOut) || Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			Distance /= 0.9f;
		}

		AngleVertical = Mathf.Repeat(AngleVertical, Mathf.PI * 2);
		AngleHorizontal = Mathf.Repeat(AngleHorizontal, Mathf.PI * 2);

		if (AngleVerticalAllowLimits && AngleVertical <= AngleVerticalMinimumLimit)
			AngleVertical = AngleVerticalMinimumLimit + field_AngleVerticalDistanceFromLimits;
		if (AngleVerticalAllowLimits && AngleVertical >= AngleVerticalMaximumLimit)
			AngleVertical = AngleVerticalMaximumLimit - field_AngleVerticalDistanceFromLimits;
		
		transform.position = new Vector3
			(
			Distance * Mathf.Cos(AngleHorizontal) * Mathf.Cos(AngleVertical),
			Distance * Mathf.Sin(AngleVertical),
			Distance * Mathf.Sin(AngleHorizontal) * Mathf.Cos(AngleVertical)
			) + Center;
		transform.LookAt(Center);
		
	}
}
