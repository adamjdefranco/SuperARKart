using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class SimpleCarController : MonoBehaviour
{
	public List<AxleInfo> axleInfos;
	// the information about each individual axle
	public float maxMotorTorque;
	// maximum torque the motor can apply to wheel
	public float maxSteeringAngle;
	// maximum steer angle the wheel can have
	public float maxBrakingForce;

	public Rigidbody carRB;

	private float up;
	private float right;
    
	private float hAxis = 0f;
	private float vAxis = 0f;

	public bool controllable = true;

	public void Update(){
		if(controllable && carRB.velocity.magnitude < 0.001f && !Physics.Raycast(transform.position,transform.up*-1,0.3f,LayerMask.NameToLayer("Floor"))){
			controllable = false;
			StartCoroutine (waitAndReorient (2.0f));
		}
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.up * -1*0.3f);
    }

	private IEnumerator waitAndReorient(float time){
		controllable = false;
		yield return new WaitForSeconds(time);
		reorientCar (transform.position);
		carRB.useGravity = true;
		controllable = true;
	}

	public void FixedUpdate ()
	{
		float motor = 0;
		float steering = 0;
		if (controllable) {
                vAxis = CrossPlatformInputManager.GetAxis("Vertical");
                hAxis = CrossPlatformInputManager.GetAxis("Horizontal");
		}
		steering = maxSteeringAngle * hAxis;
		motor = maxMotorTorque * vAxis;

		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.collider.steerAngle = steering;
				axleInfo.rightWheel.collider.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.collider.motorTorque = motor;
				axleInfo.rightWheel.collider.motorTorque = motor;
			} else {
				axleInfo.leftWheel.collider.brakeTorque = maxBrakingForce;
				axleInfo.rightWheel.collider.brakeTorque = maxBrakingForce;
			}
			ApplyLocalPositionToVisuals(axleInfo.leftWheel);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel);
		}
	}

	private void reorientCar(Vector3 position){
		GameObject theCar = this.gameObject;

		theCar.GetComponent<Rigidbody> ().useGravity = false;
		theCar.transform.position = position;
		theCar.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		theCar.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		theCar.transform.rotation = Quaternion.identity;

		hAxis = 0f;
		vAxis = 0f;
	}

	public void respawnCar() {
		controllable = false;
		reorientCar (new Vector3 (0, 3, 0));
		carRB.useGravity = true;
		controllable = true;
	}

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(Wheel wheel)
    {
        if (wheel.mesh == null) {
            return;
        }

        Transform visualWheel = wheel.mesh.transform;

        Vector3 position;
        Quaternion rotation;
        wheel.collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        if (wheel.shouldFlip)
        {
            visualWheel.transform.rotation = rotation * Quaternion.Euler(new Vector3(0,180,0));
        }
        else {
            visualWheel.transform.rotation = rotation;
        }
	}
}

[System.Serializable]
public class AxleInfo
{
	public Wheel leftWheel;
	public Wheel rightWheel;
	public bool motor;
	// is this wheel attached to motor?
	public bool steering;
	// does this wheel apply steer angle?
}

[System.Serializable]
public class Wheel 
{
	public WheelCollider collider;
	public GameObject mesh;
    public bool shouldFlip = false;
}
