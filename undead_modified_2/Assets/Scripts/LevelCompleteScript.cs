using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    public CharacterControl cc;
    public GameObject endmenu;
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

            endmenu.SetActive(true);
            Time.timeScale = 0;
            cc.isPause = true;
        }
    }
}
