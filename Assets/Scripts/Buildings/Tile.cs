using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	public enum TYPE
	{
		Default,
		Base,
		Wall,
		Water,
		Bridge,
		Obelesk,
		Mine
	}

	public Text txt;
	public GameObject sprite;
	public GM.Teams team = GM.Teams.None;
	public TYPE type = TYPE.Default;
	public Vector2 position;

	void Start()
	{
		transform.SetParent(GameObject.Find("Board").transform);


	}

	void FixedUpdate()
	{

		//fps: 12
		if (type == TYPE.Obelesk)
		{
			int index = (int)(Time.time * 12) % 24;
			sprite.GetComponent<Image>().sprite = Art.obelesks[index];
		}
	}

	public void OnClick()
	{
		switch (type)
		{
			case TYPE.Default:
				UpdateTile(TYPE.Wall);
				break;
			case TYPE.Base:
				break;
			case TYPE.Wall:
				break;
			case TYPE.Water:
				placeBridge();
				break;
			case TYPE.Bridge:
				setRotation();
				break;
			case TYPE.Obelesk:
				break;
			case TYPE.Mine:
				break;
			default:
				break;
		}


	}

	public void Initialize(int x, int y)
	{
		position = new Vector2(x, y);
		transform.SetParent(GM.GetTile((int)position.x, (int)position.y).gameObject.transform);
		GetComponent<RectTransform>().localPosition = new Vector3();

		UpdateTile();

	}

	public void setTeam(GM.Teams team)
	{
		this.team = team;

	}

	public void UpdateTile(TYPE t)
	{
		type = t;
		refresh();
	}

	public void UpdateTile()
	{
		refresh();
	}

	public void refresh()
	{
		switch (type)
		{
			case TYPE.Default:
				sprite.transform.Rotate(0, 0, Random.Range(0, 5) * 60);

				break;
			case TYPE.Base:

				switch (team)
				{
					case GM.Teams.Red:
						sprite.GetComponent<Image>().sprite = Art.bases[0];
						break;
					case GM.Teams.Green:
						sprite.GetComponent<Image>().sprite = Art.bases[1];
						break;
					case GM.Teams.Blue:
						sprite.GetComponent<Image>().sprite = Art.bases[2];
						break;
					case GM.Teams.Yellow:
						sprite.GetComponent<Image>().sprite = Art.bases[3];
						break;
				}
				break;
			case TYPE.Wall:
				UpdateWallGraphics();
				foreach (Tile t in getAdjacent())
				{
					if (t.type == TYPE.Wall)
						t.UpdateWallGraphics();
				}
				break;
			case TYPE.Water:
				sprite.GetComponent<Image>().sprite = Art.water;
				break;
			case TYPE.Mine:
				sprite.GetComponent<Image>().sprite = Art.mine;
				break;
		}
	}

	void UpdateWallGraphics()
	{

		Dictionary<Tile, GM.Direction> adjWall = new Dictionary<Tile, GM.Direction>();
		foreach (KeyValuePair<Tile, GM.Direction> t in getAdjacentDirection())
		{
			if (t.Key.type == TYPE.Wall)
				adjWall.Add(t.Key, t.Value);
		}
		if (adjWall.Count == 2)
		{
			int pair1 = 0;
			int pair2 = 0;
			int pair3 = 0;
			foreach (KeyValuePair<Tile, GM.Direction> t in adjWall)
			{
				switch (t.Value)
				{
					case GM.Direction.N:
					case GM.Direction.S:
						pair1++;
						break;

					case GM.Direction.NW:
					case GM.Direction.SE:
						pair2++;
						break;

					case GM.Direction.NE:
					case GM.Direction.SW:
						pair3++;
						break;

				}

			}

			if (pair1 > 1 || pair2 > 1 || pair3 > 1)
			{
				sprite.GetComponent<Image>().sprite = Art.walls[0];
				if (pair1 > 1)
					setRotation(GM.Direction.N);
				else if (pair2 > 1)
					setRotation(GM.Direction.NW);
				else if (pair3 > 1)
					setRotation(GM.Direction.NE);
			}
			else
			{
				this.sprite.GetComponent<Image>().sprite = Art.walls[1];
			}

		}
		else
		{
			this.sprite.GetComponent<Image>().sprite = Art.walls[1];

		}


	}

	void setRotation(GM.Direction d)
	{
		switch (d)
		{
			case GM.Direction.N:
			case GM.Direction.S:
				sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;

			case GM.Direction.NW:
			case GM.Direction.SE:
				sprite.transform.rotation = Quaternion.Euler(0, 0, 60);
				break;

			case GM.Direction.NE:
			case GM.Direction.SW:
				sprite.transform.rotation = Quaternion.Euler(0, 0, 120);
				break;


		}

	}
	
	void setRotation()
	{
		sprite.transform.rotation = Quaternion.Euler(0, 0, sprite.transform.rotation.eulerAngles.z - 60);
	}

	public List<Tile> getAdjacent()
	{
		List<Tile> adj = new List<Tile>();

		if (position.x % 2 == 0)
		{
			try { adj.Add(GM.GetTile((int)position.x, (int)position.y + 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x, (int)position.y - 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x + 1, (int)position.y)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x - 1, (int)position.y)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x - 1, (int)position.y - 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x + 1, (int)position.y - 1)); } catch { }
		}
		else
		{
			try { adj.Add(GM.GetTile((int)position.x, (int)position.y + 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x, (int)position.y - 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x + 1, (int)position.y)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x - 1, (int)position.y)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x + 1, (int)position.y + 1)); } catch { }
			try { adj.Add(GM.GetTile((int)position.x - 1, (int)position.y + 1)); } catch { }
		}


		return adj;

	}

	public Dictionary<Tile, GM.Direction> getAdjacentDirection()
	{

		Dictionary<Tile, GM.Direction> d = new Dictionary<Tile, GM.Direction>();

		if (position.x % 2 == 0)
		{
			try { d.Add(GM.GetTile((int)position.x, (int)position.y + 1), GM.Direction.S); } catch { }//s
			try { d.Add(GM.GetTile((int)position.x, (int)position.y - 1), GM.Direction.N); } catch { }//n
			try { d.Add(GM.GetTile((int)position.x + 1, (int)position.y), GM.Direction.NE); } catch { }//ne
			try { d.Add(GM.GetTile((int)position.x - 1, (int)position.y), GM.Direction.NW); } catch { }//nw
			try { d.Add(GM.GetTile((int)position.x - 1, (int)position.y - 1), GM.Direction.SW); } catch { }//sw
			try { d.Add(GM.GetTile((int)position.x + 1, (int)position.y - 1), GM.Direction.SE); } catch { }//se
		}
		else
		{
			try { d.Add(GM.GetTile((int)position.x, (int)position.y + 1), GM.Direction.N); } catch { }//n
			try { d.Add(GM.GetTile((int)position.x, (int)position.y - 1), GM.Direction.S); } catch { }//s
			try { d.Add(GM.GetTile((int)position.x + 1, (int)position.y), GM.Direction.SE); } catch { }//se
			try { d.Add(GM.GetTile((int)position.x - 1, (int)position.y), GM.Direction.SW); } catch { }//sw
			try { d.Add(GM.GetTile((int)position.x + 1, (int)position.y + 1), GM.Direction.NE); } catch { }//ne
			try { d.Add(GM.GetTile((int)position.x - 1, (int)position.y + 1), GM.Direction.NW); } catch { }//nw
		}

		return d;

	}

	void placeBridge()
	{
		List<Tile> adj = getAdjacent();
		foreach (Tile t in adj)
		{
			if (t.type == TYPE.Bridge)
				return;
		}

		type = TYPE.Bridge;
		Dictionary<Tile, GM.Direction> adjtiles = new Dictionary<Tile, GM.Direction>();
		foreach (KeyValuePair<Tile, GM.Direction> t in getAdjacentDirection())
		{
			if (t.Key.type == TYPE.Default)
				adjtiles.Add(t.Key, t.Value);
		}

		int pair1 = 0;
		int pair2 = 0;
		int pair3 = 0;
		foreach (KeyValuePair<Tile, GM.Direction> t in adjtiles)
		{
			switch (t.Value)
			{
				case GM.Direction.N:
				case GM.Direction.S:
					pair1++;
					break;

				case GM.Direction.NW:
				case GM.Direction.SE:
					pair2++;
					break;

				case GM.Direction.NE:
				case GM.Direction.SW:
					pair3++;
					break;

			}

		}

		if (pair1 > 1 || pair2 > 1 || pair3 > 1)
		{
			sprite.GetComponent<Image>().sprite = Art.bridge;
			if (pair1 > 1)
				setRotation(GM.Direction.N);
			else if (pair2 > 1)
				setRotation(GM.Direction.NW);
			else if (pair3 > 1)
				setRotation(GM.Direction.NE);


		}
	}

}
