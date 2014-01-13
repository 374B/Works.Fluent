using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Works.Fluent.Tests
{
  internal class FluentSwitchTests
  {
    [Test]
    public void Case_Should_Match_Null_Value()
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
    public void Case_Should_Throw_ArgumentNullException_When_Values_Is_Null()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentSwitch<string>
          .Comparer((a, b) => { return a == b; })
          .Case((IEnumerable<string>)null);
      });

      //FluentSwitch<string>
      //  .Comparer((a, b) => { return a == b; })
      //  .Case(nullCase)
      //    .Do(() =>
      //    {
      //      handled = true;
      //    })
      //  .Default(() =>
      //  {
      //  })
      //  .Switch(nullTest);

      //Assert.IsTrue(handled);
    }

    [Test]
    public void Case_With_Single_Arg_Should_Execute_On_Match()
    {
      bool executed = false;

      FluentSwitch<string>
        .Comparer((a, b) => { return a == b; })
        .Case("A")
          .Do(() => { executed = true; })
        .Default(() => { });

      Assert.IsTrue(executed);
    }

    [Test]
    public void Case_With_Single_Arg_Should_Not_Execute_When_Not_Matched()
    {
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
    public void Default_Should_Allow_Null()
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
  }
}