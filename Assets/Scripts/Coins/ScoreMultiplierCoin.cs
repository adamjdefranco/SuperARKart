using UnityEngine;
using System.Collections;

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

        yield return new WaitForSeconds(effectTime);

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
