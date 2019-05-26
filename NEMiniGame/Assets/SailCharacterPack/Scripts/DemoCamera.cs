using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoCamera : MonoBehaviour {

	public List<Transform> Targets;
	public List<Vector2> ZoomLimits;

	public float RotationSpeed;
	public float ZoomSpeed;

	private int currentTargetIndex;
	private Transform currentTarget;
	private Transform rigTransform;

	// Use this for initialization
	void Start () {
		currentTargetIndex = 0;
		currentTarget = Targets[0];
		rigTransform = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		if (rigTransform.position != currentTarget.position)
		{
			rigTransform.position = Vector3.Lerp (rigTransform.position, currentTarget.position, Time.deltaTime);
		}

		if (Input.anyKeyDown)
			currentTargetIndex++;

		if (currentTargetIndex >= Targets.Count)
			currentTargetIndex = 0;

		currentTarget = Targets[currentTargetIndex];

		rigTransform.Rotate (Vector3.up, Time.deltaTime * RotationSpeed * Input.GetAxis ("Mouse X"), Space.World);

		float tiltAmount = Time.deltaTime * RotationSpeed * Input.GetAxis ("Mouse Y");
		if (rigTransform.rotation.eulerAngles.x + tiltAmount < 90 && rigTransform.rotation.eulerAngles.x + tiltAmount > 0)
			rigTransform.Rotate (Vector3.right, tiltAmount, Space.Self);

		float zoomPosition = Mathf.Clamp (transform.localPosition.z + Time.deltaTime * Input.GetAxis ("Mouse ScrollWheel") * ZoomSpeed,
		                                  ZoomLimits[currentTargetIndex].y, ZoomLimits[currentTargetIndex].x);
		transform.localPosition = new Vector3(0, 1, zoomPosition);
	}
}
