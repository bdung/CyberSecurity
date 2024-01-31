using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    private static HomeManager instance;                             //instance variable
    public static HomeManager Instance { get => instance; }

    [Header("Object Flags")]
    public GameObject flagPolice;
    public GameObject flagDegree1;
    public GameObject flagDegree2;
    public GameObject flagDegree3;
    public GameObject flagDegree4;
    public GameObject policeCharacter;
    public GameObject playerCharacter;
    [Header("Degree ")]
    public GameObject degree1Object;
    public GameObject degree2Object;
    public GameObject degree3Object;
    public GameObject degree4Object;
    [Header("NPC ")]
    public GameObject NPCDegree1Obect;
    public GameObject NPCDegree2Obect;
    public GameObject NPCDegree3Obect;
    public GameObject NPCDegree4Obect;
    [Header("Level up")]
    public GameObject LevelUpObject;
    public GameObject PassedDegree1Panel;
    public GameObject PassedDegree2Panel;
    public GameObject PassedDegree3Panel;
    public GameObject PassedDegree4Panel;
    [Header("Profiles")]
    public GameObject ProfilesObject;
    public GameObject badgeOfDegree1Object;
    public GameObject badgeOfDegree2Object;
    public GameObject badgeOfDegree3Object;
    public GameObject badgeOfDegree4Object;
    public TMP_Text usernameText;
    public TMP_Text experienceText;
    public TMP_Text levelText;
    public TMP_Text usernameToolbarText;
    public TMP_Text levelToolbarText;
    public TMP_Text experienceToolbarText;


    [Header("Button")]
    public GameObject btnJoin;
    // public GameObject btnExitGame;
    public bool isOpenBtn = false;
    // public GameObject flagDegree3;
    // public GameObject flagDegree4;

    public bool isOpenPoliceCharacter = true;
    public int NPC = -1;

    public int id_level;

    // Start is called before the first frame update
    void Start()
    {
        // flagPolice.SetActive(false);
        // flagDegree1.SetActive(false);
        // flagDegree2.SetActive(false);
        // flagDegree3.SetActive(false);
        // flagDegree4.SetActive(false);



        // btnJoin.SetActive(false);
        // NPC = -1;
        // btnExitGame.SetActive(false);
        id_level = APIUser.Instance.GetUser().id_level;
        UpdateProfile();
    }
    private void Awake()
    {
        if (instance == null)                                               //if instance is null
        {
            instance = this;                                                //set this as instance
            // DontDestroyOnLoad(gameObject);                                  //make it DontDestroyOnLoad
        }

    }


    // profile object
    public void UpdateProfile()
    {
        usernameText.text = usernameToolbarText.text = APIUser.Instance.GetUser().username;
        levelText.text = levelToolbarText.text = APIUser.Instance.GetUser().id_level.ToString();
        experienceText.text = experienceToolbarText.text = APIUser.Instance.GetUser().experience.ToString();

        if (id_level == 2)
        {
            badgeOfDegree1Object.SetActive(true);
        }
        if (id_level == 3)
        {
            badgeOfDegree2Object.SetActive(true);
        }
        if (id_level == 4)
        {
            badgeOfDegree3Object.SetActive(true);
        }
        if (id_level == 5)
        {
            badgeOfDegree4Object.SetActive(true);
        }

    }
    public void Btn_display_Profile()
    {
        Debug.Log("Btn_display_Profile");
        ProfilesObject.SetActive(true);
    }
    public void Btn_Exit_Profile()
    {
        Debug.Log("Btn_Exit_Profile");
        ProfilesObject.SetActive(false);
    }
    public void Btn_TurnOffSound_Profile()
    {
        ProfilesObject.SetActive(false);
    }
    public void Btn_Share_Profile()
    {
        ProfilesObject.SetActive(false);
    }
    public void Btn_Join()
    {
        Debug.Log("indexNPC: " + NPC);

        switch (NPC)
        {

            case -1:
                {
                    ActiveHomePage.Instance.isOpenFlagPolice = true;
                    APIUser.Instance.UpdateIdLevel(0);
                    id_level = 0;
                    FindAnyObjectByType<Dialogue>().EndDialogue();

                    break;
                }
            // police 
            case 0:
                {
                    switch (id_level)
                    {
                        case 0:
                            {
                                Debug.Log("Enter beginning game");
                                APIUser.Instance.UpdateIsOpenStartGame(true);
                                APIUser.Instance.UpdateIdLevel(1);
                                id_level = 1;
                                ActiveHomePage.Instance.isOpenFlagDegree1 = true;
                                ActiveHomePage.Instance.isOpenFlagPolice = false;

                                break;
                            }
                        case 1:
                            {
                                APIUser.Instance.UpdateIsOpenDegree1(true);
                                ActiveHomePage.Instance.isOpenFlagDegree1 = true;
                                break;
                            }
                        case 2:
                            {
                                APIUser.Instance.UpdateIsOpenDegree2(true);
                                ActiveHomePage.Instance.isOpenFlagDegree2 = true;
                                break;
                            }
                        case 3:
                            {
                                APIUser.Instance.UpdateIsOpenDegree3(true);
                                ActiveHomePage.Instance.isOpenFlagDegree3 = true;
                                break;
                            }
                        case 4:
                            {
                                APIUser.Instance.UpdateIsOpenDegree4(true);
                                ActiveHomePage.Instance.isOpenFlagDegree4 = true;

                                break;
                            }
                    }
                    FindAnyObjectByType<Dialogue>().EndDialogue();

                    break;
                }
            case 1:
                {

                    APIUser.Instance.UpdateIsOpenDegree1(true);

                    // scene play game degree1
                    StartCoroutine(SetActiveDialogue("Degree1Game2"));
                    break;
                }
            case 2:
                {

                    APIUser.Instance.UpdateIsOpenDegree2(true);

                    // scene play game degree1
                    StartCoroutine(SetActiveDialogue("TypingHome"));
                    break;
                }
            case 3:
                {

                    APIUser.Instance.UpdateIsOpenDegree2(true);

                    // scene play game degree1
                    StartCoroutine(SetActiveDialogue("Degree3Game1"));
                    // scene play game degree1

                    break;
                }
        }
    }
    IEnumerator SetActiveDialogue(string nameScene)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nameScene);

    }

    public void ClosePanelPassedDegree()
    {
        PassedDegree1Panel.SetActive(false);
        PassedDegree2Panel.SetActive(false);
        PassedDegree3Panel.SetActive(false);
        PassedDegree4Panel.SetActive(false);
        LevelUpObject.SetActive(false);

    }
    public void Btn_NextLevelUp()
    {
        Debug.Log("Exit level up");
        if (id_level < 5)
        {
            APIUser.Instance.UpdateIdLevel(id_level + 1);
            id_level += 1;

            ActiveHomePage.Instance.isOpenFlagPolice = true;
        }
        PassedDegree1Panel.SetActive(false);
        PassedDegree2Panel.SetActive(false);
        PassedDegree3Panel.SetActive(false);
        PassedDegree4Panel.SetActive(false);
        LevelUpObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        // Update profile information

        UpdateProfile();

        if (id_level == 1)
        {
            degree1Object.SetActive(true);
            degree2Object.SetActive(false);
            degree3Object.SetActive(false);
            degree4Object.SetActive(false);

            NPCDegree1Obect.SetActive(true);
            NPCDegree2Obect.SetActive(false);
            NPCDegree3Obect.SetActive(false);
            NPCDegree4Obect.SetActive(false);
        }
        if (id_level == 2)
        {
            degree1Object.SetActive(true);
            degree2Object.SetActive(true);
            degree3Object.SetActive(false);
            degree4Object.SetActive(false);

            NPCDegree1Obect.SetActive(true);
            NPCDegree2Obect.SetActive(true);
            NPCDegree3Obect.SetActive(false);
            NPCDegree4Obect.SetActive(false);
        }
        if (id_level == 3)
        {
            degree1Object.SetActive(true);
            degree2Object.SetActive(true);
            degree3Object.SetActive(true);
            degree4Object.SetActive(false);

            NPCDegree1Obect.SetActive(true);
            NPCDegree2Obect.SetActive(true);
            NPCDegree3Obect.SetActive(true);
            NPCDegree4Obect.SetActive(false);
        }
        if (id_level == 4)
        {
            degree1Object.SetActive(true);
            degree2Object.SetActive(true);
            degree3Object.SetActive(true);
            degree4Object.SetActive(true);

            NPCDegree1Obect.SetActive(true);
            NPCDegree2Obect.SetActive(true);
            NPCDegree3Obect.SetActive(true);
            NPCDegree4Obect.SetActive(true);
        }
        if (ActiveHomePage.Instance.isOpenFlagPolice)
        {
            flagPolice.SetActive(true);

        }
        else
        {
            flagPolice.SetActive(false);
        }
        if (ActiveHomePage.Instance.isOpenFlagDegree1)
        {
            flagDegree1.SetActive(true);

        }
        else
        {
            flagDegree1.SetActive(false);
        }
        if (ActiveHomePage.Instance.isOpenFlagDegree2)
        {
            flagDegree2.SetActive(true);

        }
        else
        {
            flagDegree2.SetActive(false);
        }
        if (ActiveHomePage.Instance.isOpenFlagDegree3)
        {
            flagDegree3.SetActive(true);

        }
        else
        {
            flagDegree3.SetActive(false);
        }
        if (ActiveHomePage.Instance.isOpenFlagDegree4)
        {
            flagDegree4.SetActive(true);

        }
        else
        {
            flagDegree4.SetActive(false);
        }

        if (isOpenPoliceCharacter)
        {
            policeCharacter.SetActive(true);
            playerCharacter.SetActive(false);
        }
        else
        {
            policeCharacter.SetActive(false);
            playerCharacter.SetActive(true);
        }
        if (isOpenBtn)
        {
            btnJoin.SetActive(true);
            // btnExitGame.SetActive(true);
        }
        else
        {
            btnJoin.SetActive(false);
            // btnExitGame.SetActive(false);
        }
        // open dialog level up
        if (APIUser.Instance.GetUser().experience >= 10000 && id_level == 4)
        {
            PassedDegree4Panel.SetActive(true);
        }
        else if (APIUser.Instance.GetUser().experience >= 8000 && id_level == 3)
        {
            PassedDegree3Panel.SetActive(true);
        }
        else if (APIUser.Instance.GetUser().experience >= 4000 && id_level == 2)
        {
            PassedDegree2Panel.SetActive(true);
        }
        else if (APIUser.Instance.GetUser().experience >= 2000 && id_level == 1)
        {
            PassedDegree1Panel.SetActive(true);
        }
        if (id_level == 5)
        {
            LevelUpObject.SetActive(true);
        }
    }
}