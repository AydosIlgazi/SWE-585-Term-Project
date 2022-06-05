using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isCreatedByQueuePool= false;
    public bool isCreatedByStackPool= false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyBullet(ObjectPooler objectPooler){
        StartCoroutine(Destroy(objectPooler));
    }
    IEnumerator Destroy(ObjectPooler objectPooler)
    {
        yield return new WaitForSeconds(5);
        if(isCreatedByQueuePool || isCreatedByStackPool){
            this.gameObject.SetActive(false);
            if(isCreatedByStackPool){
                objectPooler.PushBulletToStack(this.gameObject);
            }
        }
        else{
            Destroy(this.gameObject);
        }
    }
}
