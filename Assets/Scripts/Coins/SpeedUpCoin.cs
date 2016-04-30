using UnityEngine;
using System.Collections;

public class SpeedUpCoin : Coin
{

    public float effectTime;
    public float newBrakingForce;
    public float newMotorTorque;

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

        car.maxBrakingForce = newBrakingForce;
        car.maxMotorTorque = newMotorTorque;

        yield return new WaitForSeconds(effectTime);

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
