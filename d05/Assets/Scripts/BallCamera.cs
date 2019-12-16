using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public float moveSpeed;
    public float speedH, speedV;

    Transform freeViewTarget;
    Transform ball;
    Rigidbody rb;
    float yaw, pitch;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        ball = GameObject.FindWithTag("Ball").transform;
        rb = GetComponent<Rigidbody>();
    }

    //void RaycastMouse()
    //{
    //    Vector3 mouse = Input.mousePosition;
    //    Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(
    //                                                    mouse.x,
    //                                                    mouse.y,
    //                                                    player.transform.position.y));
    //    Vector3 forward = mouseWorld - player.transform.position;
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity))
    //    {

    //    }
    //}

    //void UpdateVelocity(float newValue, string toReplace)
    //{
    //    Vector3 tmp = rb.velocity;
    //    if (toReplace == "x")
    //        tmp.x = newValue;
    //    else if (toReplace == "x")
    //        tmp.y = newValue;
    //    else if (toReplace == "x")
    //        tmp.z = newValue;
    //    rb.velocity = tmp;
    //}

    void HandleInputFreeView()
    {
        float speed = moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q))
            rb.AddForce(transform.up * speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.E))
            rb.AddForce(transform.up * -speed, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(transform.right * -speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.right * speed, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        if (!Input.anyKey)
            rb.velocity = Vector3.zero;
        //rb.velocity = -t
        //transform.position -= transform.forward * speed;
        //transform.Translate(-transform.forward * speed);
        //transform.Translate(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.isInFreeView)
        {
            //Vector3 pos = Input.mousePosition;
            //pos.z = -0.1f;
            //pos = Camera.main.ScreenToWorldPoint(pos);
            //transform.LookAt(pos);
            HandleInputFreeView();
            Debug.DrawRay(transform.position, transform.forward * 10, Color.black, 5);
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0);
            //transform.LookAt(freeViewTarget);
        }
        else 
        {
            transform.LookAt(ball);
        }
    }
}
