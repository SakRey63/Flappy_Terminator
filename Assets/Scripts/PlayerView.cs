using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private DisplayCounter _display;

    public void ChangeValue()
    {
        _display.ChangeValue();
    }
}
