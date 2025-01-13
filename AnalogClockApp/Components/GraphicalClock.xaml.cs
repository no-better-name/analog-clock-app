using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnalogClockApp.Components;

public partial class GraphicalClock : ContentView
{
    public static readonly BindableProperty ShownTimeProperty = BindableProperty.Create(
        nameof(ShownTime),
        typeof(DateTime),
        typeof(GraphicalClock),
        DateTime.MinValue,
        propertyChanged: InvalidateOnPropertyChange
        );

    public GraphicalClock()
    {
        InitializeComponent();
    }

    public DateTime ShownTime
    {
        get => (DateTime)GetValue(ShownTimeProperty);
        set => SetValue(ShownTimeProperty, value);
    }

    private static void InvalidateOnPropertyChange(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is GraphicalClock clock)
        {
            clock.analogClock.Time = (DateTime)newValue;
            clock.clockGraphicalView.Invalidate();
        }
    }
}