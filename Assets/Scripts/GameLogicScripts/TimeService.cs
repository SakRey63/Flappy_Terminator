using System;
using UnityEngine;

public class TimeService : MonoBehaviour
{
    public event Action ReturnableToPool;
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            ReturnableToPool?.Invoke();
        }
    }
}