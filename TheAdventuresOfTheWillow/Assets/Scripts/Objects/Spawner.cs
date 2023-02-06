using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _object;
	
	public void Spawn()
	{
		Instantiate(_object, transform.position, Quaternion.identity);
	}
}
