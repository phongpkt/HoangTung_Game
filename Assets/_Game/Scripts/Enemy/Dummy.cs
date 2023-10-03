using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprites;
    public void OnHit(int damage)
    {
        sprites.color = Color.red;
        Invoke(nameof(ResetColor), 0.5f);
    }
    public void ResetColor()
    {
        sprites.color = Color.white;
    }
}
