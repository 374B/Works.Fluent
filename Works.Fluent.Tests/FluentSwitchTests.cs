using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Works.Fluent.Tests
{
  internal class FluentSwitchTests
  {
    [Test]
    public void Case_Should_Throw_ArgumentNullException_When_Values_Is_Null()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case((IEnumerable<string>)null);
      });
    }

    [Test]
    public void Comparer_Should_Return_An_Object_Instance()
    {
      var obj = FluentSwitch<string>
        .Comparer((a, b) => { return a.Equals(b); });

      Assert.IsNotNull(obj);
    }

    [Test]
    public void Comparer_Should_Throw_ArgumentNullException_When_Comparer_Is_Null()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer(null);
      });
    }

    [Test]
    public void Default_Should_Allow_Null_Action()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
          .Default(null);
      });
    }

    [Test]
    public void Do_Should_Allow_Null_Action()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do((Action)null);
      });
    }

    [Test]
    public void Do_T_Should_Allow_Null_Action()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do((Action<string>)null);
      });
    }

    [Test]
    public void Null_Default_Action_Should_Be_Handled()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
            .Default(null)
          .Switch("X");
      });
    }

    [Test]
    public void Null_Do_Action_Should_Be_Handled()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do((Action)null)
            .Default(() => { })
          .Switch("A");
      });
    }

    [Test]
    public void Null_Do_T_Action_Should_Be_Handled()
    {
      Assert.DoesNotThrow(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do((Action<string>)null)
            .Default(() => { })
          .Switch("A");
      });
    }

    [Test]
    public void Switch_Should_Execute_Matched_Case()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { executed = true; })
        .Default(() => { })
        .Switch("A");

      Assert.IsTrue(executed);
    }

    [Test]
    public void Switch_Should_Match_Null_Value()
    {
      bool handled = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case((string)null)
          .Do(() => { handled = true; })
        .Default(() => { })
        .Switch(null);

      Assert.IsTrue(handled);
    }

    [Test]
    public void Switch_Should_Not_Execute_Unmatched_Case()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { executed = true; })
        .Default(() => { })
        .Switch("B");

      Assert.IsFalse(executed);
    }

    [Test]
    public void SwitchAll_Should_Execute_All_Matched()
    {
      bool aExecuted = false;
      bool bExecuted = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { aExecuted = true; })
        .Case("B")
          .Do(() => { bExecuted = true; })
        .Default(() => { })
        .SwitchAll("A", "B");

      Assert.IsTrue(aExecuted && bExecuted);
    }

    [Test]
    public void SwitchAll_Should_Execute_Default_When_No_Matches()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { })
        .Case("B")
          .Do(() => { })
        .Default(() => { executed = true; })
        .SwitchAll("1", "2");

      Assert.IsTrue(executed);
    }

    [Test]
    public void SwitchAll_Should_Not_Execute_Default_When_A_Match_Is_Made()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { })
        .Case("B")
          .Do(() => { })
        .Default(() => { executed = true; })
        .SwitchAll("A", "B");

      Assert.IsFalse(executed);
    }

    [Test]
    public void SwitchAll_Should_Not_Execute_Unmatched()
    {
      bool aExecuted = false;
      bool bExecuted = false;
      bool cExecuted = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { aExecuted = true; })
        .Case("B")
          .Do(() => { bExecuted = true; })
        .Case("C")
          .Do(() => { cExecuted = true; })
        .Default(() => { })
        .SwitchAll("A", "B");

      Assert.IsTrue(aExecuted && bExecuted && !cExecuted);
    }

    [Test]
    public void SwitchAll_Should_Throw_ArgumentNullException_When_Params_Null()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
          .Default(() => { })
          .SwitchAll(null);
      });
    }

    [Test]
    public void SwitchAll_Should_Throw_ArgumentNullException_When_Values_Null()
    {
      List<string> values = null;

      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
          .Default(() => { })
          .SwitchAll(values);
      });
    }

    [Test]
    public void SwitchFirst_Should_Execute_Matched_Case()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { executed = true; })
        .Default(() => { })
        .SwitchFirst("B", "A");

      Assert.IsTrue(executed);
    }

    [Test]
    public void SwitchFirst_Should_Executed_Default_When_No_Matches()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { })
        .Case("B")
          .Do(() => { })
        .Default(() => { executed = true; })
        .SwitchFirst("1", "2");

      Assert.IsTrue(executed);
    }

    [Test]
    public void SwitchFirst_Should_Not_Execute_Default_When_A_Match_Is_Made()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { })
        .Case("B")
          .Do(() => { })
        .Default(() => { executed = true; })
        .SwitchFirst("A", "B");

      Assert.IsFalse(executed);
    }

    [Test]
    public void SwitchFirst_Should_Not_Execute_Subsequent_Matches()
    {
      bool aExecuted = false;
      bool bExecuted = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { aExecuted = true; })
        .Case("B")
          .Do(() => { bExecuted = true; })
        .Default(() => { })
        .SwitchFirst("A", "B");

      Assert.IsTrue(aExecuted && !bExecuted);
    }

    [Test]
    public void SwitchFirst_Should_Not_Execute_Unmatched()
    {
      bool aExecuted = false;
      bool bExecuted = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { aExecuted = true; })
        .Case("B")
          .Do(() => { bExecuted = true; })
        .Default(() => { })
        .SwitchFirst("1", "2");

      Assert.IsFalse(aExecuted || bExecuted);
    }

    [Test]
    public void SwitchFirst_Should_Throw_ArgumentNullException_When_Params_Null()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
          .Default(() => { })
          .SwitchFirst(null);
      });
    }

    [Test]
    public void SwitchFirst_Should_Throw_ArgumentNullException_When_Values_Null()
    {
      List<string> values = null;

      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case("A")
            .Do(() => { })
          .Default(() => { })
          .SwitchFirst(values);
      });
    }
  }
}