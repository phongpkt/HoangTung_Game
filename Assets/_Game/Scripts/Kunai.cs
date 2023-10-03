using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float speed = 10f;
    void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        rb.velocity = transform.right * speed;
        Invoke(nameof(OnDespawn), 4f);
    }
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy target = collision.GetComponent<Enemy>();
            target.OnHit(50);
            OnDespawn();
        }
    }
}
