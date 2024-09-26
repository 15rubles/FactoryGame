using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{

    private float timeForAppear = 2.5f;
    private float timer = 0;
    private float fadeSpeed = 0.2f;
    [SerializeField] private bool fadingOutStarted = false;
    [SerializeField] private bool fadingInStarted = false;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private TMP_Text tutorialTextShadow;

    void OnEnable()
    {
        fadingOutStarted = false;
        fadingInStarted = false;
        tutorialText.gameObject.SetActive(true);
        tutorialTextShadow.gameObject.SetActive(true);
        Color color = tutorialText.color;
        Color shadowColor = tutorialTextShadow.color;
        tutorialText.color = new Color(color.r, color.g, color.b, 1);
        tutorialTextShadow.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, 1);
    }

    void OnDisable()
    {
        tutorialText.gameObject.SetActive(false);
        tutorialTextShadow.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                timer = 0;
                fadingInStarted = false;
                if (!fadingOutStarted)
                {
                    fadingOutStarted = true;
                    FadeOutText(fadeSpeed);
                }
            }
        }

        if (timer >= timeForAppear)
        {

            fadingOutStarted = false;
            if (!fadingInStarted)
            {
                fadingInStarted = true;
                FadeInText(fadeSpeed);
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public void FadeInText(float duration)
    {
        StartCoroutine(FadeTextToFullAlpha(duration, tutorialText, tutorialTextShadow));
    }

    // Fade out the text
    public void FadeOutText(float duration)
    {
        StartCoroutine(FadeTextToZeroAlpha(duration, tutorialText, tutorialTextShadow));
    }

    private IEnumerator FadeTextToFullAlpha(float duration, TMP_Text text, TMP_Text shadow)
    {
        Color color = text.color;
        Color shadowColor = shadow.color;
        if (color.a != 1)
        {
            for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
            {
                text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
                shadow.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, Mathf.Lerp(0, 1, t));
                yield return null;
            }
            text.color = new Color(color.r, color.g, color.b, 1); // Ensure full opacity at the end
            shadow.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, 1);
        }
    }

    private IEnumerator FadeTextToZeroAlpha(float duration, TMP_Text text, TMP_Text shadow)
    {
        Color color = text.color;
        Color shadowColor = shadow.color;
        if (color.a != 0)
        {
            for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
            {
                text.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1, 0, t));
                shadow.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, Mathf.Lerp(1, 0, t));
                yield return null;
            }
            text.color = new Color(color.r, color.g, color.b, 0); // Ensure full transparency at the end
            shadow.color = new Color(shadowColor.r, shadowColor.g, shadowColor.b, 0);

            fadingOutStarted = false;
        }
    }
}
