using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	[SerializeField]
	private bool gasPushed = false;
	[SerializeField]
	private bool brakePushed = false;
	[SerializeField]
	private bool leftPushed = false;
	[SerializeField]
	private bool rightPushed = false;

	private float hAxis = 0f;
	private float vAxis = 0f;

	public bool controllable = true;

	public void Update(){
//		Debug.Log ("Car controller is still alive!");
		if(carRB.velocity.magnitude < 0.001f && Physics.Raycast(transform.position,Vector3.down,0.1f,LayerMask.NameToLayer("Floor"))){
			Debug.Log("We have fallen over. Reset");
			controllable = false;
			StartCoroutine (waitAndReorient (2.0f));
		}
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
			if (Input.GetAxis ("Horizontal") > 0.1f || Input.GetAxis ("Horizontal") < -0.1f) {
//			Debug.Log ("Steering with the keyboard");
				hAxis = Input.GetAxis ("Horizontal");
			} else {
//			Debug.Log ("Steering with touch");
				hAxis = Mathf.Clamp (hAxis + 0.25f * (rightPushed ? 1f : 0f) - 0.25f * (leftPushed ? 1f : 0f), -1f, 1f);
			}
			if (Input.GetAxis ("Vertical") > 0.1f || Input.GetAxis ("Vertical") < -0.1f) {
//			Debug.Log ("Throttle with the keyboard");
				vAxis = Input.GetAxis ("Vertical");
			} else {
//			Debug.Log ("Throttle with touch");
				vAxis = Mathf.Clamp (vAxis + 0.25f * (gasPushed ? 1f : 0f) - 0.25f * (brakePushed ? 1f : 0f), -1f, 1f);
			}
		}
		steering = maxSteeringAngle * hAxis;
		motor = maxMotorTorque * vAxis;

		//steering = maxSteeringAngle * Input.GetAxis ("Horizontal");
		//motor = maxMotorTorque * Input.GetAxis ("Vertical");
			
//		Debug.Log ("M: " + motor);
//		Debug.Log ("S:" + steering);
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

	public void pushGas(){
//		Debug.Log ("Pushing the gas");
		gasPushed = true;
	}

	public void releaseGas(){
//		Debug.Log ("Releasing the gas");
		gasPushed = false;
	}

	public void pushBrake(){
//		Debug.Log ("Pushing the brake");
		brakePushed = true;
	}

	public void releaseBrake(){
//		Debug.Log ("Releasing the brake");
		brakePushed = false;
	}

	public void pushLeft(){
//		Debug.Log ("Pushing the left");
		leftPushed = true;
	}

	public void releaseLeft(){
//		Debug.Log ("Releasing the left");
		leftPushed = false;
	}

	public void pushRight(){
//		Debug.Log ("Pushing the right");
		rightPushed = true;
	}

	public void releaseRight(){
//		Debug.Log ("Releasing the right");
		rightPushed = false;
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
		visualWheel.transform.rotation = rotation;
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
}
