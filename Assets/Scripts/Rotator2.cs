/*using UnityEngine;
using System.Collections;

public class Rotator2 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (20, 0, 0) * Time.deltaTime * 10);
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("Collision");
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			Debug.Log ("Collecting a pickup");
			this.gameObject.SetActive (false); 
			IncrementScore incScore = GameObject.FindObjectOfType<IncrementScore> ();
			incScore.addToScore();
		}
	}
}*/
