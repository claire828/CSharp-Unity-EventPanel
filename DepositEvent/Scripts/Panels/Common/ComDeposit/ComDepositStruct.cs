using Assets.Common.Scripts.Datas.UIData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComDepositStruct
{

    public IResourceItem[] Rewards { get; private set; }
    public bool HasAchieve { get; private set; }
    public int Target { get; private set; }
    public bool IsPersonal { get; private set; }

    public DepositEventType Type { get; private set; }
    public ComDepositStruct(DepositEventType type, int target, IResourceItem[] rewards, bool bAchievement)
    {
        this.Type = type;
     
        Target = target;
        Rewards = rewards;
        HasAchieve = bAchievement;
        
    }

    /// <summary>
    /// ComDepositStruct多載,用於區分個人資料及全服資料
    /// </summary>
    /// <param name="type"></param>
    /// <param name="target"></param>
    /// <param name="rewards"></param>
    /// <param name="bAchievement"></param>
    /// <param name="isPersonal"></param>
    public ComDepositStruct(DepositEventType type, int target, IResourceItem[] rewards, bool bAchievement, bool isPersonal)
    {
        this.Type = type;

        Target = target;
        Rewards = rewards;
        HasAchieve = bAchievement;
        IsPersonal = isPersonal;
    }


}
