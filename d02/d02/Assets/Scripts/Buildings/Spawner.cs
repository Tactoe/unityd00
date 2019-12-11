using UnityEngine;
using System.Collections;

public class Spawner : Building
{

    public float spawnDelay;
    public GameObject unitToSpawn;

    float spawnTimer;
    void SpawnUnit()
    {
        GameObject tmp = Instantiate(unitToSpawn);
        tmp.transform.position = transform.position + Vector3.one * Random.Range(1, 4);
    }

    void Start()
    {
        spawnTimer = spawnDelay;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0)
        {
            SpawnUnit();
            spawnTimer = spawnDelay;
        }
        else
            spawnTimer -= Time.deltaTime;
        if (health <= 0)
        {
            Debug.Log(gameObject.tag + " lost!");
            Destroy(gameObject);
        }
    }
}
