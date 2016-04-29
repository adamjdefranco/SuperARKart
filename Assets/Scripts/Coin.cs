using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	CoinSpawningSystem spawner;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (20, 0, 0) * Time.deltaTime * 10);
	}

	public void setSpawner(CoinSpawningSystem spawner){
		this.spawner = spawner;
	}

	void OnTriggerEnter(Collider col) {
		Debug.Log ("COIN COLLISION");
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
			if (this.spawner != null) {
				this.spawner.removeCoin (this);
			}
			onPickup ();
		}
	}

	protected virtual void onPickup(){
		GameObject.FindObjectOfType<IncrementScore> ().addToScore (100);
	}
}
