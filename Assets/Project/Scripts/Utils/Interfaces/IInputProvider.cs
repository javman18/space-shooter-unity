using UnityEngine;

namespace SpaceShooter.Utils.Interfaces
{
    public interface IInputProvider
    {
        Vector2 Move { get; }
        bool Fire { get; }
    }

}
