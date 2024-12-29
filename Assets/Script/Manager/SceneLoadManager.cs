using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoadManager
{
    // 현재 로딩 상태를 저장할 Action (UI 업데이트용)
    public static Action<float> OnLoadingProgress;

    // 씬을 로드하는 메인 함수
    public static async Awaitable LoadSceneAsync(string targetSceneName, string loadingSceneName = "LoadingScene")
    {
        SceneManager.LoadScene(loadingSceneName);
        await LoadTargetScene(targetSceneName);
    }

    private static async Awaitable LoadTargetScene(string targetSceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            OnLoadingProgress?.Invoke(progress);

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }

            await Task.Yield();
        }

        asyncOperation.allowSceneActivation = true;

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }
    }
}