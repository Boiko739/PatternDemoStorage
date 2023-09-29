﻿using System;

namespace Patterns.DIExample_Zenject.Scripts.PlayerInput
{
    public abstract class InputHandler
    {
        public event Action<Enemy> EnemyClicked;
        public abstract void Handle();

        protected void SendEnemyClicked(Enemy enemy)
        {
            EnemyClicked?.Invoke(enemy);
        }
    }
}