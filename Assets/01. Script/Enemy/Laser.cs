using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("ÁöÀÌÀ×");
            collision.GetComponent<Player>().OnHit(3);
        }
    }
}
