using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DepositQuest : MonoBehaviour
{
    [SerializeField] private int requiredDeposit;
    [SerializeField] private TMP_Text activeQuestText;

    [Header("Text Prompts")] 
    [SerializeField] private float textPromptsDelay = 3f;
    [SerializeField] private List<string> textPrompts;

    [SerializeField] private UnityEvent objectiveCompleted;
    [SerializeField] private UnityEvent<float, float> progressUpdated;
    
    private float _deposited;
    private bool objectiveCompleteTriggered;

    public float Deposited
    {
        get => _deposited;
        private set
        {
            _deposited = value;
            progressUpdated?.Invoke(value, requiredDeposit);
            
            if(_deposited >= requiredDeposit && !objectiveCompleteTriggered)
            {
                objectiveCompleted?.Invoke();
                objectiveCompleteTriggered = true;
            }
        }
    }
    
    private IEnumerator Start()
    {
        UpdateQuestText();

        yield return new WaitForSeconds(textPromptsDelay);
        
        foreach (var textPrompt in textPrompts)
            TextPrompt.Instance.AddPrompt(textPrompt);
    }

    public void UpdateQuestText() => activeQuestText.text = $"Mine {requiredDeposit - Deposited} Ore. Drop it off at the Depot.";
    
    public void Deposit(float amount)
    {
        Deposited += amount;
    }
}