﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public abstract class Card : MonoBehaviour
{
    public Vector2 employeePaperRight = new Vector2(400, 250);
    public Vector2 employeePaperLeft = new Vector2(100, 50);

    public Vector2 reportPaperRight = new Vector2(600, 700);
    public Vector2 reportPaperLeft = new Vector2(100, 150);

    private float sizeChangeOffsetRight = 80f;
    private float sizeChangeOffsetLeft = 20f;

    [HideInInspector]
    public bool changeSize = false;

    public abstract void ChangeSizeToRight();
    public abstract void ChangeSizeToLeft();

    public void Check(float panelWidth)
    {
        if (transform.localPosition.x >= -Screen.width / 2 + panelWidth + sizeChangeOffsetRight)
        {
            ChangeSizeToRight();
        }
        else if (transform.localPosition.x <= Screen.width / 2 - panelWidth - sizeChangeOffsetLeft)
        {
            ChangeSizeToLeft();
        }
    }
}
