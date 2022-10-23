using TMPro;
using uClicker;
using UnityEngine;
using UnityEngine.UI;

public class Binder : MonoBehaviour
{
    [SerializeField] private ClickerManager clickerManager;

    public TextMeshProUGUI Name;
    //public TextMeshProUGUI Cost;
    public TextMeshProUGUI Count;
    private UnlockableComponent ClickerComponent;
    private Button Button;

    public void Buy()
    {
        if (ClickerComponent is Upgrade)
        {
			clickerManager.BuyUpgrade(ClickerComponent as Upgrade);
        }
        else {
			clickerManager.BuyBuilding(ClickerComponent as Building);
			UpdateCount();
		}
    }

	public void UpdateCount() {
		var building = ClickerComponent as Building;
		int buildingCount = 0;
		if (building != null) {
			clickerManager.State.EarnedBuildings.TryGetValue(building, out buildingCount);
		}
		Count.text = buildingCount.ToString();
	}

    public void Bind(Upgrade availableUpgrade)
    {
        ClickerComponent = availableUpgrade;
        this.Name.text = availableUpgrade.ItemName;
        //this.Cost.text = availableUpgrade.Cost.ToString();
        //this.Description.text = GenerateUpgradeString(availableUpgrade.UpgradePerk);
        Button = this.GetComponent<Button>();
		Button.onClick.AddListener(Buy);
		clickerManager.OnTick.AddListener(IsActive);
		clickerManager.OnBossWin.AddListener(IsActive);
		clickerManager.OnBossLose.AddListener(IsActive);
        IsActive();
		UpdateCount();
	}

    public void Bind(Building availableBuilding)
    {
        ClickerComponent = availableBuilding;
        this.Name.text = availableBuilding.ItemName;
        //this.Cost.text = _clickerManager.BuildingCost(availableBuilding).ToString();
        /*this.Description.text = string.Format("+{0} {1}s per second", availableBuilding.YieldAmount.Amount,
            availableBuilding.YieldAmount.Currency.name);*/
        Button = this.GetComponent<Button>();
		Button.onClick.AddListener(Buy);
		clickerManager.OnTick.AddListener(IsActive);
		clickerManager.OnBossWin.AddListener(IsActive);
		clickerManager.OnBossLose.AddListener(IsActive);
        IsActive();
		UpdateCount();
    }

    private void IsActive()
    {
        Button.interactable = !clickerManager.isBossStage && clickerManager.CanBuy(ClickerComponent);
    }

    private string GenerateUpgradeString(UpgradePerk[] availableUpgradeUpgradePerk)
    {
        string text = "";
        foreach (var upgradePerk in availableUpgradeUpgradePerk)
        {
            ClickerComponent component = upgradePerk.TargetBuilding ??
                                         (ClickerComponent) upgradePerk.TargetClickable ?? upgradePerk.TargetCurrency;
            text += string.Format("{0}s {1} to {2}", upgradePerk.Operation, upgradePerk.Amount, component.name);
        }

        return text;
    }
}