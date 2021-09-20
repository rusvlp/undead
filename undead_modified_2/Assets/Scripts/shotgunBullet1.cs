using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunBullet1 : MonoBehaviour
{
    public int angleOfShotBullet;
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        Vector3 screenMousePosition = Input.mousePosition;
        Vector3 wmp = _camera.ScreenToWorldPoint(screenMousePosition);
        // wmp.Normalize();
        wmp -= transform.position;
        wmp.z = 0;
        wmp.Normalize();
        float rot_z = Mathf.Atan2(wmp.y, wmp.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z + angleOfShotBullet);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("zombie"))
            {
                hitInfo.collider.GetComponent<zombie>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
