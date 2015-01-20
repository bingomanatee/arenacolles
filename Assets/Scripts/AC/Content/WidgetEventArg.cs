using System;
using System.Collections;

namespace AC
{
  public class WidgetEventArg<T> : EventArgs
  {
    private T m_currentValue;
    public T CurrentValue{ get { return m_currentValue; } }
    public WidgetEventArg (T currentValue)
    {
      m_currentValue = currentValue;
    }
		
  }
}
