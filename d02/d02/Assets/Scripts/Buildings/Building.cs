using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

    public int health;
    public int maxHealth;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
