using System.Collections;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private GameObject currentLevel;   // Lưu cái Level đang chạy trong scene
    private GameObject levelPrefab; // Lưu cái bản gốc Prefab để tí nữa Restart

    // Hàm OpenLevel cũ (Dành cho việc chọn level từ Menu)
    public void OpenLevel(GameObject levelPrefab)
    {
        // Logic cũ của bạn: Destroy -> Create ngay lập tức
        // Vẫn dùng được khi đi từ Menu vào, vì lúc đó chưa có HUD nào tồn tại.
        // Nhưng để code gọn, bạn có thể gọi Coroutine luôn cũng được, nhưng nó sẽ trễ 1 frame (không đáng kể).

        if (currentLevel != null) Destroy(currentLevel);
        this.levelPrefab = levelPrefab;
        OpenLevelAfterCleanup(levelPrefab);
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
