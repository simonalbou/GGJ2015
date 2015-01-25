using UnityEngine;
using System.Collections;

public class DaggerEventHandler : MonoBehaviour {

	public Controller parent;


	public void DoneStabbing () {
		parent.DoneAttacking();
	}
}
