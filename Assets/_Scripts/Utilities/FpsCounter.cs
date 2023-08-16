using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FpsCounter : MonoBehaviour
{
    private TMP_Text text;

    private bool _isFpsCounterActive = true;

    private List<int> lastFpsData = new List<int>();

    private void Awake()
    {
        for(int i =  0; i < 10; i++)
            lastFpsData.Add(0);

        text = GetComponent<TMP_Text>();

        _isFpsCounterActive = PlayerPrefs.GetInt("IsFpsCounter") == 1;
        text.enabled = _isFpsCounterActive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) ChangeActiveStatus();

        if (!_isFpsCounterActive) return;

        text.text = GetFPS().ToString();
    }

    private void ChangeActiveStatus()
    {
        _isFpsCounterActive = !_isFpsCounterActive;
        text.enabled = _isFpsCounterActive;

        PlayerPrefs.SetInt("IsFpsCounter", _isFpsCounterActive ? 1 : 0);
    }

    private int GetFPS()
    {
        lastFpsData.RemoveAt(0);
        lastFpsData.Add((int)(1f / Time.deltaTime));

        int sum = 0;
        foreach(int i in lastFpsData)
            sum += i;

        return sum / lastFpsData.Count;
    }
}
