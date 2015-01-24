using UnityEngine;
using System.Collections;

public class BoardData : MonoBehaviour {

	public Transform self;

	public int width, height;
	public Vector2 tileSize;

	public GameObject v_tile;
	public GameObject[,] tiles;
	public Sprite v_upperLeftCorner, v_upperRightCorner, v_lowerLeftCorner, v_lowerRightCorner;
	public Sprite v_upperBorder, v_rightBorder,  v_lowerBorder, v_leftBorder;
	public Sprite v_lava;

	private Vector3 spawn, offsetX, offsetY;

	[HideInInspector]
	public SpriteRenderer[,] tilesSprites;

	void Awake ()
	{
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

		for(int w=0; w<width; w++)
		{
			tilesSprites[w,0].sprite = v_lowerBorder;
			tilesSprites[w,height-1].sprite = v_upperBorder;
		}
		for(int h=0; h<height; h++)
		{
			tilesSprites[0,h].sprite = v_leftBorder;
			tilesSprites[width-1,h].sprite = v_rightBorder;
		}

		tilesSprites[0,0].sprite = v_lowerLeftCorner;
		tilesSprites[0,height-1].sprite = v_upperLeftCorner;
		tilesSprites[width-1,0].sprite = v_lowerRightCorner;
		tilesSprites[width-1,height-1].sprite = v_upperRightCorner;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
