using System;
using UnityEngine;

namespace Model.Game
{
    public static class ChangeNameEvent
    {
        public static event Action<string> NameChanged;

        public static void OnNameChanged(string name)
        {
            NameChanged?.Invoke(name);
        }
    }
    
    public static class ChangeColorEvent
    {
        public static event Action<Color> ColorChanged;

        public static void OnColorChanged(Color color)
        {
            ColorChanged?.Invoke(color);
        }
    }
}