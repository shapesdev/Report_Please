﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IManager
{
    private DataLoadManager loadManager;
    private Tuple<List<AreasNGrabbags>, Dictionary<int, List<IScenario>>> scenarioData;

    public GraphicManager graphicManager;

    private Card currentCard;
    private float currentPanelWidth;

    public void Initialize()
    {
        loadManager = new DataLoadManager();
        scenarioData = loadManager.GetAreaAndDayData();

        graphicManager.OnDragRight += GraphicManager_OnDragRight;
    }

    private void GraphicManager_OnDragRight(object sender, DragRightEventArgs e)
    {
        currentCard = e.card;
        currentPanelWidth = e.panelWidth;

        currentCard.Check(currentPanelWidth, scenarioData.Item2[10][0].GetReportType());
    }

    public void FixedManagerUpdate()
    {

    }

    public void ManagerUpdate()
    {

    }
}
