/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This behaviour associates a Virtual Button with a game object. Use the
    /// functionality in ImageTargetBehaviour to create and destroy Virtual Buttons
    /// at run-time.
    /// </summary>
    public class VirtualButtonBehaviour : VirtualButtonAbstractBehaviour
    {
		public GameObject theTestCube;

		void Update() {
			if (this.Pressed) {
				Debug.Log ("Triggered");
				Debug.Log (theTestCube.activeSelf);
				if (theTestCube.activeSelf) {
					theTestCube.SetActive (false);
				} else {
					theTestCube.SetActive (true);
				}
			}
			//MonoBehavior.StartCoroutine(pauseForSecond(2.0F));
		}
    }

	/*private IEnumerator pauseForSecond(float time) {
		this.enabled = false;
		yield return new WaitForSeconds(waitTime);

	}*/
}
