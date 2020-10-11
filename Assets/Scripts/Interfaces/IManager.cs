﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManager 
{
    void Initialize();
    void ManagerUpdate();
    void FixedManagerUpdate();
}