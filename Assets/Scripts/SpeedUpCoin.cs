using UnityEngine;
using System.Collections;

public class SpeedUpCoin : Coin {

    public float effectTime;
    public float newBrakingForce;
    public float newMotorTorque;
    
    private float originalBraking;
    private float originalMotor;

	protected override void onPickup(SimpleCarController car){
        StartCoroutine(applySpeedup(car));
	}

    IEnumerator applySpeedup(SimpleCarController car)
    {
        yield return new WaitForEndOfFrame();
        //originalMotor = car.maxMotorTorque;
        //originalBraking = car.maxBrakingForce;

        //car.maxBrakingForce = newBrakingForce;
        //car.maxMotorTorque = newMotorTorque;

        //yield return new WaitForSeconds(effectTime);

        //car.maxMotorTorque = originalMotor;
        //car.maxBrakingForce = originalBraking;

    }
}
