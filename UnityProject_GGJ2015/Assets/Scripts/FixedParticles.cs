using UnityEngine;
using System.Collections;

public class FixedParticles : MonoBehaviour {

	public Renderer self;
	public string sortingLayerName;

	// Use this for initialization
	void Awake () {
		self.sortingLayerName = sortingLayerName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
