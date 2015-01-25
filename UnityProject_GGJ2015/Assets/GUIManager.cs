using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

	public GameObject arrowLeft;
	public GameObject arrowRight;
	public GameObject fight;
	public GameObject tuto;
	public GameObject quit;
	public GameObject bottom;
	public GameObject back;
	private GameObject[] arrayMenu;
	private int index;
	private GameObject selected;
	public GameObject canvasMenu;
	public GameObject canvasTuto;
	public GameObject canvasFight;
	private bool isMenuActive;
	private bool isTutoActive;
	private bool isFightActive;

	public GameObject oneTurn;
	public GameObject twoTurns;
	public GameObject threeTurns;
	public GameObject level6;
	public GameObject level9;
	public GameObject level12;
	public GameObject startGame;
	public GameObject backFight;
	public GameObject randomLevel;

	private GameObject[] menuListX;
	private GameObject[][] menuList;

	private int indexX;
	private int indexY;
	private GameObject selectedFight;
	private GameObject selectedTurn;
	private GameObject selectedLevel;
	private GameObject selectedMenu;

	public AudioClip selectClip;
	public AudioClip moveClip;
	public AudioClip backClip;

	// Use this for initialization
	void Start () {

		menuList = new GameObject[3][];

		for (int j=0; j < menuList.Length; j++) {
			menuList[j] = new GameObject[3];
		}

		menuList [0] [0] = oneTurn;			menuList [1] [0] = twoTurns;		menuList [2] [0] = threeTurns;
		menuList [0] [1] = level6;			menuList [1] [1] = level9;			menuList [2] [1] = level12;
		menuList [0] [2] = backFight;		menuList [1] [2] = randomLevel;		menuList [2] [2] = startGame;

		isMenuActive = true;
		isTutoActive = false;
		arrayMenu = new GameObject[4];
		arrayMenu [0] = fight;
		arrayMenu [1] = tuto;
		arrayMenu [2] = quit;
		arrayMenu [3] = back;
		index = 0;
		indexX = 0;
		indexY = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow) && isMenuActive == true) {
			index ++;
			audio.PlayOneShot(moveClip);
		}
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) && isMenuActive == true) {
			index --;
			audio.PlayOneShot(moveClip);
		}
		if (Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.LeftControl)) {
			if(selected == quit){
				Application.Quit();
			}
			if(selected == tuto){
				audio.PlayOneShot(selectClip);
				canvasMenu.SetActive(false);
				canvasTuto.SetActive(true);
				isMenuActive = false;
				isTutoActive = true;
				arrowLeft.transform.parent = canvasTuto.transform;
				arrowRight.transform.parent = canvasTuto.transform;
				index = 3;
			}
			if(selected == fight){
				audio.PlayOneShot(selectClip);
				canvasMenu.SetActive(false);
				canvasFight.SetActive(true);
				isFightActive = true;
				isMenuActive = false;
			}
			if(selected == back){
				audio.PlayOneShot(backClip);
				if(isTutoActive){
					canvasMenu.SetActive(true);
					canvasTuto.SetActive(false);
					isMenuActive = true;
					isTutoActive = false;
					index = 0;
					arrowLeft.transform.parent = canvasMenu.transform;
					arrowRight.transform.parent = canvasMenu.transform;
				}
			}
		}
		if (index > 2 && isMenuActive == true) {
			index = 0;
		}
		if (index < 0 && isMenuActive == true) {
			index = 2;
		}
		if (index == 0 && isMenuActive == true) {
			arrowLeft.transform.localPosition = new Vector3(-150,34,0);
			arrowRight.transform.localPosition = new Vector3(150,34,0);
		}
		if (index == 1 && isMenuActive == true) {
			arrowLeft.transform.localPosition = new Vector3(-230,-52,0);
			arrowRight.transform.localPosition = new Vector3(230,-52,0);
		}
		if (index == 2 && isMenuActive == true) {
			arrowLeft.transform.localPosition = new Vector3(-135,-134,0);
			arrowRight.transform.localPosition = new Vector3(135,-134,0);
		}
		if (index == 3 && isTutoActive == true) {
			arrowLeft.transform.localPosition = new Vector3(-180,-280,0);
			arrowRight.transform.localPosition = new Vector3(180,-280,0);
		}

		selected = arrayMenu [index];

		if (isFightActive) {
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
				indexY ++;
				audio.PlayOneShot(moveClip);
			}
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
				indexY --;
				audio.PlayOneShot(moveClip);
			}
			if (Input.GetKeyDown (KeyCode.Q) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
				indexX --;
				audio.PlayOneShot(moveClip);
			}
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.RightArrow)) {
				indexX ++;
				audio.PlayOneShot(moveClip);
			}
		}
		if (indexX > 2 && isFightActive == true) {
			indexX = 0;
		}
		if (indexX < 0 && isFightActive == true) {
			indexX = 2;
		}
		if (indexY > 2 && isFightActive == true) {
			indexY = 0;
		}
		if (indexY < 0 && isFightActive == true) {
			indexY = 2;
		}
		if (indexY == 0) {
			selectedFight = selectedTurn;
			Debug.Log("turn");
		}
		if (indexY == 1) {
			selectedFight = selectedLevel;
			Debug.Log("level");
		}
		if (indexY == 2) {
			selectedFight = selectedMenu;
			Debug.Log("m");
		}

		selectedFight = menuList [indexX] [indexY];
		Debug.Log (selectedTurn);
		Debug.Log (selectedLevel);
		Debug.Log (selectedMenu);
		//selectedFight.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 1);

	}
}
