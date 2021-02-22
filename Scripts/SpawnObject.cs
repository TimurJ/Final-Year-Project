using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
	public GameObject[] objects;// used to instantiate a object that is inputted into the array

	private void Start()
	{
		int rand = Random.Range(0, objects.Length);// randomly chooses one of the objects from the array
		GameObject instance = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);// instantiates the object
		instance.transform.parent = transform;// parents the tiles to the rooms 
	}
}
