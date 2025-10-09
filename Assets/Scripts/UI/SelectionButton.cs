using UnityEngine;

public class SelectionButton : MenuButton
{
    [SerializeField] protected string selectionValue;
    [SerializeField] protected ShopHUD shopHUD;
    override public void OnClick()
    {
        base.OnClick();
        shopHUD.selectedUseCase = selectionValue;
        shopHUD.UpdateUseCaseHUD();

    }
}
