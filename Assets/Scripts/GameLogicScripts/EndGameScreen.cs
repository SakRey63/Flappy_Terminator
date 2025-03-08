using System;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;
    
    public void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.image.raycastTarget = false;
        ActionButton.interactable = false;
    }

    public  void Open()
    {
        WindowGroup.alpha = 1f;
        ActionButton.image.raycastTarget = true;
        ActionButton.interactable = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}