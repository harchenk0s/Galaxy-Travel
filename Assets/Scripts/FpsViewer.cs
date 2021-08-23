using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FpsCounter))]
public class FpsViewer : MonoBehaviour
{

    [SerializeField] private Text _label = null;
    private FpsCounter _fpsCounter;

    private void Awake()
    {
        _fpsCounter = GetComponent<FpsCounter>();
    }

    private void Start()
    {
        StartCoroutine(SetFPS());
    }

    private IEnumerator SetFPS()
    {
        while (true)
        {
            _label.text = _fpsCounter.FPS.ToString() + " fps";
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
