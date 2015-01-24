using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

public enum TileName{
	Default,
	UpperLeftCorner,
	UpperBorder,
	UpperRightCorner,
	RightBorder,
	LowerRightCorner,
	LowerBorder,
	LowerLeftCorner,
	LeftBorder,
	Lava
}

public class BoardData : MonoBehaviour {

	public Transform self;

	private int width, height;
	public Vector2 tileSize;

	public GameObject v_tile;
	public GameObject[,] tiles;
	public Sprite v_default;
	public Sprite v_upperLeftCorner, v_upperRightCorner, v_lowerLeftCorner, v_lowerRightCorner;
	public Sprite v_upperBorder, v_rightBorder,  v_lowerBorder, v_leftBorder;
	public Sprite v_lava;

	public const string Default = "0";
	public const string UpperLeftCorner = "1";
	public const string UpperBorder = "2";
	public const string UpperRightCorner = "3";
	public const string RightBorder = "4";
	public const string LowerRightCorner = "5";
	public const string LowerBorder = "6";
	public const string LowerLeftCorner = "7";
	public const string LeftBorder = "8";
	public const string Lava = "9";

	[HideInInspector]
	public Vector3 spawn, offsetX, offsetY;

	[HideInInspector]
	public SpriteRenderer[,] tilesSprites;


	string[][] readFile(string file){
		string text = System.IO.File.ReadAllText(file);
		string[] lines = Regex.Split(text, "\r\n");
		int rows = lines.Length;
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++) {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}
		return levelBase;
	}


	void Awake ()
	{

		string[][] jagged = readFile(Application.dataPath+"/level.txt");

		width = jagged[0].Length;
		height = jagged.Length;


		offsetX = new Vector3(tileSize.x, -tileSize.y, 0);
		offsetY = new Vector3(tileSize.x, tileSize.y, 0);

		tiles = new GameObject[width,height];
		tilesSprites = new SpriteRenderer[width,height];
		for(int i=0; i<width; i++)
		{
			for(int j=0; j<height; j++)
			{
				spawn = self.position + offsetX * i + offsetY * j;
				tiles[i,j] = (GameObject) Instantiate (v_tile, spawn, Quaternion.identity);
				tilesSprites[i,j] = tiles[i,j].GetComponent<SpriteRenderer>();
				tiles[i,j].transform.parent = self;
			}
		}

		// create planes based on matrix
		for (int y = 0; y < jagged.Length; y++) {
			for (int x = 0; x < jagged[0].Length; x++) {
				switch (jagged[y][x]){
				case Default:
					break;
				case UpperLeftCorner:
					tilesSprites[x,y].sprite = v_upperLeftCorner;
					break;
				case UpperBorder:
					tilesSprites[x,y].sprite = v_upperBorder;
					break;
				case UpperRightCorner:

					break;
				case RightBorder:

					break;
				case LowerRightCorner:

					break;
				case LowerBorder:

					break;
				case LowerLeftCorner:

					break;
				case LeftBorder:
			
					break;
				case Lava:

					break;
				}
			}
		} 


//		for(int w=0; w<width; w++)
//		{
//			tilesSprites[w,0].sprite = v_lowerBorder;
//			tilesSprites[w,height-1].sprite = v_upperBorder;
//		}
//		for(int h=0; h<height; h++)
//		{
//			tilesSprites[0,h].sprite = v_leftBorder;
//			tilesSprites[width-1,h].sprite = v_rightBorder;
//		}
//
//		tilesSprites[0,0].sprite = v_lowerLeftCorner;
//		tilesSprites[0,height-1].sprite = v_upperLeftCorner;
//		tilesSprites[width-1,0].sprite = v_lowerRightCorner;
//		tilesSprites[width-1,height-1].sprite = v_upperRightCorner;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
