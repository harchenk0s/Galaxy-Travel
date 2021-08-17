using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FpsCounter))]
public class FpsViewer : MonoBehaviour
{
   [SerializeField] private Text _label;
    private FpsCounter _fpsCounter;

    private void Awake()
    {
        _fpsCounter = GetComponent<FpsCounter>();
    }

    private void Start()
    {
        StartCoroutine(SetFPS());
    }

    private void Update()
    {
        //_label.text = Mathf.Clamp(_fpsCounter.FPS, 0, 99).ToString();
        
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
