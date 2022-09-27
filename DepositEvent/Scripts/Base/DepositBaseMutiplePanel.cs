using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using HeavyDutyInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class DepositBaseMutiplePanel<T> : MonoBehaviour, IComDeposit
{

    protected BasicRegister<DepositEventType, T> UIRegister;
    public void Show(DepositEventType type)
    {
      
        PageType = type;
        RegisterPanel();
        CreateTargetPage();
        InitialBannerInfo();
        InitialExtraInfo();
    }

    protected abstract void RegisterPanel();
    protected abstract void InitialExtraInfo();
    protected abstract void InitialBannerInfo();

    protected abstract void CreateTargetPage();

    protected T IPage
    {
        get;
        set;
    }
    

    public void Destroy()
    {
        Destroy(gameObject);
    }
   

    public DepositEventType PageType { get; set; }

    public virtual DepositUIStyle UIStyle { get { throw new NotImplementedException("活動共用UI - 需實做Page類型"); } }

    protected DepositEventUIData DepositUIData { get { return ((DepositEventUIData)PBDatas.UIDatas.UI(SID.DepositEventController)); } }
}
