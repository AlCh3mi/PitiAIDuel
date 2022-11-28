using System;
using System.Collections;
using System.Collections.Generic;
using Mothership;
using UnityEngine;

public class DestroyMotherShipQuest : MonoBehaviour
{
    [SerializeField] private List<string> prompts;
    [SerializeField] private Turret mothership;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        TextPrompt.Instance.AddPrompt("Find and Destroy the Mother Ship!");
        FindObjectOfType<Compass>().SetTarget(mothership.transform);

        foreach (var prompt in prompts)
        {
            TextPrompt.Instance.AddPrompt(prompt);
        }
    }
}