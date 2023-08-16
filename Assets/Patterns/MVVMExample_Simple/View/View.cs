using UnityEngine;

namespace Patterns.MVVMExample_Simple
{
    public abstract class View : MonoBehaviour
    {
        protected ViewModel _viewModel;

        public virtual void Init(ViewModel viewModel)
        {
            _viewModel = viewModel;

            // Підписка на зміну STR,DEX і VIT
            _viewModel.StrView.OnChanged += DisplayStrView;
            _viewModel.DexView.OnChanged += DisplayDexView;
            _viewModel.VitView.OnChanged += DisplayVitView;

            _viewModel.StatsToSpendView.OnChanged += DisplayStatsToSpend;

            // Підписка на зміну кнопВідписка від змінок
            _viewModel.StrButtonEnabled.OnChanged += OnStrButtonEnabled;
            _viewModel.DexButtonEnabled.OnChanged += OnDexButtonEnabled;
            _viewModel.VitButtonEnabled.OnChanged += OnVitButtonEnabled;
        }

        // Команди на активацію кнопок
        protected abstract void OnStrButtonEnabled(bool val);
        protected abstract void OnDexButtonEnabled(bool val);
        protected abstract void OnVitButtonEnabled(bool val);

        // Команди на відмальовку значень
        protected abstract void DisplayStatsToSpend(int val);
        protected abstract void DisplayStrView(int val);
        protected abstract void DisplayDexView(int val);
        protected abstract void DisplayVitView(int val);

        protected virtual void Dispose()
        {
            // Відписка від змін STR,DEX і VIT
            _viewModel.StrView.OnChanged -= DisplayStrView;
            _viewModel.DexView.OnChanged -= DisplayDexView;
            _viewModel.VitView.OnChanged -= DisplayVitView;

            _viewModel.StatsToSpendView.OnChanged -= DisplayStatsToSpend;

            // Відписка від змін стану кнопок
            _viewModel.StrButtonEnabled.OnChanged -= OnStrButtonEnabled;
            _viewModel.DexButtonEnabled.OnChanged -= OnDexButtonEnabled;
            _viewModel.VitButtonEnabled.OnChanged -= OnVitButtonEnabled;
        }

        protected void OnDestroy()
        {
            Dispose();
        }
    }
}