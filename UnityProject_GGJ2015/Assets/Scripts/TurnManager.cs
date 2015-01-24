using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	int turnIndex;
	public BoardData board;
	public Controller[] players;
	public Camera cam;

	void Start ()
	{
		turnIndex = 0;
	}
	
	void Update ()
	{
		if(players[turnIndex].leftInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 3;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].rightInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 1;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].upInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 0;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].downInput)
		{
			if(players[turnIndex].DoMove())
			{
				players[turnIndex].storedMoves[0] = 2;
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
		if(x < 0 || y < 0) return false;
		if(x >= board.width) return false;
		if(y >= board.height) return false;

		foreach(Controller ctrl in players)
		{
			if(ctrl.position.x == x && ctrl.position.y == y) return false;
		}

		return true;
	}

	public void AttackTile(int x, int y)
	{
		foreach(Controller ctrl in players)
		{
			if(ctrl.position.x == x && ctrl.position.y == y)
			{
				ctrl.Die();
				return;
			}
		}
	}
}
