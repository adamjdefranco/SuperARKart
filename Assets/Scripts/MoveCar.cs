using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.Vehicles.Car
{
	//[RequireComponent(typeof (CarController))]
public class MoveCar : MonoBehaviour {

	public float thrust;
	public Rigidbody rb;
	public VirtualJoystick joystick;
		public int steerAngle;

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
	}

	/*void Update() 
	{
		float moveVertical = joystick.Vertical() * 2;//Input.GetAxis ("Vertical");
		float moveHorizontal = joystick.Horizontal();//Input.GetAxis ("Horizontal");

		//Vector3 turns = new Vector3 (0, moveHorizontal);
		//Vector3 movement = new Vector3 (0, 0, moveVertical);

		//rb.AddRelativeTorque (turns * (thrust/5));
		//rb.AddForce (movement * thrust);

			rb.velocity += moveVertical * transform.forward * thrust;

			rb.AddTorque(transform.up * moveHorizontal * steerAngle, ForceMode.Acceleration);

		if (Input.GetKey(KeyCode.UpArrow)) {
			rb.AddForce (transform.forward * thrust);
		}
		if (Input.GetKey(KeyCode.DownArrow)) {
			rb.AddForce (-transform.forward * thrust);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			rb.AddRelativeTorque (Vector3.up * thrust);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rb.AddRelativeTorque (Vector3.down * thrust);
		}
	}*/





	/*private CarController m_Car; // the car controller we want to use


	private void Awake()
	{
		// get the car controller
		m_Car = GetComponent<CarController>();
	}


	private void FixedUpdate()
	{
		// pass the input to the car!
		float v = joystick.Vertical();
		float h = joystick.Horizontal();
		//float handbrake = CrossPlatformInputManager.GetAxis("Jump");
		//m_Car.Move(h, v, v, handbrake);
		m_Car.Move(h, v, v, 0f);
	}*/
}
}

