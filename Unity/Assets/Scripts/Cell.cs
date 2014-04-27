using System;
using TouchScript.Gestures;
using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour
{
	private TapGesture tapGesture;
	private PressGesture pressGesture;

	public GameObject PrefabFloor;
	public GameObject PrefabWall;
	public GameObject PrefabEnemy;

	private GameObject field_floor;
	private GameObject field_wall;
	private GameObject field_enemy;

	void Start()
	{

		tapGesture = GetComponent<TapGesture>();
		if (tapGesture != null)
		{
			tapGesture.Tapped += (sender, args) =>
			{
				Debug.Log(transform.name + " tapped");

				if (field_wall == null)
				{
					AddObjects(CellObjects.WALL);
				}
				else
				{
					RemoveObjects(CellObjects.WALL);
				}
			};
		}
		pressGesture = GetComponent<PressGesture>();
		if (pressGesture != null)
		{
			pressGesture.Pressed += (sender, args) =>
			{
				Debug.Log(transform.name + " pressed");
			};
		}
	}

	void Update()
	{
		;
	}

	public CellObjects GetObjects()
	{
		CellObjects cellObjects = CellObjects.NONE;

		if (field_floor != null)
			cellObjects ^= CellObjects.FLOOR;
		if (field_wall != null)
			cellObjects ^= CellObjects.WALL;
		if (field_enemy != null)
			cellObjects ^= CellObjects.ENEMY;

		return cellObjects;
	}
	public void AddObjects(CellObjects param_objects)
	{
		if ((param_objects & CellObjects.FLOOR) == CellObjects.FLOOR && field_floor == null)
		{
			field_floor = Instantiate(PrefabFloor, PrefabFloor.transform.position + transform.position, Quaternion.identity) as GameObject;
			field_floor.transform.parent = transform;
		}
		if ((param_objects & CellObjects.WALL) == CellObjects.WALL && field_wall == null && field_enemy == null)
		{
			field_wall = Instantiate(PrefabWall, PrefabWall.transform.position + transform.position, Quaternion.identity) as GameObject;
			field_wall.transform.parent = transform;
			field_wall.GetComponent<Wall>().Appear(delegate { });
		}
		if ((param_objects & CellObjects.ENEMY) == CellObjects.ENEMY && field_enemy == null && field_wall == null)
		{
			field_enemy = Instantiate(PrefabEnemy, PrefabEnemy.transform.position + transform.position, Quaternion.identity) as GameObject;
			field_enemy.transform.parent = transform;
		}
	}
	public void RemoveObjects(CellObjects param_objects)
	{
		if ((param_objects & CellObjects.FLOOR) == CellObjects.FLOOR && field_floor != null)
		{
			Destroy(field_floor);
			field_floor = null;
		}
		if ((param_objects & CellObjects.WALL) == CellObjects.WALL && field_wall != null)
		{
			field_wall.GetComponent<Wall>().Dissolve(delegate { Destroy(field_wall); });
			field_wall = null;
		}
		if ((param_objects & CellObjects.ENEMY) == CellObjects.ENEMY && field_enemy != null)
		{
			Destroy(field_enemy);
			field_enemy = null;
		}
	}

	[Flags]
	public enum CellObjects
	{
		NONE = 0,
		FLOOR = 1,
		WALL = 2,
		HERO = 4,
		ENEMY = 8
	}

	class AnimationPlateUpDown
	{
		private GameObject field_gameObject;
		private Vector3 field_positionInitial;
		private Vector3 field_positionShifted;
		private Vector3 field_positionPrevious;
		private float field_timeOfStart;
		private float field_timeOfAnimation;

		public AnimationPlateUpDown(GameObject param_gameObject)
		{
			field_gameObject = param_gameObject;

			field_positionInitial = field_gameObject.transform.position;
			field_positionPrevious = field_positionInitial;
			field_positionShifted = field_positionInitial + new Vector3(0, -0.1f, 0);

			field_timeOfStart = Time.fixedTime;
			field_timeOfAnimation = 0.2f;
		}

		public bool Animate()
		{
			float lerp = (Time.fixedTime - field_timeOfStart) / field_timeOfAnimation;

			if (lerp < 0.5f)
			{
				Vector3 positionNext = Vector3.Lerp(field_positionInitial, field_positionShifted, lerp * 2);
				field_gameObject.transform.position += positionNext - field_positionPrevious;
				field_positionPrevious = positionNext;
				return false;
			}
			else if (lerp < 1)
			{
				Vector3 positionNext = Vector3.Lerp(field_positionShifted, field_positionInitial, (lerp - 0.5f) * 2);
				field_gameObject.transform.position += positionNext - field_positionPrevious;
				field_positionPrevious = positionNext;
				return false;
			}
			else
			{
				field_gameObject.transform.position += field_positionInitial - field_positionPrevious;
				return true;
			}
		}
	}
}
