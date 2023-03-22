using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public float speed = 2;
    public int direction = 1;
    public float start;
    public float end;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((direction * speed) * Time.deltaTime, 0, 0);
        //Debug.Log(transform.position.x);
        if (transform.position.x >= end)
        {
            direction = -1;
        }

        if (transform.position.x <= start)
        {
            direction = 1;
        }
    }
}
