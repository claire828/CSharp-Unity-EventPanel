

/// 儲值消費主按鈕
public class DepositEventBtn : MonoBehaviour
{

    private Action<DepositEventType> Call;
    public DepositEventType BtnType { get; private set; }
    public TabButton TabBtn { get; private set; }

    private GameObject Tip { get; set; }


    public DepositEventBtn(GameObject btn, DepositEventType type, TabButtonGroup group, string btnName)
    {
        TabBtn = btn.GetComponent<TabButton>();
        TabBtn.Group = group;
        BtnType = type;
        btn.name = btnName;

        Transform tip = TabBtn.transform.Find("tip");
        if (!tip) return;
        
        Tip = tip.gameObject;
        Tip.SetActive(type == DepositEventType.Purchase || type == DepositEventType.Spend);
        
    }

}
