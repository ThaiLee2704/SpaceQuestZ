using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionManager : Singleton<LevelSelectionManager>
{
    [SerializeField] private List<GameObject> allLevels;

    private GameObject currentLevel;   // Lưu cái Level đang chạy trong scene
    private GameObject levelPrefab; // Lưu cái bản gốc Prefab để tí nữa Restart
    private int currentLevelIndex = -1;

    // Hàm OpenLevel cũ (Dành cho việc chọn level từ Menu)
    public void OpenLevel(GameObject levelPrefab)
    {
        currentLevelIndex = allLevels.IndexOf(levelPrefab);

        if (currentLevel != null) Destroy(currentLevel);
        this.levelPrefab = levelPrefab;
        OpenLevelAfterCleanup(levelPrefab);
    }

    public void NextLevel()
    {
        if (currentLevelIndex < allLevels.Count - 1)
        {
            currentLevelIndex++;

            GameObject nextLevelPrefabs = allLevels[currentLevelIndex];


            OpenLevel(nextLevelPrefabs);
        }
    }

    // --- Hàm dành cho nút Restart ---
    public void RestartCurrentLevel()
    {
        if (levelPrefab != null)
        {
            StartCoroutine(RestartLevelRoutine());
        }
    }

    private IEnumerator RestartLevelRoutine()
    {
        // 1. Hủy Level cũ
        DestroyCurrentLevel();

        // 2. QUAN TRỌNG NHẤT: Chờ đến cuối frame
        // Để Unity dọn dẹp sạch sẽ cái HUDManager cũ đi đã
        yield return new WaitForEndOfFrame();

        // 3. Giờ mới tạo Level mới (Lúc này HUDManager cũ đã chết hẳn)
        OpenLevelAfterCleanup(levelPrefab);
    }

    // Tách hàm OpenLevel ra để tái sử dụng logic tạo mới
    private void OpenLevelAfterCleanup(GameObject levelPrefab)
    {
        UIManager.Instant.HideAllPanel();

        // Tạo level mới
        currentLevel = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity);

        // Reset thời gian
        Time.timeScale = 1f;
    }

    // --- Hàm dành cho nút Về Main Menu ---
    public void DestroyCurrentLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
            currentLevel = null;
        }
    }
}
