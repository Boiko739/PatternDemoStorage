namespace Patterns.MVVMExample_Simple
{
    public abstract class ViewModel
    {
        protected Model _model;

        // Максимальне значення STR, DEX і VIT
        private const int MAX_STAT_VALUE = 18;

        // Значення параметрів STR, DEX і VIT
        public ReactiveProperty<int> StrView = new();
        public ReactiveProperty<int> DexView = new();
        public ReactiveProperty<int> VitView = new();
        public ReactiveProperty<int> StatsToSpendView = new();

        // Активність кнопок STR+, DEX+, VIT+
        public ReactiveProperty<bool> StrButtonEnabled = new();
        public ReactiveProperty<bool> DexButtonEnabled = new();
        public ReactiveProperty<bool> VitButtonEnabled = new();

        public ViewModel(Model model)
        {
            _model = model;

            // При зміні в моделі STR,DEX і VIT
            // Оновлюємо їх значення для в'ю-моделі
            _model.STR.OnChanged += OnModelStrChanged;
            _model.DEX.OnChanged += OnModelDexChanged;
            _model.VIT.OnChanged += OnModelVitChanged;

            StatsToSpendView.OnChanged += OnModelStatsToSpendViewChanged;
            OnResetButtonClicked();
        }

        private void OnModelStrChanged(int val)
        {
            StrView.Value = val;
        }
        private void OnModelDexChanged(int val)
        {
            DexView.Value = val;
        }
        private void OnModelVitChanged(int val)
        {
            VitView.Value = val;
        }

        public void OnIncreaseStrBtnClicked()
        {
            IncreasePropertyValue(StrView);
        }
        public void OnIncreaseDexBtnClicked()
        {
            IncreasePropertyValue(DexView);
        }
        public void OnIncreaseVitBtnClicked()
        {
            IncreasePropertyValue(VitView);
        }
        
        /// <summary>
        /// Скидання до стану моделі
        /// </summary>
        public void OnResetButtonClicked()
        {
            StrView.Value = _model.STR.Value;
            DexView.Value = _model.DEX.Value;
            VitView.Value = _model.VIT.Value;

            StatsToSpendView.Value = _model.StatsToSpend;

            DefineButtonsStatus();
        }

        /// <summary>
        /// Підтвердження змін
        /// </summary>
        public void OnSubmitBtnClicked()
        {
            _model.STR.Value = StrView.Value;
            _model.DEX.Value = DexView.Value;
            _model.VIT.Value = VitView.Value;

            _model.StatsToSpend = StatsToSpendView.Value;
        }

        /// <summary>
        /// Перевіряємо, активні, чи неактивні кнопки "+"
        /// </summary>
        private void DefineButtonsStatus()
        {
            StrButtonEnabled.Value = StrView.Value < MAX_STAT_VALUE;
            DexButtonEnabled.Value = DexView.Value < MAX_STAT_VALUE;
            VitButtonEnabled.Value = VitView.Value < MAX_STAT_VALUE;

            if (StatsToSpendView.Value <= 0)
            {
                StrButtonEnabled.Value = false;
                DexButtonEnabled.Value = false;
                VitButtonEnabled.Value = false;
            }
        }
        
        private void IncreasePropertyValue(ReactiveProperty<int> property)
        {
            property.Value += 1;
            StatsToSpendView.Value -= 1;
        }
        
        private void OnModelStatsToSpendViewChanged(int obj)
        {
            DefineButtonsStatus();
        }

        // Відписуємось
        public virtual void Dispose()
        {
            _model.STR.OnChanged -= OnModelStrChanged;
            _model.DEX.OnChanged -= OnModelDexChanged;
            _model.VIT.OnChanged -= OnModelVitChanged;

            StatsToSpendView.OnChanged -= OnModelStatsToSpendViewChanged;
        }
    }
}