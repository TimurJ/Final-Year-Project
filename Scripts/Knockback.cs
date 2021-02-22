using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	public float thrust;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
            Rigidbody2D Enemy = collision.GetComponent<Rigidbody2D>();
            if(Enemy != null)
			{
                Enemy.isKinematic = false;
                Vector2 difference = Enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Enemy.AddForce(difference, ForceMode2D.Impulse);
                Enemy.isKinematic = true;
			}
		}
	}
}
