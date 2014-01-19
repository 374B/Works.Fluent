Works.Fluent
============

<h3>FluentSwitch</h3>

A replacement for the standard switch statement. Uses a fluent style interface and provides much more flexibility compared to the standard switch statement.

A simple switch statement such as:

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
      
Can we rewritten using FluentSwitch as follows:

      FluentSwitch<string>
        .Comparer(String.Equals)
        .Case("A").Do(HandleA)
        .Case("B").Do(HandleB)
        .Case("C").Do(HandleC)
        .Default(HandleOther)
        .Switch(value);
        
The only real gain here is more compact and elegant code.

FluentSwitch gets more useful when you start running into the limitations of the standard switch statement. For example, how do you switch strings using a case insensitive comparison using the standard switch statement? A common approach is

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
      
With FluentSwitch you can do this in a much more elegant and correct way:

    FluentSwitch<string>
      .Comparer(StringComparer.OrdinalIgnoreCase.Equals)
      .Case("A").Do(HandleA)
      .Case("B").Do(HandleB)
      .Case("C").Do(HandleC)
      .Default(HandleOther)
      .Switch(value);
      
Comparer takes any method or function with a signature of Func<T,T,bool> so we can leverage the StringComparer.OrdinalIgnoreCase.Equals method or we could write our own method if we wanted.

FluentSwitch also provides much better handling of collections. If for example you wanted to loop through a collection and try to match each value you might traditionally write something like:

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
      
This can be rewritten using FluentSwitch almost identically to the above examples, however instead of calling the Switch method we call the SwitchAll method instead. E.G:

    FluentSwitch<string>
      .Comparer(String.Equals)
      .Case("A").Do(HandleA)
      .Case("B").Do(HandleB)
      .Case("C").Do(HandleC)
      .Default(HandleOther)
      .SwitchAll(values);


There's also the situation where you only want to "switch" the first matching value and then stop. Traditionally this is messy. You might have something like:

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
      
With FluentSwitch, it's the same as the previous example, but instead of the SwitchAll method we use the SwitchFirst method.

    FluentSwitch<string>
      .Comparer(String.Equals)
      .Case("A").Do(HandleA)
      .Case("B").Do(HandleB)
      .Case("C").Do(HandleC)
      .Default(HandleOther)
      .SwitchFirst(values);
