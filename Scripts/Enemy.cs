using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int attack;
    public float speed;
    public float chaseRadius;
    public float attackRadius;
    private float dazedTime;
    public float startDazedTime;
    public Transform target;
    public Transform homePosition;

    //public GameObject bloodEffect;
    private Animator anim;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckDistance();

        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject, 0.8f);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player"))
		{
            collision.GetComponent<Health>().TakeDamage(attack);
        }

    }

	public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position)>attackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position ,target.position, speed * Time.deltaTime);
		}
    }
}
