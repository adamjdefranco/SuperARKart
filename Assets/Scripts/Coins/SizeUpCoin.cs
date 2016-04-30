using UnityEngine;
using System.Collections;

public class SizeUpCoin : Coin
{

    public float effectTime;
    public Vector3 newScale;
    public float massScaleFactor;

    private Vector3 originalScale;
    private float originalMass;

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

        yield return new WaitForSeconds(effectTime);

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
