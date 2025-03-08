using System;

public class StartScreen : Window
{
    public event Action PlayButtonClicked;
    
    public  void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.image.raycastTarget = false;
        ActionButton.interactable = false;
    }
    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}