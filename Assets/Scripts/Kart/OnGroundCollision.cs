using UnityEngine;
using System.Collections;

public class OnGroundCollision : MonoBehaviour {

	public GameObject ExplosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if( col.gameObject.layer == LayerMask.NameToLayer("Player")) {
//			col.gameObject.GetComponentInParent<Rigidbody> ().useGravity = false;
//			col.transform.parent.position = new Vector3 (0, 3, 0);
//			col.gameObject.GetComponentInParent<Rigidbody> ().velocity = Vector3.zero;
//			col.gameObject.GetComponentInParent<Rigidbody> ().angularVelocity = Vector3.zero;
//			col.transform.parent.rotation = Quaternion.identity;

			GameObject explosion = Instantiate (ExplosionPrefab) as GameObject;
			explosion.transform.position = col.GetComponent<Collider>().gameObject.transform.position;

			GameObject.FindObjectOfType<SimpleCarController>().respawnCar();
			StartCoroutine ("WaitForSomeTime", col.gameObject.GetComponentInParent<Rigidbody> ());

			StartCoroutine (destroyExplostion (explosion));
		} 
	}

	IEnumerator destroyExplostion(GameObject explosion){
		yield return new WaitForSeconds(1f);
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
