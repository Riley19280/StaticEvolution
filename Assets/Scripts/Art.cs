using UnityEngine;
using UnityEngine.UI;
using System;

public static class Art
{
	//all the assets used for the game
	public static GameObject Tile;



	public static AudioClip Cannon;
	public static Sprite[] tiles = new Sprite[9];
	public static Sprite[] bases = new Sprite[4];
	public static Sprite[] obelesks = new Sprite[24];
	public static Sprite[] walls = new Sprite[2];

	public static Sprite tilehr;
	public static Sprite water;
	public static Sprite mine;
	public static Sprite bridge;
	//this loads them into memory
	public static void LoadContent()
	{
		//Screens

		Tile = (GameObject)Resources.Load("Prefabs/tile");


		tiles[0] = Resources.Load<Sprite>("Art/tile");
		tiles[1] = Resources.Load<Sprite>("Art/tile2");
		tiles[2] = Resources.Load<Sprite>("Art/tile3");
		tiles[3] = Resources.Load<Sprite>("Art/tile4");
		tiles[4] = Resources.Load<Sprite>("Art/tile5");
		tiles[5] = Resources.Load<Sprite>("Art/tile6");
		tiles[6] = Resources.Load<Sprite>("Art/tile7");
		tiles[7] = Resources.Load<Sprite>("Art/water");
		tiles[8] = Resources.Load<Sprite>("Art/tile9");


		bases[0] = Resources.Load<Sprite>("Art/base_red");
		bases[1] = Resources.Load<Sprite>("Art/base_green");
		bases[2] = Resources.Load<Sprite>("Art/base_blue");
		bases[3] = Resources.Load<Sprite>("Art/base_yellow");

		obelesks[0] = Resources.Load<Sprite>("Art/obelesk/obelesk_1");
		obelesks[1] = Resources.Load<Sprite>("Art/obelesk/obelesk_2");
		obelesks[2] = Resources.Load<Sprite>("Art/obelesk/obelesk_3");
		obelesks[3] = Resources.Load<Sprite>("Art/obelesk/obelesk_4");
		obelesks[4] = Resources.Load<Sprite>("Art/obelesk/obelesk_5");
		obelesks[5] = Resources.Load<Sprite>("Art/obelesk/obelesk_6");
		obelesks[6] = Resources.Load<Sprite>("Art/obelesk/obelesk_7");
		obelesks[7] = Resources.Load<Sprite>("Art/obelesk/obelesk_8");
		obelesks[8] = Resources.Load<Sprite>("Art/obelesk/obelesk_9");
		obelesks[9] = Resources.Load<Sprite>("Art/obelesk/obelesk_10");
		obelesks[10] = Resources.Load<Sprite>("Art/obelesk/obelesk_11");
		obelesks[11] = Resources.Load<Sprite>("Art/obelesk/obelesk_12");
		obelesks[12] = Resources.Load<Sprite>("Art/obelesk/obelesk_13");
		obelesks[13] = Resources.Load<Sprite>("Art/obelesk/obelesk_14");
		obelesks[14] = Resources.Load<Sprite>("Art/obelesk/obelesk_15");
		obelesks[15] = Resources.Load<Sprite>("Art/obelesk/obelesk_16");
		obelesks[16] = Resources.Load<Sprite>("Art/obelesk/obelesk_17");
		obelesks[17] = Resources.Load<Sprite>("Art/obelesk/obelesk_18");
		obelesks[18] = Resources.Load<Sprite>("Art/obelesk/obelesk_19");
		obelesks[19] = Resources.Load<Sprite>("Art/obelesk/obelesk_20");
		obelesks[20] = Resources.Load<Sprite>("Art/obelesk/obelesk_21");
		obelesks[21] = Resources.Load<Sprite>("Art/obelesk/obelesk_22");
		obelesks[22] = Resources.Load<Sprite>("Art/obelesk/obelesk_23");
		obelesks[23] = Resources.Load<Sprite>("Art/obelesk/obelesk_24");

		walls[0] = Resources.Load<Sprite>("Art/wall_0");
		walls[1] = Resources.Load<Sprite>("Art/wall_1");


		tilehr = Resources.Load<Sprite>("Art/tilehr2");

		water = Resources.Load<Sprite>("Art/water");
		mine = Resources.Load<Sprite>("Art/mine");
		bridge = Resources.Load<Sprite>("Art/bridge");
	}



}