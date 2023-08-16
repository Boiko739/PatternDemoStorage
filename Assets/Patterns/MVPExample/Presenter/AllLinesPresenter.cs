using System.Collections.Generic;

namespace Patterns.MVPExample
{
    public class AllLinesPresenter : Presenter
    {
        protected override bool AnalyzeResult()
        {
            var state = _model.State;
            bool isWin =
                state[0] == state[1] && state[0] == state[2] || // Верхня лінія
                state[3] == state[4] && state[3] == state[5] || // Центральна лінія
                state[6] == state[7] && state[6] == state[8];   // Нижня лінія

            return isWin;
        }

        public AllLinesPresenter(Model model) : base(model)
        {
        }
    }
}