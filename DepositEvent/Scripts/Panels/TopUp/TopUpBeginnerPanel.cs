using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using Common.Players;
using HeavyDutyInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopUpBeginnerPanel : DepositBaseMutiplePanel<TopUpStruct>
{

    [Comment("BannerFixedTitle 從上到下:4", CommentType.Info)]
    public Text[] FixedTitles;

    [Comment("Banner", CommentType.Info)]
    public RawImage Banner;

    [Comment("BG", CommentType.Info)]
    public RawImage BG;

    public GameObject RewardContainer;
    public GameObject DetailBtn;

   
    public TopUpBeginnerPanel(DepositEventType type, GameObject panelContainer)
    {
        GameObject resObj = Resources.Load("DepositEvent/Prefab/TopUpBeginnerPanel", typeof(GameObject)) as GameObject;
        panelContainer.ExAddChildAsync(resObj, go =>
        {
            TopUpBeginnerPanel panel = go.transform.GetComponent<TopUpBeginnerPanel>();
            panel.Show(type);
        }
        );
    }



    protected override void RegisterPanel()
    {
      //this panel do not use register method due to imcomplex 
    }

    #region Banner
    protected override void InitialBannerInfo()
    {
        BG.ExSetTextureFromBundle(IPage.MainPictureURL, 1);
        Banner.ExSetTextureFromBundle(IPage.ImgURL, 1);
        FixedTitles[0].text = IPage.BannerTitle;
        FixedTitles[1].text = IPage.Descrip;
        FixedTitles[2].text = IPage.FinishDateTitle;
        FixedTitles[4].text = IPage.SendToMailMsg;
    }

    #endregion


    #region 介面定義 - 請覆寫實做
    protected override void InitialExtraInfo()
    {
        InitialTimeEx();
        InitialPriza();
    }

    protected override void CreateTargetPage()
    {
        this.IPage = DepositUIData.TopUpData[this.PageType];
    }
   

    public override DepositUIStyle UIStyle { get { return DepositUIStyle.TopUp; } }
    #endregion




    #region 按鈕反應
    public void OnGoClick()
    {
        CoroutineV2.StartCoroutine(MarketDepositController.Show(SID.DepositEventController));
    }

    public void OnShowBigCard()
    {
        if (IsCardasMainPromo) IPage.IResource.AddBigCardObj(MainControl.NowCanvas);
        
    }



    #endregion


    private void InitialTimeEx()
    {
       
        TimerEx.Countdown(this, this.IPage.CountDownTime)
               .On1Second(x =>
                {
                    this.IPage.CountDownTime = (int)x;
                    UpdateTimeMsg();
                 }
               )
               .OnCompleted(x =>
               {
                   this.IPage.CountDownTime = 0;
                   TimerEx.RemoveTimer(this);
                   TimeOutGoOtherPage();
               })
               .Start();
    }

    private void TimeOutGoOtherPage()
    {
        bool isExistFeatureOnlySelf = this.DepositUIData.MenuMax == 1;
        if (isExistFeatureOnlySelf) return;

        int baseIndex = 0;
        DepositEventType firstSortFeature = this.DepositUIData.GetTypeBySortID(baseIndex);
        this.DepositUIData.DepositEventUIEvent.DispatchCallback(DepositEventTypeEvent.GoOtherFeature, new object[] { firstSortFeature });
    }


    private void UpdateTimeMsg()
    {
        this.FixedTitles[3].text = this.IPage.CountDownTime.ExFormatTimeDDHHMMSSV2();
    }
    private void InitialPriza()
    {
        RewardContainer.ExRemoveAllChildren();
        IPage.Prizes.ForEach(x => x.AddIconObj(RewardContainer));
        DetailBtn.SetActive(IsCardasMainPromo);
    }

    void OnDisable()
    {
        TimerEx.RemoveTimer(this);
    }

    private bool IsCardasMainPromo { get { return IPage.IResource.ResourceType == ResourceItemType.CARD; } }

}
