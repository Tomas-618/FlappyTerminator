using System;

public interface IReadOnlyHeartsEvents
{
    event Action<int> Damaged;

    event Action Died;
}
