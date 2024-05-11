using System;

public interface IDamagable
{
    void TakeDamage();
}

public interface IDamagable<T> where T : IComparable, IFormattable
{
    void TakeDamage(in T damage);
}
