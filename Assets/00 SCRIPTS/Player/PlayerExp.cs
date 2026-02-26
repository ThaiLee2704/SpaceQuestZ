using System.Collections.Generic;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    [SerializeField] private int experience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] private List<int> playerLevels;

    private void OnEnable()
    {
        Observer.AddListener(CONSTANT.OBSERVER_DROP_EXP, OnGetExp);
    }

    private void OnDisable()
    {
        Observer.RemoveListener(CONSTANT.OBSERVER_DROP_EXP, OnGetExp);
    }

    private void Start()
    {
        SetUpExpLevels();

        experience = 0;
        HUDManager.Instant.UpdateExpSlider(experience, playerLevels[currentLevel]);
    }

    private void SetUpExpLevels()
    {
        if (playerLevels.Count == 0)
        {
            playerLevels.Add(100);
        }

        for (int i = playerLevels.Count; i < maxLevel; i++)
        {
            //Lượng exp level tiếp theo bằng lượng exp level trước đó * 1.1 + 15
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count - 1] * 1.1f + 15));
        }
    }

    private void OnGetExp(object[] datas)
    {
        int expAmount = (int)datas[0];
        GetExp(expAmount);
    }

    public void GetExp(int exp)
    {
        if (currentLevel >= maxLevel - 1 && experience >= playerLevels[currentLevel])
            return;

        experience += exp;

        while (currentLevel < maxLevel - 1 && experience >= playerLevels[currentLevel])
            LevelUp();

        if (currentLevel >= maxLevel - 1 && experience > playerLevels[currentLevel])
            experience = playerLevels[currentLevel];

        HUDManager.Instant.UpdateExpSlider(experience, playerLevels[currentLevel]);
    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel];
        currentLevel++;
        //HUDManager.Instant.UpdateExpSlider(experience, playerLevels[currentLevel]);
        Observer.Notify("playerLevelUp", null);
    }
}
