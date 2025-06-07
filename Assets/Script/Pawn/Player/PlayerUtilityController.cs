using System;
using Script.Pawn.Player;
using Script.Prop;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUtilityController : MonoBehaviour
{
    private RaycastInteractableDetector _raycastInteractableDetector;
    private ItemInventory _itemInventory;
    private PlayerInput _playerInput;

    //[SerializeField] private GameObject EyePanel;
    [SerializeField] private AudioListener AudioListener;
    //[SerializeField] private GameObject RulePanel;
    
    //귀막기
    private bool _primaryActive = false;
    //눈막기
    private bool _secondaryActive = false;
    //규칙 출력
    private bool _ruleActive = false;
    
    public void Init(Player player)
    {
        _raycastInteractableDetector = player.RaycastInteractableDetector;
        _itemInventory = player.ItemInventory;
        _playerInput = player.PlayerInput;
        PlayerInputHandler playerInputHandler = player.PlayerInputHandler;

        playerInputHandler.OnInteractEvent = player.Interact;
        playerInputHandler.OnPrimaryEvent = Primary;
        playerInputHandler.OnSecondaryEvent = Secondary;
        playerInputHandler.OnCheckRulesEvent = CheckRules;
    }
    
    private void Primary()
    {
        _primaryActive = !_primaryActive;
        AudioListener.volume = _primaryActive ? 0f : 1f;
    }
    private void Secondary()
    {
        _secondaryActive = !_secondaryActive;
        UIManager.Instance.GetUI<EyeUI>().gameObject.SetActive(_secondaryActive);
    }
    private void CheckRules()
    {
        _ruleActive = !_ruleActive;
        RuleUI ruleUI = UIManager.Instance.GetUI<RuleUI>();
        ruleUI.gameObject.SetActive(_ruleActive);
        if(_ruleActive == true)
            ruleUI.InitRule();
    }
}
