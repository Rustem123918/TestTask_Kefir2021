using System;
using UnityEngine;

public class PistolModel : IGun
{
    public event Action OnFire;

    private readonly float fireDelay;

    private float lastFireTime;
    public PistolModel(float fireDelay)
    {
        this.fireDelay = fireDelay;
        this.lastFireTime = 0;
    }
    public void Fire()
    {
        if (!CheckFireDelay())
            return;

        lastFireTime = Time.time;
        OnFire?.Invoke();
    }
    private bool CheckFireDelay()
    {
        return (Time.time - lastFireTime) > fireDelay ? true : false;
    }
}
