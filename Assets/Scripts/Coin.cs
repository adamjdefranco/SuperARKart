using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	CoinSpawningSystem spawner;
	private IncrementScore userScore;

	void Awake() {
		userScore = GameObject.FindObjectOfType<IncrementScore> ();
	}

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
			Debug.Log ("Here5");
			onPickup ();
		}
	}

	protected virtual void onPickup(){
		userScore.addToScore (100);
		spawner.plusOneHundred.SetActive (true);
		StartCoroutine (WaitForHalfASecond());
	}

	IEnumerator WaitForHalfASecond() {
		Debug.Log ("Here");
		yield return new WaitForSeconds(1f);
		Debug.Log ("Here1");
		spawner.plusOneHundred.SetActive (false);
		Debug.Log ("Here2");
	}


}
