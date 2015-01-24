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
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].rightInput)
		{
			if(players[turnIndex].MoveRight())
			{
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].upInput)
		{
			if(players[turnIndex].MoveUp())
			{
				ChangeTurn();
				return;
			}
		}
		if(players[turnIndex].downInput)
		{
			if(players[turnIndex].MoveDown())
			{
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
