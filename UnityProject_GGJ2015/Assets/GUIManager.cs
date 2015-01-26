using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
		isFightActive = false;
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

		selectedFight = menuList [indexX] [indexY];

		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow) && isMenuActive == true) {
			index ++;
			audio.PlayOneShot(moveClip);
		}
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) && isMenuActive == true) {
			index --;
			audio.PlayOneShot(moveClip);
		}
		if (Input.GetKeyDown (KeyCode.E) || Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.LeftControl)) {
			if(selected == quit && isMenuActive == true){
				Application.Quit();
			}
			if(selected == tuto  && isMenuActive == true){
				audio.PlayOneShot(selectClip);
				canvasMenu.SetActive(false);
				canvasTuto.SetActive(true);
				isMenuActive = false;
				isTutoActive = true;
				arrowLeft.transform.parent = canvasTuto.transform;
				arrowRight.transform.parent = canvasTuto.transform;
				index = 3;
			}
			if(selected == fight && isMenuActive == true){
				indexX = 0;
				audio.PlayOneShot(selectClip);
				canvasMenu.SetActive(false);
				canvasFight.SetActive(true);
				isFightActive = true;
				isMenuActive = false;
				arrowLeft.transform.parent = canvasFight.transform;
				arrowRight.transform.parent = canvasFight.transform;
			}
			if(selected == back && isTutoActive == true){
				audio.PlayOneShot(backClip);
				if(isTutoActive){
					canvasMenu.SetActive(true);
					canvasTuto.SetActive(false);
					isMenuActive = true;
					isTutoActive = false;
					index = 0;
					indexX = 0;
					indexY = 0;
					arrowLeft.transform.parent = canvasMenu.transform;
					arrowRight.transform.parent = canvasMenu.transform;
				}
			}
			if(selectedFight == backFight && isFightActive == true){
				audio.PlayOneShot(backClip);
				if(isFightActive){
					canvasMenu.SetActive(true);
					canvasFight.SetActive(false);
					isMenuActive = true;
					isFightActive = false;
					index = 0;
					indexX = 0;
					indexY = 0;
					arrowLeft.transform.parent = canvasMenu.transform;
					arrowRight.transform.parent = canvasMenu.transform;
				}
			}
			if(selectedFight == randomLevel & isFightActive == true){
				audio.PlayOneShot(selectClip);
				// DO RANDOM SHIT

				string[] sceneNames = new string[3];
				sceneNames[0] = "Scene_6x6";
				sceneNames[1] = "Scene_9x9";
				sceneNames[2] = "Scene_12x12";


				Controller.staticMem = Random.Range(1, 4);
				Debug.Log (Controller.staticMem);
				Application.LoadLevelAsync(sceneNames[Random.Range(0,3)]);




			}

			if(selectedFight == startGame & isFightActive == true){
				audio.PlayOneShot(selectClip);
				// START GAME
				if(selectedTurn == oneTurn){
					Controller.staticMem = 1;
				}
				if(selectedTurn == twoTurns){
					Controller.staticMem = 2;
				}
				if(selectedTurn == threeTurns){
					Controller.staticMem = 3;
				}
				//Debug.Log(Controller.staticMem);
				if(selectedLevel == level6){
					Application.LoadLevelAsync("Scene_6x6");
				}
				if(selectedLevel == level9){
					Application.LoadLevelAsync("Scene_9x9");
				}
				if(selectedLevel == level12){
					Application.LoadLevelAsync("Scene_12x12");
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
		if (indexY == 0 && isFightActive == true) {
			selectedTurn = selectedFight;
			arrowLeft.transform.parent = selectedTurn.transform;
			arrowRight.transform.parent = selectedTurn.transform;
			arrowLeft.transform.localPosition = new Vector3 (-120, 5, 0);
			arrowRight.transform.localPosition = new Vector3 (120, 5, 0);
		} 
		if (indexY == 1 && isFightActive == true) {
			selectedLevel = selectedFight;
			arrowLeft.transform.parent = selectedLevel.transform;
			arrowRight.transform.parent = selectedLevel.transform;
			arrowLeft.transform.localPosition = new Vector3 (-380, -110, 0);
			arrowRight.transform.localPosition = new Vector3 (380, -110, 0);
		}
		if (indexY == 2 && isFightActive == true) {
			selectedMenu = selectedFight;
			arrowLeft.transform.parent = selectedMenu.transform;
			arrowRight.transform.parent = selectedMenu.transform;
			arrowLeft.transform.localPosition = new Vector3(-120/selectedMenu.transform.localScale.x,0,0);
			arrowRight.transform.localPosition =  new Vector3(120/selectedMenu.transform.localScale.x,0,0);

		}

		if (isFightActive && indexY == 0) {

			menuList[0][0].GetComponent<Text> ().color = new Color (0, 0, 0, 0.2f);
            menuList[1][0].GetComponent<Text> ().color = new Color (0, 0, 0, 0.2f);
            menuList[2][0].GetComponent<Text> ().color = new Color (0, 0, 0, 0.2f);
			
			selectedTurn.GetComponent<Text> ().color = new Color (0, 0, 0, 1);
		}

		if (isFightActive && indexY == 1) {

			menuList[0][1].GetComponent<Image>().color = new Color (menuList[0][1].GetComponent<Image>().color.r,
			                                                        menuList[0][1].GetComponent<Image>().color.g,
			                                                        menuList[0][1].GetComponent<Image>().color.b, 
			                                                        0.2f);
			menuList[0][1].GetComponentInChildren<Text>().color = new Color (0, 0, 0, 0.2f);

			//=======================================================================

			menuList[1][1].GetComponent<Image>().color = new Color (menuList[1][1].GetComponent<Image>().color.r,
			                                                        menuList[1][1].GetComponent<Image>().color.g,
			                                                        menuList[1][1].GetComponent<Image>().color.b, 
			                                                        0.2f);

			menuList[1][1].GetComponentInChildren<Text>().color = new Color (0, 0, 0, 0.2f);


			//=======================================================================

			menuList[2][1].GetComponent<Image>().color = new Color (menuList[2][1].GetComponent<Image>().color.r,
			                                                        menuList[2][1].GetComponent<Image>().color.g,
			                                                        menuList[2][1].GetComponent<Image>().color.b, 
			                                                        0.2f);

			menuList[2][1].GetComponentInChildren<Text>().color = new Color (0, 0, 0, 0.4f);
		
			//=======================================================================

			if(selectedLevel != null){
				selectedLevel.GetComponent<Image>().color = new Color (selectedLevel.GetComponent<Image>().color.r,
				                                                       selectedLevel.GetComponent<Image>().color.g,
				                                                       selectedLevel.GetComponent<Image>().color.b, 
				                                                       1f);

				selectedLevel.GetComponentInChildren<Text>().color = new Color (0, 0, 0, 1f);
			}

		

//			foreach(SpriteRenderer i in arraySprite3){
//				i.color = new Color (0, 0, 0, 1f);
//			}
		}
	}
}
