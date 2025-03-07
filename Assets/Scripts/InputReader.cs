using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action Attack;
    public event Action Force; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Force?.Invoke();
        }
    }
}