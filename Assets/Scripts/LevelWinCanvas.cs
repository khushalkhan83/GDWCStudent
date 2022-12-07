using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWinCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image screenFadeImage;
    [SerializeField] GameObject contents;
    int totalPickups;

    private void Awake()
    {
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<LevelWinCanvas>().Length;
        if (gameSessionCount > 1)
        {

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        totalPickups = GameObject.FindObjectsOfType<Pickup>().Length;
    }

    void UpdateScoreText()
    {
        scoreText.text = FindObjectOfType<Score>().GetScore().ToString("00") + "/" + totalPickups.ToString("00");
    }

    public void NextLevelButton()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneIndex + 1);
        SceneManager.LoadScene(sceneIndex + 1);
        StartCoroutine(FadeIn(2f));
        totalPickups = GameObject.FindObjectsOfType<Pickup>().Length;

    }

    public IEnumerator FadeIn(float durationInSeconds)
    {
        contents.SetActive(false);

        for (float i = durationInSeconds; i > 0; i -= 0.01f)
        {
            screenFadeImage.color = new Color(screenFadeImage.color.r, screenFadeImage.color.g, screenFadeImage.color.b, i / durationInSeconds);
            yield return new WaitForSeconds(0.01f);
        }

    }

    public IEnumerator FadeOut(float durationInSeconds)
    {
        for (float i = 0; i < durationInSeconds; i += 0.01f)
        {
            screenFadeImage.color = new Color(screenFadeImage.color.r, screenFadeImage.color.g, screenFadeImage.color.b, i / durationInSeconds);
            yield return new WaitForSeconds(0.01f);
        }
        contents.SetActive(true);
        UpdateScoreText();
    }


}
