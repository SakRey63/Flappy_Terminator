using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Quaternion _lockAtTarget = Quaternion.Euler(0, 180 , 0);
    
    public void CreateDirection(float direction)
    {
        if (direction < 0)
        {
            transform.rotation = _lockAtTarget;
        }
    }
}