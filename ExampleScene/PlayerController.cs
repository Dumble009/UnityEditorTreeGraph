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
    }
}
