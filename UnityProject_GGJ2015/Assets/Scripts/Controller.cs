using UnityEngine;
using System.Collections;

public enum AttackRange { SingleAttack, SpinAttack, LongShot }
public enum DeathType { Stabbed, Lava, FellOff }

public class Controller : MonoBehaviour
{
	public float speed;
	[HideInInspector]
	public Vector3 targetPos;

	public bool isPlayer2;
	private KeyCode[] leftKeys, rightKeys, upKeys, downKeys, attackKeys;
	[HideInInspector]
	public bool leftInput, rightInput, upInput, downInput, attackInput;

	[HideInInspector]
	public int orientation; // 0 means up, 1 means right, 2 means down, 3 means left
	public Vector2 position;

	public Transform self;
	public Renderer selfRenderer, daggerRenderer;
	public Animator selfAnim, daggerAnim;

	public BoardData board;
	public TurnManager manager;

	public int memoryAmount;
	[HideInInspector]
	public int[] storedMoves; // 0 means up, 1 means right, 2 means down, 3 means left

	[HideInInspector]
	public AttackRange range;

	[HideInInspector]
	public int cooldown;

	public GameObject v_exploLava;

	private bool dead, fell;

	[HideInInspector]
	public bool stillMoving, stillAttacking;

	private int spinStacks, shootStacks;

	// Use this for initialization
	void Start () {
		LoadInput();
		storedMoves = new int[memoryAmount];
		for(int i=0; i<storedMoves.Length; i++)
		{
			storedMoves[i] = -1;
		}

		orientation = isPlayer2 ? 2 : 1;

		range = AttackRange.SingleAttack;

		cooldown = 0;

		RefreshPosition();

		self.position = targetPos;

		stillMoving = false;

		manager.ChangeTurn();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(fell) self.Translate(speed * 0.75f * Time.deltaTime * Vector3.down);
		if(dead) return;
		if(stillMoving && !stillAttacking)
		{
			//Debug.Log (name);
			self.Translate(Vector3.Normalize(targetPos-self.position) * speed * Time.deltaTime);
			if(Vector3.Distance(targetPos, self.position) < 0.1f)
			{
				self.position = targetPos;
				stillMoving = false;
				EndTurn();
			}

		}
		UpdateInput();
		//if(leftInput) Debug.Log (name);
	}

	// Animation Event
	public void DoneAttacking()
	{
		stillAttacking = false;
		stillMoving = false;
		EndTurn ();
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

	void EndTurn()
	{
		if(position.x < 0 || position.y < 0 || position.x >= board.width || position.y >= board.height)
		{
			if(position.x < 0 || position.y >= board.height) selfRenderer.sortingOrder = -100;
			if(position.x >= board.width || position.y < 0) selfRenderer.sortingOrder = 100;
			Die (DeathType.FellOff);
			return;
		}
		
		if(board.isDeadly((int)position.x, (int)position.y)) Die (DeathType.Lava);

		if(manager.isOnCollectible(position))
		{
			manager.DestroyCollectible();
			switch(manager.currentAvailableBonus)
			{
				case 0 :
					range = AttackRange.SpinAttack;
					spinStacks++;
					break;
				case 1 :
					range = AttackRange.LongShot;
					shootStacks++;
					break;
			}
		}

		manager.ChangeTurn();
	}

	public bool DoMove()
	{
		manager.selfAudio.PlayOneShot(manager.SFX[1]);

		switch(storedMoves[storedMoves.Length-1])
		{
			case -1 :
				return false;
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
		stillAttacking = true;
		if(range == AttackRange.SingleAttack) SingleAttack();
		if(range == AttackRange.SpinAttack) SpinAttack();
		if(range == AttackRange.LongShot) LongShot();
	}

	public void Die(DeathType death)
	{
		dead = true;
		if(death == DeathType.FellOff)
		{
			fell = true;
			manager.selfAudio.PlayOneShot(manager.SFX[3]);
			selfAnim.SetTrigger ("TR_FellOff");
		}
		if(death == DeathType.Lava)
		{
			selfAnim.SetTrigger ("TR_Lava");
			manager.selfAudio.PlayOneShot(manager.SFX[2]);
		}
		if(death == DeathType.Stabbed)
		{
			selfAnim.SetTrigger ("TR_Stabbed");
			DeathSound();
		}
		manager.EndGame (isPlayer2);
	}

	public void SingleAttack()
	{
		daggerAnim.SetTrigger("TR_Stabs");
		switch(orientation)
		{
			case 0 :
				manager.AttackTile (position.x, position.y+1);
				break;
			case 1 :
				manager.AttackTile (position.x+1, position.y);
				break;
			case 2:
				manager.AttackTile (position.x, position.y-1);
				break;
			case 3 :
				manager.AttackTile (position.x-1, position.y);
				break;
		}
	}

	public void SpinAttack()
	{
		spinStacks--;
		if(spinStacks == 0)
		{
			range = shootStacks == 0 ? AttackRange.SingleAttack : AttackRange.LongShot;
		}
		selfAnim.SetTrigger("TR_Spins");
		manager.AttackTile (position.x, position.y+1);
		manager.AttackTile (position.x+1, position.y);
		manager.AttackTile (position.x, position.y-1);
		manager.AttackTile (position.x-1, position.y);
	}

	public void LongShot()
	{
		//daggerAnim.SetTrigger("TR_Shoots");
		manager.selfAudio.PlayOneShot(manager.SFX[6]);

		shootStacks--;
		if(shootStacks == 0)
		{
			range = spinStacks == 0 ? AttackRange.SingleAttack : AttackRange.SpinAttack;
		}

		int i = 0;
		switch(orientation)
		{
			case 0 :
				manager.AttackTile (position.x, position.y+1);
				while(i<board.height)
				{
					i++;
					if(!board.isWalkable((int)position.x, (int)position.y+i)) break;
						manager.AttackTile (position.x, position.y+i);
				}
				break;
			case 1 :
				manager.AttackTile (position.x+1, position.y);
				while(i<board.width)
				{
					i++;
					if(!board.isWalkable((int)position.x+i, (int)position.y)) break;
						manager.AttackTile (position.x+i, position.y);
				}
				break;
			case 2:
				manager.AttackTile (position.x, position.y-1);
				while(i<board.height)
				{
					i++;
					if(!board.isWalkable((int)position.x, (int)position.y-i)) break;
						manager.AttackTile (position.x, position.y-i);
				}
				break;
			case 3 :
				manager.AttackTile (position.x-1, position.y);
				while(i<board.width)
				{
					i++;
					if(!board.isWalkable((int)position.x-i, (int)position.y)) break;
						manager.AttackTile (position.x-i, position.y);
				}
				break;
		}
	}

	public void RefreshPosition()
	{
		targetPos = board.self.position + board.offsetX * position.x + board.offsetY * position.y;
		stillMoving = true;
		selfRenderer.sortingOrder = (int) (position.x - position.y)*4+2;
		daggerRenderer.sortingOrder = (int) (position.x - position.y)*4+2;
		if(orientation == 0 || orientation == 3) daggerRenderer.sortingOrder--;
		else daggerRenderer.sortingOrder++;
		selfAnim.SetInteger("I_Orientation", orientation);
		daggerAnim.SetInteger("I_Orientation", orientation);
	}

	// animation event
	public void MagmaDeath()
	{
		GameObject vfx = Instantiate (v_exploLava, self.position+Vector3.up*0.5f, Quaternion.identity) as GameObject;
		foreach(ParticleSystem ps in vfx.GetComponentsInChildren<ParticleSystem>())
		{
			ps.Play();
		}
	}

	// animation event
	public void DeathSound()
	{
		manager.selfAudio.PlayOneShot(manager.SFX[4]);
	}

	// animation event
	public void SpinSound()
	{
		manager.selfAudio.PlayOneShot(manager.SFX[5]);
	}
}