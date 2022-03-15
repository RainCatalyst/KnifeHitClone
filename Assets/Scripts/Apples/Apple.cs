using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] GameObject explodeEffect;

    public void Explode()
    {
        Instantiate(explodeEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
