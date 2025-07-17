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
    private List<Button> _menuButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _menuSelection = 0;
        _menuButtons[_menuSelection].Select();
    }

    public void ChangeMenuOption(InputAction.CallbackContext inputContext)
    {
        if (inputContext.started)
        {
            if (inputContext.ReadValue<Vector2>().y == -1)
            {
                _menuSelection++;
            }
            else
            {
                _menuSelection--;
            }
            CheckMenuLocationIsValid();
            _menuButtons[_menuSelection].Select();     
        } 
    }

    private void CheckMenuLocationIsValid()
    {
        if(_menuSelection >= _menuButtons.Count){ _menuSelection = 0; }
        if(_menuSelection < 0){ _menuSelection = _menuButtons.Count -1; }
    }

    public void Submit(InputAction.CallbackContext inputContext)
    {
        if (inputContext.canceled)
        {
            _menuButtons[_menuSelection].onClick.Invoke();
        }  
        
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
