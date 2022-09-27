using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using Common.Settings;
using HeavyDutyInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
///儲值累積 & 累積消費 (週/日) & 等級報酬  & 七日登  (7個UI共用Data Class)
/// </summary>
public class ComDepositPanel : DepositBaseMutiplePanel<IDepositEventPage>
{
   
    [Comment("無限捲軸", CommentType.Info)]
    public ScrollRectV2 ScrollRect;
    [Comment("BannerFixedTitle 從上到下:4", CommentType.Info)]
    public Text[] FixedTitles;
    [Comment("BannerDynamicInfo 從上到下:2", CommentType.Info)]
    public Text[] DynamicMsg;
    [Comment("BannerEventIcon 從上到下:2", CommentType.Info)]
    public RawImage[] ActSeaEventIcon;
    [Comment("Banner", CommentType.Info)]
    public RawImage Banner;
     [Comment("前往按鈕", CommentType.Info)]
    public ButtonEx2 GoBtn;

    public GameObject MiddleInfoObj;
    public Text MiddleInfoTitle;
    public Text MiddleInfoTxt;

   

    #region LoadUI
  
    public ComDepositPanel(DepositEventType type, GameObject panelContainer)
    {
        GameObject resObj = new GameObject();
        resObj = Resources.Load("DepositEvent/Prefab/ComPanel", typeof(GameObject)) as GameObject;
        panelContainer.ExAddChildAsync(resObj, go =>{
                ComDepositPanel panel = go.transform.GetComponent<ComDepositPanel>();
                panel.Show(type);
            }
        );
    }


    protected override void RegisterPanel()
    {
        
        UIRegister = new BasicRegister<DepositEventType, IDepositEventPage>();
        UIRegister.RegisterType(DepositEventType.Purchase, typeof(ComPurchaseDailyImp));
        UIRegister.RegisterType(DepositEventType.PurchaseWeekly, typeof(ComPurchaseLongTermImp));
        UIRegister.RegisterType(DepositEventType.Spend, typeof(ComSpentDailyImp));
        UIRegister.RegisterType(DepositEventType.SpendWeekly, typeof(ComSpentLongTermImp));
        UIRegister.RegisterType(DepositEventType.RankTask, typeof(ComRankTaskImp));
        UIRegister.RegisterType(DepositEventType.SevenDayLoginTask, typeof(ComSevenDayImp));
    }

    #endregion

    #region Banner
    protected override void InitialBannerInfo()
    {
        Banner.ExSetTextureFromBundle(IPage.ImgURL, 1);
        Common.Resource.PBResource.SetResourceImg(ActSeaEventIcon[1], ResourceItemType.TOKEN, IPage.ActSeaEventIconID);
        
        BannerDepositStruct bannerStruct = new BannerDepositStruct() {
            IPage = IPage,
            FixedTitles = FixedTitles,
            DynamicMsg = DynamicMsg,
            MiddleInfoTitle = MiddleInfoTitle,
            MiddleInfoTxt = MiddleInfoTxt,
            MiddleInfoObj = MiddleInfoObj,
            GoBtn = GoBtn,
            ScrollObj = ScrollRect.gameObject,
         };
        SubComBannerImp BannerProcess = new SubComBannerImp();
        BannerProcess.Execute(bannerStruct);
    }

    #endregion



    public void SetItem(int index, GameObject go)
    {
        ComDepositStruct item = IPage.FeatureData.AchievementData[index];
        ComDepositItem raw = go.GetComponent<ComDepositItem>();
        if (IPage.DepositUIType == DepositEventType.SevenDayLoginTask){
            string whichDay = (index+1).ToString();
            string achieveDay = "(" + IPage.FeatureData.Total.ToString() + "/" + item.Target + ")";
            raw.SetItem(item, IPage.ScrollItemTitle.ExCombineString(whichDay, achieveDay));
        } else
        {
            raw.SetItem(item, IPage.ScrollItemTitle.ExCombineString(item.Target));
        }    
    }

   
    protected override void CreateTargetPage(){
        IPage = this.UIRegister.Resolve(PageType);
    }


   public override DepositUIStyle UIStyle { get { return DepositUIStyle.Common; } }



   public void OnGoClick()
   {
       CoroutineV2.StartCoroutine(MarketDepositController.Show(SID.DepositEventController));

   }




}
