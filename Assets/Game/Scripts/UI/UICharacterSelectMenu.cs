using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectMenu : UIBase
{
    private const string path_CharacterInfoPanel = "CharacterInfoPanel";
    private const string path_CharacterInfoIcon = path_CharacterInfoPanel + "/CharacterInfo/Img_CharacterIcon";
    private const string path_CharacterInfoName = path_CharacterInfoPanel + "/CharacterInfo/Text_CharacterName";
    private const string path_WeaponInfoPanel = "WeaponInfoPanel";
    private const string path_WeaponInfoIcon = path_WeaponInfoPanel + "/WeaponInfo/Img_ItemIcon";
    private const string path_WeaponInfoName = path_WeaponInfoPanel + "/WeaponInfo/Text_ItemName";
    private const string path_Img_LevelFilter = path_WeaponInfoPanel + "/Img_ItemLevelFilter";
    private const string path_Trans_WeaponProperties_Parent = path_WeaponInfoPanel + "/WeaponInfo/Properties";
    private const string path_Btn_StartGame = "Btn_StartGame";
    private const string path_CharactersParent = "CharacterList";
    private const string path_Item_Weapons_Parent = "WeaponList";
    private Image img_CharacterInfoIcon;
    private Image img_WeaponInfoIcon;
    private Image img_WeaponLevelFilter;
    private TextMeshProUGUI text_WeaponInfoName;
    private TextMeshProUGUI text_CharacterInfoName;
    private Button btn_StartGame;
    private Transform trans_WeaponProperties_Parent;
    private Transform trans_CharacterProperties_Parent;
    private Transform trans_Item_Characters_Parent;
    private Transform trans_Item_Weapons_Parent;
    public GameObject prefab_Character;
    public GameObject prefab_Item_Weapon;
    public List<CharacterData_SO> allCharactersData = new List<CharacterData_SO>();
    public List<ShopItemData_Weapon_SO> currentCharacterAvailableWeapons = new List<ShopItemData_Weapon_SO>();
    [SerializeField][DisplayOnly]
    private Item_Weapon_CharacterSelectMenu selectWeapon;//初始武器选择
    private Item_Character selectCharacter;//初始角色选择

    private void Awake() {
        img_WeaponLevelFilter = transform.Find(path_Img_LevelFilter).GetComponent<Image>();
        img_CharacterInfoIcon = transform.Find(path_CharacterInfoIcon).GetComponent<Image>();
        text_CharacterInfoName = transform.Find(path_CharacterInfoName).GetComponent<TextMeshProUGUI>();
        img_WeaponInfoIcon = transform.Find(path_WeaponInfoIcon).GetComponent<Image>();
        text_WeaponInfoName = transform.Find(path_WeaponInfoName).GetComponent<TextMeshProUGUI>();
        btn_StartGame = transform.Find(path_Btn_StartGame).GetComponent<Button>();
        trans_Item_Characters_Parent = transform.Find(path_CharactersParent);
        trans_Item_Weapons_Parent = transform.Find(path_Item_Weapons_Parent);
        trans_WeaponProperties_Parent = transform.Find(path_Trans_WeaponProperties_Parent);
    }

    private void OnEnable() {
        btn_StartGame.onClick.AddListener(OnBtnStartGameClick);
        GenerateAllCharacters();
        GenerateAllWeapons();
    }

    private void OnDisable() {
        btn_StartGame.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        uiID = UIID.CharacterSelectMenu;
    }

    public void SelectWeapon(Item_Weapon_CharacterSelectMenu weapon)
    {
        if(selectWeapon != null)
        {
            selectWeapon.UnSelectWeapon();
        }
        selectWeapon = weapon;
    }

    public void SelectCharacter(Item_Character character)
    {
        if(selectCharacter != null)
        {
            selectCharacter.UnSelectCharacter();
        }
        selectCharacter = character;
    }

    private void GenerateAllWeapons()
    {
        if(trans_Item_Weapons_Parent.childCount > 0)
        {
            ClearAllWeapons();
        }
        for(int i = 0; i < currentCharacterAvailableWeapons.Count; i++)
        {
            Item_Weapon_CharacterSelectMenu itemWeapon = Instantiate(prefab_Item_Weapon,trans_Item_Weapons_Parent).GetComponent<Item_Weapon_CharacterSelectMenu>();
            itemWeapon.InitItemPropUI(currentCharacterAvailableWeapons[i],this);
            // itemWeapon.InitItemPropUI();
        }
    }

    private void ClearAllWeapons()
    {
        for(int i = 0; i < trans_Item_Weapons_Parent.childCount; i++)
        {
            Destroy(trans_Item_Weapons_Parent.GetChild(i).gameObject);
        }
    }

    private void GenerateAllCharacters()
    {
        if(trans_Item_Characters_Parent.childCount > 0)
        {
            ClearAllCharacters();
        }
        for(int i = 0; i < allCharactersData.Count; i++)
        {
            Item_Character itemCharacter = Instantiate(prefab_Character,trans_Item_Characters_Parent).GetComponent<Item_Character>();
            itemCharacter.characterData = allCharactersData[i];
            itemCharacter.InitUIInfo(allCharactersData[i],this);
        }
    }

    private void ClearAllCharacters()
    {
        for(int i = 0; i < trans_Item_Characters_Parent.childCount; i++)
        {
            Destroy(trans_Item_Characters_Parent.GetChild(i).gameObject);
        }
    }

    private void OnBtnStartGameClick()
    {
        CloseUI();
        AddWeaponToGameInventory();
        EventManager.instance.OnGenerateWeaonInInventory();
        EventManager.instance.OnStartGame();
    }

    private void AddWeaponToGameInventory()
    {
        Inventory_Weapon weapon = new Inventory_Weapon
        {
            weaponLevel = selectWeapon.itemLevel,
            weaponData = selectWeapon.itemData as ShopItemData_Weapon_SO
        };
        GameInventory.Instance.AddWeaponToInventory(weapon);
    }

    public void UpdateSelectingCharacterInfo(CharacterData_SO data)
    {
        img_CharacterInfoIcon.sprite = data.CharacterIcon;
        text_CharacterInfoName.text = data.CharacterName;
    }

    public void UpdateSelectedCharacterInfo()
    {
        if(selectCharacter == null) return;
        img_CharacterInfoIcon.sprite = selectCharacter.characterData.CharacterIcon;
        text_CharacterInfoName.text = selectCharacter.characterData.CharacterName;
    }

    public void UpdateSelectingWeaponInfo(ShopItemData_Weapon_SO data)
    {
        ClearWeaponProperties();
        img_WeaponInfoIcon.sprite = data.itemIcon;
        text_WeaponInfoName.text = data.itemName;
        GameObject text_Property = trans_WeaponProperties_Parent.GetChild(0).gameObject;
        SetPropertyText(data,text_Property.GetComponent<TextMeshProUGUI>(),data.itemProperties[0].weaponProperty,data.itemProperties[0].propertyValue);
        for(int i = 1; i < data.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property,trans_WeaponProperties_Parent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair dataPair = data.itemProperties[i];
            SetPropertyText(data,textComp,dataPair.weaponProperty,dataPair.propertyValue);
        }
        SetItemLevelFilterColor(data.itemLevel);
    }

    public void UpdateSelectedWeaponInfo()
    {
        if(selectWeapon == null) return;
        ShopItemData_Weapon_SO data = selectWeapon.itemData as ShopItemData_Weapon_SO;
        ClearWeaponProperties();
        img_WeaponInfoIcon.sprite = data.itemIcon;
        text_WeaponInfoName.text = data.itemName;
        GameObject text_Property = trans_WeaponProperties_Parent.GetChild(0).gameObject;
        SetPropertyText(data,text_Property.GetComponent<TextMeshProUGUI>(),data.itemProperties[0].weaponProperty,data.itemProperties[0].propertyValue);
        for(int i = 1; i < data.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property,trans_WeaponProperties_Parent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair dataPair = data.itemProperties[i];
            SetPropertyText(data,textComp,dataPair.weaponProperty,dataPair.propertyValue);
        }
        SetItemLevelFilterColor(data.itemLevel);
    }

    private void ClearWeaponProperties()
    {
        for(int i = 1; i < trans_WeaponProperties_Parent.childCount; i++)
        {
            Destroy(trans_WeaponProperties_Parent.GetChild(i).gameObject);
        }
    }

    protected void SetItemLevelFilterColor(int itemLevel)
    {
        switch(itemLevel)
        {
            case 1:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level01;
            break;
            case 2:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level02;
            break;
            case 3:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level03;
            break;
            case 4:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level04;
            break;
            case 5:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level05;
            break;
            default:break;
        }
    }

    private void SetPropertyText(ShopItemData_Weapon_SO data,TextMeshProUGUI textComp,WeaponProperty weaponProperty,float propertyValue)
    {
        propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(weaponProperty,propertyValue,data.itemLevel,data.itemLevel);
        switch(weaponProperty)
        {
            case WeaponProperty.Damage:
                textComp.text = "Damage:" + propertyValue * (GameCoreData.PlayerData.damageMul * 0.01f + 1);
            break;
            case WeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:" + propertyValue;
            break;
            case WeaponProperty.FireInterval:
                textComp.text = "FireInterval:" + (propertyValue / (GameCoreData.PlayerData.attackSpeedMul * 0.01f + 1)).ToString("F2");
            break;
            case WeaponProperty.PushBack:
                textComp.text = "PushBack:" + propertyValue;
            break;
            case WeaponProperty.AttackRange:
                textComp.text = "AttackRange:" + (propertyValue + GameCoreData.PlayerData.attackRange).ToString();
            break;
            case WeaponProperty.StealHP:
                textComp.text = "StealHP:" + (propertyValue + GameCoreData.PlayerData.stealHP).ToString();
            break;
            case WeaponProperty.DamageThrough:
                textComp.text = "DamageThrough:" + propertyValue;
            break;
        }
    }
}