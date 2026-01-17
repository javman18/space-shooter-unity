using System;

namespace SpaceShooter.Utils.Interfaces
{
    public interface IHasDeathEvent
    {
        event Action Died;
    }
}
