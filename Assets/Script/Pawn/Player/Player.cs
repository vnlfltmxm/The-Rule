using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Pawn
{
    public PlayerBodyCondition PlayerBodyCondition;
    public PlayerController PlayerController;
    public ItemHolder ItemHolder;
    [HideInInspector] public PlayerInput PlayerInput;
    [HideInInspector] public Rigidbody Rigidbody;

    protected override void Init()
    {
        PlayerBodyCondition = new PlayerBodyCondition();
        PlayerController = new PlayerController();
        ItemHolder = new ItemHolder();
        PlayerInput = GetComponent<PlayerInput>();
        
        PlayerBodyCondition.Init();
        PlayerController.Init(PlayerInput);
    }
}
