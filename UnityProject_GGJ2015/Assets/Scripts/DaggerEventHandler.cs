using UnityEngine;
using System.Collections;

public class DaggerEventHandler : MonoBehaviour {

	public Controller parent;
	public AudioSource selfAudio;

	public void DoneStabbing () {
		parent.DoneAttacking();
	}

	public void Sound()
	{
		selfAudio.Play ();
	}
}
