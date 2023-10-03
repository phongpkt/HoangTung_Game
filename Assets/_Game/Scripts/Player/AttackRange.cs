using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
	[SerializeField] private GameObject owner;
	[SerializeField] private int attackDamage;
	private void OnTriggerEnter2D(Collider2D collider2D) 
	{
		if(collider2D.CompareTag(Constants.ENEMY) && collider2D.gameObject != owner)	
		{
			Enemy enemy = collider2D.GetComponent<Enemy>();
			enemy.OnHit(attackDamage);
			Debug.Log("Damage to: " + enemy + " - " + attackDamage);
		}
        else if (collider2D.CompareTag(Constants.PLAYER) && collider2D.gameObject != owner)
        {
            Player player = collider2D.GetComponent<Player>();
            player.OnHit(attackDamage);
            Debug.Log("Damage to: " + player + " - " + attackDamage);
        }
    }
}
