using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSystem : MonoBehaviour
{
    #region - Singleton Pattern -
    public static MenuSystem Instance;
    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        Instance = this;
    }
    #endregion

    [SerializeField] private List<MenuData> _menus        = new List<MenuData>();
    [SerializeField] private MenuData       _currentMenu  = null;
    private MenuData       _lastMenu     = null;

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
            if (_currentMenu != null)
                _lastMenu = _currentMenu;
            if (_lastMenu != null)
                _lastMenu.ChangeState(false);

            _currentMenu = data;
            _currentMenu.ChangeState(true);
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

    private CanvasGroup  cg          = null;

    public CanvasGroup Cg
    {
        get
        {
            if (cg == null)
            {
                if (panel == null)
                {
                    Debug.Log("Panel is null!");
                    return null;
                }

                if (panel.GetComponent<CanvasGroup>())
                    return cg = panel.GetComponent<CanvasGroup>();
                else return cg = panel.AddComponent<CanvasGroup>();
            }
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