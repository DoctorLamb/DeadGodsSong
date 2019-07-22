using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform firePoint;
    public Rigidbody2D bullet;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        //Fire a bullet prefab from the fire point in the right direction
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
