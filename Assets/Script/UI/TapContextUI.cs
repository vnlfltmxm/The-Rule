using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TapContextUI : MonoBehaviour
{
    [SerializeField] private List<TapContext> _tapContexts = new List<TapContext>();
    public event Action<TapContext> OnClickTapEvent;

    private void Awake()
    {
        foreach (var tapContext in _tapContexts)
        {
            tapContext.AddEventListener(() => OnClickTap(tapContext));
        }
    }
    private void OnClickTap(TapContext clickedContext)
    {
        foreach (var tapContext in _tapContexts)
        {
            if (tapContext == clickedContext)
            {
                tapContext.SetActiveContext(true); // 클릭된 버튼의 GameObject 활성화
            }
            else
            {
                tapContext.SetActiveContext(false); // 다른 버튼의 GameObject 비활성화
            }
        }

        OnClickTapEvent?.Invoke(clickedContext);
    }
}
[Serializable]
public class TapContext
{
    [SerializeField] private Button _tap;
    [SerializeField] private GameObject _context;

    public void AddEventListener(UnityAction onClickAction)
    {
        _tap.onClick.AddListener(onClickAction);
    }
    public void SetActiveContext(bool isActive)
    {
        if (_context != null)
        {
            _context.SetActive(isActive);
        }
    }
}
