using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _skyboxes = new List<Material>();

    public void ChangeToRandom()
    {
        RenderSettings.skybox = _skyboxes[Random.Range(0, _skyboxes.Count - 1)];
    }
}
