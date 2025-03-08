using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action Attacked;
    public event Action Forced; 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attacked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Forced?.Invoke();
        }
    }
}