using UnityEngine;
using Script.Prop;

public class BloodMachine : MonoBehaviour, IInteractableBloodMachine
{
    private const float COST = 0.2f;

    public bool IsInteractable { get; set; } = true;

    public void InteractObject(PlayerBodyCondition type)
    {
        if (CafeRule._isBlackTeaConcentrate)
        {
            return;
        }

        var costValue = type.Hp * COST;

        type.OnDamage((int)costValue);

        CafeRule._isBlackTeaConcentrate = true;
    }
}
