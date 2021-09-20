using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject zombie;
    public Transform spawnpos1;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnpos11 = new Vector3(spawnpos1.position.x, spawnpos1.position.y, -2f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        Instantiate(zombie, spawnpos11, transform.rotation);
       // Instantiate(zombie, spawnpos11, transform.rotation);
       //Instantiate(zombie, spawnpos11, transform.rotation);
       // Instantiate(zombie, spawnpos11, transform.rotation);
       // Instantiate(zombie, spawnpos11, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
