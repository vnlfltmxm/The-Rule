using UnityEngine;

public class InteractUI : UIBase
{
    public void SetActiveUI(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
