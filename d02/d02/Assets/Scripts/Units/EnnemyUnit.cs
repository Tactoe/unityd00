using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyUnit : Unit
{
    // Start is called before the first frame update

    void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        yes = gameObject.GetComponent<AudioSource>();
        target = transform.position;
        opponentTag = "Player";
    }
}
