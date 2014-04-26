using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
	private bool field_isDissolving = false;

	void Start ()
	{
	}
	
	void Update ()
	{
		;
	}

	public void Appear(Action param_callback)
	{

		FindObjectOfType<SimpleAnimator>().Animate(new AnimationPlateUpDown(gameObject));
		//FindObjectOfType<SimpleAnimator>().Animate(new AnimationThresholdTransparency(gameObject, false), param_callback);
	}

	public void Dissolve(Action param_callback)
	{
		Debug.Log(field_isDissolving);
		if (field_isDissolving)
			return;
		field_isDissolving = true;

		FindObjectOfType<SimpleAnimator>().Animate(new AnimationDissolve(gameObject), param_callback);
	}


	class AnimationPlateUpDown : ISimpleAnimation
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

	class AnimationDissolve : ISimpleAnimation
	{
		private GameObject field_gameObject;
		private float field_thresholdInitial;
		private float field_thresholdFinal;
		private float field_thresholdPrevious;
		private Color field_colorInitial;
		private Color field_colorFinal;
		private Color field_colorPrevious;
		private float field_timeOfStart;
		private float field_timeOfAnimation;

		//private Material field_sharedMaterial;

		public AnimationDissolve(GameObject param_gameObject)
		{
			field_gameObject = param_gameObject;

			//field_sharedMaterial = field_gameObject.renderer.sharedMaterial;

			field_thresholdInitial = 1;
			field_thresholdPrevious = field_thresholdInitial;
			field_thresholdFinal = 0;

			field_colorInitial = field_gameObject.renderer.material.GetColor("_Color");
			field_colorPrevious = field_colorInitial;
			field_colorFinal = Color.black;

			field_timeOfStart = Time.fixedTime;
			field_timeOfAnimation = 2f;
		}

		public bool Animate()
		{
			float lerp = (Time.fixedTime - field_timeOfStart) / field_timeOfAnimation;

			if (lerp < 1f)
			{
				float thresholdNext = field_thresholdInitial * (1 - lerp) + field_thresholdFinal * lerp;
				Color colorNext = Color.Lerp(field_colorInitial, field_colorFinal, lerp);
				//Debug.Log("Prev " + field_thresholdPrevious);
				//Debug.Log("Next " + thresholdNext);
				//Debug.Log("Valu " + field_gameObject.renderer.sharedMaterial.GetFloat("_Threshold"));
				//Debug.Log("Prev " + field_colorPrevious);
				//Debug.Log("Next " + colorNext);
				//Debug.Log("Valu " + field_gameObject.renderer.material.GetColor("_Color"));
				field_gameObject.renderer.material.SetColor("_Color", colorNext);
				field_colorPrevious = colorNext;
				field_gameObject.renderer.material.SetFloat("_Threshold", thresholdNext);
				field_gameObject.renderer.material.SetFloat("_BorderThickness", thresholdNext < 0.9f ? 0.1f : 1 - thresholdNext);
				field_thresholdPrevious = thresholdNext;
				return false;
			}
			else
			{
				//field_gameObject.renderer.material.SetFloat("_Threshold", field_gameObject.renderer.material.GetFloat("_Threshold") - field_thresholdPrevious + field_thresholdFinal);
				//field_gameObject.renderer.material = field_sharedMaterial;
				return true;
			}
		}
	}
}
