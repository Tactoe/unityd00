using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflate : MonoBehaviour {

    Vector3 gonflage;
    float degonflage = 1;
	// Use this for initialization
	void Start () {
        gonflage = new Vector3(1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            gonflage += new Vector3(1, 1, 1);
        }
        degonflage = 1 * Time.deltaTime;
        if (gonflage.x >= 0)
            gonflage -= new Vector3(degonflage, degonflage, degonflage);
        this.gameObject.transform.localScale = gonflage;

    }
}
