using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SizeUpCoin : Coin
{

    public float effectTime;
    public Vector3 newScale;
    public float massScaleFactor;

    private Vector3 originalScale;
    private float originalMass;


	void Start() {
	}

	void Update() {
		transform.Rotate (new Vector3 (20, 0, 0) * Time.deltaTime * 10);
	}


    public override IEnumerator triggerEffect(SimpleCarController car)
    {
        if(car == null)
        {
            Debug.Log("Broken Car!");
            yield break;
        }
        originalScale = car.transform.root.gameObject.transform.localScale;
        originalMass = car.carRB.mass;

        car.transform.root.gameObject.transform.localScale = newScale;
        car.carRB.mass *= massScaleFactor;

		base.spawner.powerupText.SetActive(true);
		base.spawner.powerupText.GetComponent<Text> ().text = "Size Up";
		base.spawner.nonePowerupText.SetActive (false);
		GameObject.FindObjectOfType<SoundController> ().playPowerupSound ();
		base.spawner.powerupTimeLeftFillImage.GetComponent<decreaseFillAmount> ().resetFillAmount ();

        yield return new WaitForSeconds(effectTime);
		base.spawner.powerupText.SetActive (false);
		base.spawner.nonePowerupText.SetActive (true);

        car.carRB.mass = originalMass;
        car.transform.root.gameObject.transform.localScale = originalScale;
    }

    public override bool isPowerupCoin()
    {
        return true;
    }

    public override bool canPickupDuringPowerup()
    {
        return false;
    }

}
