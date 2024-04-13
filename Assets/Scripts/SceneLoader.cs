using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator blackScreenAnim;
    private AsyncOperation loadingSceneOperation;
    private bool isLoading;

    public void StartSceneLoading(string scene)
    {
        if (!isLoading)
        {
            isLoading = true;
            blackScreenAnim.SetTrigger("FadeIn");
            loadingSceneOperation = SceneManager.LoadSceneAsync(scene);
            loadingSceneOperation.allowSceneActivation = false;
        }
    }

    public void FadeInEnd()
    {
        loadingSceneOperation.allowSceneActivation = true;
    }
}
