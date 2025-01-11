using System;
using Script.Pawn.Player;
using Script.Prop;
using UnityEngine;

public class PlayerUtilityController : MonoBehaviour
{
    private RaycastPropDetector _raycastPropDetector;

    [SerializeField] private GameObject EyePanel;
    [SerializeField] private AudioListener AudioListener;
    [SerializeField] private GameObject RulePanel;
    
    public void Init(Player player)
    {
        _raycastPropDetector = player.RaycastPropDetector;
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
            InteractiveProp prop = _raycastPropDetector.CurrentDetectedProp;
            if(prop is ItemContainer itemContainer)
            {
                itemContainer.Interact();
                UIManager.Instance.OpenUI<TradeInventoryUI>()
                    .Init(Player.Instance.PlayerData.InventoryData, itemContainer.InventoryData);
            }
            else if(prop is InteractiveDevice device)
            {
                device.Interact();
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
