using System;
using System.Collections;
using UnityEngine;

public class LaserModel
{
    public event Action OnFire;
    public event Action OnChargeEnd;

    public float MaxCharge => maxCharge;
    public float CurrentCharge => currentCharge;
    public float TimeLeft => Mathf.Clamp(regenerateTimeDelay - Time.time + lastFireTime, 0, regenerateTimeDelay);

    private readonly float maxCharge;
    private readonly float decreaseChargeSpeed;
    private readonly float increaseChargeSpeed;
    private readonly float regenerateTimeDelay;

    private float currentCharge;
    private float lastFireTime;

    private Coroutine increaseChargeRoutine;
    public LaserModel(float maxCharge, float decreaseChargeSpeed, float increaseChargeSpeed, float regenerateTimeDelay)
    {
        this.maxCharge = maxCharge;
        this.decreaseChargeSpeed = decreaseChargeSpeed;
        this.increaseChargeSpeed = increaseChargeSpeed;
        this.regenerateTimeDelay = regenerateTimeDelay;

        this.currentCharge = maxCharge;

        CoroutineManager.Instance.StartRoutine(RegenerationRoutine());
    }
    public void Fire()
    {
        if (currentCharge == 0f)
        {
            OnChargeEnd?.Invoke();
            return;
        }
        if (increaseChargeRoutine != null)
        {
            Debug.Log("Stop increase routine");
            CoroutineManager.Instance.StopRoutine(increaseChargeRoutine);
            increaseChargeRoutine = null;
        }

        currentCharge = Mathf.Max(0f, currentCharge - decreaseChargeSpeed * Time.deltaTime);
        lastFireTime = Time.time;
        OnFire?.Invoke();
    }
    private IEnumerator IncreaseChargeRoutine()
    {
        while(true)
        {
            currentCharge = Mathf.Min(maxCharge, currentCharge + increaseChargeSpeed * Time.deltaTime);
            if (currentCharge >= maxCharge)
                yield break;
            yield return null;
        }
    }
    private IEnumerator RegenerationRoutine()
    {
        while(true)
        {
            if (CheckRegenerateTimeDelay() && increaseChargeRoutine == null && currentCharge < maxCharge)
            {
                Debug.Log("Start increase routine");
                increaseChargeRoutine = CoroutineManager.Instance.StartRoutine(IncreaseChargeRoutine());
            }

            yield return null;
        }
    }
    private bool CheckRegenerateTimeDelay()
    {
        return (Time.time - lastFireTime) > regenerateTimeDelay ? true : false;
    }
}
