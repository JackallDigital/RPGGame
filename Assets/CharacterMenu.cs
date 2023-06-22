using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    public TextMeshProUGUI levelText, hitpointText, coinText, upgradeCostText, xperienceText;

    //Logic for images/sprites
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xperienceBar;

    //Character selection
    public void OnArrowClick(bool right) {
        if (right) {
            currentCharacterSelection++;

            //if we went too far, meaning no more characters to select from we need to go back to start
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count) {
                currentCharacterSelection = 0;
            }

            OnSelectionChanged();
        }
        else {
            currentCharacterSelection--;

            //if we went too far, meaning no more characters to select from we need to go back to start
            if (currentCharacterSelection < 0) {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    public void OnSelectionChanged() {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon upgrade
    public void OnUpgradeClick() {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    //Update the character information
    public void UpdateMenu() {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];

        if(GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count) {
            upgradeCostText.text = "MAX LV";
        }
        else {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }
        
        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint.ToString() +"/"+GameManager.instance.player.maxHitPoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();

        //XP bar
        int currentLevel = GameManager.instance.GetCurrentLevel();

        if(currentLevel == GameManager.instance.experienceTable.Count) {
            xperienceText.text = GameManager.instance.experience.ToString() + " total experience points"; //display total xp at max level
            xperienceBar.localScale = Vector3.one;
        }
        else {
            int previousLevelExperience = GameManager.instance.GetExperienceToLevel(currentLevel -1);
            int currentLevelExperience = GameManager.instance.GetExperienceToLevel(currentLevel);
            

            int difference = currentLevelExperience - previousLevelExperience;
            int currentExperienceIntoLevel = GameManager.instance.experience - previousLevelExperience;

            float completionRatio = (float)currentExperienceIntoLevel / (float)difference;
            xperienceBar.localScale = new Vector3(completionRatio, 1, 1);
            xperienceText.text = currentExperienceIntoLevel.ToString() + "/" + difference.ToString();
        }
    }
}
