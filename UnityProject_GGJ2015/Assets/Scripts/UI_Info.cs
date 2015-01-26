using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Info : MonoBehaviour {

	public Text self;

	// Use this for initialization
	void Start () {
		self.text = "Delay : "+Controller.staticMem.ToString()+" turn";
		if(Controller.staticMem>1) self.text += "s";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
