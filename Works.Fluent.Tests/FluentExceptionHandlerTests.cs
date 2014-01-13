using System;
using NUnit.Framework;

namespace Works.Fluent.Tests
{
  internal class FluentExceptionHandlerTests
  {
    [Test]
    public void Handle_Should_Return_An_Object_Instance()
    {
      var obj = FluentExceptionHandler
        .Handle<Exception>(ex =>
        {
        });

      Assert.NotNull(obj);
    }

    [Test]
    public void Handle_Should_Throw_ArgumentNullException_For_Null_Handler()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentExceptionHandler.Handle<Exception>(null);
      });
    }

    [Test]
    public void Try_Should_Execute_Action()
    {
      bool executed = false;

      FluentExceptionHandler
        .Handle<Exception>(ex =>
        {
        })
        .Try(() =>
        {
          executed = true;
        });

      Assert.IsTrue(executed);
    }

    [Test]
    public void Try_Should_Handle_Registered_Exception()
    {
      bool handled = false;

      FluentExceptionHandler
        .Handle<Exception>(ex =>
        {
          handled = true;
        })
        .Try(() =>
        {
          throw new Exception();
        });

      Assert.IsTrue(handled);
    }

    [Test]
    public void Try_Should_Not_Handle_Unregistered_Exception()
    {
      bool handled = false;

      FluentExceptionHandler
        .Handle<ArgumentException>(ex =>
        {
          handled = true;
        })
        .Try(() =>
        {
          throw new Exception();
        });

      Assert.IsFalse(handled);
    }

    [Test]
    public void Try_T_Should_Return_Default_When_Exception()
    {
      string expected = "X";
      string defaultValue = expected;

      string actual = FluentExceptionHandler
        .Handle<Exception>(ex =>
        {
        })
        .Try(() =>
        {
          throw new Exception();
        }, defaultValue);

      Assert.IsTrue(expected == defaultValue);
    }

    [Test]
    public void Try_T_Should_Return_Func_Result_When_No_Exceptions()
    {
      string expected = "1";

      string actual = FluentExceptionHandler
        .Handle<Exception>(ex =>
        {
        })
        .Try(() =>
        {
          return "1";
        }, "2");

      Assert.IsTrue(actual == expected);
    }

    [Test]
    public void Unhandled_Should_Handle_Unregistered_Exceptions()
    {
      bool handledByUnhandled = false;

      FluentExceptionHandler
        .Handle<ArgumentNullException>(ex =>
        {
        })
        .Unhandled(ex =>
        {
          handledByUnhandled = true;
        })
        .Try(() =>
        {
          throw new Exception();
        });

      Assert.IsTrue(handledByUnhandled);
    }

    [Test]
    public void Unhandled_Should_Not_Handle_Registered_Exceptions()
    {
      bool handledByUnhandled = false;

      FluentExceptionHandler
        .Handle<ArgumentNullException>(ex =>
        {
        })
        .Unhandled(ex =>
        {
          handledByUnhandled = true;
        })
        .Try(() =>
        {
          throw new ArgumentNullException();
        });

      Assert.IsFalse(handledByUnhandled);
    }

    [Test]
    public void Unhandled_Should_Throw_ArgumentNullException_For_Null_Handler()
    {
      Assert.Throws<ArgumentNullException>(() =>
      {
        FluentExceptionHandler
          .Handle<ArgumentNullException>(ex =>
          {
          })
          .Unhandled(null);
      });
    }
  }
}