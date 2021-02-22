using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Environment"))
		{
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            CinemachineScript.Instance.ShakeCamera(3f, .2f);
        }
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
		{
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            enemy.TakeDamage(damage);
            CinemachineScript.Instance.ShakeCamera(3f, .2f);
        }
    }
}
