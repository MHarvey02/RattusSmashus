using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    private int _menuSelection;

    [SerializeField]
    Animator _myAnimator;

    [SerializeField]
    private List<Button> _mainMenuButtons;

    [SerializeField]
    private List<Button> _worldSelectButtons;

    [SerializeField]
    private List<Button> _tutorialButtons;

    [SerializeField]
    private List<Button> _streetsButtons;

    [SerializeField]
    private List<Button> _constructionButtons;

    [SerializeField]
    private List<Button> _subwayButtons;

    [SerializeField]
    private List<Button> _sewerButtons;

    //Tracks which buttons are to be displayed on the screen
    [SerializeField]
    private List<Button> _currentMenu;

    [SerializeField]
    private MenuSounds _mySounds;

    public void Awake()
    {
        _menuSelection = 0;
        _currentMenu = _mainMenuButtons;
    }
    void Start()
    { 
        _currentMenu[_menuSelection].Select();
    }
    //Uses the Y value of the input vector to move through the menu
    public void ChangeMenuOption(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            _mySounds.ChangeOption();
            if (inputContext.ReadValue<Vector2>().y == -1)
            {
                _menuSelection++;
            }
            else
            {
                _menuSelection--;
            }
            CheckMenuLocationIsValid();
            _currentMenu[_menuSelection].Select();     
        } 
    }
    //Make sure a button is selected
    private void CheckMenuLocationIsValid()
    {
        if(_menuSelection >= _currentMenu.Count){ _menuSelection = 0; }
        if(_menuSelection < 0){ _menuSelection = _currentMenu.Count -1; }
    }
    //Moves the menu selection back to the default position
    private void ResetSelect()
    {
        _menuSelection = 0;
        _currentMenu[_menuSelection].Select();
    }
    //Process the choice of the player
    public void Submit(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            _mySounds.Select();
            _currentMenu[_menuSelection].onClick.Invoke();
            
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    //Uses the name of the button to work out the level to load in the scenemangager
    public void PlaySelectedLevel()
    {
        int LevelIndex = Int32.Parse(_currentMenu[_menuSelection].gameObject.name);
        SceneManager.LoadScene(LevelIndex);
    }

    public void OpenLevelSelector()
    {

        _currentMenu = _worldSelectButtons;
        _myAnimator.Play("LevelSelectEnter");
        ResetSelect();
    }

    public void BackToMainMenu()
    {
        _currentMenu = _mainMenuButtons;
        _myAnimator.Play("MainEnter");
        ResetSelect();
    }

    public void OpenTutorialButtons()
    {
        _myAnimator.Play("TutorialEnter");
        _currentMenu = _tutorialButtons;
        ResetSelect();
    }

    public void OpenStreetsButtons()
    {
        _myAnimator.Play("StreetsEnter");
        _currentMenu = _streetsButtons;
        ResetSelect();
    }

    public void OpenConstructionButtons()
    {
        _myAnimator.Play("ConstructionEnter");
        _currentMenu = _constructionButtons;
        ResetSelect();
    }

    public void OpenSubwayButtons()
    {
        _myAnimator.Play("SubwayEnter");
        _currentMenu = _subwayButtons;
        ResetSelect();
    }

    public void OpenSewerButtons()
    {
        _myAnimator.Play("SewerEnter");
        _currentMenu = _sewerButtons;
        ResetSelect();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
