using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coin : MonoBehaviour {

	public CoinSpawningSystem spawner;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (20, 0, 0) * Time.deltaTime * 10);
	}

	public void setSpawner(CoinSpawningSystem spawner){
		this.spawner = spawner;
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            SimpleCarController car = col.gameObject.transform.root.GetComponent<SimpleCarController>();
			if (this.spawner != null) {
                this.spawner.pickedUpCoin(this,car);
			}
		}
	}

    public virtual IEnumerator triggerEffect(SimpleCarController car)
    {
        GameObject.FindObjectOfType<IncrementScore>().addToScore(100);
		this.spawner.plusOneHundred.GetComponent<Text>().text = "+ " + (GameObject.FindObjectOfType<IncrementScore> ().scoreScaleFactor * 100);
		this.spawner.plusOneHundred.SetActive (true);


		GameObject.FindObjectOfType<SoundController> ().playCoinSound ();
//		this.spawner.sizePowerupText.SetActive(true);
//		this.spawner.nonePowerupText.SetActive (false);
		yield return new WaitForSeconds (1f);
		//this.spawner.sizePowerupText.SetActive(false);
		this.spawner.plusOneHundred.SetActive (false);
        yield break;
    }


    public virtual bool canPickupDuringPowerup()
    {
        return true;
    }

    public virtual bool isPowerupCoin()
    {
        return false;
    }
    
}
