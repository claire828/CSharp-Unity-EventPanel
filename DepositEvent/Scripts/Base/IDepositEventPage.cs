using Assets.Common.Scripts.Datas.UIData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 累積儲值+累積消費Interface
/// </summary>
public interface IDepositEventPage
{
    DepositEventType DepositUIType { get; }

    SubDepositEventUIType SubDepositUIType { get; }
    bool IsDaily { get; }
     
    #region Banner Area
    /// 累積儲值 將豪華獎勵一網打盡 - 字串
    string BannerTittle { get; }
    /// 重置時間 - Server 
    string ResetTime { get; }
    /// 活動結束日 - server 
    string EndDate { get; }
    /// 獎勵發送到信箱 - 字串
    string StrRewadTip { get; }

  

    string ImgURL { get; }

    /// 潮汐之海Icon ID
    int ActSeaEventIconID { get; }

    #endregion


    #region 中間橫槓區域
    /// 累積儲值金額標題 - 字串
    string AccumulatedTitle { get; }
    /// 累積儲值金額數量
    int AccumulatedAmount { get; }
    /// 座標
    Vector3 AccumulatedObjLocated { get; }
    /// 前往按鈕是否顯示
    bool ShowGoBtn { get; }

    bool ShowMiddleInfo { get; }

    #endregion


    #region Data 

    ComDepositFeatureData FeatureData { get; }
    DepositEventUIData DepositUIData { get; }

    #endregion


    string ScrollItemTitle { get; }

    string ScrollItemTitle_2 { get; }

   




}
