using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllLinesController : Controller
{
    public AllLinesController(Model model) : base(model)
    {
    }
    protected override bool AnalyzeWinResult(List<int> state)
    {
        bool isWin =
            state[0] == state[1] && state[0] == state[2] || // Верхня лінія
            state[3] == state[4] && state[3] == state[5] || // Центральна лінія
            state[6] == state[7] && state[6] == state[8];   // Нижня лінія

        return isWin;
    }
}
