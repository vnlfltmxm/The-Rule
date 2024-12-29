using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName = "Junhyung";
    public void ChangeScene()
    {
        SceneLoadManager.LoadSceneAsync(_sceneName);
    }
}
