using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvitedTaskItem : MonoBehaviour
{

    public Text Title;
    public Text SubTitle;
    public GameObject RewardContainer;
    public GameObject FinishIcon;

    public void SetItem(Assets.Common.Scripts.Datas.UIData.FriendInvitedData.FriendInvitedTaskStruct task)
    {
        Title.text = task.Title;
        SubTitle.text = task.SubTitle;
        RewardContainer.ExRemoveAllChildren();
        task.Rewards.ForEach(x => x.AddIconObj(RewardContainer));
        RewardContainer.SetActive(!task.Achieve);
        FinishIcon.SetActive(task.Achieve);
    }
}
