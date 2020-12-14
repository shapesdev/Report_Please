﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BasicRuleDisplayer : MonoBehaviour
{
    public TMP_Text[] rules;

    public event EventHandler<PageClosedEventArgs> OnPageBack = (sender, e) => { };

    public void DisplayRules(List<string> rulesList)
    {
        int lastIteration = 0;

        for (int i = 0; i < rulesList.Count; i++)
        {
            rules[i].text = rulesList[i];
            lastIteration = i;
        }
        for(int i = lastIteration + 1; i < rules.Length; i++)
        {
            rules[i].text = "";
        }

        gameObject.SetActive(true);
    }

    public void GoBackToHomePage()
    {
        var eventArgs = new PageClosedEventArgs();
        OnPageBack(this, eventArgs);
    }

    public void TurnOnInspectorMode()
    {
        foreach (var rule in rules)
        {
            rule.raycastTarget = true;
            rule.color = ColorHelper.instance.InspectorModeColor;
        }
    }

    public void TurnOffInspectorMode()
    {
        foreach(var rule in rules)
        {
            rule.raycastTarget = false;
            rule.color = ColorHelper.instance.NormalModeColor;
        }
    }
}
