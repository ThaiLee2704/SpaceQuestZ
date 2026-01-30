using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PanelType
{
    MainMenuPanel,
    LevelsPanel,
    SettingPanel,
    HelpPanel,
    PausePanel,
    GameOver,
    GameWin
}

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;

    private PanelType panelToReturn;

    private void Start()
    {
        OpenPanel(PanelType.MainMenuPanel);
        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH, OnGameOver);
    }

    private void OnDestroy()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_PLAYERDEATH, OnGameOver);
    }

    public void HideAllPanel()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    public void OpenPanel(PanelType type)
    {
        levelsPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);

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
            case PanelType.GameOver:
                gameOverPanel.SetActive(true);
                break;
            case PanelType.GameWin:
                gameWinPanel.SetActive(true);
                break;
        }
    }
    public void OnPlayGameClicked()
    {
        panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.LevelsPanel);
    }
    public void OnSettingOfMainMenuClicked()
    {
        panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.SettingPanel);
    }
    public void OnSettingOfPauseClicked()
    {
        panelToReturn = PanelType.PausePanel;

        OpenPanel(PanelType.SettingPanel);
    }
    public void OnHelpClicked()
    {
        panelToReturn = PanelType.MainMenuPanel;

        OpenPanel(PanelType.HelpPanel);
    }
    public void OnQuitClicked()
    {
        Debug.Log("Đã thoát game!"); 
        Application.Quit();          
    }
    public void OnPauseClicked()
    {
        AudioManager.Instant.PlayPauseSound();
        OpenPanel(PanelType.PausePanel);
        Time.timeScale = 0f;
    }
    public void OnResumeClicked()
    {
        AudioManager.Instant.PlayUnpauseSound();
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
        //HideAllPanel();

        // Kiểm tra xem cần quay về đâu
        if (panelToReturn == PanelType.PausePanel)
        {
            OpenPanel(PanelType.PausePanel);
        }
        else if (panelToReturn == PanelType.MainMenuPanel) // Giả sử bạn có logic này từ MainMenu
        {
            if (levelsPanel.activeSelf)
                levelsPanel.SetActive(false);
            if (helpPanel.activeSelf)
                helpPanel.SetActive(false);
            if (settingPanel.activeSelf)
                settingPanel.SetActive(false);
        }
    }
    private void OnGameOver(object[] datas)
    {
        StartCoroutine(ShowGameOverPanel());
    }
    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(2);
        OpenPanel(PanelType.GameOver);
        LevelManager.Instant.DestroyCurrentLevel();
    }
    public void OnGameWin()
    {
        LevelManager.Instant.DestroyCurrentLevel();
        OpenPanel(PanelType.GameWin);
    }

    //Xử lý PausePanel bằng Keyword
    public void OnPauseClickedByKeyword()
    {
        if (mainMenuPanel.activeSelf) return;
        if (settingPanel.activeSelf) return;

        if (pausePanel.activeSelf == false)
        {
            AudioManager.Instant.PlayPauseSound();
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            AudioManager.Instant.PlayUnpauseSound();
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
