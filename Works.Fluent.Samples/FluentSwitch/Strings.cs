using System;
using System.Collections.Generic;
using System.Linq;

namespace Works.Fluent.Samples.FluentSwitch
{
  public static class Strings
  {
    public static void Simple()
    {
      var value = "C";

      switch (value)
      {
        case "A":
          HandleA();
          break;

        case "B":
          HandleB();
          break;

        case "C":
          HandleC();
          break;

        default:
          HandleOther();
          break;
      }

      FluentSwitch<string>
        .Comparer(String.Equals)
        .Case("A").Do(HandleA)
        .Case("B").Do(HandleB)
        .Case("C").Do(HandleC)
        .Default(HandleOther)
        .Switch(value);
    }

    public void Case_Insensitive()
    {
      var value = "C";

      switch (value.ToLower())
      {
        case "a":
          HandleA();
          break;

        case "b":
          HandleB();
          break;

        case "c":
          break;

        default:
          HandleOther();
          break;
      }

      FluentSwitch<string>
        .Comparer(StringComparer.OrdinalIgnoreCase.Equals)
        .Case("A").Do(HandleA)
        .Case("B").Do(HandleB)
        .Case("C").Do(HandleC)
        .Default(HandleOther)
        .Switch(value);
    }

    public void List_All_Matches()
    {
      var values = new List<string> { "A", "B", "C" };

      foreach (var value in values)
      {
        switch (value)
        {
          case "A":
            HandleA();
            break;

          case "B":
            HandleB();
            break;

          case "C":
            HandleC();
            break;

          default:
            HandleOther();
            break;
        }
      }

      FluentSwitch<string>
        .Comparer(String.Equals)
        .Case("A").Do(HandleA)
        .Case("B").Do(HandleB)
        .Case("C").Do(HandleC)
        .Default(HandleOther)
        .SwitchAll(values);
    }

    public void List_First_Match()
    {
      var values = new List<string> { "A", "B", "C" };

      bool handled = false;

      foreach (var value in values)
      {
        switch (value)
        {
          case "A":
            HandleA();
            break;

          case "B":
            HandleB();
            break;

          case "C":
            HandleC();
            break;

          default:
            HandleOther();
            break;
        }

        if (handled)
          break;
      }

      FluentSwitch<string>
        .Comparer(String.Equals)
        .Case("A").Do(HandleA)
        .Case("B").Do(HandleB)
        .Case("C").Do(HandleC)
        .Default(HandleOther)
        .SwitchFirst(values);
    }

    private static void HandleA()
    {
    }

    private static void HandleB()
    {
    }

    private static void HandleC()
    {
    }

    private static void HandleOther()
    {
    }
  }
}