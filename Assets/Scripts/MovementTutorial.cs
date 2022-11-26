using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    [SerializeField] private float promptStartDelay = 3f;
    [SerializeField] private List<string> textPrompts;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(promptStartDelay);
        
        foreach (var textPrompt in textPrompts)
            TextPrompt.Instance.AddPrompt(textPrompt);
    }
}