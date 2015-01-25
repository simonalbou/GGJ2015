using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {

	public float speed;
	public Transform self;
	public Vector3 moveDir;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		self.Translate (moveDir * speed * Time.deltaTime, Space.World);
	}

	public void GoUp(Vector3 init)
	{
		self.localScale = new Vector3(1, -1, 1);
		moveDir = new Vector3(2, 1);
		moveDir = moveDir.normalized;
		self.position = init;
	}

	public void GoRight(Vector3 init)
	{
		self.localScale = new Vector3(1, 1, 1);
		moveDir = new Vector3(2, -1);
		moveDir = moveDir.normalized;
		self.position = init;
	}

	public void GoDown(Vector3 init)
	{
		self.localScale = new Vector3(-1, 1, 1);
		moveDir = new Vector3(-2, -1);
		moveDir = moveDir.normalized;
		self.position = init;
	}

	public void GoLeft(Vector3 init)
	{
		self.localScale = new Vector3(-1, 1, 1);
		moveDir = new Vector3(-2, 1);
		moveDir = moveDir.normalized;
		self.position = init;
	}
}
