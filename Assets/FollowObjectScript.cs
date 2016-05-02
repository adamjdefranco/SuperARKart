using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class FollowObjectScript : MonoBehaviour {

	public GameObject targetObject;
	private bool staticCam;

	Vector3 distanceVector;

	// Use this for initialization
	void Start () {
		staticCam = true;
		if (targetObject == null) {
			Debug.LogError ("No Initial Camera Target Selected!");
		}
		distanceVector = transform.position - targetObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = targetObject.transform.position + distanceVector;
		transform.LookAt(targetObject.transform.position);
	}
}
