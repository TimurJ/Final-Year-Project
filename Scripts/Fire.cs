using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform firePoint;
    public GameObject firePrefab;

    void Update()
    {
		if (Input.GetButtonDown("Fire1"))
		{
            Shoot();
		}
    }

    void Shoot()
	{
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
	}
}
