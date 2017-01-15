using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public static class TerrainGeneration
{
	public static void GenerateTilemap()
	{
		float width = 96;
		float height = 84;

		float stdhozOffset = width / 4;//width / 2;
		float stdvertOffset = height / 2;

		float vertmove = 0;
		float hozmove = 0;

		for (int i = 0; i < GM.mapSize.x; i++)
		{

			if (i % 2 == 0)
				vertmove = stdvertOffset;
			else
				vertmove = 0;


			for (int j = 0; j < (int)GM.mapSize.y; j++)
			{

				hozmove = i * stdhozOffset;
				GameObject g = (GameObject)GameObject.Instantiate(Art.Tile, new Vector3((i * width) - (hozmove), (j * height) - (vertmove), 0), Quaternion.identity);

				GM.tilemap[i, j] = g.GetComponent<Tile>();

				g.name = i + " " + j;
				g.GetComponent<Tile>().position = new Vector2(i, j);
				g.GetComponent<Tile>().txt.text = "tile_" + i + "_" + j;
				g.GetComponent<Tile>().UpdateTile();
			}

		}


		GenerateBases();
		wall();
		GenerateObelesks(25, 25, 11);
		GenerateRiver(25, 25, 75);
		GenerateMines(10);
	}

	public static void wall()
	{
		GM.GetTile(11, 8).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(11, 9).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(11, 10).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(11, 11).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(11, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(11, 13).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(12, 14).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(13, 14).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(14, 14).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(14, 13).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(13, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(12, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(17, 14).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(16, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(17, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(15, 11).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(19, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(20, 12).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(21, 11).UpdateTile(Tile.TYPE.Wall);


		GM.GetTile(7, 14).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(8, 15).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(9, 15).UpdateTile(Tile.TYPE.Wall);

		GM.GetTile(12, 17).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(13, 16).UpdateTile(Tile.TYPE.Wall);
		GM.GetTile(14, 16).UpdateTile(Tile.TYPE.Wall);
	}

	static void GenerateBases()
	{
		GM.GetTile(0, (int)GM.mapSize.y - 1).setTeam(GM.Teams.Red);//top left
		GM.GetTile(0, (int)GM.mapSize.y - 1).UpdateTile(Tile.TYPE.Base);
		GM.bases.Add(GM.GetTile(0, (int)GM.mapSize.y - 1));

		GM.GetTile((int)GM.mapSize.x - 1, (int)GM.mapSize.y - 1).setTeam(GM.Teams.Green);//top right
		GM.GetTile((int)GM.mapSize.x - 1, (int)GM.mapSize.y - 1).UpdateTile(Tile.TYPE.Base);
		GM.bases.Add(GM.GetTile((int)GM.mapSize.x - 1, (int)GM.mapSize.y - 1));

		GM.GetTile(0, 0).setTeam(GM.Teams.Blue);//bottom left
		GM.GetTile(0, 0).UpdateTile(Tile.TYPE.Base);
		GM.bases.Add(GM.GetTile(0, 0));

		GM.GetTile((int)GM.mapSize.x - 1, 0).setTeam(GM.Teams.Yellow);//bottom right
		GM.GetTile((int)GM.mapSize.x - 1, 0).UpdateTile(Tile.TYPE.Base);
		GM.bases.Add(GM.GetTile((int)GM.mapSize.x - 1, 0));

	}

	static void GenerateRiver(int xstart, int ystart, int length)
	{
		GM.GetTile(xstart, ystart).UpdateTile(Tile.TYPE.Water);

		Vector2 prev = new Vector2(xstart, ystart);

		List<Vector2> placed = new List<Vector2>();

		for (int i = 0; i < length - 2; i++)
		{
			List<Tile> possible = GM.GetTile((int)prev.x, (int)prev.y).getAdjacent();
			List<Tile> removes = new List<Tile>();

			possible.RemoveAll(item => item == null);

			foreach (Tile tile in possible)
			{
				int ct = 0;
				foreach (Tile tile1 in tile.getAdjacent())
				{
					if (tile1 != null)
						if (tile1.type == Tile.TYPE.Water)
							ct++;
				}
				if (ct > 1)
				{
					removes.Add(tile);
				}

				if (tile.type != Tile.TYPE.Default)
					removes.Add(tile);

			}
			foreach (Tile tile in removes)
			{
				possible.Remove(tile);
			}
			removes.Clear();

			if (possible.Count > 0)
			{
				Vector2 newpos = possible[Random.Range(0, possible.Count)].position;

				GM.GetTile((int)newpos.x, (int)newpos.y).UpdateTile(Tile.TYPE.Water);
				placed.Add(new Vector2(newpos.x, newpos.y));
				prev = newpos;
			}
			else
			{
				prev = placed[Random.Range(0, placed.Count - 1)];


			}
		}



	}

	static void GenerateObelesks(int xstart, int ystart, int dist)
	{
		GM.GetTileInLine(xstart, ystart, dist, GM.Direction.S).UpdateTile(Tile.TYPE.Obelesk);
		GM.GetTileInLine(xstart, ystart, dist, GM.Direction.NW).UpdateTile(Tile.TYPE.Obelesk);
		GM.GetTileInLine(xstart, ystart, dist, GM.Direction.NE).UpdateTile(Tile.TYPE.Obelesk);
	}

	static void GenerateMines(int number)
	{
		Random r = new Random();

		//generating near bases
		int maxDistFromBase = 7;
		int minDistFromBase = 4;//FIXME: doesnt work properly
		foreach (Tile t in GM.bases)
		{
			List<Tile> availTiles = new List<Tile>();


			//
			foreach (GM.Direction d in GM.Direction.GetValues(typeof(GM.Direction)))
			{
				List<Tile> tiles = new List<Tile>();
				tiles = GM.GetTilesInLine((int)t.position.x, (int)t.position.y, maxDistFromBase, d);
				try { tiles.RemoveRange(0, minDistFromBase); } catch { }
				availTiles.AddRange(tiles);
			}
			//

			availTiles[Random.Range(0, availTiles.Count - 1)].UpdateTile(Tile.TYPE.Mine);
		}

		for (int i = 0; i < number; i++)
		{
			int x = (int)Random.Range(0, GM.mapSize.x);
			int y = (int)Random.Range(0, GM.mapSize.y);

			if (GM.GetTile(x, y).type == Tile.TYPE.Default)
			{
				GM.GetTile(x, y).UpdateTile(Tile.TYPE.Mine);
			}
			else
				i--;
		}

	}

}
