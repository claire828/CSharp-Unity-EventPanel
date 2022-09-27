using UnityEngine;
using System.Collections;
using Assets.Common.Scripts.Stage;
using System;
using Common.Settings;
using System.Collections.Generic;
using Common.MaterialItem;
using Assets.Common.Scripts.Datas.UIData;
using HeavyDutyInspector;
using Catan.UI;
using Common.Resource;
using Common.Data;
using Engine.Net;
using UnityEngine.UI;
using UpdateUserData.Event;
using Tip;
using UniLinq;
using Common.Audio;
using Engine.AudioV2;


/// 儲值活動
public class DepositEventController : BaseUIStage
{

    [Comment("主按扭-無限捲軸", CommentType.Info)]
    public ScrollRectV2 MenuScroll;
    [Comment("主按扭Prefabs", CommentType.Info)]
    public GameObject[] MenuButton;
    [Comment("主按扭TabGroup", CommentType.Info)]
    public TabButtonGroup BtnGroup;
    [Comment("放主UI的Panel", CommentType.Info)]
    public GameObject PanelContainer;

    /// 包裝過的Btn, 有後續功能產出再續包裝,動態產出對應資訊按鈕
    private DepositEventBtn[] MenuTabButton;

  
    /// Initial only run once
    private void Initial()
    {
        //先設定目前的頁面類型
        this.DepositEventData.SetCurrPage(this.DepositEventData.ShowTypeAtFirst);
        this.DepositEventData.ShowTypeAtFirst = DepositEventType.None;
        CoroutineV2.StartCoroutine(RequireDepositEventInfo());
    }

   

    #region 主邏輯

    private BasicRegister<DepositUIStyle, IComDeposit> UIRegister;
    private void RegisterPanel()
    {
        UIRegister = new BasicRegister<DepositUIStyle, IComDeposit>();
        UIRegister.RegisterType(DepositUIStyle.Friend, typeof(FriendInvitedPanel));
        UIRegister.RegisterType(DepositUIStyle.TopUp, typeof(TopUpBeginnerPanel));
        UIRegister.RegisterType(DepositUIStyle.Common, typeof(ComDepositPanel));
    }


    /// 每次新增UI總類,要在這邊增加篩選類型, 建出對應模組
    private DepositUIStyle GetUIStyleByPageType(DepositEventType page)
    {
        switch (page)
        {
            case DepositEventType.FriendInvited:
                return DepositUIStyle.Friend;

            case DepositEventType.TopUp:
            case DepositEventType.TopUpSP:
                return DepositUIStyle.TopUp;

            case DepositEventType.Spend:
            case DepositEventType.SpendWeekly:
            case DepositEventType.Purchase:
            case DepositEventType.PurchaseWeekly:
            case DepositEventType.RankTask:
            case DepositEventType.SevenDayLoginTask:
            case DepositEventType.ContinuousLogin:
            case DepositEventType.OldPlayerContinuousLogin:
            case DepositEventType.ActSeaCollection:
                return DepositUIStyle.Common;

            case DepositEventType.Exchange:
                return DepositUIStyle.Exchange;

            default:
                throw new NotSupportedException("IDepositEven 系統類型尚未定義UI Style!!"); 
        }
    }
    private IComDeposit SubUI { get { return PanelContainer.transform.GetComponentInChildren<IComDeposit>(); } }
    private DepositEventType CurrentPage { get { return DepositEventData.CurrentPage; } }
    public DepositEventUIData DepositEventData { get { return (DepositEventUIData)UIData; } }

    #endregion

    

    #region 主選單目錄

    /// 目前的選單數量
    public void SetItem(int index, GameObject go)
    {
        //假如按鈕列表都建立過, 就跳過
        if (MenuTabButton[index] != null) return;
        //動態建立切頁主按鈕
        int nTypeBySort = (int)this.DepositEventData.GetTypeBySortID(index);
        var btn = go.ExAddChild(MenuButton[nTypeBySort-1]);
        //將建立的按鈕收藏起來做管理 並 加入按鈕點擊CallBack
        MenuTabButton[index] = new DepositEventBtn(btn, (DepositEventType)nTypeBySort, BtnGroup, MenuBtnName + nTypeBySort);

        int sortID = DepositEventData.GetSortIDByType(DepositEventData.CurrentPage);
        btn.GetComponent<TabButton>().IsOnWithoutDispatchEvent = index == sortID;
    
    }

    private readonly string MenuBtnName = "Btn";
    #endregion


    #region stage基礎設定

    public override void AwakeStage()
    {
        //建立版子
        UITitle.CreateTitle(gameObject, PBSettingsV3.String[22001].Msg, this.OnReturn);
        Initial();
        DepositEventUIData.ShowNotice.Value = false;
    }

    public override SID NowSID { get { return SID.DepositEventController; } }
    public Action OnReturn
    {
        get
        {
            return () =>
            {
                CoroutineV2.StartCoroutine(MainMenuController.ShowTheme(SID.MainMenuController));

            };
        }
    }

    #endregion

}




