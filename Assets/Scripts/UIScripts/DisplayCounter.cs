using TMPro;
using UnityEngine;

public class DisplayCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private int _number;
    
    public void ChangeValue()
    {
        _number++;
        
        _text.text = _number.ToString();
    }

    public void Reset()
    {
        _number = 0;
        
        _text.text = _number.ToString();
    }
}