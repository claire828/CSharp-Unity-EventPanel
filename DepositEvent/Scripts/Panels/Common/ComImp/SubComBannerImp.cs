using Assets.Common.Scripts.Datas.UIData;
using Common.Settings;
using HeavyDutyInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SubComBannerImp
{

    public void Execute(BannerDepositStruct bannerStruct)
    {
        ISubUIElement sub = GetSubUI(bannerStruct.IPage.SubDepositUIType);
        sub.Execute(bannerStruct);
       
    }




    private ISubUIElement GetSubUI(SubDepositEventUIType subType)
    {
        if (subType == SubDepositEventUIType.SubPurchase) return new SubPurchase();
        if (subType == SubDepositEventUIType.SubTask) return new SubTask();
        if (subType == SubDepositEventUIType.SubCollection) return new SubCollection();

        throw new NotSupportedException("子類型介面尚未實做:"+subType);
    }



    #region SubUI - UI處理


    abstract class SubUI : ISubUIElement
    {
         protected BannerDepositStruct BannerStruct { get; set; }

        public void Execute(BannerDepositStruct bannerStruct)
        {
            BannerStruct = bannerStruct;
            AddDepositMiddleInfomation();
            AddingDepositInfomation();
            AdjustScrollBarPosition();
        }

        protected abstract void AddingDepositInfomation();

        protected virtual void AdjustScrollBarPosition()
        { 
            
        }

        private void AddDepositMiddleInfomation()
        {
            BannerStruct.MiddleInfoTitle.text = BannerStruct.IPage.AccumulatedTitle;
            BannerStruct.MiddleInfoTxt.text = BannerStruct.IPage.AccumulatedAmount.ToString();
            BannerStruct.GoBtn.gameObject.SetActive(BannerStruct.IPage.ShowGoBtn);
            BannerStruct.MiddleInfoObj.SetActive(BannerStruct.IPage.ShowMiddleInfo);
            BannerStruct.MiddleInfoObj.transform.localPosition = BannerStruct.IPage.AccumulatedObjLocated;
        }

       

    }





    /// <summary>
    /// 儲值消費 & 累積儲值使用 (會有中間的消費除值金額UI)
    /// </summary>
    class SubPurchase : SubUI
    {
        protected override void AddingDepositInfomation()
        {
            BannerStruct.FixedTitles[0].text = BannerStruct.IPage.BannerTittle;
            BannerStruct.FixedTitles[1].text = BannerStruct.IPage.IsDaily ? PBSettingsV3.String[22006].Msg : "";
            BannerStruct.FixedTitles[2].text = PBSettingsV3.String[22007].Msg;
            BannerStruct.FixedTitles[3].text = BannerStruct.IPage.StrRewadTip;
            BannerStruct.DynamicMsg[0].text = BannerStruct.IPage.IsDaily ? BannerStruct.IPage.ResetTime : "";
            BannerStruct.DynamicMsg[1].text = BannerStruct.IPage.EndDate;
        }
        protected override void AdjustScrollBarPosition()
        {
            BannerStruct.ScrollObj.transform.localPosition = new Vector2(0f, -307f);
        }
       
    }


    class SubTask : SubUI
    {
        protected override void AdjustScrollBarPosition()
        {
            BannerStruct.ScrollObj.transform.localPosition = new Vector2(0f, -248f);
        }
        protected override void AddingDepositInfomation()
        {
            BannerStruct.FixedTitles.ForEach(x => x.text = "");
            BannerStruct.DynamicMsg.ForEach(x => x.text = "");
            BannerStruct.FixedTitles[0].text = BannerStruct.IPage.BannerTittle;
            BannerStruct.FixedTitles[3].text = BannerStruct.IPage.StrRewadTip;
            BannerStruct.DynamicMsg[1].text = BannerStruct.IPage.EndDate;
        }


    }

    class SubCollection : SubUI
    {
        protected override void AdjustScrollBarPosition()
        {
            BannerStruct.ScrollObj.transform.localPosition = new Vector2(0f, -248f);
        }
        protected override void AddingDepositInfomation()
        {
            BannerStruct.FixedTitles.ForEach(x => x.text = "");
            BannerStruct.DynamicMsg.ForEach(x => x.text = "");
            BannerStruct.FixedTitles[0].text = BannerStruct.IPage.BannerTittle;
            BannerStruct.FixedTitles[1].text = BannerStruct.IPage.ResetTime;
            BannerStruct.FixedTitles[3].text = BannerStruct.IPage.StrRewadTip;
            BannerStruct.DynamicMsg[1].text = BannerStruct.IPage.EndDate;
            BannerStruct.DynamicMsg[2].text = PBSettingsV3.String[22030].Msg;
        }
    }





#endregion


  





    interface ISubUIElement
    {
        void Execute(BannerDepositStruct bannerStruct);
      
    }




}



public class BannerDepositStruct
{
    public IDepositEventPage IPage;
    public Text[] FixedTitles;
    public Text[] DynamicMsg;
    public Text MiddleInfoTitle;
    public Text MiddleInfoTxt;
    public GameObject MiddleInfoObj;
    public ButtonEx2 GoBtn;
    public GameObject ScrollObj;
}

