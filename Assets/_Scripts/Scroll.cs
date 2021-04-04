using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    Vector2 initPos;
    float speed = -0.5f;
    public Transform center;
    void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        transform.position = initPos + Vector2.right * Mathf.Repeat(Time.time * speed, 10);
    }
}
