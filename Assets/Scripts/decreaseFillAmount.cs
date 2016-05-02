using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class decreaseFillAmount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Image> ().fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Image>().fillAmount -= 1.0f/5 * Time.deltaTime;
	}

	public void resetFillAmount() {
		gameObject.GetComponent<Image> ().fillAmount = 1;
	}

}
