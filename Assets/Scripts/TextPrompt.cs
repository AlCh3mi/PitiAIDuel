using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float characterTiming = 0.1f;
    [SerializeField] private float lingerDuration = 2f;
    
    public static TextPrompt Instance;
    
    private Queue<string> promptQ = new ();

    private Coroutine displayMsgRoutine;

    public void AddPrompt(string msg)
    {
        if (displayMsgRoutine == null)
            displayMsgRoutine = StartCoroutine(DisplayMessage(msg));
        else
            promptQ.Enqueue(msg);
    }

    private IEnumerator DisplayMessage(string msg)
    {
        var tmpString = string.Empty;
        var waitForSeconds = new WaitForSeconds(characterTiming);

        foreach (var character in msg)
        {
            tmpString += character;
            text.text = tmpString;
            //play sound for each character
            yield return waitForSeconds;
        }

        yield return new WaitForSeconds(lingerDuration);
        text.text = string.Empty;

        if (promptQ.TryDequeue(out var prompt))
            displayMsgRoutine = StartCoroutine(DisplayMessage(prompt));
        else
            displayMsgRoutine = null;
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        
        Destroy(gameObject);
    }

    private void Start()
    {
        text.text = string.Empty;
    }

    [ContextMenu("TestMessage")]
    public void Test()
    {
        var testCases = new[]
        {
            "This is a test Message1",
            "This is a test Message2",
            "This is a test Message3",
            "This is a test Message4"
        };

        foreach (var testCase in testCases)
            Instance.AddPrompt(testCase);
    }
}