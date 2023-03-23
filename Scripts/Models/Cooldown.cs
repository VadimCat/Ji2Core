using System;
using Ji2.CommonCore;
using UnityEngine;

namespace Ji2.Models
{
    public class Cooldown : IUpdatable
    {
        private readonly int _maxChargesCount;
        private readonly float _chargeCooldown;
        private readonly UpdateService _updateService;

        private float _currentCooldown;
        private int _currentCharges;
        public int CurrentCharges => _currentCharges;

        public event Action<int> EventChargesUpdate; 
        public event Action<float> EventNormalCooldownProgressUpdate; 

        public Cooldown(int initialCharges, int maxChargesCount, float chargeCooldown, UpdateService updateService)
        {
            _maxChargesCount = maxChargesCount;
            _chargeCooldown = chargeCooldown;
            _updateService = updateService;
            _currentCharges = initialCharges;
            _currentCooldown = 0;
        }
        
        public bool TryUse()
        {
            bool result = _currentCharges > 0; 
            if (result)
            {
                _currentCharges--;
                EventChargesUpdate?.Invoke(_currentCharges);
            }

            return result;
        }
        
        public void OnUpdate()
        {
            if (_currentCharges < _maxChargesCount)
            {
                _currentCooldown += Time.deltaTime;
                
                if (_currentCooldown >= _chargeCooldown)
                {
                    _currentCooldown -= _currentCooldown;
                    _currentCharges++;
                    EventChargesUpdate?.Invoke(_currentCharges);
                    if (_currentCharges == _maxChargesCount)
                    {
                        _currentCooldown = 0;
                    }
                }
                EventNormalCooldownProgressUpdate?.Invoke(_currentCooldown/_chargeCooldown);
            }
        }

        public void TogglePause(bool isPaused)
        {
            if (isPaused)
            {
                _updateService.Add(this);
            }
            else
            {
                _updateService.Remove(this);
            }
        }
    }
}
