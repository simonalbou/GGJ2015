using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	int turnIndex;
	public Controller[] players;

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
	}
}
