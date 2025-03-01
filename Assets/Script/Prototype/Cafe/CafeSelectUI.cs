using UnityEngine;


public class CafeSelectUI : MonoBehaviour
{
    public void OnClickYes()
    {
        CafeRule._isConcentrate = true;

        ProcessPayMent();
    }

    public void OnClickNo()
    {
        ProcessPayMent();
    }

    private void ProcessPayMent()
    {
        CafeManager.Instance.ProcessPayment();

        gameObject.SetActive(false);
    }
}
