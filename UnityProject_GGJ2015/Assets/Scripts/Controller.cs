using UnityEngine;
using System.Collections;

public enum AttackRange { SingleAttack, SpinAttack, LongShot }

public class Controller : MonoBehaviour
{
	public bool isPlayer2;
	private KeyCode[] leftKeys, rightKeys, upKeys, downKeys, attackKeys;
	[HideInInspector]
	public bool leftInput, rightInput, upInput, downInput, attackInput;

	[HideInInspector]
	public int orientation; // 0 means up, 1 means right, 2 means down, 3 means left
	public Vector2 position;

	public Transform self;

	public BoardData board;
	public TurnManager manager;

	public int memoryAmount;
	[HideInInspector]
	public int[] storedMoves; // 0 means up, 1 means right, 2 means down, 3 means left

	[HideInInspector]
	public AttackRange range;

	// Use this for initialization
	void Start () {
		LoadInput();
		storedMoves = new int[memoryAmount];
		for(int i=0; i<storedMoves.Length; i++)
		{
			storedMoves[i] = -1;
		}

		RefreshPosition();

		orientation = isPlayer2 ? 0 : 2;

		range = AttackRange.SingleAttack;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInput();
		//if(leftInput) Debug.Log (name);
	}

	void LoadInput()
	{
		if(isPlayer2)
		{
			leftKeys = KeyBindingP2.leftKeys;
			rightKeys = KeyBindingP2.rightKeys;
			upKeys = KeyBindingP2.upKeys;
			downKeys = KeyBindingP2.downKeys;
			attackKeys = KeyBindingP2.attackKeys;
		}
		else
		{
			leftKeys = KeyBindingP1.leftKeys;
			rightKeys = KeyBindingP1.rightKeys;
			upKeys = KeyBindingP1.upKeys;
			downKeys = KeyBindingP1.downKeys;
			attackKeys = KeyBindingP1.attackKeys;
		}
	}

	void UpdateInput()
	{
		/**
		UpdateSingleInput(leftKeys, leftInput);
		UpdateSingleInput(rightKeys, rightInput);
		UpdateSingleInput(upKeys, upInput);
		UpdateSingleInput(downKeys, downInput);
		//*/

		leftInput = false;
		foreach(KeyCode key in leftKeys)
		{
			if(Input.GetKeyDown(key))
			{
				leftInput = true;
			}
		}

		rightInput = false;
		foreach(KeyCode key in rightKeys)
		{
			if(Input.GetKeyDown(key))
			{
				rightInput = true;
			}
		}

		upInput = false;
		foreach(KeyCode key in upKeys)
		{
			if(Input.GetKeyDown(key))
			{
				upInput = true;
			}
		}

		downInput = false;
		foreach(KeyCode key in downKeys)
		{
			if(Input.GetKeyDown(key))
			{
				downInput = true;
			}
		}

		attackInput = false;
		foreach(KeyCode key in attackKeys)
		{
			if(Input.GetKeyDown(key))
			{
				attackInput = true;
			}
		}
	}

	void UpdateSingleInput(KeyCode[] keys, bool input)
	{
		input = false;
		foreach(KeyCode key in keys)
		{
			if(Input.GetKeyDown(key))
			{
				input = true;
			}
		}
	}

	public void ResetInputs()
	{
		leftInput = false;
		rightInput = false;
		upInput = false;
		downInput = false;
	}

	public bool DoMove()
	{
		switch(storedMoves[storedMoves.Length-1])
		{
			case -1 :
				break;
			case 0 :
				MoveUp();
				break;
			case 1 :
				MoveRight ();
				break;
			case 2 :
				MoveDown ();
				break;
			case 3 :
				MoveLeft();
				break;
			case 4 :
				Attack();
				break;
		}

		for(int i=1; i<storedMoves.Length; i++)
		{
			storedMoves[i] = storedMoves[i-1];
		}

		return true;
	}

	public bool MoveLeft()
	{
		//if(position.x == 0) return false;

		orientation = 3;
		if(manager.CheckRoom(position.x-1, position.y)) position.x--;
		RefreshPosition();
		//self.Translate(-board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveRight()
	{
		//if(position.x == board.width-1) return false;

		orientation = 1;
		if(manager.CheckRoom(position.x+1, position.y)) position.x++;
		RefreshPosition();
		//self.Translate(board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}

	public bool MoveUp()
	{
		//if(position.y == board.height-1) return false;

		orientation = 0;
		if(manager.CheckRoom(position.x, position.y+1)) position.y++;
		RefreshPosition();
		//self.Translate(board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveDown()
	{
		//if(position.y == 0) return false;

		orientation = 2;
		if(manager.CheckRoom(position.x, position.y-1)) position.y--;
		RefreshPosition();
		//self.Translate(-board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}

	public void Attack()
	{
		if(range == AttackRange.SingleAttack) SingleAttack();
		if(range == AttackRange.SpinAttack) SpinAttack();
		if(range == AttackRange.LongShot) LongShot();
	}

	public void Die()
	{

	}

	public void SingleAttack()
	{

	}

	public void SpinAttack()
	{

	}

	public void LongShot()
	{

	}

	public void RefreshPosition()
	{
		self.position = board.self.position + board.offsetX * position.x + board.offsetY * position.y;
	}
}