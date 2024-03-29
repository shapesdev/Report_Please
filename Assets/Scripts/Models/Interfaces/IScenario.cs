﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScenario
{
    ReportType GetReportType();
    Tester GetTester();
    Discrepancy GetDiscrepancy();
    int GetCaseID();
    string GetTitle();
    bool IsEmployeeIdMissing();
}
