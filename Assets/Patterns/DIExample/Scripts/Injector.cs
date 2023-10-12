﻿using System;
using Patterns.DIExample.Scripts;
using Patterns.DIExample.Scripts.PlayerInput;
using Patterns.DIExample.Scripts.Services.DamageVisualizer;
using Patterns.DIExample.Scripts.Services.EnemyFactory;
using UnityEngine;

/// <summary>
/// Не обязан быть монобехом, можно в него все данные заливать конструктором
/// </summary>
public class Injector : MonoBehaviour
{
    [SerializeField] private DamageVisualizer _damageVisualizer;

    [Header("Damage Data")]
    [SerializeField] private Player _player;
    [SerializeField] private CalculatorType _calculatorType;
    [SerializeField] private SimpleWeaponConfig _simpleWeaponConfig;
    [SerializeField] private RangedWeaponConfig _rangedWeaponConfig;
    [SerializeField] private CritWeaponConfig _critWeaponConfig;
   
    [Header("Enemy Spawn Data")] [SerializeField]
    private EnemySpawner _spawner;
    [SerializeField] private EnemyFactoryType _enemyFactoryType;
    [SerializeField]
    private SingleEnemyFactory _singleEnemyFactory;
    [SerializeField] private RandomEnemyFactory _randomEnemyFactory;

    [Header("Player input data")] [SerializeField]
    private InputType _inputType;
    
    [Header("UI")]
    [SerializeField] private UILayout _layout;
    
    private void Awake()
    {
        var calculator = SelectCalculator(_calculatorType);
        var inputHandler = SelectInputHandler(_inputType); 
        _player.Construct(calculator, _damageVisualizer, inputHandler);

        var factory = SelectEnemyFactory(_enemyFactoryType);
        _spawner.Construct(factory);
        
        _layout.Construct(_spawner, calculator);
    }

    /// <summary>
    /// Вообще Роберт Мартин говорит что switch-case допускается только если
    /// он используется внутри фабрики, но я решил для наглядности этот код в фабрику не запихивать
    /// </summary>
    /// <returns></returns>
    private IDamageCalculator SelectCalculator(CalculatorType calculatorType)
    {
        switch (calculatorType)
        {
            case CalculatorType.Simple:
                return new DamageCalculatorSimple(_simpleWeaponConfig);
            case CalculatorType.Range:
                return new DamageCalculatorRange(_rangedWeaponConfig);
            case CalculatorType.Crit:
                return new DamageCalculatorCritChance(_critWeaponConfig);
            default:
                return new DamageCalculatorSimple(_simpleWeaponConfig);
        }
    }

    private EnemyFactory SelectEnemyFactory(EnemyFactoryType enemyFactoryType)
    {
        switch (enemyFactoryType)
        {
            case EnemyFactoryType.Single:
                return _singleEnemyFactory;
            case EnemyFactoryType.Random:
                return _randomEnemyFactory;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyFactoryType), enemyFactoryType, null);
        }
    }

    private InputHandler SelectInputHandler(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Mouse:
                return new MouseInputHandler();
            case InputType.Keyboard_Space:
                return new KeyboardInputHandler();
            default:
                throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null);
        }
    }
}