using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public Transform firePoint;
    public GameObject Bullet;
    public LineRenderer laser;

    public GameObject LaserChargeUI;
    private Slider sliderLaser;
    AudioSource Audio;
    public AudioClip LaserSound;

    private float laserCharge = 5;//Количество выстрелов лазера
    private float LaserCharge
    {
        get { return laserCharge; }
        set
        {
            if(laserCharge + value <= 5)
                laserCharge += value;
            sliderLaser.value = laserCharge;
            
        }
    }


    private void Start()
    {
        sliderLaser = LaserChargeUI.GetComponent<Slider>();//получаем слайдер для отображение зарядов лазера
        Audio = GetComponent<AudioSource>();
        Audio.clip = LaserSound;
    }
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))//ЛКМ стрельба "пулями"
        {
            Instantiate(Bullet, firePoint.position, firePoint.rotation);
        }
        if (Input.GetButtonDown("Fire2"))//ПКМ стрельба лазером
        {
            if(laserCharge > 0)
            {
                StartCoroutine(Laser());
            }
        }
    }

    IEnumerator RestoreCharges()//восстанавливает после выстрела каждый заряд по 10 секунд
    {
        yield return new WaitForSeconds(10);
        LaserCharge = +1;
    }

    IEnumerator Laser()
    {
        RaycastHit2D[] hitInfo = Physics2D.RaycastAll(firePoint.position, firePoint.up);//получаем все объекты на пути raycast
        
        foreach(var hit in hitInfo)//Перебираем все попавшиеся на пути объекты и уничтожаем
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            Asteroid asteroid = hit.transform.GetComponent<Asteroid>();
            Shard shard = hit.transform.GetComponent<Shard>();
            if (enemy != null)
            {
                enemy.Die();
                GameManager.Score = 2;
            }
            if (asteroid != null)
            {
                asteroid.Die();
                GameManager.Score = 1;
            }
            if (shard != null)
            {
                shard.Die();
                GameManager.Score = 1;
            }
        }
        LaserCharge = -1;
        laser.SetPosition(0, firePoint.position);
        laser.SetPosition(1, firePoint.position + (firePoint.up * 50));

        laser.enabled = true;
        Audio.Play();
        yield return new WaitForSeconds(0.02f);
        laser.enabled = false;
        StartCoroutine(RestoreCharges());
    }
}
