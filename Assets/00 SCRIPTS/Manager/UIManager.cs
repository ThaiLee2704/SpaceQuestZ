using System.Collections;
using UnityEngine;

public enum ePanelType
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
    private ePanelType panelToReturn;

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;

    private void OnEnable()
    {
        OpenPanel(ePanelType.MainMenuPanel);

        Observer.AddListener(CONSTANT.OBSERVER_PLAYERDEATH, OnGameOver);
    }

    private void OnDisable()
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

    public void OpenPanel(ePanelType type)
    {
        levelsPanel.SetActive(false);
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);

        switch (type)
        {
            case ePanelType.MainMenuPanel:
                mainMenuPanel.SetActive(true);
                break;
            case ePanelType.LevelsPanel:
                levelsPanel.SetActive(true);
                break;
            case ePanelType.SettingPanel:
                settingPanel.SetActive(true);
                break;
            case ePanelType.HelpPanel:
                helpPanel.SetActive(true);
                break;
            case ePanelType.PausePanel:
                pausePanel.SetActive(true);
                break;
            case ePanelType.GameOver:
                gameOverPanel.SetActive(true);
                break;
            case ePanelType.GameWin:
                gameWinPanel.SetActive(true);
                break;
        }
    }
    public void OnPlayGameClicked()
    {
        panelToReturn = ePanelType.MainMenuPanel;

        AudioManager.Instant.StopMainMenuSound();
        OpenPanel(ePanelType.LevelsPanel);
    }
    public void OnSettingOfMainMenuClicked()
    {
        panelToReturn = ePanelType.MainMenuPanel;

        OpenPanel(ePanelType.SettingPanel);
    }
    public void OnSettingOfPauseClicked()
    {
        panelToReturn = ePanelType.PausePanel;

        OpenPanel(ePanelType.SettingPanel);
    }
    public void OnHelpClicked()
    {
        panelToReturn = ePanelType.MainMenuPanel;

        OpenPanel(ePanelType.HelpPanel);
    }
    public void OnQuitClicked()
    {
        Debug.Log("Đã thoát game!"); 
        Application.Quit();          
    }
    public void OnPauseClicked()
    {
        AudioManager.Instant.PlayPauseSound();
        OpenPanel(ePanelType.PausePanel);
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
        LevelSelectionManager.Instant.DestroyCurrentLevel();

        // 2. Mở Main Menu lên
        HideAllPanel();
        mainMenuPanel.SetActive(true);

        // 3. Trả lại thời gian (để animation ở menu còn chạy nếu có)
        Time.timeScale = 1f;
    }
    public void OnBackClicked()
    {
        // Kiểm tra xem cần quay về đâu
        if (panelToReturn == ePanelType.PausePanel)
        {
            OpenPanel(ePanelType.PausePanel);
        }
        else if (panelToReturn == ePanelType.MainMenuPanel) // Giả sử bạn có logic này từ MainMenu
        {
            if (levelsPanel.activeSelf)
            {
                levelsPanel.SetActive(false);
                AudioManager.Instant.PlayMainMenuSound();
            }
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
        AudioManager.Instant.PlayGameOverSound();
        OpenPanel(ePanelType.GameOver);
        LevelSelectionManager.Instant.DestroyCurrentLevel();
    }
    public void OnGameWin()
    {
        LevelSelectionManager.Instant.DestroyCurrentLevel();
        AudioManager.Instant.PlayGameWinSound();
        OpenPanel(ePanelType.GameWin);
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
