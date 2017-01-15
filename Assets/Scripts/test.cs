using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{

	public Vector2 alwaysUpdate = new Vector2(12, 14);

	void Start()
	{
		Art.LoadContent();
		TerrainGeneration.GenerateTilemap();
	}

	void Update()
	{
		GM.GetTile((int)alwaysUpdate.x, (int)alwaysUpdate.y).UpdateTile();
	}

}
