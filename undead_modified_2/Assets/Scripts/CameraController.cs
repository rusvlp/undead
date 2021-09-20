using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // GameObject shotgun;
    public CharacterControl chcon = null;
    private Camera _camera;
    public float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update() 
    {
        transform.position = new Vector3(chcon.transform.position.x, chcon.transform.position.y, -10f) ;
     
      
    }
}
