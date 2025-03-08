using System;
using Script.Pawn.Player;
using Script.Prop;
using UnityEngine;

public class PlayerUtilityController : MonoBehaviour
{
    private RaycastPropDetector _raycastPropDetector;
    private ItemInventory _itemInventory;

    [SerializeField] private GameObject EyePanel;
    [SerializeField] private AudioListener AudioListener;
    [SerializeField] private GameObject RulePanel;
    
    public void Init(Player player)
    {
        _raycastPropDetector = player.RaycastPropDetector;
        _itemInventory = player.ItemInventory;
        PlayerInputHandler playerInputHandler = player.PlayerInputHandler;

        playerInputHandler.OnInteractEvent = Interact;
        playerInputHandler.OnPrimaryEvent = Primary;
        playerInputHandler.OnSecondaryEvent = Secondary;
        playerInputHandler.OnCheckRulesEvent = CheckRules;
    }

    private void Interact()
    {
        Logger.Log("Interact", Color.red);
        if (_raycastPropDetector.HasDetected == true)
        {
            IInteractable prop = _raycastPropDetector.CurrentDetectedInteractable;
            if(prop is Item item)
            {
                item.Interact();
                _itemInventory.AddItem(item);
            }
            else if(prop is InteractiveDevice device)
            {
                device.Interact();
            }
            else if(prop is MonsterBase monster)
            {
                monster.Interact();
            }
        }
    }
    //귀막기
    private bool primaryActive = false;
    private void Primary()
    {
        primaryActive = !primaryActive;
        AudioListener.volume = primaryActive ? 0f : 1f;
    }
    //눈막기
    private bool secondaryActive = false;
    private void Secondary()
    {
        secondaryActive = !secondaryActive;
        EyePanel.gameObject.SetActive(secondaryActive);
    }
    private bool ruleActive = false;
    private void CheckRules()
    {
        ruleActive = !ruleActive;
        RulePanel.gameObject.SetActive(ruleActive);
    }
}
