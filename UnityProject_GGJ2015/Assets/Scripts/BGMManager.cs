using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

	public AudioSource self;
	public AudioClip start, loop;

	private float timer;
	private bool changed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(changed) return;
		timer += Time.deltaTime;

		if(timer > start.length)
		{
			self.clip = loop;
			self.Play ();
			changed = true;
		}
	}
}
