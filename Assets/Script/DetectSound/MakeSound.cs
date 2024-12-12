using System.Collections;
using UnityEngine;

public class MakeSound
{
    public MakeSound(MonoBehaviour monoBehaviour)
    {
        Initialize(monoBehaviour);
    }

    private MonoBehaviour _monobehaviour;
    private Coroutine _soundCoroutine;
    private WaitForSeconds _intervalTime;

    private float _soundRadius;
    private float _soundIntervalTime;
    private LayerMask _targetLayer;

    private void Initialize(MonoBehaviour monoBehaviour)
    {
        _monobehaviour = monoBehaviour;
        
        _soundIntervalTime = 1f;
        _targetLayer = LayerMask.GetMask("InvadeObject");
        _intervalTime = new WaitForSeconds(_soundIntervalTime);
    }

    public void StartSound(bool isSprint, float soundRadius)
    {
        if (isSprint)
        {
            _soundRadius = soundRadius;

            _soundCoroutine = _monobehaviour.StartCoroutine(MakeSoundCoroutine());
        }
        else
        {
            if(_soundCoroutine != null)
            {
                _monobehaviour.StopCoroutine(_soundCoroutine);

                _soundCoroutine = null; 
            }
        }
    }

    private IEnumerator MakeSoundCoroutine()
    {
        
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(_monobehaviour.transform.position, _soundRadius, _targetLayer);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    ISoundTrace soundTrace = collider.GetComponent<ISoundTrace>();

                    if (soundTrace != null)
                    {
                        soundTrace.OnHearSound(_monobehaviour.transform.position, _monobehaviour.transform);
                    }
                }
            }

            yield return _intervalTime;
        }
    }
}
