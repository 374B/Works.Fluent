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
      /// <summary>
      /// Swith the Value
      /// </summary>
      /// <param name="Value"></param>
      /// <returns></returns>
      ISwitch<TI> Switch(TI Value);

      /// <summary>
      /// Switch all values in the Values collection
      /// </summary>
      /// <param name="Values"></param>
      /// <returns></returns>
      ISwitch<TI> SwitchAll(params TI[] Values);

      /// <summary>
      /// Switch all values in the Values collection
      /// </summary>
      /// <param name="Values"></param>
      /// <returns></returns>
      ISwitch<TI> SwitchAll(IEnumerable<TI> Values);

      /// <summary>
      /// Switch the first matched value in the Values collection
      /// </summary>
      /// <param name="Values"></param>
      /// <returns></returns>
      ISwitch<TI> SwitchFirst(params TI[] Values);

      /// <summary>
      /// Switch the first matched value in the Values collection
      /// </summary>
      /// <param name="Values"></param>
      /// <returns></returns>
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
      private readonly List<CaseDefinition> cases = new List<CaseDefinition>();
      private readonly List<CaseDefinitionWithReturn> casesWithDefs = new List<CaseDefinitionWithReturn>();
      private readonly Func<T, T, bool> comparer;
      private readonly List<T> current = new List<T>();
      private Action defaultAction;

      public FluentSwitchImpl(Func<T, T, bool> Comparer)
      {
        this.comparer = Comparer;
      }

      public ISwitchDo<T> Case(T Value)
      {
        current.Clear();
        current.Add(Value);
        return this;
      }

      public ISwitchDo<T> Case(params T[] Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        current.Clear();
        current.AddRange(Values);
        return this;
      }

      public ISwitchDo<T> Case(IEnumerable<T> Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        current.Clear();
        current.AddRange(Values);
        return this;
      }

      public ISwitch<T> Default(Action Action)
      {
        defaultAction = Action;
        return this;
      }

      public ISwitchCaseOrDefault<T> Do(Action Action)
      {
        foreach (var value in current)
        {
          var local = value;
          cases.Add(new CaseDefinition(local, Action));
        }

        current.Clear();
        return this;
      }

      public ISwitchCaseOrDefault<T> Do(Action<T> Action)
      {
        foreach (var value in current)
        {
          var local = value;
          casesWithDefs.Add(new CaseDefinitionWithReturn(local, Action));
        }

        current.Clear();
        return this;
      }

      public ISwitch<T> Switch(T Value)
      {
        CaseDefinitionWithReturn matchWithReturn;

        if (TryMatchCase(Value, out matchWithReturn))
        {
          ExecuteAction(Value, matchWithReturn.Action);
        }
        else
        {
          CaseDefinition match;
          if (TryMatchCase(Value, out match))
          {
            ExecuteAction(match.Action);
          }
          else
          {
            ExecuteAction(defaultAction);
          }
        }

        return this;
      }

      public ISwitch<T> SwitchAll(params T[] Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return SwitchEnumerable(Values, false);
      }

      public ISwitch<T> SwitchAll(IEnumerable<T> Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return SwitchEnumerable(Values, false);
      }

      public ISwitch<T> SwitchFirst(params T[] Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return SwitchEnumerable(Values, true);
      }

      public ISwitch<T> SwitchFirst(IEnumerable<T> Values)
      {
        if (Values == null)
          throw new ArgumentNullException("Values", "Values must not be null");

        return SwitchEnumerable(Values, true);
      }

      private void ExecuteAction(Action Action)
      {
        if (Action != null)
        {
          Action();
        }
      }

      private void ExecuteAction(T Value, Action<T> Action)
      {
        if (Action != null)
        {
          Action(Value);
        }
      }

      private ISwitch<T> SwitchEnumerable(IEnumerable<T> Values, bool BreakOnMatch)
      {
        bool anyMatches = false;

        foreach (var v in Values)
        {
          var local = v;
          CaseDefinitionWithReturn matchWithReturn;
          if (TryMatchCase(local, out matchWithReturn))
          {
            anyMatches = true;
            ExecuteAction(local, matchWithReturn.Action);
            if (BreakOnMatch)
            {
              break;
            }
          }
          else
          {
            CaseDefinition match;
            if (TryMatchCase(local, out match))
            {
              anyMatches = true;
              ExecuteAction(match.Action);
              if (BreakOnMatch)
              {
                break;
              }
            }
          }
        }

        if (!anyMatches)
        {
          ExecuteAction(defaultAction);
        }

        return this;
      }

      private bool TryMatchCase(T Value, out CaseDefinition Match)
      {
        Match = null;
        foreach (var c in cases)
        {
          if (comparer(c.Value, Value))
          {
            Match = c;
            break;
          }
        }

        return Match != null;
      }

      private bool TryMatchCase(T Value, out CaseDefinitionWithReturn Match)
      {
        Match = null;

        foreach (var c in casesWithDefs)
        {
          if (comparer(c.Value, Value))
          {
            Match = c;
            break;
          }
        }

        return Match != null;
      }

      private class CaseDefinition
      {
        public CaseDefinition(T Value, Action Action)
        {
          this.Value = Value;
          this.Action = Action;
        }

        public Action Action { get; private set; }

        public T Value { get; private set; }
      }

      private class CaseDefinitionWithReturn
      {
        public CaseDefinitionWithReturn(T Value, Action<T> Action)
        {
          this.Value = Value;
          this.Action = Action;
        }

        public Action<T> Action { get; private set; }

        public T Value { get; private set; }
      }
    }

    #endregion Implementations
  }
}