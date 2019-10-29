using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeTicker : MonoBehaviour
{
	private void Awake()
	{
		btComponents = GameObject.FindObjectsOfType<BehaviourTreeComponent>();
	}
	BehaviourTreeComponent[] btComponents;
	void Update()
    {
		if (btComponents != null)
		{
			foreach (BehaviourTreeComponent bt in btComponents)
			{
				bt.Tick();
			}
		}
    }
}
