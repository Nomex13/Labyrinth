using UnityEngine;
using System.Collections;

public class Gameplay : MonoBehaviour
{
	public Field Field;

	// Use this for initialization
	void Start ()
	{
		Field.MapLoad(0);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void MapLoadNext()
	{
		Field.MapLoadNext();
	}
	public void MapReload()
	{
		Field.MapReload();
	}
	public void MapLoadPrevious()
	{
		Field.MapLoadPrevious();
	}

	public void WallPlace(int param_x, int param_y)
	{
		//Field.
	}
}

public static class Global
{
	private static Gameplay field_gameplay;
	public static Gameplay Gameplay
	{
		get
		{
			if (field_gameplay != null)
				return field_gameplay;

			field_gameplay = GameObject.Find("$").GetComponent("GamePlay") as Gameplay;
			return field_gameplay;
		}
	}
}