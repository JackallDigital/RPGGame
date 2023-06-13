using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake() {
        if(GameManager.instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();


        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices = new List<int> { 100, 200, 300, 400, 500, 1000, 1200, 1400, 1600, 1800, 3000, 3500, 4000, 8000, 16000, 50000};
    public List<int> experienceTable;
    public List<string> keyInInventory;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coins;
    public int experience;

    //Floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon() {
        //is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if(coins >= weaponPrices[weapon.weaponLevel]) {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience System
    public int GetCurrentLevel() {
        int getLevel = 0;
        int add = 0;

        while(experience >= add) {
            add += experienceTable[getLevel];
            getLevel++;

            //check if we are max level
            if(getLevel == experienceTable.Count) {
                return getLevel;
            }
        }
        return getLevel;
    }

    public int GetExperienceToLevel(int level) {
        int i = 0;
        int xp = 0;

        while(i < level) {
            xp += experienceTable[i];
            i++;
        }
        return xp;
    }

    public void GrantExperience(int xp) {
        int currentLevel = GetCurrentLevel();
        experience += xp;

        if(currentLevel <= GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    public void OnLevelUp() {
        player.OnLevelUp();
    }

    //Save state
    /*
     * INT preferedSKin
     * INT coins
     * INT experience
     * INT weaponLEvel
     */
    public void SaveState() {
        string saving = "";

        saving += "0" + "|";
        saving += coins.ToString() + "|";
        saving += experience.ToString() + "|";
        saving += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", saving);
    }

    //Load state
    public void LoadState(Scene s, LoadSceneMode mode) {
        Debug.Log("Load state");
        //if we don't have a save state return
        if (!PlayerPrefs.HasKey("SaveState")) {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //TODO Change player skin

        coins = int.Parse(data[1]);

        //Experience
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Weapon
        weapon.SetWeaponLevel(int.Parse(data[3]));

        player.transform.position = GameObject.Find("SpawnPosition").transform.position;

    }
}
