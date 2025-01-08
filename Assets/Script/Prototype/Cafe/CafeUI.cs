using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CafeUI : MonoBehaviour
{
    [Header("MenuUI")]
    [SerializeField] private GameObject _menuUI;

    [Header("ClerkDialog")]
    [SerializeField] ClerkDialog _clerkDialog;

    public void OnActiveMenuUI()
    {
        StartCoroutine(OnMenuUI());
    }

    private IEnumerator OnMenuUI()
    {
        var cinemachineBrain = Camera.main.gameObject.GetComponent<CinemachineBrain>();

        if (cinemachineBrain != null)
        {
            float blendingTime = cinemachineBrain.DefaultBlend.BlendTime;
            
            yield return new WaitForSeconds(blendingTime);
        }

        _clerkDialog.gameObject.SetActive(true);

        yield return StartCoroutine(_clerkDialog.StartDialog());
    }
}
