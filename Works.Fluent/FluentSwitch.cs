using System;
using System.Collections.Generic;
using System.ComponentModel;
using Works.Fluent.Helpers;

namespace Works.Fluent
{
  public class FluentSwitch<T> : FluentInterface
  {
    #region Entry Points

    public static ISwitchCase<T> Comparer(Func<T, T, bool> Comparer)
    {
      if (Comparer == null)
        throw new ArgumentNullException("Comparer", "Comparer must not be null");

      return new FluentSwitchImpl(Comparer);
    }

    #endregion Entry Points

    #region Interfaces

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISwitch<TI> : IFluentInterface
    {
      ISwitch<TI> Switch(TI Value);

      ISwitch<TI> SwitchAll(params TI[] Values);

      ISwitch<TI> SwitchAll(IEnumerable<TI> Values);

      ISwitch<TI> SwitchFirst(params TI[] Values);

      ISwitch<TI> SwitchFirst(IEnumerable<TI> Values);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISwitchCase<TI> : IFluentInterface
    {
      ISwitchDo<TI> Case(TI Value);

      ISwitchDo<TI> Case(params TI[] Values);

      ISwitchDo<TI> Case(IEnumerable<TI> Values);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISwitchCaseOrDefault<TI> : ISwitchCase<TI>
    {
      ISwitch<TI> Default(Action Action);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ISwitchDo<TI> : IFluentInterface
    {
      ISwitchCaseOrDefault<TI> Do(Action Action);

      ISwitchCaseOrDefault<TI> Do(Action<TI> Action);
    }

    #endregion Interfaces

    #region Ctor

    private FluentSwitch()
    {
    }

    #endregion Ctor

    #region Implementations

    private class FluentSwitchImpl : ISwitchCase<T>, ISwitchCaseOrDefault<T>, ISwitch<T>, ISwitchDo<T>
    {
      private readonly Func<T, T, bool> comparer;

      private Action defaultAction;

      public FluentSwitchImpl(Func<T, T, bool> Comparer)
      {
        this.comparer = Comparer;
      }

      public ISwitchDo<T> Case(T Value)
      {
        return this;
      }

      public ISwitchDo<T> Case(params T[] Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return this;
      }

      public ISwitchDo<T> Case(IEnumerable<T> Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return this;
      }

      public ISwitch<T> Default(Action Action)
      {
        defaultAction = Action;
        return this;
      }

      public ISwitchCaseOrDefault<T> Do(Action Action)
      {
        return this;
      }

      public ISwitchCaseOrDefault<T> Do(Action<T> Action)
      {
        return this;
      }

      public ISwitch<T> Switch(T Value)
      {
        return this;
      }

      public ISwitch<T> SwitchAll(params T[] Values)
      {
        return this;
      }

      public ISwitch<T> SwitchAll(IEnumerable<T> Values)
      {
        return this;
      }

      public ISwitch<T> SwitchFirst(params T[] Values)
      {
        return this;
      }

      public ISwitch<T> SwitchFirst(IEnumerable<T> Values)
      {
        return this;
      }
    }

    #endregion Implementations
  }
}