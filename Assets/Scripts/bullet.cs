using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Transform firePoint;
    public float bulletForce = 45f;
    // Start is called before the first frame update
    void Start()
    {
        foreach( Transform child in transform.parent)
        {
            firePoint = child;
            break;
        }
        //firePoint = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(firePoint);
        Shot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shot()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Debug.Log(firePoint.up * bulletForce);
        rb.AddForce(firePoint.up * bulletForce);
    }


    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyBullet();
    }
}
