using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn1 : MonoBehaviour {

    public Transform spawn;
    public GameObject Goblin_rouge;
    private float repeatrate = 5;
    int numberofspawn;
    private float timer = 0.0f;
    private float spawnTime = 2.0f;
    // Use this for initialization
    void SpawnMinion()
    {

        Vector3 spawnPoint = Random.insideUnitCircle.normalized * 5;
        InvokeRepeating("spawnmionion", 5f, repeatrate);
        Vector3 LookDirection = spawn.position;
        LookDirection.y = 0f;
        LookDirection.z = 0f;
        Instantiate(Goblin_rouge, spawn.position, spawn.rotation);
    }
    void Update()
    {


        //InvokeRepeating("spawnmionion", 1f, repeatrate);

        //minion = GameObject.FindWithTag("enemy").transform;
        timer += Time.deltaTime;
        if (timer > spawnTime)
        {
            SpawnMinion();
            timer = 0.0f;
        }
    }
    /*
    InvokeRepeating("spawnmionion", 5f, repeatrate);

        //minion = GameObject.FindWithTag("enemy").transform;
    SpawnMinion(1);

    Vector3 spawnPoint = Random.insideUnitCircle.normalized*spawnDistance;
         Vector3 lastSpawnPoint = Vector3.zero;
    */
}
    
    



    




