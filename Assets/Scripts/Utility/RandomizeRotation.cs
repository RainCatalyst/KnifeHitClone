using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    [SerializeField] float min;
    [SerializeField] float max;

    private void Start() {
        Vector3 rotation = transform.eulerAngles;
        rotation.z = Random.Range(min, max);
        transform.eulerAngles = rotation;
    }
}
