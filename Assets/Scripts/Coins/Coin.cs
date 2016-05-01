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
