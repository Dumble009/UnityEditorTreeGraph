using UnityEngine;

public class BulletSpitter : MonoBehaviour
{
	[SerializeField]
	GameObject bullet_prefab;
	public void Spit(Vector3 origin, Vector3 direction)
	{
		GameObject bullet = Instantiate(bullet_prefab, origin, Quaternion.identity) as GameObject;
		bullet.transform.LookAt(bullet.transform.position + direction);
	}
}
