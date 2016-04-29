using UnityEngine;
using System.Collections;

public class FollowCamScript : MonoBehaviour {

	public GameObject target;

	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		if (cam == null) {
			Debug.LogError ("No Camera found for follow script.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (cam != null && target != null) {
			cam.transform.LookAt (target.transform);
		}
	}
}
