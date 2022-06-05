using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    Queue<GameObject> bulletQueuePool;
    Stack<GameObject> bulletStackPool;
    public GameObject bulletPrefab;
    public int bulletPoolSize =1000;
    // Start is called before the first frame update
    void Start()
    {
        bulletQueuePool = new Queue<GameObject>();
        bulletStackPool = new Stack<GameObject>();
        for(int i=0 ;i<bulletPoolSize;i++){
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().isCreatedByQueuePool = true;
            bullet.SetActive(false);
            bulletQueuePool.Enqueue(bullet);
        }
        for(int i=0 ;i<bulletPoolSize;i++){
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.GetComponent<Bullet>().isCreatedByStackPool = true;
            bullet.SetActive(false);
            bulletStackPool.Push(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SpawnBulletFromQueuePool(Vector3 position, Quaternion rotation){
        GameObject spawnedBullet = bulletQueuePool.Dequeue();
        spawnedBullet.transform.position =position;
        spawnedBullet.transform.rotation = rotation;
        spawnedBullet.SetActive(true);
        bulletQueuePool.Enqueue(spawnedBullet);
        return spawnedBullet;
    }

    public GameObject SpawnBulletFromStackPool(Vector3 position, Quaternion rotation){
        GameObject spawnedBullet = bulletStackPool.Pop();
        spawnedBullet.transform.position =position;
        spawnedBullet.transform.rotation = rotation;
        spawnedBullet.SetActive(true);
        return spawnedBullet;
    }

    public void PushBulletToStack(GameObject bullet){
        bulletStackPool.Push(bullet);

    }
}
