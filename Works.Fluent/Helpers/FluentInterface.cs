using System.ComponentModel;

namespace Works.Fluent.Helpers
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public abstract class FluentInterface : IFluentInterface
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new static void Equals(object A, object B) { }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public new static void ReferenceEquals(object A, object B) { }
  }
}