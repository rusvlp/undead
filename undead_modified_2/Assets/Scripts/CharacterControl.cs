using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControl : MonoBehaviour
{
    public GameObject ptr;
    public GameObject deadIndicator;

    public int amountOfAmmoSg;
    public int amountOfAmmoAr;
    public int amountOfAmmoPistol;

    public Text currentammo;
    public Text totalammo;
    public Text hpam;

    public int reloadTime;
    public int recoilFromSg;
    public int hp;
    public GameObject bullet;
    public int angle_1;
    public int angle_2;
    public Slider slider;
    public zombie zombie;
    public Canvas can;
    public Transform shotPoint;
    public Camera _camera;
    public int healAmount;

    public float startTimeBtwShots;
    public float startTimeBtwShotsAr;

    public float speed = 20;
    public bool isPause = false;


    public Sprite sg;
    public Sprite pistol;
    public Sprite ar;

    public SpriteRenderer sr;

    private double tmpRec;
    private float timeBtwShots;
    private float timeBtwShotsAr;

    private int currentAmmoPistol;
    private int currentAmmoSg;
    private int currentAmmoAr;
    private int tmppistol;
    private int tmpsg;
    private int tmpar;


    private int currentTotalAmmoPistol;
    private int currentTotalAmmoSg;
    private int currentTotalAmmoAr;
    private int currentGunId = 2;
    //public Vector3 wmps = Vector3.zero;


    // Start is called before the first frame update
    public void Start()
    {
        currentTotalAmmoSg = 30;
        currentAmmoSg = amountOfAmmoSg;

        currentTotalAmmoPistol = 18;
        currentAmmoPistol = amountOfAmmoPistol;

        currentTotalAmmoAr = 60;
        currentAmmoAr = amountOfAmmoAr;

        transform.position = new Vector3(0f, 0f, -2f);
        _camera = Camera.main;
        //Vector3 wmps = Vector3.zero;
    }




    public int recoilPos(int x)
    {
        int y = -(x * x) + 3;
        return y;
    }

    // Update is called once per frame
    public void Update()
    {

        Vector3 screenMousePosition = Input.mousePosition;
        Vector3 wmp = _camera.ScreenToWorldPoint(screenMousePosition);

        wmp -= transform.position;
        wmp.z = 0;
        wmp.Normalize();
        float rot_z = Mathf.Atan2(wmp.y, wmp.x) * Mathf.Rad2Deg;

        if (!isPause)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
       
        Quaternion rot_1 = Quaternion.Euler(0f, 0f, rot_z + angle_1 - 90);
        Quaternion rot_2 = Quaternion.Euler(0f, 0f, rot_z + angle_2 - 90);
        Quaternion rot_zero = Quaternion.Euler(0f, 0f, 0f);

        //if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        //{
        //    wmps = _camera.ScreenToWorldPoint(screenMousePosition);
        //    wmps.z = 0;
        //}



        // управление
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += wmp * speed / 3 * Time.deltaTime;
        }
         if (Input.GetKey(KeyCode.S))
        {
            transform.position -= wmp * speed / 3 * Time.deltaTime;
        }

         if (Input.GetKey(KeyCode.A))
        {

            transform.position += -transform.right * speed / 3 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * speed / 3 * Time.deltaTime;
        }


        // смена оружия
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sr.sprite = pistol;
            currentGunId = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sr.sprite = sg;
            currentGunId = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        { 
            sr.sprite = ar;
            currentGunId = 3;
        }

        // стрельба
        if (timeBtwShots <= 0 && Input.GetMouseButtonDown(0) && currentAmmoSg > 0 && currentGunId == 2 && !isPause)         //  дробовик
        {
            // при нажатии лкм происходит присвоение значения отдачи временной переменной отдачи (для инициализации отдач   
            tmpRec = (double)recoilFromSg / 10;
            Instantiate(bullet, shotPoint.position, transform.rotation);
            Instantiate(bullet, shotPoint.position, rot_1);
            Instantiate(bullet, shotPoint.position, rot_2);
            timeBtwShots = startTimeBtwShots * Time.deltaTime;
            currentAmmoSg--;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (timeBtwShots <= 0 && Input.GetMouseButtonDown(0) && currentAmmoPistol > 0 && currentGunId == 1 && !isPause)      // пистолет (стреляет без отдачи) 
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            timeBtwShots = startTimeBtwShots * Time.deltaTime;
            currentAmmoPistol--;

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (timeBtwShotsAr <=0 && Input.GetMouseButton(0) && currentAmmoAr > 0 && currentGunId == 3 && !isPause)     // автомат
        {
            Instantiate(bullet, shotPoint.position, transform.rotation);
            timeBtwShotsAr = startTimeBtwShotsAr * Time.deltaTime;
            currentAmmoAr--;
        }
        else
        {
            timeBtwShotsAr -= Time.deltaTime;
        }

        //перезарядка

        if (Input.GetKeyDown(KeyCode.R) && currentGunId == 2 && currentTotalAmmoSg > 0 && !isPause)                                                           // дробовик
        {
            tmpsg = currentTotalAmmoSg;
            currentTotalAmmoSg -= amountOfAmmoSg - currentAmmoSg;
            if (currentTotalAmmoSg < 0) currentTotalAmmoSg = 0;
            if (currentTotalAmmoSg >= amountOfAmmoSg) currentAmmoSg = amountOfAmmoSg;
            else if (currentTotalAmmoSg + currentAmmoSg >= amountOfAmmoSg) currentAmmoSg = amountOfAmmoSg;
            else currentAmmoSg += tmpsg;

            if (currentAmmoSg >= amountOfAmmoSg) currentAmmoSg = amountOfAmmoSg;
                
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGunId == 1 && currentTotalAmmoPistol > 0 && !isPause) // пистолет
        {
            tmppistol = currentTotalAmmoPistol;
            currentTotalAmmoPistol -= amountOfAmmoPistol - currentAmmoPistol;
            if (currentTotalAmmoPistol < 0) currentTotalAmmoPistol = 0;
            if (currentTotalAmmoPistol >= amountOfAmmoPistol) currentAmmoPistol = amountOfAmmoPistol;
            else if (currentTotalAmmoPistol + currentAmmoPistol >= amountOfAmmoPistol) currentAmmoPistol = amountOfAmmoPistol;
            else currentAmmoPistol += tmppistol;

            if (currentAmmoPistol >= amountOfAmmoPistol) currentAmmoPistol = amountOfAmmoPistol;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentGunId == 3 && currentTotalAmmoAr > 0 && !isPause) // автомат
        {
            tmpar = currentTotalAmmoAr;
            currentTotalAmmoAr -= amountOfAmmoAr - currentAmmoAr;
            if (currentTotalAmmoAr < 0) currentTotalAmmoAr = 0;
            if (currentTotalAmmoAr >= amountOfAmmoAr) currentAmmoAr = amountOfAmmoAr;
            else if (currentTotalAmmoAr + currentAmmoAr >= amountOfAmmoAr) currentAmmoAr = amountOfAmmoAr;
            else currentAmmoAr += tmpar;

            if (currentAmmoAr >= amountOfAmmoAr) currentAmmoAr = amountOfAmmoAr;
        }

        // отображение количества патронов на экране
        if (currentGunId == 2)                                                 //дробовик
        {
            currentammo.text = currentAmmoSg.ToString();
            totalammo.text = currentTotalAmmoSg.ToString();
        }

        if (currentGunId == 1)                                                 // пистолет
        {
            currentammo.text = currentAmmoPistol.ToString();
            totalammo.text = currentTotalAmmoPistol.ToString();
        }

        if (currentGunId == 3)                                                 // автомат
        {
            currentammo.text = currentAmmoAr.ToString();
            totalammo.text = currentTotalAmmoAr.ToString();
        }

        // отображение хп (число) на экране

        hpam.text = hp.ToString();




        if (Input.GetKeyDown(KeyCode.E))                                                // спавн зомби по кнопке Е
        {
             Instantiate(zombie, shotPoint.position + transform.up * 10, rot_zero);
             zombie.cc = this;
        }
        slider.value = hp;

        if (tmpRec > (double)recoilFromSg / 10000)    // при каждом выполнении Update временная переменная уменьшает свое значение в N раз. Действие будет срабатывать, пока временная переменная больше, чем отдача / 1000
        {
            if (Input.GetKey(KeyCode.W))
                 transform.position -= (transform.up * (int)tmpRec * Time.deltaTime); // + (wmp * speed * Time.deltaTime / 3);
            else
                  transform.position -= transform.up * (int)tmpRec * Time.deltaTime;
             tmpRec /= 1.025;
         }

        if (hp <= 0)
        {
            deadIndicator.gameObject.SetActive(true);
                Destroy(gameObject);
        }

    }
    void fixedUpdate()
    {

    }

    public void pickUpAmmoPistol()
    {
        currentTotalAmmoPistol += amountOfAmmoPistol;
    }

    public void pickUpAmmoSg()
    {
        currentTotalAmmoSg += amountOfAmmoSg;
    }

    public void pickUpAmmoAr()
    {
        currentTotalAmmoAr += amountOfAmmoAr;
    }

    public void takeDamagePlayer(int damag)
    {
        hp -= damag;
    }

    public void heal(int ha)
    {
        if (hp + ha >= 100) hp = 100;
        else hp += ha;
    }
}


