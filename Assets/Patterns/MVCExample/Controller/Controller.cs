using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Controller
{
   protected Model _model;

   public Controller(Model model)
   {
      _model = model;
   }

   // Його також можна зробити абстрактним,
   // якщо в нас буде не чистий рандом, а різні опції
   public void Spin()
   {
      List<int> tempState = new int[_model.State.Count].ToList();
      for (int i = 0; i < _model.State.Count; i++)
      {
         tempState[i] = Random.Range(0, 6);
      }

      _model.SetState(tempState);
      _model.SetWinState(AnalyzeWinResult(tempState));
   }

   protected abstract bool AnalyzeWinResult(List<int> newState);
}
