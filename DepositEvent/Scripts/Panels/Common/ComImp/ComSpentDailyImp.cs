using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using Common.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每日累積消費資料頁面
/// </summary>
public class ComSpentDailyImp : IDepositEventPage
{
    public virtual DepositEventType DepositUIType { get { return DepositEventType.Spend; } }
    public SubDepositEventUIType SubDepositUIType { get { return SubDepositEventUIType.SubPurchase; } }
    public virtual bool IsDaily { get { return true; } }
    #region Banner Area
    /// <summary>
    /// 累積儲值 將豪華獎勵一網打盡 - 字串
    /// </summary>

    public virtual string BannerTittle { get { return PBSettingsV3.String[22028].Msg; } }
    /// <summary>
    /// 重置時間 - Server 
    /// </summary>
    public string ResetTime { get { return FeatureData.ResetMsg; } }
    /// <summary>
    /// 活動結束日 - server 
    /// </summary>
    public string EndDate { get { return FeatureData.EndMsg; } }
    /// <summary>
    /// 獎勵發送到信箱 - 字串
    /// </summary>
    public string StrRewadTip { get { return PBSettingsV3.String[22002].Msg; } }
    /// <summary>
    /// Banner Pic
    /// </summary>
    /// <param name="pic"></param>
    /// <returns></returns>
    public string ImgURL { get { return string.Format("images/DepositEvent/{0}", (int)DepositEventType.Spend); } }
    ///<summary>
    /// ActSea Icon ID
    /// </summary>
    public int ActSeaEventIconID { get { return 0; } }

    #endregion


    #region 中間橫槓區域
    /// <summary>
    /// 累積儲值金額標題 - 字串
    /// </summary>
    public virtual string AccumulatedTitle { get { return PBSettingsV3.String[22011].Msg; } }
    /// <summary>
    /// 累積儲值金額數量
    /// </summary>
    public int AccumulatedAmount { get { return FeatureData.Total; } }
    /// <summary>
    /// 座標
    /// </summary>
    public Vector3 AccumulatedObjLocated { get { return new Vector3(0f, 75f); } }
    /// <summary>
    /// 前往按鈕是否顯示
    /// </summary>
    public bool ShowGoBtn { get { return false; } }

    public bool ShowMiddleInfo { get { return true; } }
    #endregion


    #region Data

    public ComDepositFeatureData FeatureData
    {
        get
        {
            return DepositUIData.DepositFeatures[DepositUIType];
        }
    }

    public DepositEventUIData DepositUIData { get { return ((DepositEventUIData)PBDatas.UIDatas.UI(SID.DepositEventController)); } }
    #endregion
    public virtual string ScrollItemTitle { get { return PBSettingsV3.String[22018].Msg; } }
    public string ScrollItemTitle_2 { get { return ""; } }





}
