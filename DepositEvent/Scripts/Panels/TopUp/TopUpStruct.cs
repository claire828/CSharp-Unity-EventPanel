using Assets.Common.Scripts.Datas.UIData;
using Common.Data;
using Common.Players;
using Common.Resource;
using Common.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUpStruct
{

    public DepositEventType DepositUIType { get; private set; }
    public TopUpStruct(DepositEventType type, Json data)
    {
        this.DepositUIType = type;

        CountDownTime = data.ContainsKey("end") ? data["end"].IntValue : 0;
        CardID = data.ContainsKey("card") ? data["card"].IntValue : 0;
        MainPictureName = data["pic"].StringValue;
        var promo = data["promo"];
        IResource = PBResource.GetResoureItemFromPackage((ResourceItemType)promo[0].IntValue, promo[2].IntValue, promo[1].IntValue);
        Json reward = data["reward"]; // 任務
        Prizes = new List<IResourceItem>();
        //處理任務解析
        for (int i = 0; i < reward.Count; i++)
        {
            Json resource = reward[i];
            IResourceItem iresource = PBResource.GetResoureItemFromPackage((ResourceItemType)resource[0].IntValue, resource[2].IntValue, resource[1].IntValue);
            Prizes.Add(iresource);
            
        }
    }


    public IResourceItem IResource { get; private set; }

    public int CardID { get; private set; }

    public int CountDownTime { get; set; }

    public bool IsFinished { get { return CountDownTime == 0; } }

    public string ImgURL { get { return string.Format("images/DepositEvent/{0}", (int)DepositUIType); } }

    public string MainPictureURL { get { return string.Format("images/DepositEvent/pictures/{0}", MainPictureName); } }

    private string MainPictureName { get; set;}
    public PlayerBase Player { get { return new PlayerBase(CardID); } }


    public List<IResourceItem> Prizes { get; private set; }

    public DepositEventUIData DepositUIData { get { return ((DepositEventUIData)PBDatas.UIDatas.UI(SID.DepositEventController)); } }

    public string BannerTitle { 
        get 
        {
            int index = this.DepositUIType == DepositEventType.TopUp ? 22034 : 22035;
            return PBSettingsV3.String[index].Msg;
        }
    }

    public string Descrip
    {
        get
        {
            int index = this.DepositUIType == DepositEventType.TopUp ? 22039 : 22037;
            return PBSettingsV3.String[index].Msg;

        }
    }
    public string FinishDateTitle { get { return PBSettingsV3.String[22036].Msg; } }

    public string SendToMailMsg
    {
        get
        {
                return PBSettingsV3.String[22002].Msg;
        }
    }

    
}
