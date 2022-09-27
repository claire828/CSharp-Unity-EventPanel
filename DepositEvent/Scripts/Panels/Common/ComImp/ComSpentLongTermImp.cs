using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using Common.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通累積消費資料頁面
/// </summary>
public class ComSpentLongTermImp : ComSpentDailyImp
{
    public override DepositEventType DepositUIType { get { return DepositEventType.SpendWeekly; } }

    public override bool IsDaily { get { return false; } }

    #region Banner Area
    /// <summary>
    /// 累積消費 字串
    /// </summary>
    public override string BannerTittle { get { return PBSettingsV3.String[22004].Msg; } }

    #endregion

    #region 中間橫槓區域
    /// <summary>
    /// 累積儲值金額標題 - 字串
    /// </summary>
    public override string AccumulatedTitle { get { return PBSettingsV3.String[22020].Msg; } }
  

    #endregion


    public override string ScrollItemTitle { get { return PBSettingsV3.String[22019].Msg; } }

}
