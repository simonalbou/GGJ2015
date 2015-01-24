using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	int turnIndex;
	public BoardData board;
	public Controller[] players;
	public Camera cam;

	private bool gameOver;

	void Start ()
	{
		turnIndex = 0;
		gameOver = false;
	}
	
	void Update ()
	{
		if(gameOver)
		{
			cam.backgroundColor = Color.black;
			if(Input.anyKeyDown) Application.LoadLevel(Application.loadedLevel);
			return;
		}

		if(players[turnIndex].leftInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 3;
				players[turnIndex].cooldown--;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].rightInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 1;
				players[turnIndex].cooldown--;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].upInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 0;
				players[turnIndex].cooldown--;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].downInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 2;
				players[turnIndex].cooldown--;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].attackInput && players[turnIndex].cooldown == 0)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 4;
				players[turnIndex].cooldown = players[turnIndex].memoryAmount;
				ChangeTurn();
				return;
			}
		}

	}

	public void ChangeTurn()
	{
		//Debug.Log ("hi");
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
