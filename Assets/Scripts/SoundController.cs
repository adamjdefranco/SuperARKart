using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioSource ExplosionSound;
	public AudioSource CoinSound;
	public AudioSource PowerupSound;
	public AudioSource ClockLowSound;
	public AudioSource GameOver;
	public AudioSource Whoosh;
	public AudioSource InGameMusic;

	public void playExplosionSound() { ExplosionSound.Play (); }
	public void playCoinSound() { CoinSound.Play (); }
	public void playPowerupSound() { PowerupSound.Play (); }
	public void playClockLowSound() { ClockLowSound.Play (); }
	public void StopClockLowSound() { ClockLowSound.Stop (); }
	public void playGameOverSound() { GameOver.Play (); }
	public void playWhooshSound() { Whoosh.Play (); }
	public void playInGameMusic() { InGameMusic.Play (); }

}
