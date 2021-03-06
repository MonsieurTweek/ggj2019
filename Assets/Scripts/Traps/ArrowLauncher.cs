﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : Trap 
{
    public bool isFiring = false;

   // public bool canFire = true;

    public ArrowController arrow;
    public float timeBetweenShot;
    private float shotCounter;
    public Transform firePoint;

    public Stack<ArrowController> inactiveArrows = new Stack<ArrowController>();
    public List<ArrowController> activeArrows = new List<ArrowController>();
    public int startingArrows;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < startingArrows; i++)
        {
            ArrowController newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation);
            newArrow.transform.SetParent(transform);
            newArrow.gameObject.SetActive(false);
            newArrow.name = this.name+"_Arrow_" + i;
            inactiveArrows.Push(newArrow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive == false)
        {
            return;
        }
        if(isFiring)
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShot;
                Shoot();
            }
        } else
        {
            shotCounter = 0;
        }
    }

    public void Shoot() {
        
            ArrowController newArrow = ActiveArrow();
            
            newArrow.transform.position = firePoint.position;
            newArrow.transform.rotation = firePoint.rotation;
            isFiring = false;
    }

    public ArrowController ActiveArrow() {
        ArrowController newArrow = null;
        if(inactiveArrows.Count > 0)
        {
            newArrow = inactiveArrows.Pop();
            newArrow.gameObject.SetActive(true);
            newArrow.transform.position = firePoint.position;
            newArrow.transform.rotation = firePoint.rotation;
            activeArrows.Add(newArrow);
        } else
        {
            newArrow = Instantiate(arrow, firePoint.position, firePoint.rotation);
            newArrow.transform.SetParent(transform);
            newArrow.name = this.name + "_Arrow_" + activeArrows.Count;
            activeArrows.Add(newArrow);
        }

        return newArrow;
    }

    public void DisableBullet(ArrowController arrow) {
        activeArrows.Remove(arrow);
        inactiveArrows.Push(arrow);
        arrow.gameObject.SetActive(false);
    }

    public override void ActiveTrapFromTrigger() {
        isFiring = false;
        _isActive = false;
    }

    public override void ActiveTrapFromPlayer() {

        if(_isActive == true)
        {
            isFiring = true;
        }
    }
}
