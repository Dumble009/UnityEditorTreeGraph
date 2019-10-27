using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	void Awake()
	{
		Destroy(this.gameObject, lifeTime);
	}
	[SerializeField]
	float speed = 5.0f;
	[SerializeField]
	float lifeTime = 3.0f;
    void Update()
    {
		transform.position += transform.forward * speed * Time.deltaTime;
    }
}
