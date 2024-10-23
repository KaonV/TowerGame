using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSystem : MonoBehaviour
{
    [SerializeField] private List<MenuData> _menus  = new List<MenuData>();
    [SerializeField] private MenuData _currentMenu  = null;
    [SerializeField] private MenuData _lastMenu     = null;

    private void Start()
    {
        if (_currentMenu == null && _menus.Count > 0) 
            _currentMenu = _menus[0];
    }

    public void OpenMenu(string menuName)
    {
        MenuData data = _menus.Find(e => e.tag == menuName);

        if (data != null)
        {
            if (_lastMenu != null)
                _lastMenu.ChangeState(false);
            _lastMenu = _currentMenu;

            data.ChangeState(true);

            _currentMenu = data;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}

[Serializable]
public class MenuData
{
    public string       tag         = string.Empty;
    public GameObject   panel       = null;
    public UnityEvent   onActivate  = null;
    public float        duration    = 1f;
    public CanvasGroup  cg          = null;

    public CanvasGroup Cg
    {
        get
        {
            if (!panel.GetComponent<CanvasGroup>())
            {
                cg = panel.AddComponent<CanvasGroup>();
                return cg;
            }
            else if (cg == null)
                return cg = panel.GetComponent<CanvasGroup>();
            else return cg;
        }
    }

    public void ChangeState(bool state) 
    {
        Cg.alpha            = state ? 1f : 0f;
        Cg.interactable     = state;
        Cg.blocksRaycasts   = state;
    }
}