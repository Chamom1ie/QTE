using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Laser : MonoBehaviour
{
    LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        QTEManager.instance.LaserAction += SetLaserThick;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().OnHit(3);
        }
    }
    void SetLaserThick(float laserThickness)
    {
        line.material.SetFloat("_LaserThickness", laserThickness);
    }
}
