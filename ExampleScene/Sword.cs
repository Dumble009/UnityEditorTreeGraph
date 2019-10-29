using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public void Swing()
	{
		StartCoroutine(SwingCoroutine());
	}
	[SerializeField]
	float swingSpeed = 3.0f;
	bool isSwinging = false;
	public bool IsSwinging {
		get {
			return isSwinging;
		}
	}

	IEnumerator SwingCoroutine()
	{
		isSwinging = true;
		float angle = -90.0f;
		while (angle <= 90.0f)
		{
			Debug.Log(angle);
			angle += swingSpeed * Time.deltaTime;
			Vector3 euler = transform.localRotation.eulerAngles;
			euler.y = angle;
			transform.localRotation = Quaternion.Euler(euler);
			yield return null;
		}
		transform.localRotation = Quaternion.Euler(Vector3.zero);

		isSwinging = false;
	}
}
