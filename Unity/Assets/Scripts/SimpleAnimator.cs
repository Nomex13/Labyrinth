using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class SimpleAnimator : MonoBehaviour
{
	private List<Animation> field_plateAnimations;

	void OnEnable ()
	{
		field_plateAnimations = new List<Animation>();
	}
	
	void Update ()
	{
		for (int i = 0; i < field_plateAnimations.Count; i++)
		{
			if (field_plateAnimations[i].SimpleAnimation.Animate())
			{
				field_plateAnimations[i].Callback();
				field_plateAnimations.RemoveAt(i);
				i--;
			}
		}
	}
	
	public void Animate(ISimpleAnimation param_animation, Action param_callback = null)
	{
		field_plateAnimations.Add(new Animation(param_animation, param_callback));
	}

	struct Animation
	{
		public ISimpleAnimation SimpleAnimation;
		public Action Callback;

		public Animation(ISimpleAnimation param_simpleAnimation, Action param_callback = null)
		{
			SimpleAnimation = param_simpleAnimation;
			Callback = param_callback ?? delegate { };
		}
	}
}

public interface ISimpleAnimation
{
	 bool Animate();
}
