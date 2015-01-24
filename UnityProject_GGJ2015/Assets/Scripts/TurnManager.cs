using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	int turnIndex;
	Controller[] players;

	void Start ()
	{
		turnIndex = 0;
	}
	
	void Update ()
	{
		if(players[turnIndex].leftInput)
		{
			if(players[turnIndex].MoveLeft())
			{
				players[turnIndex].storedMoves[0] = 3;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].rightInput)
		{
			if(players[turnIndex].MoveRight())
			{
				players[turnIndex].storedMoves[0] = 1;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].upInput)
		{
			if(players[turnIndex].MoveUp())
			{
				players[turnIndex].storedMoves[0] = 0;
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].downInput)
		{
			if(players[turnIndex].MoveDown())
			{
				players[turnIndex].storedMoves[0] = 2;
				ChangeTurn();
				return;
			}
		}
	}

	public void ChangeTurn()
	{
		turnIndex++;
		if(turnIndex == players.Length) turnIndex = 0;
	}
}
