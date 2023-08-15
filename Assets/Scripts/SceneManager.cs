using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public Transform[] rooms;
    public GameObject playerOrigin;
    public Camera mainCamera;

    [SerializeField] Canvas fadingCanvas;
    [SerializeField] float fadeInDuration = 3;
    UnityEngine.UI.Image fadingImage;

    protected virtual void Awake()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        if (!fadingCanvas)
        {
            fadingCanvas = transform.GetComponentInChildren<Canvas>();
            fadingCanvas.worldCamera = mainCamera;
        }
        if (!fadingImage)
        {
            fadingImage = fadingCanvas.GetComponentInChildren<UnityEngine.UI.Image>();
        }

        SetFadeColorAlpha(fadingImage, 1);
        StartCoroutine(Fade(false, fadeInDuration));
    }

    protected IEnumerator Fade(bool _toBlack, float _fadeDuration)
    {
        int multiplier = _toBlack ? 1 : -1;
        float fadingValue = _toBlack ? 0 : 1;
        float fadingValuePerFrame = multiplier / (60f * _fadeDuration);

        for (int i = 0; i < (_fadeDuration * 60f); i++)
        {
            fadingValue += fadingValuePerFrame;
            SetFadeColorAlpha(fadingImage, fadingValue);
            yield return new WaitForFixedUpdate();
        }
        SetFadeColorAlpha(fadingImage, _toBlack ? 1 : 0);
    }

    protected void SetFadeColorAlpha(UnityEngine.UI.Image _image, float _alphaValue)
    {
        Color fadeColor = _image.color;
        fadeColor.a = _alphaValue;
        _image.color = fadeColor;
    }

}
