using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    [SerializeField]Text startText;
    float blinkSpeed = 0.5f;

    void Start()
    {
        if (startText != null)
        {
            StartCoroutine(BlinkText());
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }
    private IEnumerator BlinkText()
    {
        while (true)
        {
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 0);
            yield return new WaitForSeconds(blinkSpeed);
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, 1);
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
    private void LoadNextScene()
    {
        Manager.instance.RestartGame();
    }
}
