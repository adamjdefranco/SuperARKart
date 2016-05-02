using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUpCoin : Coin
{

    public float effectTime;
    public float speedScale;

    private float originalBraking;
    private float originalMotor;

    public override IEnumerator triggerEffect(SimpleCarController car)
    {
        if(car == null)
        {
            Debug.Log("Broken Car!");
            yield break;
        }
        originalMotor = car.maxMotorTorque;
        originalBraking = car.maxBrakingForce;

        Debug.Log("Motor: " + originalMotor);
        Debug.Log("Brake: " + originalBraking);

        car.maxBrakingForce *= speedScale;
        car.maxMotorTorque *= speedScale;

		base.spawner.powerupTimeLeftFillImage.GetComponent<decreaseFillAmount> ().resetFillAmount ();
		base.spawner.powerupText.SetActive(true);
		base.spawner.powerupText.GetComponent<Text> ().text = "Speed Boost";
		base.spawner.nonePowerupText.SetActive (false);
		GameObject.FindObjectOfType<SoundController> ().playPowerupSound ();
        yield return new WaitForSeconds(effectTime);
		base.spawner.powerupText.SetActive (false);
		base.spawner.nonePowerupText.SetActive (true);

        car.maxMotorTorque = originalMotor;
        car.maxBrakingForce = originalBraking;
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
