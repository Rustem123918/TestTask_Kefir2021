using System;
using System.Collections;
using UnityEngine;

public class LaserModel
{
    public event Action OnFire;
    public event Action OnChargeEnd;
    public event Action OnStopIncreaseChargeRoutine;
    public event Action OnStartIncreaseChargeRoutine;

    public float MaxCharge => maxCharge;
    public float CurrentCharge => currentCharge;
    public float TimeLeft => Mathf.Clamp(regenerateTimeDelay - Time.time + lastFireTime, 0, regenerateTimeDelay);

    private readonly float maxCharge;
    private readonly float decreaseChargeSpeed;
    private readonly float increaseChargeSpeed;
    private readonly float regenerateTimeDelay;

    private float currentCharge;
    private float lastFireTime;

    private bool increaseChargeRoutineIsGoing;
    public LaserModel(float maxCharge, float decreaseChargeSpeed, float increaseChargeSpeed, float regenerateTimeDelay)
    {
        this.maxCharge = maxCharge;
        this.decreaseChargeSpeed = decreaseChargeSpeed;
        this.increaseChargeSpeed = increaseChargeSpeed;
        this.regenerateTimeDelay = regenerateTimeDelay;

        this.currentCharge = maxCharge;

        increaseChargeRoutineIsGoing = false;
    }
    public void Fire()
    {
        if (currentCharge == 0f)
        {
            OnChargeEnd?.Invoke();
            return;
        }
        if (increaseChargeRoutineIsGoing)
        {
            Debug.Log("Stop increase routine");
            increaseChargeRoutineIsGoing = false;
            OnStopIncreaseChargeRoutine?.Invoke();
        }

        currentCharge = Mathf.Max(0f, currentCharge - decreaseChargeSpeed * Time.deltaTime);
        lastFireTime = Time.time;
        OnFire?.Invoke();
    }
    public IEnumerator IncreaseChargeRoutine()
    {
        while(true)
        {
            currentCharge = Mathf.Min(maxCharge, currentCharge + increaseChargeSpeed * Time.deltaTime);
            if (currentCharge >= maxCharge)
                yield break;
            yield return null;
        }
    }
    public IEnumerator RegenerationRoutine()
    {
        while(true)
        {
            if (CheckRegenerateTimeDelay() && !increaseChargeRoutineIsGoing && currentCharge < maxCharge)
            {
                Debug.Log("Start increase routine");
                increaseChargeRoutineIsGoing = true;
                OnStartIncreaseChargeRoutine?.Invoke();
            }

            yield return null;
        }
    }
    private bool CheckRegenerateTimeDelay()
    {
        return (Time.time - lastFireTime) > regenerateTimeDelay ? true : false;
    }
}
