using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _skyboxes = new List<Material>();

    public void ChangeToRandom()
    {
        RenderSettings.skybox = _skyboxes[Random.Range(0, _skyboxes.Count - 1)];
        Texture2D texture = (Texture2D)RenderSettings.skybox.GetTexture("_FrontTex");
        RenderSettings.fogColor = texture.GetPixel(texture.width / 2, texture.height / 2);
    }
}
