using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
	public void AddHealth()
	{
		GetComponent<Health>().GetHealth(1);
		Destroy(gameObject);
	}
}
