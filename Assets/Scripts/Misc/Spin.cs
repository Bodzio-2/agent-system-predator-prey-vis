using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 10f;

    void FixedUpdate()
    {
        Vector3 rotation = new Vector3(0f, spinSpeed, 0f);
        transform.Rotate(rotation * Time.fixedDeltaTime);
    }
}
