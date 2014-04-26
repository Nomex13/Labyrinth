using System;
using System.Configuration;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Field : MonoBehaviour
{
	public GameObject PrefabCell;

	public float CellSizeX = 1f;
	public float CellSizeY = 1f;

	public float IntervalSizeX = 0.1f;
	public float IntervalSizeY = 0.1f;

	[Range(1,100)]
	public int CellCountX = 10;
	[Range(1, 100)]
	public int CellCountY = 10;

	private GameObject[][] field_cells;

	void Start ()
	{
		LoadMap("Levels/Level 1");
	}
	
	void Update ()
	{
		;
	}

	void UnloadMap()
	{
		for (int x = 0; x < CellCountX; x++)
		{
			for (int y = 0; y < CellCountY; y++)
			{
				Destroy(field_cells[x][y]);
			}
		}
	}

	void LoadMap(String param_mapName)
	{
		TextAsset mapTextAsset = Resources.Load(param_mapName) as TextAsset;
		if (mapTextAsset == null)
		{
			Debug.Log("Couldn't load map \"" + param_mapName + "\"");
			return;
		}
		String mapText = mapTextAsset.text;

		Debug.Log("Map: \r\n" + mapText);

		mapText = mapText.Replace("\r\n", "\n");
		mapText = mapText.Replace("\r", "\n");
		String[] mapLines = mapText.Split('\n');

		if (mapLines.Length == 0)
		{
			Debug.Log("Map is empty");
			return;
		}

		int length = mapLines[0].Length;
		foreach (String line in mapLines)
		{
			if (line.Length != length)
			{
				Debug.Log("Line \"" + line + "\" length is not " + length);
				return;
			}
		}

		char[][] mapChars = new char[length][];
		for (int i = 0; i < length; i++)
		{
			mapChars[i] = new char[mapLines.Count()];
			for (int j = 0; j < mapLines.Count(); j++)
			{
				mapChars[i][j] = mapLines[j][i];
			}
		}

		CellCountX = length;
		CellCountY = mapLines.Count();

		Vector3 position = transform.position - new Vector3((CellCountX - 1) / 2f * (CellSizeX + IntervalSizeX), 0, (CellCountY - 1) / 2f * (CellSizeY + IntervalSizeX));
		field_cells = new GameObject[CellCountX][];
		for (int x = 0; x < CellCountX; x++)
		{
			field_cells[x] = new GameObject[CellCountY];
			for (int y = 0; y < CellCountY; y++)
			{
				field_cells[x][y] = Instantiate(PrefabCell, PrefabCell.transform.position + position + new Vector3(x * (CellSizeX + IntervalSizeX), 0, y * (CellSizeY + IntervalSizeY)), Quaternion.identity) as GameObject;
				field_cells[x][y].name = "Cell " + x + " " + y;
				field_cells[x][y].transform.parent = transform;
			}
		}

		for (int x = 0; x < CellCountX; x++)
		{
			for (int y = 0; y < CellCountY; y++)
			{
				switch (mapLines[x][y])
				{
					case ' ':
						field_cells[x][y].SendMessage("AddObjects", Cell.CellObjects.FLOOR);
						break;
					case '#':
						field_cells[x][y].SendMessage("AddObjects", Cell.CellObjects.FLOOR ^ Cell.CellObjects.WALL);
						break;
					case '@':
						field_cells[x][y].SendMessage("AddObjects", Cell.CellObjects.FLOOR ^ Cell.CellObjects.ENEMY);
						break;
					case '+':
						field_cells[x][y].SendMessage("AddObjects", Cell.CellObjects.FLOOR ^ Cell.CellObjects.HERO);
						break;
					default:
						Debug.Log("Symbol \"" + mapLines[x][y] + "\" is not supported.");
						break;
				}
			}
		}
	}

	void GenerateMap()
	{
		String map = "";
		for (int x = 0; x < CellCountX; x++)
		{
			for (int y = 0; y < CellCountY; y++)
			{
				if (Random.Range(0, 100) > 80)
				{
					map += "#";
				}
			}
			map += "\r\n";
		}
	}
}
