using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using RDG;
public class CharacterController : MonoBehaviour
{
    Animator characterAnimator;

    //attackerSystem
    float timer;
    public float attackCooldown;
    public GameObject[] foods;
    public Transform attackStartPos, attackEndPos;

    public GameObject circleRoadGo;


    public GameObject[] fakeFood;


    public GameObject[] npcGo;

    //levelSystem
    public int npcLevel;
    public int foodLevel;
    public int areaLevel;

    public Button npcLevelButton, foodLevelButton, areaLevelButton;

    public int money;
    public TextMeshProUGUI moneyText, npcLevelText, foodLevelText, areaLevelText,npcMoneyText,foodMoneyText,areaMoneyText;

    //settingsSystem
    public GameObject settingsPannel;
    public GameObject soundOnButton, soundOffButton, vibrationOnButton, vibrationOffButton;
    public bool vibrationBool, soundBool;


    float dontTouchTimer;
    AudioSource sourceAudio;
    void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, true);
        Application.targetFrameRate = 100;
        timer = attackCooldown;
        characterAnimator = GetComponent<Animator>();
        LevelSystem();
        moneyText.text = money.ToString();
        sourceAudio = GetComponent<AudioSource>();
    }

    void LevelSystem()
    {
        dontTouchTimer = 10;
        npcLevelButton.gameObject.SetActive(true);
        foodLevelButton.gameObject.SetActive(true);
        areaLevelButton.gameObject.SetActive(true);
        settingsPannel.SetActive(false);
        vibrationBool = (PlayerPrefs.GetInt("vibration") != 0);
        soundBool = (PlayerPrefs.GetInt("sound") != 0);
        if (vibrationBool==false)
        {
            vibrationOnButton.SetActive(true);
        }
        if (vibrationBool==true)
        {
            vibrationOffButton.SetActive(true);
        }
        if (soundBool==false)
        {
            soundOnButton.SetActive(true);
        }
        if (soundBool==true)
        {
            soundOffButton.SetActive(true);
        }
        money = PlayerPrefs.GetInt("money");
        areaLevel = PlayerPrefs.GetInt("areaLevel");
        if (areaLevel==0)
        {
            areaLevel = 1;
            PlayerPrefs.SetInt("areaLevel", areaLevel);
        }
        npcLevel = PlayerPrefs.GetInt("npcLevel");
        if (npcLevel == 0)
        {
            npcLevel = 1;
            PlayerPrefs.SetInt("npcLevel", npcLevel);
        }
        foodLevel = PlayerPrefs.GetInt("foodLevel");
        if (foodLevel == 0)
        {
            foodLevel = 1;
            PlayerPrefs.SetInt("foodLevel", foodLevel);
        }
        for (int i = 0; i < npcLevel; i++)
        {
            npcGo[i].SetActive(true);
        }
        circleRoadGo.transform.localScale = new Vector3(43 + (areaLevel * 20), 43 + (areaLevel * 20), 63);

        if (areaLevel<2  && npcLevel>=4 || money<npcLevel*500)
        {
            npcLevelButton.interactable = false;
        }
        else
        {
            npcLevelButton.interactable= true;
        }
        if (money<areaLevel*2000)
        {
            areaLevelButton.interactable = false;
        }
        else
        {
            areaLevelButton.interactable= true;
        }
        if (money<foodLevel*250)
        {
            foodLevelButton.interactable = false;
        }
        else
        {
            foodLevelButton.interactable = true;
        }
        npcLevelText.text = npcLevel.ToString();
        foodLevelText.text=foodLevel.ToString();
        areaLevelText.text = areaLevel.ToString();
        npcMoneyText.text=(npcLevel*500).ToString();
        foodMoneyText.text = (foodLevel * 250).ToString();
        areaMoneyText.text = (areaLevel * 2000).ToString();
        fakeFood[foodLevel].SetActive(true);

    }
    public void AttackSystem()
    {
        if (timer >= attackCooldown)
        {
            if (soundBool==false)
            {
                sourceAudio.Play();
            }
            if (vibrationBool==false)
            {
                Vibration.Vibrate(30, -1);
            }
            dontTouchTimer = 0;
            characterAnimator.Play("ShootAnim");
            timer = 0;
            StartCoroutine(timers());
        }

        IEnumerator timers()
        {
            npcLevelButton.gameObject.GetComponent<Animator>().Play("CustomerEndAnim");
            foodLevelButton.gameObject.GetComponent<Animator>().Play("FoodEndAnim");
            areaLevelButton.gameObject.GetComponent<Animator>().Play("AreaEndAnim");
            yield return new WaitForSeconds(1);
            npcLevelButton.gameObject.SetActive(false);
            foodLevelButton.gameObject.SetActive(false);
            areaLevelButton.gameObject.SetActive(false);
        }
    }

    public void InsantiateShoot()
    {
        fakeFood[foodLevel].SetActive(false);
        GameObject foodGo = Instantiate(foods[foodLevel], attackStartPos.position, Quaternion.identity);
        foodGo.transform.DOMove(attackEndPos.position, .75f);
        Destroy(foodGo, .7f);
    }
    public void foodActive()
    {
        //fakeFood[1].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        dontTouchTimer+= Time.deltaTime;
        if (dontTouchTimer >= 5) 
        {
            npcLevelButton.gameObject.SetActive(true);
            foodLevelButton.gameObject.SetActive(true);
            areaLevelButton.gameObject.SetActive(true);
        }
        if (areaLevel < 2 && npcLevel >= 4 || money < npcLevel * 500)
        {
            npcLevelButton.interactable = false;
        }
        else
        {
            npcLevelButton.interactable = true;
        }
        if (npcLevel>=8)
        {
            npcLevelButton.interactable = false;
        }
        if (money < areaLevel * 2000)
        {
            areaLevelButton.interactable = false;
        }
        else
        {
            areaLevelButton.interactable = true;
        }
        if (money < foodLevel * 250 || foodLevel>=3)
        {
            foodLevelButton.interactable = false;
        }
        else
        {
            foodLevelButton.interactable = true;
        }
        if (areaLevel>=2)
        {
            areaLevelButton.interactable = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            money += 1000;
            PlayerPrefs.SetInt("money", money);
            moneyText.text = money.ToString();
        }
        if (timer >= attackCooldown)
        {
            fakeFood[foodLevel].SetActive(true);

        }
        circleRoadGo.transform.Rotate(new Vector3(0, 0, -40 * Time.deltaTime));

        timer += Time.deltaTime;
    }

    public void areaLevelSystem()
    {
        money -= areaLevel * 2000;
        areaLevel++;
        PlayerPrefs.SetInt("areaLevel",areaLevel);
        circleRoadGo.transform.localScale = new Vector3(43 + (areaLevel * 20), 43 + (areaLevel * 20), 63);
        npcLevelText.text = npcLevel.ToString();
        foodLevelText.text = foodLevel.ToString();
        areaLevelText.text = areaLevel.ToString();
        npcMoneyText.text = (npcLevel * 500).ToString();
        foodMoneyText.text = (foodLevel * 250).ToString();
        areaMoneyText.text = (areaLevel * 2000).ToString();
        moneyText.text = money.ToString();

    }
    public void customerLevelSystem()
    {
        money -= npcLevel * 500;
        npcLevel++;
        PlayerPrefs.SetInt("npcLevel", npcLevel);
        npcGo[npcLevel-1].SetActive(true);
        npcLevelText.text = npcLevel.ToString();
        foodLevelText.text = foodLevel.ToString();
        areaLevelText.text = areaLevel.ToString();
        npcMoneyText.text = (npcLevel * 500).ToString();
        foodMoneyText.text = (foodLevel * 250).ToString();
        areaMoneyText.text = (areaLevel * 2000).ToString();
        moneyText.text = money.ToString();

    }
    public void foodLevelSystem()
    {
        fakeFood[foodLevel].SetActive(false);
        money -= foodLevel * 250;
        foodLevel++;
        PlayerPrefs.SetInt("foodLevel", foodLevel);
        npcLevelText.text = npcLevel.ToString();
        foodLevelText.text = foodLevel.ToString();
        areaLevelText.text = areaLevel.ToString();
        npcMoneyText.text = (npcLevel * 500).ToString();
        foodMoneyText.text = (foodLevel * 250).ToString();
        areaMoneyText.text = (areaLevel * 2000).ToString();
        moneyText.text = money.ToString();

    }


    public void vibrationOnSystem()
    {
        vibrationBool = false;
        PlayerPrefs.SetInt("vibration", (vibrationBool ? 1 : 0));
        vibrationOnButton.SetActive(true);
        vibrationOffButton.SetActive(false);
    }
    public void vibrationOffSystem()
    {
        vibrationBool = true;
        PlayerPrefs.SetInt("vibration", (vibrationBool ? 1 : 0));
        vibrationOnButton.SetActive(false);
        vibrationOffButton.SetActive(true);
    }
    public void soundOnSystem()
    {
        soundBool = false;
        PlayerPrefs.SetInt("sound", (soundBool ? 1 : 0));
        soundOnButton.SetActive(true);
        soundOffButton.SetActive(false);
    }
    public void soundOffSystem()
    {
        soundBool = true;
        PlayerPrefs.SetInt("sound", (soundBool ? 1 : 0));
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
    }
    public void settingsPannelOff()
    {
        settingsPannel.SetActive(false);
    }
    public void settingsPannelOn()
    {
        settingsPannel.SetActive(true);
    }
}