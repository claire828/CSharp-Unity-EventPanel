using Assets.Common.Scripts.Datas.UIData;
using cn.sharesdk.unity3d;
using Common.Data;
using Common.Settings;
using Engine.Net;
using HeavyDutyInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniLinq;
public class FriendInvitedPanel : MonoBehaviour, IComDeposit
{
    [Comment("無限捲軸", CommentType.Info)]
    public ScrollRectV2 ScrollRect;

    [Comment("BannerFixedTitle 從上到下:3", CommentType.Info)]
    public Text[] FixedTitles;

    [Comment("BannerDynamicInfo 從上到下:2", CommentType.Info)]
    public Text[] DynamicMsg;

    [Comment("Banner", CommentType.Info)]
    public RawImage Banner;



    /// <summary>
    /// 顯示累積儲值&累積消費內容
    /// </summary>

    public FriendInvitedPanel(DepositEventType type, GameObject panelContainer)
    {
        GameObject resObj = Resources.Load("DepositEvent/Prefab/InvitedPanel", typeof(GameObject)) as GameObject;
        panelContainer.ExAddChildAsync(resObj, go =>
        {
            FriendInvitedPanel panel = go.transform.GetComponent<FriendInvitedPanel>();
            panel.Show(type);
        }
        );
    }


    private float InviteMax { get { return PBSettingsV3.Fixed[6701].Value; } }

    private float InviteLV { get { return PBSettingsV3.Fixed[6704].Value; } }

    public string ImgURL { get { return string.Format("images/DepositEvent/{0}", (int)DepositEventType.FriendInvited); } }

    public void Show(DepositEventType type)
    {
        PageType = type;
        Banner.ExSetTextureFromBundle(ImgURL, 1);
        this.ReSortOrder();
        ScrollRect.itemCount = this.FeatureData.AllTasks.Count;
    }


    private List<Assets.Common.Scripts.Datas.UIData.FriendInvitedData.FriendInvitedTaskStruct> SortTasks { get;  set; }
    private void ReSortOrder()
    {
        var achieveTask = this.FeatureData.AllTasks.Where(x => x.Achieve).ToList();
        var unAchieveTask = this.FeatureData.AllTasks.Where(x => !x.Achieve).ToList();
        SortTasks = new List<FriendInvitedData.FriendInvitedTaskStruct>();
        unAchieveTask.ForEach(x => SortTasks.Add(x));
        achieveTask.ForEach(x => SortTasks.Add(x));
    
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }


    public void SetItem(int index, GameObject go)
    {
        InvitedTaskItem raw = go.GetComponent<InvitedTaskItem>();
        raw.SetItem(SortTasks[index]);
    }


    public DepositEventType PageType { get; set; }



    public void OnTwitter()
    {
        MainControl.Instance.ClickTwitterShare();  
    }



    public void OnLine()
    {
        MainControl.Instance.ClickLineShare();
      
    }


    public void OnMail()
    {
        Mail.OpenShareFriend();
    }


    public FriendInvitedData FeatureData
    {
        get
        {
            return DepositUIData.FriendInvitedData;
        }
    }

    public DepositEventUIData DepositUIData { get { return ((DepositEventUIData)PBDatas.UIDatas.UI(SID.DepositEventController)); } }


    /// <summary>
    ///取得SNS招待碼
    /// </summary>
    public static IEnumerator GetSNSInvitedCode(bool updateShow = false)
    {
        DebugEx.Log("SendRID431 : 取得SNS招待碼");
        RID431Request req = new RID431Request();

        yield return NetManager.Send(req);

        if (req.IsResultCorrect())
        {
            if (req.Status == 0)
            {
                PBDatas.UserData.SetOwnSNSCode(req.Result.StringValue);
            }
            else
            {
                DebugEx.Log("ERR 取得自己的SNS招待碼失敗");
            }
        }
    }


    public DepositUIStyle UIStyle { get { return DepositUIStyle.Friend; } }



}
