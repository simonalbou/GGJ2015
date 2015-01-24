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

	// Use this for initialization
	void Start () {
		LoadInput();
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

	public bool MoveLeft()
	{
		if(position.x == 0) return false;

		position.x--;
		self.Translate(-board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveRight()
	{
		if(position.x == board.width-1) return false;
		position.x++;
		self.Translate(board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}

	public bool MoveUp()
	{
		if(position.y == board.height-1) return false;
		position.y++;
		self.Translate(board.tileSize.x, board.tileSize.y, 0);
		return true;
	}

	public bool MoveDown()
	{
		if(position.y == 0) return false;
		position.y--;
		self.Translate(-board.tileSize.x, -board.tileSize.y, 0);
		return true;
	}
}
