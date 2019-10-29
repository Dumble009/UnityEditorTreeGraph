using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSenser : Senser
{
	[SerializeField]
	Transform target;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform == target)
		{
			Detect();
		}
	}
}
