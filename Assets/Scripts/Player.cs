using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject bullet;
    public Transform bulletPos;
    public GameManager gameManager;
    public GameObject BulletPooler;
    private bool isFiring= false;
    private ObjectPooler bulletPooler;
    void Start()
    {
        bulletPooler = BulletPooler.GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && !isFiring && gameManager.isGameRunning && !gameManager.isProfilerModeActive){
            StartCoroutine(Fire());
        }
        if(gameManager.isProfilerModeActive && !isFiring){
            StartCoroutine(Fire());
        }

    }

    IEnumerator Fire()
    {
        isFiring = true;
        if(gameManager.isQueuePoolActive){
            bullet = bulletPooler.SpawnBulletFromQueuePool(bulletPos.position, bulletPos.rotation);
        }
        else if(gameManager.isStackPoolActive){
            bullet = bulletPooler.SpawnBulletFromStackPool(bulletPos.position, bulletPos.rotation);
        }
        else{
            bullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        }
        bullet.GetComponent<Rigidbody>().velocity = cam.transform.forward * 25;
        bullet.GetComponent<Bullet>().DestroyBullet(bulletPooler);
        float wait = 1-gameManager.fireRate;
        if(gameManager.isWaitPoolActive)
        {
            yield return WaitPool.Wait(wait);
        }
        else{
            yield return new WaitForSeconds(wait);
        }
        isFiring = false;
    }

}
