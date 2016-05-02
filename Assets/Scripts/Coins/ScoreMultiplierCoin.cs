using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreMultiplierCoin : Coin
{

    public float effectTime;
    public float scoreScale;

    private float originalScoreScale;

    public override IEnumerator triggerEffect(SimpleCarController car)
    {
        if(car == null)
        {
            Debug.Log("Broken Car!");
            yield break;
        }

        IncrementScore scoreHolder = GameObject.FindObjectOfType<IncrementScore>();

        originalScoreScale = scoreHolder.scoreScaleFactor;

        scoreHolder.scoreScaleFactor = scoreScale;

		base.spawner.powerupTimeLeftFillImage.GetComponent<decreaseFillAmount> ().resetFillAmount ();
		base.spawner.powerupText.SetActive(true);
		base.spawner.powerupText.GetComponent<Text> ().text = "Multiplier " + scoreScale + "X";
		base.spawner.nonePowerupText.SetActive (false);
		if (scoreScale == .5f) {
			GameObject.FindObjectOfType<SoundController> ().playHalfCoinMultiplierSound ();
		} else {
			GameObject.FindObjectOfType<SoundController> ().playPowerupSound ();
		}

        yield return new WaitForSeconds(effectTime);
		base.spawner.powerupText.SetActive (false);
		base.spawner.nonePowerupText.SetActive (true);

        scoreHolder.scoreScaleFactor = originalScoreScale;
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
