using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Instance is null in UI Mnager");
            }

            return _instance;
        }
    }

    [SerializeField]
    private Light _light;
    [SerializeField]
    private Text _score, _currentWeapon;
    [SerializeField]
    private GameObject[] _weapons;
    [SerializeField]
    private GameObject _pausMenu;

    private int _lightPref;
    private int _prevWeapon;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _lightPref = PlayerPrefs.GetInt("Brightness");
    }

    private void Update()
    {
        switch (_lightPref)
        {
            case 0:
                _light.intensity = 1.2f;
                break;
            case 1:
                _light.intensity = 1.3f;
                break;
            case 2:
                _light.intensity = 1.4f;
                break;
            case 3:
                _light.intensity = 1.5f;
                break;
            case 4:
                _light.intensity = 1.6f;
                break;
            case 5:
                _light.intensity = 1.7f;
                break;
            case 6:
                _light.intensity = 1.8f;
                break;
        }
    }

    public void ScoreUpdate(int score)
    {
        _score.text = "" + score;
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlayButtonClip();
        Time.timeScale = 0;
        _pausMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        AudioManager.Instance.PlayButtonClip();
        _pausMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        AudioManager.Instance.PlayButtonClip();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitToMainMenu()
    {
        AudioManager.Instance.PlayButtonClip();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CurrentWeaponUpdate(int currentWeapon)
    {
        _weapons[_prevWeapon].SetActive(false);
        if (currentWeapon == 3)
        {
            _currentWeapon.text = "MAX";
        }
        else
        {
            _currentWeapon.text = "" + currentWeapon;
        }
        _weapons[currentWeapon - 1].SetActive(true);
        _prevWeapon = currentWeapon - 1;
    }
}
