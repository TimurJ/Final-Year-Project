using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPose;
    public LayerMask whatIsEnemy;
    public float attackRange;
    public int damage;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
		{
			if (Input.GetKey(KeyCode.Space))
			{
                anim.SetBool("isAttacking", true);
                timeBtwAttack = startTimeBtwAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPose.position, attackRange, whatIsEnemy);
				for (int i = 0; i < enemiesToDamage.Length; i++)
				{
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    CinemachineScript.Instance.ShakeCamera(3f, .2f);
                }            
            }
		}
		else
		{
            anim.SetBool("isAttacking", false);
            timeBtwAttack -= Time.deltaTime;
		}
    }

	private void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPose.position, attackRange);
	}
}
