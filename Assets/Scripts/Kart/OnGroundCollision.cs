using UnityEngine;
using System.Collections;

public class OnGroundCollision : MonoBehaviour {

	public GameObject ExplosionPrefab;
	private bool CanCreateNewExplosion;

	// Use this for initialization
	void Start () {
		CanCreateNewExplosion = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the trigger was the ground collider, the player has crashed, show an explosion and place user back at the beginning
	void OnTriggerEnter(Collider col) {
		if( col.gameObject.layer == LayerMask.NameToLayer("Player")) {

			if (CanCreateNewExplosion) {
				// Plays explosion
				GameObject.FindObjectOfType<SoundController> ().playExplosionSound ();
				GameObject explosion = Instantiate (ExplosionPrefab) as GameObject;
				explosion.transform.position = col.GetComponent<Collider> ().gameObject.transform.position;
				CanCreateNewExplosion = false;
				StartCoroutine (destroyExplostion (explosion));
			}

			GameObject.FindObjectOfType<SimpleCarController>().respawnCar();
			StartCoroutine ("WaitForSomeTime", col.gameObject.GetComponentInParent<Rigidbody> ());
		} 
	}

	IEnumerator destroyExplostion(GameObject explosion){
		yield return new WaitForSeconds(1f);
		CanCreateNewExplosion = true;
		Destroy (explosion);
	}

	IEnumerator WaitForSomeTime(Rigidbody car) {
		yield return new WaitForSeconds (1.0f);
		dropCar (car);
	}

	void dropCar(Rigidbody rb) {
		//rb.isKinematic = false;
		rb.useGravity = true;
	}
}
