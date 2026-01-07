using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Components
{
    [RequireComponent(typeof(Button))]
    public class MonoButton : MonoComponent<Button>
    {
        private readonly HashSet<IButtonInteractablePart> _interactableParts = new();
        private readonly HashSet<IButtonClickedPart> _clickableParts = new();
        
        private readonly HashSet<Action> _clickActions = new();
        
        public bool IsLocked => !Component.interactable;
        
        private void OnDestroy()
        {
            Component.onClick.RemoveListener(OnClick);
        }

        private void Start()
        {
            Component.onClick.AddListener(OnClick);
            RefreshParts();
        }

        public void RefreshParts()
        {
            var parts = GetComponentsInChildren<IButtonPart>(true);
            
            foreach (var part in parts)
            {
                if (part is IButtonInteractablePart interactablePart)
                {
                    _interactableParts.Add(interactablePart);
                }

                if (part is IButtonClickedPart clickablePart)
                {
                    _clickableParts.Add(clickablePart);
                }
            }
            
            _clickableParts.RemoveWhere(part => part == null);
            _interactableParts.RemoveWhere(part => part == null);
            
        }
        
        public void SetInteractable(bool interactable)
        {
            Component.interactable = interactable;
            foreach (var part in _interactableParts)
            {
                part.SetInteractable(interactable);
            }
        }
        
        public void SubscribeOnClick(Action action)
        {
            _clickActions.Add(action);
        }
        
        public void UnsubscribeOnClick(Action action)
        {
            _clickActions.Remove(action);
        }

        private void OnClick()
        {
            foreach (var action in _clickActions)
            {
                action.Invoke();
            }
            
            foreach (var part in _clickableParts)
            {
                part.OnButtonClicked();
            }
        }
        
    }
}