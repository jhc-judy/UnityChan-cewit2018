using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    float timeCounter = 0;
    float speed;
    float width;
    float height;

	// Use this for initialization
	void Start () {
        speed = 2;
        width = 50;
        height = 50;
	}
	
	// Update is called once per frame
	void Update () {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * height;
        float y = Mathf.Sin(timeCounter) * width;
        float z = 0;

        transform.position = new Vector3(x, y, z);
	}
}
