using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PanelType
{
    MainMenuPanel,
    LevelsPanel,
    SettingPanel,
    HelpPanel,
    PausePanel
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject pausePanel;

    private PanelType _panelToReturn;

    private void Start()
    {
        OpenPanel(PanelType.MainMenuPanel);
    }

    public void HideAllPanel()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OpenPanel(PanelType type)
    {
        levelsPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
        pausePanel.SetActive(false);

        switch (type)
        {
            case PanelType.MainMenuPanel:
                mainMenuPanel.SetActive(true);
                break;
            case PanelType.LevelsPanel:
                levelsPanel.SetActive(true);
                break;
            case PanelType.SettingPanel:
                settingPanel.SetActive(true);
                break;
            case PanelType.HelpPanel:
                helpPanel.SetActive(true);
                break;
            case PanelType.PausePanel:
                pausePanel.SetActive(true);
                break;
        }
    }
    public void OnPlayGameClicked()
    {
        _panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.LevelsPanel);
    }
    public void OnSettingOfMainMenuClicked()
    {
        _panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.SettingPanel);
    }
    public void OnSettingOfPauseClicked()
    {
        _panelToReturn = PanelType.PausePanel;

        OpenPanel(PanelType.SettingPanel);
    }
    public void OnHelpClicked()
    {
        _panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.HelpPanel);
    }
    public void OnQuitClicked()
    {
        Debug.Log("Đã thoát game!"); 
        Application.Quit();          
    }
    public void OnPauseClicked()
    {
        OpenPanel(PanelType.PausePanel);
        Time.timeScale = 0f;
    }
    public void OnResumeClicked()
    {
        HideAllPanel();
        Time.timeScale = 1f;
    }
    public void OnMainMenuClicked()
    {
        // 1. Bảo LevelManager hủy màn chơi hiện tại đi cho nhẹ máy
        LevelManager.Instant.DestroyCurrentLevel();

        // 2. Mở Main Menu lên
        HideAllPanel();
        mainMenuPanel.SetActive(true);

        // 3. Trả lại thời gian (để animation ở menu còn chạy nếu có)
        Time.timeScale = 1f;
    }

    public void OnBackClicked()
    {
        HideAllPanel();

        // Kiểm tra xem cần quay về đâu
        if (_panelToReturn == PanelType.PausePanel)
        {
            pausePanel.SetActive(true);
            // Vẫn giữ TimeScale = 0
        }
        else if (_panelToReturn == PanelType.MainMenuPanel) // Giả sử bạn có logic này từ MainMenu
        {
            mainMenuPanel.SetActive(true);
        }
    }

    //Xử lý PausePanel bằng Keyword
    public void OnPauseClickedByKeyword()
    {
        //Mỗi lần Active PausePanel thì các GO đang selected sẽ đặt về null
        //Ví dụ như các button ở lần trước khi ấn vào sẽ thành selected và đổi
        //màu qua màu selected.
        EventSystem.current.SetSelectedGameObject(null);

        if (mainMenuPanel.activeSelf) return;
        if (settingPanel.activeSelf) return;

        if (pausePanel.activeSelf == false)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
