using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

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

	public static int levelNumber;

	[HideInInspector]
	public int width, height;
	public Vector2 tileSize;

	public GameObject v_tile;
	public GameObject v_prop;
	public GameObject[,] tiles;
	public GameObject[,] props;
	public Sprite v_default;
	public Sprite v_upperLeftCorner, v_upperRightCorner, v_lowerLeftCorner, v_lowerRightCorner;
	public Sprite v_upperBorder, v_rightBorder,  v_lowerBorder, v_leftBorder;
	public Sprite v_lava, v_column, v_columnSmall, v_rock, v_fire, v_statue;
	public GameObject particlesLava, particlesFire, v_pine;

	public const string Default = "0";
	public const string UpperLeftCorner = "1";
	public const string UpperBorder = "2";
	public const string UpperRightCorner = "3";
	public const string RightBorder = "4";
	public const string LowerRightCorner = "5";
	public const string LowerBorder = "6";
	public const string LowerLeftCorner = "7";
	public const string LeftBorder = "8";

	public const string None = "0";
	public const string Column = "1";
	public const string ColumnSmall = "2";
	public const string Lava = "3";
	public const string Rock = "4";
	public const string Fire = "5";
	public const string Statue = "6";
	public const string Pine = "7";


	[HideInInspector]
	public List<Sprite> spriteCollision;

	[HideInInspector]
	public Vector3 spawn, offsetX, offsetY;

	[HideInInspector]
	public SpriteRenderer[,] tilesSprites;

	[HideInInspector]
	public SpriteRenderer[,] propsSprites;


	string[][] readFile(string file){
		string text = file;
		string[] lines = Regex.Split(text, "\r\n");
		int rows = lines.Length;
		string[][] levelBase = new string[rows][];
		for (int i = 0; i < lines.Length; i++) {
			string[] stringsOfLine = Regex.Split(lines[i], " ");
			levelBase[i] = stringsOfLine;
		}
		return levelBase;
	}

	public bool isWalkable(int x, int y){
		if(spriteCollision.Contains(propsSprites[x,y].sprite) || propsSprites[x,y].gameObject == v_pine){
			return false;
		}
		return true;
	}

	public bool isDeadly(int x, int y){
		if(propsSprites[x,y].sprite == v_lava){
			return true;
		}
		return false;
	}


	void Awake ()
	{

//		object file = Resources.Load ("level" + levelNumber.ToString (), typeof(object));
//		object fileProps = Resources.Load ("level" + levelNumber.ToString ()+"Props", typeof(object));
		TextAsset textAsset = Resources.Load ("level" + levelNumber.ToString (), typeof (TextAsset)) as TextAsset;
		TextAsset textAssetProps = Resources.Load ("level" + levelNumber.ToString ()+"Props", typeof (TextAsset)) as TextAsset;
		string[][] jagged = readFile(textAsset.text);
		string[][] jaggedProps = readFile(textAssetProps.text);

		spriteCollision = new List<Sprite> ();
		spriteCollision.Add (v_lava);
		spriteCollision.Add (v_column);
		spriteCollision.Add (v_columnSmall);
		spriteCollision.Add (v_rock);
		spriteCollision.Add (v_fire);
		spriteCollision.Add (v_statue);

		width = jagged[0].Length;
		height = jagged.Length;

		offsetX = new Vector3(tileSize.x, -tileSize.y, 0);
		offsetY = new Vector3(tileSize.x, tileSize.y, 0);

		tiles = new GameObject[width,height];
		tilesSprites = new SpriteRenderer[width,height];
		props = new GameObject[width,height];
		propsSprites = new SpriteRenderer[width,height];


		for(int j=0; j<height; j++)
		{
			for(int i=0; i<width; i++)
			{
				spawn = self.position + offsetX * i + offsetY * j;
				tiles[i,j] = (GameObject) Instantiate (v_tile, spawn, Quaternion.identity);
				tilesSprites[i,j] = tiles[i,j].GetComponent<SpriteRenderer>();
				tiles[i,j].transform.parent = self;
			}
		}


		for(int j=0; j<height; j++)
		{
			for(int i=0; i<width; i++)
			{
				spawn = self.position + offsetX * i + offsetY * j;
				props[i,j] = (GameObject) Instantiate(v_prop, spawn, Quaternion.identity);
				propsSprites[i,j] = props[i,j].GetComponent<SpriteRenderer>();
				props[i,j].transform.parent = self;
			}
		}
	
		
		//génération sol
		for (int y = 0; y < jagged.Length; y++) {
			for (int x = 0; x < jagged[0].Length; x++) {
				switch (jagged[x][y]){
				case Default:
					tilesSprites[x,y].sprite = v_default;
					break;
				case UpperLeftCorner:
					tilesSprites[x,y].sprite = v_upperLeftCorner;
					break;
				case UpperBorder:
					tilesSprites[x,y].sprite = v_upperBorder;
					break;
				case UpperRightCorner:
					tilesSprites[x,y].sprite = v_upperRightCorner;
					break;
				case RightBorder:
					tilesSprites[x,y].sprite = v_rightBorder;
					break;
				case LowerRightCorner:
					tilesSprites[x,y].sprite = v_lowerRightCorner;
					break;
				case LowerBorder:
					tilesSprites[x,y].sprite = v_lowerBorder;
					break;
				case LowerLeftCorner:
					tilesSprites[x,y].sprite = v_lowerLeftCorner;
					break;
				case LeftBorder:
					tilesSprites[x,y].sprite = v_leftBorder;
					break;
				}
			}
		} 



		//génération props
		for (int y = 0; y < jaggedProps.Length; y++) {
			for (int x = 0; x < jaggedProps[0].Length; x++) {
				switch (jaggedProps[x][y]){
				case None:
					break;
				case Column:
					propsSprites[x,y].sprite = v_column;
					//Debug.Log(isWalkable(x,y)); // <==
					break;
				case ColumnSmall:
					propsSprites[x,y].sprite = v_columnSmall;
					break;
				case Lava:
					propsSprites[x,y].sprite = v_lava;
					GameObject cloneLava = Instantiate(particlesLava, propsSprites[x,y].gameObject.transform.position + new Vector3(0,0.2f,0), Quaternion.identity) as GameObject;
					ParticleSystem[] particlesArrayLava = cloneLava.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem i in particlesArrayLava){
						i.gameObject.layer = LayerMask.NameToLayer("Particles");
					}
					break;
				case Rock:
					propsSprites[x,y].sprite = v_rock;
					break;
				case Fire:
					propsSprites[x,y].sprite = v_fire;
					GameObject cloneFire = Instantiate(particlesFire, propsSprites[x,y].gameObject.transform.position + new Vector3(0,0.6f,0), Quaternion.identity) as GameObject;
					ParticleSystem[] particlesArrayFire = cloneFire.GetComponentsInChildren<ParticleSystem>();
					foreach (ParticleSystem i in particlesArrayFire){
						i.gameObject.layer = LayerMask.NameToLayer("Particles");
					}
					break;
				case Statue:
					propsSprites[x,y].sprite = v_statue;
					break;
				case Pine:
					GameObject clonePine = Instantiate(v_pine, propsSprites[x,y].gameObject.transform.position + new Vector3(0,0.3f,0), Quaternion.identity) as GameObject;
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
