using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RepairQuest : MonoBehaviour
{
    [SerializeField] private UnityEvent questCompleted;
    [SerializeField] private TMP_Text activeQuestText;

    private bool questComplete;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        
        TextPrompt.Instance.AddPrompt("Spend mined resources to Repair the Broken Survival Craft");
        TextPrompt.Instance.AddPrompt("Your mining beam can also repair repairable structures");
        TextPrompt.Instance.AddPrompt("BEWARE, whatever destroyed this survival craft may still be nearby");
        TextPrompt.Instance.AddPrompt("Press E to fire short range missiles");
    }

    public void UpdateQuestLog(float current, float max)
    {
        if(questComplete)
            return;
        
        if (current >= max)
        {
            questComplete = true;
            questCompleted?.Invoke();
            Debug.Log("QUEST COMPLETED INVOKED");
            activeQuestText.text = "Quest Completed";
            return;
        }
        
        activeQuestText.text = $"Survival Craft Health: {current}/{max}";
    }
}