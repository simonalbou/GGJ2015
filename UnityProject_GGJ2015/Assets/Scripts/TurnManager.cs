using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	int turnIndex;
	public BoardData board;
	public Controller[] players;
	public Camera cam;

	private bool gameOver;

	public Transform glowTileUp, glowTileDown, glowTileRight, glowTileLeft;

	private float gameOverTimestamp;

	void Start ()
	{
		turnIndex = 0;
		gameOver = false;
	}
	
	void Update ()
	{
		if(gameOver && gameOverTimestamp > Time.time+3)
		{
			cam.backgroundColor = Color.black;
			if(Input.anyKeyDown) Application.LoadLevel(Application.loadedLevel);
		}

		if(gameOver) return;

		if(players[turnIndex].stillMoving || players[turnIndex].stillAttacking) return;

		if(players[turnIndex].leftInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 3;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				//ChangeTurn();
				return;
			}
			else
			{
				players[turnIndex].storedMoves[0] = 3;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				ChangeTurn ();
				return;
			}
		}
		if(players[turnIndex].rightInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 1;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				//ChangeTurn();
				return;
			}
			else
			{
				players[turnIndex].storedMoves[0] = 1;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				ChangeTurn ();
				return;
			}
		}
		if(players[turnIndex].upInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 0;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				//ChangeTurn();
				return;
			}
			else
			{
				players[turnIndex].storedMoves[0] = 0;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				ChangeTurn ();
				return;
			}
		}
		if(players[turnIndex].downInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 2;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				//ChangeTurn();
				return;
			}
			else
			{
				players[turnIndex].storedMoves[0] = 2;
				if(players[turnIndex].cooldown > 0) players[turnIndex].cooldown--;
				ChangeTurn ();
				return;
			}
		}
		if(players[turnIndex].attackInput && players[turnIndex].cooldown == 0)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 4;
				players[turnIndex].cooldown = players[turnIndex].memoryAmount;
				//ChangeTurn();
				return;
			}
			else
			{
				players[turnIndex].storedMoves[0] = 4;
				players[turnIndex].cooldown = players[turnIndex].memoryAmount;
				ChangeTurn ();
				return;
			}
		}

		if(players[turnIndex].attackInput && players[turnIndex].cooldown == 0)
			Debug.Log ("heh");
	}

	public void ChangeTurn()
	{
		turnIndex++;
		if(turnIndex == players.Length) turnIndex = 0;

		switch(turnIndex)
		{
			case 0 :
				cam.backgroundColor = new Color(0,0,0.25f);	
				break;
			default :
				cam.backgroundColor = new Color(0.25f,0,0);
				break;
		}

		glowTileUp.position = Vector3.up*3000;
		glowTileDown.position = Vector3.up*3000;
		glowTileLeft.position = Vector3.up*3000;
		glowTileRight.position = Vector3.up*3000;

		if(gameOver) return;

		int x = (int)players[turnIndex].position.x;
		int y = (int)players[turnIndex].position.y;

		if(board.isWalkable(x-1, y) && !board.isDeadly (x-1, y) && board.isExisting(x-1, y))
			glowTileLeft.position = board.tiles[x-1,y].transform.position;
		if(board.isWalkable(x+1, y) && !board.isDeadly (x+1, y) && board.isExisting(x+1, y))
			glowTileRight.position = board.tiles[x+1,y].transform.position;
		if(board.isWalkable(x, y+1) && !board.isDeadly (x, y+1) && board.isExisting(x, y+1))
			glowTileUp.position = board.tiles[x,y+1].transform.position;
		if(board.isWalkable(x, y-1) && !board.isDeadly (x, y-1) && board.isExisting(x, y-1))
			glowTileDown.position = board.tiles[x,y-1].transform.position;
	}

	public bool CheckRoom(float x, float y)
	{
		if(x < 0 || y < 0) return true; // il mourra
		if(x >= board.width) return true; // là aussi
		if(y >= board.height) return true; // là aussi

		foreach(Controller ctrl in players)
		{
			if(ctrl.position.x == x && ctrl.position.y == y) return false;
		}

		return true;
	}

	public void AttackTile(float x, float y)
	{
		foreach(Controller ctrl in players)
		{
			if(ctrl.position.x == x && ctrl.position.y == y)
			{
				ctrl.Die(DeathType.Stabbed);
				return;
			}
		}
	}

	public void EndGame(bool pOneWins)
	{
		gameOver = true;
		cam.backgroundColor = Color.black;
	}
}
