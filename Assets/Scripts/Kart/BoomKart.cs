using UnityEngine;
using System.Collections;

public class BoomKart : MonoBehaviour {

	public GameObject ExplosionPrefab;
	public Transform respawnPosition;
	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.gameObject.layer == LayerMask.NameToLayer("Explosive")) {
			GameObject explosion = Instantiate (ExplosionPrefab) as GameObject;
			explosion.transform.position = other.collider.gameObject.transform.position;
			StartCoroutine (destroyExplostion (explosion));
//			transform.position = respawnPosition.position;
//			transform.rotation = respawnPosition.rotation;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
	}

	IEnumerator destroyExplostion(GameObject explosion){
		yield return new WaitForSeconds(1f);
		Destroy (explosion);
	}
}
