using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclingMotion : MonoBehaviour
{

    public float Speed = 0.5f;
    public float Radius = 0.1f;

    private Vector3 center;
    private float angle;

    private void Start()
    {
        center = transform.position;
        angle = 0.0f;
    }

    private void Update()
    {

        angle += Speed * Time.deltaTime;
        if (angle >= 360.0f)
            angle -= 360.0f;

        var offset = new Vector3(Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)) * Radius;
        transform.position = center + offset;
    }
}