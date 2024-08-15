using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectMenu : UIBase
{
    private const string path_CharacterInfoPanel = "CharacterInfoPanel";
    private const string path_CharacterInfoIcon = path_CharacterInfoPanel + "/CharacterInfo/Img_CharacterIcon";
    private const string path_CharacterInfoName = path_CharacterInfoPanel + "/CharacterInfo/Text_CharacterName";
    private const string path_CharacterInfoRareLevel = path_CharacterInfoPanel + "/CharacterInfo/Text_CharacterRareLevel";
    private const string path_Trans_CharacterProperties_Parent = path_CharacterInfoPanel + "/CharacterInfo/Abilities";
    private const string path_WeaponInfoPanel = "WeaponInfoPanel";
    private const string path_WeaponInfoIcon = path_WeaponInfoPanel + "/WeaponInfo/Img_ItemIcon";
    private const string path_WeaponInfoName = path_WeaponInfoPanel + "/WeaponInfo/Text_ItemName";
    private const string path_WeaponInfoRareLevel = path_WeaponInfoPanel + "/WeaponInfo/Text_ItemRareLevel";
    private const string path_Img_LevelFilter = path_WeaponInfoPanel + "/Img_ItemLevelFilter";
    private const string path_Trans_WeaponProperties_Parent = path_WeaponInfoPanel + "/WeaponInfo/Properties";
    private const string path_Btn_StartGame = "Btn_StartGame";
    private const string path_SelectTip = "SelectTip";
    private const string path_Btn_BackToMainMenu = "Btn_BackToMainMenu";
    private const string path_CharactersParent = "CharacterList";
    private const string path_Item_Weapons_Parent = "WeaponList";
    private Image img_CharacterInfoIcon;
    private Image img_WeaponInfoIcon;
    private Image img_WeaponLevelFilter;
    private TextMeshProUGUI text_WeaponInfoName;
    private TextMeshProUGUI text_WeaponRareLevel;
    private TextMeshProUGUI text_CharacterInfoName;
    private TextMeshProUGUI text_CharacterRareLevel;
    private Button btn_StartGame;
    private Button btn_BackToMainMenu;
    private Transform trans_WeaponProperties_Parent;
    private Transform trans_CharacterProperties_Parent;
    private Transform trans_Item_Characters_Parent;
    private Transform trans_Item_Weapons_Parent;
    public GameObject prefab_Character;
    public GameObject prefab_Item_Weapon;
    public Transform selectTip;
    public List<CharacterData_SO> allCharactersData = new List<CharacterData_SO>();
    public List<ShopItemData_Weapon_SO> currentCharacterAvailableWeapons = new List<ShopItemData_Weapon_SO>();
    [SerializeField]
    private Item_Weapon_CharacterSelectMenu selectWeapon;//初始武器选择
    private Item_Character selectCharacter;//初始角色选择
    private Tweener showTipTween;

    private void Awake()
    {
        img_WeaponLevelFilter = transform.Find(path_Img_LevelFilter).GetComponent<Image>();
        img_CharacterInfoIcon = transform.Find(path_CharacterInfoIcon).GetComponent<Image>();
        text_CharacterInfoName = transform.Find(path_CharacterInfoName).GetComponent<TextMeshProUGUI>();
        text_CharacterRareLevel = transform.Find(path_CharacterInfoRareLevel).GetComponent<TextMeshProUGUI>();
        img_WeaponInfoIcon = transform.Find(path_WeaponInfoIcon).GetComponent<Image>();
        text_WeaponInfoName = transform.Find(path_WeaponInfoName).GetComponent<TextMeshProUGUI>();
        text_WeaponRareLevel = transform.Find(path_WeaponInfoRareLevel).GetComponent<TextMeshProUGUI>();
        btn_StartGame = transform.Find(path_Btn_StartGame).GetComponent<Button>();
        btn_BackToMainMenu = transform.Find(path_Btn_BackToMainMenu).GetComponent<Button>();
        trans_Item_Characters_Parent = transform.Find(path_CharactersParent);
        trans_Item_Weapons_Parent = transform.Find(path_Item_Weapons_Parent);
        trans_WeaponProperties_Parent = transform.Find(path_Trans_WeaponProperties_Parent);
        trans_CharacterProperties_Parent = transform.Find(path_Trans_CharacterProperties_Parent);
        selectTip = transform.Find(path_SelectTip);
        selectTip.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        btn_StartGame.onClick.AddListener(OnBtnStartGameClick);
        btn_BackToMainMenu.onClick.AddListener(OnBtnBackToMainMenuClick);
        GenerateAllCharacters();
        GenerateAllWeapons();
    }

    private void OnDisable()
    {
        btn_StartGame.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        uiID = eUIID.CharacterSelectMenu;
    }

    public void SelectWeapon(Item_Weapon_CharacterSelectMenu weapon)
    {
        if (selectWeapon != null)
        {
            selectWeapon.UnSelectWeapon();
        }
        selectWeapon = weapon;
    }

    public void SelectCharacter(Item_Character character)
    {
        if (selectCharacter != null && character != selectCharacter)
        {
            selectCharacter.UnSelectCharacter();
        }
        selectCharacter = character;
    }

    private void GenerateAllWeapons()
    {
        if (trans_Item_Weapons_Parent.childCount > 0)
        {
            ClearAllWeapons();
        }
        for (int i = 0; i < currentCharacterAvailableWeapons.Count; i++)
        {
            Item_Weapon_CharacterSelectMenu itemWeapon = Instantiate(prefab_Item_Weapon, trans_Item_Weapons_Parent).GetComponent<Item_Weapon_CharacterSelectMenu>();
            itemWeapon.InitItemPropUI(currentCharacterAvailableWeapons[i], this);
            itemWeapon.itemLevel = currentCharacterAvailableWeapons[i].itemLevel;
            // itemWeapon.InitItemPropUI();
        }
    }

    private void ClearAllWeapons()
    {
        for (int i = 0; i < trans_Item_Weapons_Parent.childCount; i++)
        {
            Destroy(trans_Item_Weapons_Parent.GetChild(i).gameObject);
        }
    }

    private void GenerateAllCharacters()
    {
        if (trans_Item_Characters_Parent.childCount > 0)
        {
            ClearAllCharacters();
        }
        for (int i = 0; i < allCharactersData.Count; i++)
        {
            Item_Character itemCharacter = Instantiate(prefab_Character, trans_Item_Characters_Parent).GetComponent<Item_Character>();
            itemCharacter.characterData = allCharactersData[i];
            itemCharacter.InitUIInfo(allCharactersData[i], this);
        }
    }

    private void ClearAllCharacters()
    {
        for (int i = 0; i < trans_Item_Characters_Parent.childCount; i++)
        {
            Destroy(trans_Item_Characters_Parent.GetChild(i).gameObject);
        }
    }

    private bool CheckSelectionComplete()
    {
        if(selectCharacter != null && selectWeapon != null)
        {
            return true;
        }
        return false;
    }

    private void OnBtnStartGameClick()
    {
        if(!CheckSelectionComplete())
        {
            if(showTipTween != null && showTipTween.IsPlaying())
            {
                showTipTween.Pause();
                showTipTween = null;
            }
            Debug.Log("Please Select Both The Character And Weapon");
            selectTip.gameObject.SetActive(true);
            showTipTween = selectTip.DOScale(new Vector3(1.2f,1.2f),0.2f).OnComplete(()=>
            {
                selectTip.DOScale(new Vector3(1f,1f),0.2f).OnComplete(()=>
                {
                    EyreUtility.SetDelay(1f,()=>{
                        selectTip.localScale.Set(1f,1f,1f);
                        selectTip.gameObject.SetActive(false);
                    });
                });
            });
            return;
        }
        CloseUI();
        AddWeaponToGameInventory();
        EventManager.instance.OnSetCharacterData(selectCharacter.characterData);
        EventManager.instance.OnGenerateWeaonInInventory();
        EventManager.instance.OnStartGame();
        EventManager.instance.OnEnableLocomotionInput();
        EventManager.instance.OnEnableUIInput();
    }

    private void OnBtnBackToMainMenuClick()
    {
        ClearCharacterAndWeaponInfoPanel();
        CloseUI();
        EventManager.instance.OnOpenUI(eUIID.MainMenu);
    }

    private void AddWeaponToGameInventory()
    {
        Inventory_Weapon weapon = new Inventory_Weapon
        {
            weaponLevel = selectWeapon.itemLevel,
            weaponData = selectWeapon.itemData as ShopItemData_Weapon_SO,
            sellPrice = selectWeapon.itemData.itemCost,
        };
        GameInventory.Instance.AddWeaponToInventory(weapon);
    }

    private void ClearCharacterInfoPanel()
    {
        img_CharacterInfoIcon.sprite = null;
        img_CharacterInfoIcon.color = new Color(1,1,1,0);
        text_CharacterInfoName.text = "";
        text_CharacterRareLevel.text = "";
        ClearCharacterProperties();
    }

    private void ClearWeaponInfoPanel()
    {
        img_WeaponInfoIcon.sprite = null;
        img_WeaponInfoIcon.color = new Color(1,1,1,0);
        text_WeaponInfoName.text = "";
        text_WeaponRareLevel.text = "";
        ClearWeaponProperties();
    }

    private void ClearCharacterAndWeaponInfoPanel()
    {
        ClearWeaponInfoPanel();
        ClearCharacterInfoPanel();
    }

    public void UpdateSelectingCharacterInfo(CharacterData_SO data)
    {
        ClearCharacterProperties();
        img_CharacterInfoIcon.sprite = data.characterIcon;
        img_CharacterInfoIcon.color = Color.white;
        text_CharacterInfoName.text = data.characterName;
        text_CharacterRareLevel.text = "Normal";
        GameObject text_Property = trans_CharacterProperties_Parent.GetChild(0).gameObject;
        GameObject[] textList = new GameObject[7];
        for(int i = 0; i < 7; i++)
        {
            textList[i] = Instantiate(text_Property,trans_CharacterProperties_Parent);
        }
        SetCharacterPropertyText(text_Property.GetComponent<TextMeshProUGUI>(), ePlayerProperty.MaxHP,data.HP);
        SetCharacterPropertyText(textList[0].GetComponent<TextMeshProUGUI>(), ePlayerProperty.HPRegeneration,data.hpRegeneratePerSecond);
        SetCharacterPropertyText(textList[1].GetComponent<TextMeshProUGUI>(), ePlayerProperty.lifeSteal,data.lifeStealRate);
        SetCharacterPropertyText(textList[2].GetComponent<TextMeshProUGUI>(), ePlayerProperty.DamageMul,data.damageMul);
        SetCharacterPropertyText(textList[3].GetComponent<TextMeshProUGUI>(), ePlayerProperty.AttackSpeed,data.attackSpeedMul);
        SetCharacterPropertyText(textList[4].GetComponent<TextMeshProUGUI>(), ePlayerProperty.CriticalRate,data.criticalRate);
        SetCharacterPropertyText(textList[5].GetComponent<TextMeshProUGUI>(), ePlayerProperty.AttackRange,data.attackRange);
        SetCharacterPropertyText(textList[6].GetComponent<TextMeshProUGUI>(), ePlayerProperty.MoveSpeed,data.moveSpeed);
    }

    public void UpdateSelectedCharacterInfo()
    {
        if (selectCharacter == null) return;
        ClearCharacterProperties();
        CharacterData_SO data = selectCharacter.characterData;
        img_CharacterInfoIcon.sprite = data.characterIcon;
        img_CharacterInfoIcon.color = Color.white;
        text_CharacterInfoName.text = data.characterName;
        text_CharacterRareLevel.text = "Normal";
        GameObject text_Property = trans_CharacterProperties_Parent.GetChild(0).gameObject;
        GameObject[] textList = new GameObject[7];
        for(int i = 0; i < 7; i++)
        {
            textList[i] = Instantiate(text_Property,trans_CharacterProperties_Parent);
        }
        SetCharacterPropertyText(text_Property.GetComponent<TextMeshProUGUI>(), ePlayerProperty.MaxHP,data.HP);
        SetCharacterPropertyText(textList[0].GetComponent<TextMeshProUGUI>(), ePlayerProperty.HPRegeneration,data.hpRegeneratePerSecond);
        SetCharacterPropertyText(textList[1].GetComponent<TextMeshProUGUI>(), ePlayerProperty.lifeSteal,data.lifeStealRate);
        SetCharacterPropertyText(textList[2].GetComponent<TextMeshProUGUI>(), ePlayerProperty.DamageMul,data.damageMul);
        SetCharacterPropertyText(textList[3].GetComponent<TextMeshProUGUI>(), ePlayerProperty.AttackSpeed,data.attackSpeedMul);
        SetCharacterPropertyText(textList[4].GetComponent<TextMeshProUGUI>(), ePlayerProperty.CriticalRate,data.criticalRate);
        SetCharacterPropertyText(textList[5].GetComponent<TextMeshProUGUI>(), ePlayerProperty.AttackRange,data.attackRange);
        SetCharacterPropertyText(textList[6].GetComponent<TextMeshProUGUI>(), ePlayerProperty.MoveSpeed,data.moveSpeed);
    }

    private void ClearCharacterProperties()
    {
        for (int i = 1; i < trans_CharacterProperties_Parent.childCount; i++)
        {
            Destroy(trans_CharacterProperties_Parent.GetChild(i).gameObject);
        }
        trans_CharacterProperties_Parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
    }

    public void UpdateSelectingWeaponInfo(ShopItemData_Weapon_SO data)
    {
        ClearWeaponProperties();
        img_WeaponInfoIcon.sprite = data.itemIcon;
        img_WeaponInfoIcon.color = Color.white;
        text_WeaponInfoName.text = data.itemName;
        GameObject text_Property = trans_WeaponProperties_Parent.GetChild(0).gameObject;
        SetWeaponPropertyText(data, text_Property.GetComponent<TextMeshProUGUI>(), data.itemProperties[0].weaponProperty, data.itemProperties[0].propertyValue);
        for (int i = 1; i < data.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property, trans_WeaponProperties_Parent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair dataPair = data.itemProperties[i];
            SetWeaponPropertyText(data, textComp, dataPair.weaponProperty, dataPair.propertyValue);
        }
        SetItemRareLevelText(data.itemLevel);
    }

    public void UpdateSelectedWeaponInfo()
    {
        if (selectWeapon == null) return;
        ShopItemData_Weapon_SO data = selectWeapon.itemData as ShopItemData_Weapon_SO;
        ClearWeaponProperties();
        img_WeaponInfoIcon.sprite = data.itemIcon;
        img_WeaponInfoIcon.color = Color.white;
        text_WeaponInfoName.text = data.itemName;
        GameObject text_Property = trans_WeaponProperties_Parent.GetChild(0).gameObject;
        SetWeaponPropertyText(data, text_Property.GetComponent<TextMeshProUGUI>(), data.itemProperties[0].weaponProperty, data.itemProperties[0].propertyValue);
        for (int i = 1; i < data.itemProperties.Count; i++)
        {
            TextMeshProUGUI textComp = Instantiate(text_Property, trans_WeaponProperties_Parent).GetComponent<TextMeshProUGUI>();
            ShopWeaponPropertyPair dataPair = data.itemProperties[i];
            SetWeaponPropertyText(data, textComp, dataPair.weaponProperty, dataPair.propertyValue);
        }
        SetItemRareLevelText(data.itemLevel);
    }

    private void ClearWeaponProperties()
    {
        for (int i = 1; i < trans_WeaponProperties_Parent.childCount; i++)
        {
            Destroy(trans_WeaponProperties_Parent.GetChild(i).gameObject);
        }
        trans_WeaponProperties_Parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
    }

    protected void SetItemRareLevelText(int itemLevel)
    {
        switch (itemLevel)
        {
            case 1:
                text_WeaponRareLevel.text = "Normal";
                text_WeaponRareLevel.color = GameColor.ShopItem_Level01;
                break;
            case 2:
                text_WeaponRareLevel.text = "Special";
                text_WeaponRareLevel.color = GameColor.ShopItem_Level02;
                break;
            case 3:
                text_WeaponRareLevel.text = "Rare";
                text_WeaponRareLevel.color = GameColor.ShopItem_Level03;
                break;
            case 4:
                text_WeaponRareLevel.text = "Epic";
                text_WeaponRareLevel.color = GameColor.ShopItem_Level04;
                break;
            case 5:
                img_WeaponLevelFilter.color = GameColor.ShopItem_Level05;
                break;
            default: break;
        }
    }

    private void SetWeaponPropertyText(ShopItemData_Weapon_SO data, TextMeshProUGUI textComp, eWeaponProperty weaponProperty, float propertyValue)
    {
        propertyValue = GameInventory.Instance.CaculateWeaponDataByLevel(weaponProperty, propertyValue, data.itemLevel, data.itemLevel);
        switch (weaponProperty)
        {
            case eWeaponProperty.Damage:
                textComp.text = "Damage:" + propertyValue * (GameCoreData.PlayerProperties.damageMul * 0.01f + 1);
                break;
            case eWeaponProperty.CriticalMul:
                textComp.text = "CriticalMul:x" + propertyValue;
                break;
            case eWeaponProperty.FireInterval:
                textComp.text = "FireInterval:" + (propertyValue / (GameCoreData.PlayerProperties.attackSpeedMul * 0.01f + 1)).ToString("F2");
                break;
            case eWeaponProperty.KnockBack:
                textComp.text = "PushBack:" + propertyValue;
                break;
            case eWeaponProperty.AttackRange:
                textComp.text = "AttackRange:" + (propertyValue + GameCoreData.PlayerProperties.attackRange).ToString();
                break;
            case eWeaponProperty.LifeSteal:
                textComp.text = "LifeSteal:" + ((propertyValue + GameCoreData.PlayerProperties.lifeSteal) * 100).ToString() + "%";
                break;
            case eWeaponProperty.DamageThrough:
                textComp.text = "DamageThrough:" + propertyValue;
                break;
            case eWeaponProperty.CriticalRate:
                textComp.text = "CriticalRate:" + ((propertyValue * 100) + GameCoreData.PlayerProperties.criticalRate).ToString() + "%";
                break;
        }
    }

    private void SetCharacterPropertyText(TextMeshProUGUI textComp, ePlayerProperty characterProperty, float propertyValue)
    {
        string sign = "";
        switch (characterProperty)
        {
            case ePlayerProperty.MaxHP:
                if (propertyValue != 40)
                {
                    sign = propertyValue > 40 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue - 40) + "MaxHP:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.HPRegeneration:
                if (propertyValue != 0)
                {
                    sign = propertyValue > 0 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue) + "HPRegeneration:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.lifeSteal:
                if (propertyValue != 0)
                {
                    sign = propertyValue > 0 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue) + "%LifeSteal:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.DamageMul:
                if(propertyValue != 1)
                {
                    sign = propertyValue > 1 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue - 1) * 100 + "%DamageMul:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.AttackSpeed:
                if (propertyValue != 1)
                {
                    sign = propertyValue > 1 ? "+": "-";
                    textComp.text = sign + Mathf.Abs(propertyValue - 1) * 100 + "%AttackSpeed:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.CriticalRate:
                if (propertyValue != 0)
                {
                    sign = propertyValue > 0 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue) + "%CriticalRate:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.AttackRange:
                if (propertyValue != 400)
                {
                    sign = propertyValue > 400 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue - 400) + "AttackRange:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
            case ePlayerProperty.MoveSpeed:
                if (propertyValue != 10)
                {
                    sign = propertyValue > 10 ? "+" : "-";
                    textComp.text = sign + Mathf.Abs(propertyValue - 10) * 10 + "%MoveSpeed:";
                }
                else
                {
                    Destroy(textComp.gameObject);
                }
                break;
        }
        if(sign == "+")
        {
            textComp.color = Color.green;
        }
        else
        {
            textComp.color = Color.red;
        }
    }
}