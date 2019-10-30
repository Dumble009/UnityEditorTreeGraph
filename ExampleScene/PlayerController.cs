using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private void Awake()
	{
		model = GetComponent<CharacterModel1>();
	}
	CharacterModel1 model;
	void Update()
    {
		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		model.Move(transform.position + dir);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			Vector3 hitPos = hit.point;
			if (Input.GetButtonDown("Fire1"))
			{
				Vector3 direction = hitPos - transform.position;
				direction.Scale(new Vector3(1, 0, 1));
				model.SpitBullet(direction: direction);
			}
		}
    }
}
