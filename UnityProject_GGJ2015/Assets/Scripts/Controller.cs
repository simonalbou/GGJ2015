using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	public bool isPlayer2;
	private KeyCode[] leftKeys, rightKeys, upKeys, downKeys;
	[HideInInspector]
	public bool leftInput, rightInput, upInput, downInput;

	public Vector2 position;

	public Transform self;

	public BoardData board;

	public int memoryAmount;
	[HideInInspector]
	public int[] storedMoves; // 0 means up, 1 means right, 2 means down, 3 means left

	// Use this for initialization
	void Start () {
		LoadInput();
		storedMoves = new int[memoryAmount];
		for(int i=0; i<storedMoves.Length; i++)
		{
			storedMoves[i] = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateInput();
	}

	void LoadInput()
	{
		if(isPlayer2)
		{
			leftKeys = KeyBindingP2.leftKeys;
			rightKeys = KeyBindingP2.rightKeys;
			upKeys = KeyBindingP2.upKeys;
			downKeys = KeyBindingP2.downKeys;
		}
		else
		{
			leftKeys = KeyBindingP1.leftKeys;
			rightKeys = KeyBindingP1.rightKeys;
			upKeys = KeyBindingP1.upKeys;
			downKeys = KeyBindingP1.downKeys;
		}
	}

	void UpdateInput()
	{
		UpdateSingleInput(leftKeys, leftInput);
		UpdateSingleInput(rightKeys, rightInput);
		UpdateSingleInput(upKeys, upInput);
		UpdateSingleInput(downKeys, downInput);
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

	public void DoMove()
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
		}

		for(int i=1; i<storedMoves.Length; i++)
		{
			storedMoves[i] = storedMoves[i-1];
		}
	}

	public bool MoveLeft()
	{
		//if(position.x == 0) return false;

		position.x--;
		RefreshPosition();
		//self.Translate(-board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveRight()
	{
		//if(position.x == board.width-1) return false;

		position.x++;
		RefreshPosition();
		//self.Translate(board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}

	public bool MoveUp()
	{
		//if(position.y == board.height-1) return false;

		position.y++;
		RefreshPosition();
		//self.Translate(board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveDown()
	{
		//if(position.y == 0) return false;

		position.y--;
		RefreshPosition();
		//self.Translate(-board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}

	public void RefreshPosition()
	{
		self.position = board.self.position + board.offsetX * position.x + board.offsetY * position.y;
	}
}
