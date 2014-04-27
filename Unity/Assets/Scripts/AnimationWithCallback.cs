using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AnimationWithCallback : MonoBehaviour
{
	private List<AnimationCallbackContainer> field_animationCallbackContainers;
	public Animation Animation;

	public AnimationWithCallback()
	{
		field_animationCallbackContainers = new List<AnimationCallbackContainer>();
	}

	public void Play(Animation param_animation, String param_animationName, Action param_callback = null)
	{
		Play(new AnimationCallbackContainer(param_animation, param_animationName, param_callback));
	}
	public void Play(AnimationCallbackContainer param_animationCallbackContainer)
	{
		param_animationCallbackContainer.Animation.Play(param_animationCallbackContainer.Name);
		field_animationCallbackContainers.Add(param_animationCallbackContainer);
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		for (int i = field_animationCallbackContainers.Count - 1; i >= 0; i--)
		{
			if (field_animationCallbackContainers[i].Animation == null)
			{
				Debug.Log("AnimationCallback: Animation is null.");
				field_animationCallbackContainers.RemoveAt(i);
				continue;
			}
			if (field_animationCallbackContainers[i].Callback == null)
			{
				Debug.Log("AnimationCallback: Callback is null.");
				field_animationCallbackContainers.RemoveAt(i);
				continue;
			}
			if (field_animationCallbackContainers[i].Animation.isPlaying)
			{
				continue;
			}
			field_animationCallbackContainers[i].Callback();
			field_animationCallbackContainers.RemoveAt(i);
		}
	}
}

public class AnimationCallbackContainer
{
	private Animation field_animation;
	public Animation Animation { get { return field_animation; } }
	private Action field_callback;
	public Action Callback { get { return field_callback; } }
	private String field_name;
	public String Name { get { return field_name; } }

	public AnimationCallbackContainer(Animation param_animation, String param_name, Action param_callback = null)
	{
		field_animation = param_animation;
		field_name = param_name;
		field_callback = param_callback ?? delegate { };
	}
}