using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinishMenu : UIBase
{
    //------------UIComponentRelatePath
    private string path_Btn_MainMenu = "Btn_MainMenu";
    private string path_Health_PropertyValue = "PlayerStatusInfo/Properties/Health/Text_PropertyValue";
    private string path_HPRegeneration_PropertyValue = "PlayerStatusInfo/Properties/HPRegeneration/Text_PropertyValue";
    private string path_StealHP_PropertyValue = "PlayerStatusInfo/Properties/StealHP/Text_PropertyValue";
    private string path_DamageMul_PropertyValue = "PlayerStatusInfo/Properties/DamageMul/Text_PropertyValue";
    private string path_AttackSpeed_PropertyValue = "PlayerStatusInfo/Properties/AttackSpeed/Text_PropertyValue";
    private string path_CriticalRate_PropertyValue = "PlayerStatusInfo/Properties/CriticalRate/Text_PropertyValue";
    private string path_AttackRange_PropertyValue = "PlayerStatusInfo/Properties/AttackRange/Text_PropertyValue";
    private string path_MoveSpeed_PropertyValue = "PlayerStatusInfo/Properties/MoveSpeed/Text_PropertyValue";
    string path_PropInventoryParent = "PropInventory/Items";
    string path_WeaponInventoryParent = "WeaponInventory/Items";
    //------------End
    private Button btn_MainMenu;
    private TextMeshProUGUI text_Health_PropertyValue;
    private TextMeshProUGUI text_HPRegeneration_PropertyValue;
    private TextMeshProUGUI text_StealHP_PropertyValue;
    private TextMeshProUGUI text_DamageMul_PropertyValue;
    private TextMeshProUGUI text_AttackSpeed_PropertyValue;
    private TextMeshProUGUI text_CriticalRate_PropertyValue;
    private TextMeshProUGUI text_AttackRange_PropertyValue;
    private TextMeshProUGUI text_MoveSpeed_PropertyValue;
    private Transform trans_PropInventoryParent;
    private Transform trans_WeaponInventoryParent;
    [SerializeField]
    private GameObject prefab_InventoryItem_Prop;
    [SerializeField]
    private GameObject prefab_InventoryItem_Weapon;

    private void Awake() {
        btn_MainMenu = transform.Find(path_Btn_MainMenu).GetComponent<Button>();
        text_Health_PropertyValue = transform.Find(path_Health_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_HPRegeneration_PropertyValue = transform.Find(path_HPRegeneration_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_StealHP_PropertyValue = transform.Find(path_StealHP_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_DamageMul_PropertyValue = transform.Find(path_DamageMul_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackSpeed_PropertyValue = transform.Find(path_AttackSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_CriticalRate_PropertyValue = transform.Find(path_CriticalRate_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_AttackRange_PropertyValue = transform.Find(path_AttackRange_PropertyValue).GetComponent<TextMeshProUGUI>();
        text_MoveSpeed_PropertyValue = transform.Find(path_MoveSpeed_PropertyValue).GetComponent<TextMeshProUGUI>();
        trans_PropInventoryParent = transform.Find(path_PropInventoryParent);
        trans_WeaponInventoryParent = transform.Find(path_WeaponInventoryParent);
        SetAllDisplayInfo();
    }

    private void OnEnable() {
        btn_MainMenu.onClick.AddListener(OnMainMenuClick);
    }

    private void OnDisable() {
        btn_MainMenu.onClick.RemoveAllListeners();
    }

    private void SetAllDisplayInfo()
    {
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.MaxHP);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.HPRegeneration);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.lifeSteal);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.DamageMul);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.AttackSpeed);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.AttackRange);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.MoveSpeed);
        UpdatePlayerStatusPropertiesUI(ePlayerProperty.CriticalRate);
        UpdatePropInventory();
        UpdateWeaponInventory();
    }

    private void UpdateWeaponInventory()
    {
        GameInventory.Instance.inventoryWeapons.ForEach( weapon =>
        {
            Item_Weapon itemWeapon = Instantiate(prefab_InventoryItem_Weapon,trans_WeaponInventoryParent).GetComponent<Item_Weapon>();
            itemWeapon.InitItemPropUI(null,weapon.weaponData,weapon.weaponLevel,weapon.sellPrice,false);
            itemWeapon.inventory_Weapon = weapon;
        });
    }

    private void UpdatePropInventory()
    {
        GameInventory.Instance.inventoryProps.ForEach( prop =>
        {
            Item_Prop itemProp = Instantiate(prefab_InventoryItem_Prop,trans_PropInventoryParent).GetComponent<Item_Prop>();
            itemProp.InitItemPropUI(null,prop.propData,prop.propLevel,prop.propAmount,false);
        });
    }

    public override void InitUI()
    {
        uiID = eUIID.FinishMenu;
    }

    private void OnMainMenuClick()
    {
        Time.timeScale = 1;
        GlobalVar.gameStatus = eGameStatus.MainMenu;
        LevelManager.levelStatus = eLevelStatus.Ended;
        CloseUI();
        EventManager.instance.OnCloseUI(eUIID.ShopMenu);
        EventManager.instance.OnCloseUI(eUIID.UpgradeMenu);
        EventManager.instance.OnOpenUI(eUIID.MainMenu);
        EventManager.instance.OnDisableLocomotionInput();
        EventManager.instance.OnGameover();
        EventManager.instance.OnLevelEnd();
        GameCoreData.PlayerProperties.ResetPlayerProperties();
    }

    private void UpdatePlayerStatusPropertiesUI(ePlayerProperty playerProperty)
    {
        switch(playerProperty)
        {
            case ePlayerProperty.MaxHP:
                int healthPropertyValue = GameCoreData.PlayerProperties.maxHP;
                text_Health_PropertyValue.text = healthPropertyValue.ToString();
            break;
            case ePlayerProperty.HPRegeneration:
                int hpRegenerationPropertyValue = GameCoreData.PlayerProperties.hpRegeneration;
                text_HPRegeneration_PropertyValue.text = hpRegenerationPropertyValue.ToString();
            break;
            case ePlayerProperty.lifeSteal:
                int stealHPPropertyValue = GameCoreData.PlayerProperties.lifeSteal;
                text_StealHP_PropertyValue.text = stealHPPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.DamageMul:
                int damageMulPropertyValue = GameCoreData.PlayerProperties.damageMul;
                text_DamageMul_PropertyValue.text = damageMulPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackSpeed:
                int attackSpeedPropertyValue = GameCoreData.PlayerProperties.attackSpeedMul;
                text_AttackSpeed_PropertyValue.text = attackSpeedPropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.CriticalRate:
                int criticalRatePropertyValue = GameCoreData.PlayerProperties.criticalRate;
                text_CriticalRate_PropertyValue.text = criticalRatePropertyValue.ToString() + "%";
            break;
            case ePlayerProperty.AttackRange:
                int attackRangePropertyValue = GameCoreData.PlayerProperties.attackRange;
                text_AttackRange_PropertyValue.text = attackRangePropertyValue.ToString();
            break;
            case ePlayerProperty.MoveSpeed:
                int moveSpeedPropertyValue = GameCoreData.PlayerProperties.moveSpeed;
                text_MoveSpeed_PropertyValue.text = moveSpeedPropertyValue.ToString() + "%";
            break;
        }
    }

}