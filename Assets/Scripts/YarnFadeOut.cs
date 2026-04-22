using UnityEngine;
using System.Collections;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class YarnFadeManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public CanvasGroup fadeCanvas;

    public float fadeDuration = 2f;
    public string sceneToLoad;

    void Awake()
    {
        if (dialogueRunner == null)
        {
            Debug.LogError("DialogueRunner not assigned!");
            return;
        }

        Debug.Log("Registering fade_out command");

        dialogueRunner.AddCommandHandler("fade_out", FadeOut);
    }

    void Start()
    {
        fadeCanvas.alpha = 0f;
        fadeCanvas.blocksRaycasts = false;
    }

    void FadeOut()
    {
        Debug.Log("fade_out command triggered");

        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        fadeCanvas.blocksRaycasts = true;

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = 1f;

        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
    }
}