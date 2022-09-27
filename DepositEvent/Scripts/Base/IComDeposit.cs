using Assets.Common.Scripts.Datas.UIData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 儲值消費活動共用Interface
public interface IComDeposit
{

    void Show(DepositEventType type);
    void Destroy();

    DepositEventType PageType { get; set; }

    DepositUIStyle UIStyle { get; }

}
