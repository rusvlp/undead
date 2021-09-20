using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeToDestroy;
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    public zombie zombie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   private void Update()
    {
        float destroyCountdown = 0;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("zombie"))
            {
                hitInfo.collider.GetComponent<zombie>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }

        destroyCountdown += Time.deltaTime * speed;

        if (destroyCountdown >= Time.deltaTime * speed * timeToDestroy)
        {
            Destroy(gameObject);
        }

        transform.position += transform.up * speed * Time.deltaTime;


    }


}
