using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComDepositItem : MonoBehaviour {

    public Text Title;
    public GameObject RewardContainer;
    public GameObject AchieveIcon;
    public GameObject UnachieveObj;

    public void SetItem(ComDepositStruct item, string title)
    {

        Title.text = title;
        AchieveIcon.SetActive(item.HasAchieve);
        UnachieveObj.SetActive(!item.HasAchieve);
        RewardContainer.ExRemoveAllChildren();
        item.Rewards.ForEach(x => x.AddIconObj(RewardContainer));
    }
}
