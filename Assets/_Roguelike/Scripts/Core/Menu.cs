using System;
using System.Collections.Generic;

namespace Roguelike
{
    public class Menu
    {
        private List<IController> _controllers = new List<IController>();

        public void Init()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].Init();
            }
        }

        public void Create<T>()
        {
            var constructor = typeof(T).GetConstructor(Type.EmptyTypes);

            if (constructor == null)
            {
                throw new NullReferenceException($"Cant create controller with type {typeof(T).Name}");
            }

            var controller = (IController)constructor.Invoke(null);
            controller.Create();
            _controllers.Add(controller);
        }

        public T Get<T>()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                if (_controllers[i].GetType() == typeof(T))
                {
                    return (T)_controllers[i];
                }
            }

            return default;
        }
    }
}