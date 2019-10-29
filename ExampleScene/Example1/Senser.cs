using UnityEngine;

public delegate void Detect();
public class Senser : MonoBehaviour
{
	public event Detect DetectCallback;

	protected void Detect()
	{
		if (DetectCallback != null)
		{
			DetectCallback.Invoke();
		}
	}
}
