using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zombie : MonoBehaviour
{

    public float zombieSpeed;
    public CharacterControl cc = null;
    public float timeBtwAttacks;
    public GameObject player = null;
    public Slider hpStatusRef = null;         // ссылка на референс полоски хп
    public Canvas can = null;                 // ссылка на канвас
    public Transform hpPoint = null;          // ссылка на точку, в которой будет появляться полоска хп
   

    public int zombiedistance;
    public int damagzombie;
    public int health;
    public LayerMask whatIsSolidZombie;
    public  Camera cam;
    private Slider hpStatus;

   float attackCountdown = 0;
    // Start is called before the first frame update
    void Start()
    {
      cam = cc._camera;
      can = cc.can;

      hpStatus = (Slider)Instantiate(hpStatusRef);                // создаем новую полоску хп на основе референса
      hpStatus.transform.SetParent(can.transform, true);          // делаем ее дочерним элементом UI канваса
    }

    // Update is called once per frame
    void Update()
    {

        if (health <= 0)
        {
            Destroy(hpStatus.gameObject);
            Destroy(gameObject);
            
        }
        Vector3 rotate = -transform.position + cc.transform.position;                    // зомби следует за персонажем
        float rot_z = Mathf.Atan2(rotate.y, rotate.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z-90);
        transform.position += transform.up * zombieSpeed * Time.deltaTime / 3;

        attackCountdown += Time.deltaTime;

  //      Debug.Log(cc.hp);

        hpStatus.value = health;

        // отображение полоски хп у зомби
        Vector3 screenPos = cam.WorldToScreenPoint(hpPoint.position);      // ковертируем мировые координаты точки, где будет появляться полоска хп, в экранные
        hpStatus.transform.position = screenPos;                           // присваиваем полоске хп координаты точки
        hpStatus.transform.rotation = this.transform.rotation;             // поворачиваем полоску хп так же, как и зомби


    }
    public void TakeDamage(int damage)
    {
        health -= damage;

    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && attackCountdown >= Time.deltaTime * timeBtwAttacks) 
        {
            
            cc.takeDamagePlayer(damagzombie);
            
           
            attackCountdown = 0;

        }
        
    }


}
   
