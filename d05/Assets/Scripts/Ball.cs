using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //void HandleInput()
    //{
    //    float xAxis = Input.GetAxis("Horizontal") * speed;
    //    float yAxis = Input.GetAxis("Vertical") * speed;

    //    rb.AddForce(new Vector3(xAxis, 0, yAxis));
    //}
    // Update is called once per frame
    void Update()
    {
        //HandleInput();
    }
}
