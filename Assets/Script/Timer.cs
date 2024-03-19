using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float gameTimeInSeconds = 300f; // 5 �ҷ�
    private float elapsedTime = 0f;
    public Text timerText;

    void Update()
    {
        if (elapsedTime < gameTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimerDisplay()
    {
        float timeLeft = gameTimeInSeconds - elapsedTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        // ���������͵�ͧ��è��� ����� UI ���� ������Ŵ�ҡ���
        Debug.Log("Game Over!");
    }
}
