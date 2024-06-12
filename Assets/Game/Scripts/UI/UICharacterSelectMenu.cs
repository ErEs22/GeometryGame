using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectMenu : UIBase
{
    private const string path_CharacterInfoPanel = "CharacterInfoPanel";
    private const string path_CharacterInfoIcon = path_CharacterInfoPanel + "/CharacterInfo/Img_CharacterIcon";
    private const string path_CharacterInfoName = path_CharacterInfoPanel + "/CharacterInfo/Text_CharacterName";
    private const string path_Btn_StartGame = "Btn_StartGame";
    private const string path_CharactersParent = "CharacterList";
    private Image img_CharacterInfoIcon;
    private TextMeshProUGUI text_CharacterInfoName;
    private Button btn_StartGame;
    private Transform trans_CharactersParent;
    public GameObject prefab_Character;
    public List<CharacterData_SO> allCharactersData = new List<CharacterData_SO>();

    private void Awake() {
        img_CharacterInfoIcon = transform.Find(path_CharacterInfoIcon).GetComponent<Image>();
        text_CharacterInfoName = transform.Find(path_CharacterInfoName).GetComponent<TextMeshProUGUI>();
        btn_StartGame = transform.Find(path_Btn_StartGame).GetComponent<Button>();
        trans_CharactersParent = transform.Find(path_CharactersParent);
    }

    private void OnEnable() {
        EventManager.instance.onUpdateSelectCharacterInfo += UpdateSelectCharacterInfo;
        btn_StartGame.onClick.AddListener(OnBtnStartGameClick);
        GenerateAllCharacters();
    }

    private void OnDisable() {
        EventManager.instance.onUpdateSelectCharacterInfo -= UpdateSelectCharacterInfo;
        btn_StartGame.onClick.RemoveAllListeners();
    }

    public override void InitUI()
    {
        uiID = UIID.CharacterSelectMenu;
    }

    private void GenerateAllCharacters()
    {
        if(trans_CharactersParent.childCount > 0)
        {
            ClearAllCharacters();
        }
        for(int i = 0; i < allCharactersData.Count; i++)
        {
            Item_Character itemCharacter = Instantiate(prefab_Character,trans_CharactersParent).GetComponent<Item_Character>();
            itemCharacter.CharacterData = allCharactersData[i];
            itemCharacter.InitUIInfo();
        }
    }

    private void ClearAllCharacters()
    {
        for(int i = 0; i < trans_CharactersParent.childCount; i++)
        {
            Destroy(trans_CharactersParent.GetChild(i).gameObject);
            i--;
        }
    }

    private void OnBtnStartGameClick()
    {
        CloseUI();
        EventManager.instance.OnStartGame();
    }

    private void UpdateSelectCharacterInfo(CharacterData_SO data)
    {
        img_CharacterInfoIcon.sprite = data.CharacterIcon;
        text_CharacterInfoName.text = data.CharacterName;
    }
}