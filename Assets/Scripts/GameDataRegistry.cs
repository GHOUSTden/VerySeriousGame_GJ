using UnityEngine;
using System.Collections.Generic;

public static class GameDataRegistry
{
    public static Dictionary<string, GamblingChipsSO> chipID { get; private set; }

    private static bool isInitialized = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (isInitialized)
        {
            return;
        }

        ListAllChipsID();

        isInitialized = true;
    }

    private static void ListAllChipsID()
    {
        chipID = new Dictionary<string, GamblingChipsSO>
        {
            ["DROOLINGCAT"] = Resources.Load<GamblingChipsSO>("ScriptableObjects/GamblingChips/DroolingCat"),
            ["PLUSFIVETORANDOMSLICE"] = Resources.Load<GamblingChipsSO>("ScriptableObjects/GamblingChips/PlusFiveToRandomSlice"),
            ["PLUSFIVETOTHEHIGHESTSLICE"] = Resources.Load<GamblingChipsSO>("ScriptableObjects/GamblingChips/PlusFiveToTheHighestSlice"),
            ["PLUSFIVETOTHELOWESTSLICE"] = Resources.Load<GamblingChipsSO>("ScriptableObjects/GamblingChips/PlusFiveToTheLowestSlice"),
        };
    }
}