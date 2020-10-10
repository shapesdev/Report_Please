﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Stamp
{
    SIREN, PLUS
}

public class GraphicManager : MonoBehaviour, IManager, IGraphic
{
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEvent;
    EventSystem eventSystem;

    private GameObject selectedGO;

    private GameObject stamp;
    public GameObject stampPanel;

    private bool selected = false;
    private bool offsetSet = false;
    private Vector3 offset;

    [SerializeField]
    private RectTransform leftPanel;

    public event EventHandler<DragRightEventArgs> OnDragRight = (sender, e) => { };

    public void Initialize()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    public void ManagerUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEvent, results);

            if(results.Count > 0 && (results[0].gameObject.tag == "Selectable"))
            {
                BringGraphicToFront(results[0].gameObject);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            selected = false;
            offsetSet = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void FixedManagerUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && selected)
        {
            if (!offsetSet)
            {
                offset = Input.mousePosition - selectedGO.transform.localPosition;
                offsetSet = !offsetSet;
                Cursor.lockState = CursorLockMode.Confined;
            }

            if (Input.mousePosition.y <= Screen.height - 300f && Input.mousePosition.y >= 0f)
            {
#if UNITY_STANDALONE_OSX
    if(Input.mousePosition.x <= Screen.width && Input.mousePosition.x >= 0f)
    {
        selectedGO.transform.localPosition = Input.mousePosition - offset;
    }
#endif

#if UNITY_STANDALONE_WIN
    selectedGO.transform.localPosition = Input.mousePosition - offset;
#endif
            }
            var eventArgs = new DragRightEventArgs(leftPanel.rect.width, selectedGO.GetComponent<Card>());
            OnDragRight(this, eventArgs);
        }
    }

    private void BringGraphicToFront(GameObject go)
    {
        selectedGO = go;
        selectedGO.transform.SetSiblingIndex(transform.childCount - 3);

        selected = true;
    }

    public void StampEffect(Sprite sprite)
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;

        if(StampCanBePlaced(currentButton.transform.position) == true)
        {
            if (stamp == null && selectedGO.transform.childCount > 1)
            {
                stamp = new GameObject("Stamp");
                stamp.AddComponent<Image>();

                var stampImage = stamp.GetComponent<Image>();
                stampImage.sprite = sprite;
                stampImage.raycastTarget = false;

                for (int i = 0; i < selectedGO.transform.childCount; i++)
                {
                    if (selectedGO.transform.GetChild(i).gameObject.activeInHierarchy)
                    {
                        stamp.transform.SetParent(selectedGO.transform.GetChild(i).transform);
                    }
                }

                stamp.transform.position = currentButton.transform.position;
                stamp.transform.localScale = Vector3.one;
                stamp.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            }
        }
    }

    private bool StampCanBePlaced(Vector3 pos)
    {
        if(selectedGO != null)
        {
            for (int i = 0; i < selectedGO.transform.childCount; i++)
            {
                for (int j = 0; j < selectedGO.transform.GetChild(i).transform.childCount; j++)
                {
                    if (selectedGO.transform.GetChild(i).transform.GetChild(j).tag == "StampArea")
                    {
                        var child = selectedGO.transform.GetChild(i).transform.GetChild(j);

                        Vector3[] v = new Vector3[4];
                        child.GetComponent<RectTransform>().GetWorldCorners(v);

                        if (pos.x >= v[0].x && pos.x <= v[3].x && pos.y >= v[0].y && pos.y <= v[1].y)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void ActivateStampPanel()
    {
        // ADD ANIMATION FOR THE STAMP PANEL
        foreach(Transform child in stampPanel.transform)
        {
            if(child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}