﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorObject : MonoBehaviour
{
    [SerializeField] private CursorController.CursorType cursorType;

    private void OnMouseEnter()
    {
        CursorController.Instance.SetActiveCursorType(cursorType);
    }

    private void OnMouseExit()
    {
        CursorController.Instance.SetActiveCursorType(CursorController.CursorType.Solid);
    }
}
