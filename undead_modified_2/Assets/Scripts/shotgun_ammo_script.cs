using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun_ammo_script : MonoBehaviour
{
    public CharacterControl cc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            cc.pickUpAmmoSg();
            Destroy(gameObject);
        }

    }
}
