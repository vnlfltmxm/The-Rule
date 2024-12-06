using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindInputAction
{
    private InputActionReference currentAction = null;
    private TMP_Text bindingDisplayNameText = null;
    private GameObject selectedMarkObject;
    private InputBinding.DisplayStringOptions displayStringOptions;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    // 리바인딩 시작하는 함수. 각 버튼에 OnClick함수로 넣어줄 함수.
    public void StartRebinding()
    {
        currentAction.action.Disable();

        selectedMarkObject.SetActive(true);

        rebindingOperation = currentAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/rightButton")
            .WithCancelingThrough("<Mouse>/leftButton")
            .OnCancel(operation => RebindCancel())
            .OnComplete(operation => RebindComplete())
            .Start();
    }

    // 리바인딩이 취소됐을 때 실행될 함수
    private void RebindCancel()
    {
        rebindingOperation.Dispose();
        currentAction.action.Enable();
        selectedMarkObject.SetActive(false);
    }

    // 리바인딩이 완료됐을 때 실행될 함수
    private void RebindComplete()
    {
        selectedMarkObject.SetActive(false);
        rebindingOperation.Dispose();
        currentAction.action.Enable();
        ShowBindText();
    }

    // 바인딩된 입력 키를 버튼에 보여줄 함수
    public void ShowBindText()
    {
        var displayString = string.Empty;
        var deviceLayoutName = default(string);
        var controlPath = default(string);

        displayString =
            currentAction.action.GetBindingDisplayString(0, out deviceLayoutName, out controlPath,
                displayStringOptions);

        bindingDisplayNameText.text = displayString;
    }
}