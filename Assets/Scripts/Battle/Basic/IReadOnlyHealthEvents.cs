using System;

public interface IReadOnlyHealthEvents
{
    event Action<float> Damaged;

    event Action Died;
}
