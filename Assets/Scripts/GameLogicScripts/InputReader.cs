using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _keyE = KeyCode.E;
    [SerializeField] private KeyCode _keySpace = KeyCode.Space;
    
    public event Action Attacked;
    public event Action Forced; 
    
    private void Update()
    {
        if (Input.GetKeyDown(_keyE))
        {
            Attacked?.Invoke();
        }

        if (Input.GetKeyDown(_keySpace))
        {
            Forced?.Invoke();
        }
    }
}