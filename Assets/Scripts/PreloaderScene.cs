using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreloaderScene : MonoBehaviour
{
    [SerializeField] Slider _loadingSlider;

    private void Start()
    {
        StartCoroutine(AsyncLoadScene());
    }

    private IEnumerator AsyncLoadScene()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(1);
        while (!loadingOperation.isDone)
        {
            _loadingSlider.value = loadingOperation.progress;
            yield return null;
        }
    }
}
