using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerM : MonoBehaviour
{
    private static UIManagerM _instance;
    public static UIManagerM Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Instance is null in UI Mnager M");
            }

            return _instance;
        }
    }

    [SerializeField]
    private GameObject _optionsMenu;
    [SerializeField]
    private GameObject[] _brightness, _sound;
   
    private bool _toggleBrightness = false;
    private int arrLength;
    private bool _toggleSound = false;
    private int arrLengthS;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetString("IsFirst") == "")
        {
            PlayerPrefs.SetString("IsFirst", "False"); 
            arrLength = 3;
            PlayerPrefs.SetInt("Brightness", arrLength);
            PlayerPrefs.SetInt("Sound", arrLength);
        }
        else
        {
            arrLength = PlayerPrefs.GetInt("Brightness");
            arrLengthS = PlayerPrefs.GetInt("Sound");
        }
        SwitchSound();
        SwitchBrightness();
    }

    void SwitchSound()
    {
        int _lightPrefSound = PlayerPrefs.GetInt("Sound");

        switch (_lightPrefSound)
        {
            case 0:
                _sound[0].SetActive(false);
                _sound[1].SetActive(false);
                _sound[2].SetActive(false);
                _sound[3].SetActive(false);
                _sound[4].SetActive(false);
                _sound[5].SetActive(false);
                break;
            case 1:
                _sound[0].SetActive(true);
                _sound[1].SetActive(false);
                _sound[2].SetActive(false);
                _sound[3].SetActive(false);
                _sound[4].SetActive(false);
                _sound[5].SetActive(false);
                break;
            case 2:
                _sound[0].SetActive(true);
                _sound[1].SetActive(true);
                _sound[2].SetActive(false);
                _sound[3].SetActive(false);
                _sound[4].SetActive(false);
                _sound[5].SetActive(false);
                break;
            case 3:
                _sound[0].SetActive(true);
                _sound[1].SetActive(true);
                _sound[2].SetActive(true);
                _sound[3].SetActive(false);
                _sound[4].SetActive(false);
                _sound[5].SetActive(false);
                break;
            case 4:
                _sound[0].SetActive(true);
                _sound[1].SetActive(true);
                _sound[2].SetActive(true);
                _sound[3].SetActive(true);
                _sound[4].SetActive(false);
                _sound[5].SetActive(false);
                break;
            case 5:
                _sound[0].SetActive(true);
                _sound[1].SetActive(true);
                _sound[2].SetActive(true);
                _sound[3].SetActive(true);
                _sound[4].SetActive(true);
                _sound[5].SetActive(false);
                break;
            case 6:
                _sound[0].SetActive(true);
                _sound[1].SetActive(true);
                _sound[2].SetActive(true);
                _sound[3].SetActive(true);
                _sound[4].SetActive(true);
                _sound[5].SetActive(true);
                break;
        }
    }

    void SwitchBrightness()
    {
        int _lightPrefBrightness = PlayerPrefs.GetInt("Brightness");

        switch (_lightPrefBrightness)
        {
            case 0:
                _brightness[0].SetActive(false);
                _brightness[1].SetActive(false);
                _brightness[2].SetActive(false);
                _brightness[3].SetActive(false);
                _brightness[4].SetActive(false);
                _brightness[5].SetActive(false);
                break;
            case 1:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(false);
                _brightness[2].SetActive(false);
                _brightness[3].SetActive(false);
                _brightness[4].SetActive(false);
                _brightness[5].SetActive(false);
                break;
            case 2:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(true);
                _brightness[2].SetActive(false);
                _brightness[3].SetActive(false);
                _brightness[4].SetActive(false);
                _brightness[5].SetActive(false);
                break;
            case 3:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(true);
                _brightness[2].SetActive(true);
                _brightness[3].SetActive(false);
                _brightness[4].SetActive(false);
                _brightness[5].SetActive(false);
                break;
            case 4:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(true);
                _brightness[2].SetActive(true);
                _brightness[3].SetActive(true);
                _brightness[4].SetActive(false);
                _brightness[5].SetActive(false);
                break;
            case 5:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(true);
                _brightness[2].SetActive(true);
                _brightness[3].SetActive(true);
                _brightness[4].SetActive(true);
                _brightness[5].SetActive(false);
                break;
            case 6:
                _brightness[0].SetActive(true);
                _brightness[1].SetActive(true);
                _brightness[2].SetActive(true);
                _brightness[3].SetActive(true);
                _brightness[4].SetActive(true);
                _brightness[5].SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (_toggleBrightness)
        {
           
            if (Input.GetKeyDown(KeyCode.LeftArrow) && arrLength > 0)
            {
                _brightness[arrLength - 1].SetActive(false);
                arrLength--;
                PlayerPrefs.SetInt("Brightness", arrLength);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && arrLength < 6)
            {
                _brightness[arrLength].SetActive(true);
                arrLength++;
                PlayerPrefs.SetInt("Brightness", arrLength);
            }
        }
        else if (_toggleSound)
        {

            if (Input.GetKeyDown(KeyCode.LeftArrow) && arrLengthS > 0)
            {
                _sound[arrLengthS - 1].SetActive(false);
                arrLengthS--;
                PlayerPrefs.SetInt("Sound", arrLengthS);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && arrLengthS < 6)
            {
                _sound[arrLengthS].SetActive(true);
                arrLengthS++;
                PlayerPrefs.SetInt("Sound", arrLengthS);
            }
        }
    }

    public void StartGame()
    {
        AudioManagerM.Instance.PlayButtonClip();
        SceneManager.LoadScene(1);
    }

    public void LoadOptions()
    {
        AudioManagerM.Instance.PlayButtonClip();
        _optionsMenu.SetActive(true);
    }

    public void ToggleBrightness()
    {
        AudioManagerM.Instance.PlayButtonClip();
        _toggleBrightness = !_toggleBrightness;
    }

    public void ToggleSound()
    {
        AudioManagerM.Instance.PlayButtonClip();
        _toggleSound = !_toggleSound;
    }

    public void UnloadOptions()
    {
        AudioManagerM.Instance.PlayButtonClip();
        _optionsMenu.SetActive(false);
        _toggleBrightness = false;
        _toggleSound = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
