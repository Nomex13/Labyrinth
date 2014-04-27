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

	private int field_cellCountX = 0;
	private int field_cellCountY = 0;

	private int field_mapIndex = -1;
	private int field_mapIndexMinimum = 0;
	private int field_mapIndexMaximum = 3;
	private String field_mapName = null;
	private GameObject[][] field_cells;

	void Start ()
	{
		;
	}
	
	void Update ()
	{
		;
	}

	void MapUnload()
	{
		field_mapIndex = -1;
		field_mapName = null;

		for (int x = 0; x < field_cellCountX; x++)
		{
			for (int y = 0; y < field_cellCountY; y++)
			{
				Destroy(field_cells[x][y]);
			}
		}
	}

	public bool MapReload()
	{
		int mapIndex = field_mapIndex;
		String mapName = field_mapName;
		MapUnload();
		return mapIndex < 0 ? MapLoad(mapName) : MapLoad(mapIndex);
	}

	public bool MapLoadNext()
	{
		if (field_mapIndex < field_mapIndexMinimum || field_mapIndex >= field_mapIndexMaximum)
			return false;

		int mapIndex = field_mapIndex;
		MapUnload();

		return MapLoad(mapIndex + 1);
	}
	public bool MapLoadPrevious()
	{
		if (field_mapIndex <= field_mapIndexMinimum || field_mapIndex > field_mapIndexMaximum)
			return false;

		int mapIndex = field_mapIndex;
		MapUnload();

		return MapLoad(mapIndex - 1);
	}
	public bool MapLoad(int param_mapIndex)
	{
		if (param_mapIndex < field_mapIndexMinimum || param_mapIndex > field_mapIndexMaximum)
			return false;

		MapUnload();

		if (MapLoad("Levels/Level " + param_mapIndex))
		{
			field_mapIndex = param_mapIndex;
			return true;
		}
		else
		{
			field_mapIndex = -1;
			field_mapName = null;
			return false;
		}
	}
	public bool MapLoad(String param_mapName)
	{
		MapUnload();

		TextAsset mapTextAsset = Resources.Load(param_mapName) as TextAsset;
		if (mapTextAsset == null)
		{
			Debug.Log("Couldn't load map \"" + param_mapName + "\"");
			return false;
		}
		String mapText = mapTextAsset.text;

		Debug.Log("Map: \r\n" + mapText);

		mapText = mapText.Replace("\r\n", "\n");
		mapText = mapText.Replace("\r", "\n");
		String[] mapLines = mapText.Split('\n');

		if (mapLines.Length == 0)
		{
			Debug.Log("Map is empty");
			return false;
		}

		int length = mapLines[0].Length;
		foreach (String line in mapLines)
		{
			if (line.Length != length)
			{
				Debug.Log("Line \"" + line + "\" length is not " + length);
				return false;
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

		field_cellCountX = length;
		field_cellCountY = mapLines.Count();

		Vector3 position = transform.position - new Vector3((field_cellCountX - 1) / 2f * (CellSizeX + IntervalSizeX), 0, (field_cellCountY - 1) / 2f * (CellSizeY + IntervalSizeX));
		field_cells = new GameObject[field_cellCountX][];
		for (int x = 0; x < field_cellCountX; x++)
		{
			field_cells[x] = new GameObject[field_cellCountY];
			for (int y = 0; y < field_cellCountY; y++)
			{
				field_cells[x][y] = Instantiate(PrefabCell, PrefabCell.transform.position + position + new Vector3(x * (CellSizeX + IntervalSizeX), 0, y * (CellSizeY + IntervalSizeY)), Quaternion.identity) as GameObject;
				field_cells[x][y].name = "Cell " + x + " " + y;
				field_cells[x][y].transform.parent = transform;
			}
		}

		for (int x = 0; x < field_cellCountX; x++)
		{
			for (int y = 0; y < field_cellCountY; y++)
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
		field_mapName = param_mapName;
		return true;
	}

	void MapGenerate()
	{
		String map = "";
		for (int x = 0; x < field_cellCountX; x++)
		{
			for (int y = 0; y < field_cellCountY; y++)
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
