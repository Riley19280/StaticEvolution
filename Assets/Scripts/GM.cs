using UnityEngine;
using System.Collections.Generic;

public static class GM
{
	public enum Teams
	{
		Red,
		Green,
		Blue,
		Yellow,
		None

	}

	public enum Direction
	{
		N,
		NE,
		SE,
		S,
		SW,
		NW
	}

	static int boardSize = 50;
	public static Vector2 mapSize = new Vector2(boardSize, (int)(boardSize));
	public static Tile[,] tilemap = new Tile[(int)mapSize.x, (int)mapSize.y];

	public static List<Tile> bases = new List<Tile>();

	public static void NEWGAME()
	{

	}

	public static Tile GetTile(int x, int y)
	{
		try
		{
			return tilemap[x, y];
		}
		catch (System.Exception)
		{
			Debug.Log("Tried to get null tile");
			return null;
		}

	}

	/// <summary>
	/// Get tile in direction
	/// </summary>
	/// <returns>May return null</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="dist">Distance</param>
	/// <param name="d">Direction</param>
	public static Tile MoveDirection(int x, int y, Direction d)
	{
		switch (d)
		{
			case Direction.N:
				return GetTile(x, y + 1);

			case Direction.S:
				return GetTile(x, y - 1);

			case Direction.SE:
				if (x % 2 != 0)
					return GetTile(x + 1, y);
				else
					return GetTile(x + 1, y - 1);

			case Direction.SW:
				if (x % 2 != 0)
					return GetTile(x - 1, y);
				else
					return GM.GetTile(x - 1, y - 1);

			case Direction.NW:
				if (x % 2 != 0)
					return GetTile(x - 1, y + 1);
				else
					return GetTile(x - 1, y);

			case Direction.NE:
				if (x % 2 != 0)
					return GetTile(x + 1, y + 1);
				else
					return GetTile(x + 1, y);


		}
		return null;
	}

	public static List<Tile> GetTilesInLine(int x, int y, int dist, Direction d)
	{
		Tile lasttile = GetTile(x, y);
		List<Tile> tiles = new List<Tile>();
		for (int i = 0; i < dist; i++)
		{
			if(lasttile !=null)
			lasttile = MoveDirection((int)lasttile.position.x, (int)lasttile.position.y, d);
			tiles.Add(lasttile);
		}
		tiles.RemoveAll(item => item == null);
		return tiles;
	}
	
	public static Tile GetTileInLine(int x, int y, int dist, Direction d)
	{
		Tile lasttile = GetTile(x, y);
		for (int i = 0; i < dist; i++)
		{
			lasttile = MoveDirection((int)lasttile.position.x, (int)lasttile.position.y, d);

		}
		return lasttile;
	}
}