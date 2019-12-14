using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMoving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Untagged";
        Invoke("BecomeCatchable", 2);
        Invoke("Die", 4);
    }

    void BecomeCatchable()
    {
        gameObject.tag = "ring";
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
