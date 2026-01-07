using UnityEngine.Events;
using UnityEngine;

namespace Components
{
    public abstract class MonoComponent<TComponent, TValue> : MonoComponent<TComponent> where TComponent : Component
    {
        [SerializeField] private UnityEvent _onValueChanged;
        public TValue Value { get; private set; }
        
        public void SetValue(TValue value)
        {
            Value = value;
            ProcessSetup();
            _onValueChanged?.Invoke();
        }

        protected abstract void ProcessSetup();

    }
    
    public class MonoComponent<TComponent> : MonoBehaviour where TComponent : Component
    {
        [SerializeField] private TComponent _component;
        
        protected TComponent Component => _component;
        
        private void OnValidate()
        {
            _component ??= GetComponent<TComponent>();
        }
        
    }
}


