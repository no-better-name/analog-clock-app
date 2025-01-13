using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalogClockApp.Components
{
    internal partial class AnalogClock : IDrawable
    {
        public DateTime Time {  get; set; }
        public double ClockFrameRelativeStrokeWidth { get; set; } = 0.1;
        public double HourHandRelativeStrokeWidth { get; set; } = 0.05;
        public double MinuteHandRelativeStrokeWidth { get; set; } = 0.025;
        public double SecondHandRelativeStrokeWidth { get; set; } = 0.0125;
        public double HourHandRelativeLength { get; set; } = 0.25;
        public double MinuteHandRelativeLength { get; set; } = 0.5;
        public double SecondHandRelativeLength { get; set; } = 0.7;
        public double HourTickRelativeLength { get; set; } = 0.2;
        public double MinuteTickRelativeLength { get; set; } = 0.1;
        public double HourTickRelativeStrokeWidth { get; set; } = 0.05;
        public double MinuteTickRelativeStrokeWidth { get; set; } = 0.025;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            double clockRadius = Math.Min(dirtyRect.Width, dirtyRect.Height) / 2;
            double clockBodyRelativeRadius = 1 - ClockFrameRelativeStrokeWidth;
            double clockBodyRadius = clockBodyRelativeRadius * clockRadius;

            double hourHandLength = HourHandRelativeLength * clockBodyRadius;
            double minuteHandLength = MinuteHandRelativeLength * clockBodyRadius;
            double secondHandLength = SecondHandRelativeLength * clockBodyRadius;

            Point hourHandVector = new(
                hourHandLength * Math.Sin(Time.Hour % 12 / 12.0 * Math.Tau) + dirtyRect.Center.X,
                hourHandLength * -Math.Cos(Time.Hour % 12 / 12.0 * Math.Tau) + dirtyRect.Center.Y
                );
            Point minuteHandVector = new(
                minuteHandLength * Math.Sin(Time.Minute / 60.0 * Math.Tau) + dirtyRect.Center.X,
                minuteHandLength * -Math.Cos(Time.Minute / 60.0 * Math.Tau) + dirtyRect.Center.Y
                );
            Point secondHandVector = new(
                secondHandLength * Math.Sin((Time.Second + Time.Millisecond / 1e3 + Time.Microsecond / 1e6) / 60.0 * Math.Tau) + dirtyRect.Center.X,
                secondHandLength * -Math.Cos((Time.Second + Time.Millisecond / 1e3 + Time.Microsecond / 1e6) / 60.0 * Math.Tau) + dirtyRect.Center.Y
                );

            canvas.StrokeColor = Colors.White;

            canvas.StrokeSize = (float)(ClockFrameRelativeStrokeWidth * clockRadius);
            canvas.DrawCircle(dirtyRect.Center, clockBodyRadius);

            for (int i = 0; i < 60; i++)
            {
                if (i % 5 == 0)
                {
                    Point tickStart;
                    Point tickStop;
                    tickStart = new(
                        (1 - HourTickRelativeLength) * clockBodyRadius * Math.Sin(i / 12.0 * Math.Tau) + dirtyRect.Center.X,
                        (1 - HourTickRelativeLength) * clockBodyRadius * -Math.Cos(i / 12.0 * Math.Tau) + dirtyRect.Center.Y
                        );
                    tickStop = new(
                        clockBodyRadius * Math.Sin(i / 12.0 * Math.Tau) + dirtyRect.Center.X,
                        clockBodyRadius * -Math.Cos(i / 12.0 * Math.Tau) + dirtyRect.Center.Y
                        );
                    canvas.StrokeSize = (float)(HourTickRelativeStrokeWidth * clockRadius);
                    canvas.DrawLine(tickStart, tickStop);
                }
                else
                {
                    Point tickStart;
                    Point tickStop;
                    tickStart = new(
                        (1 - MinuteTickRelativeLength) * clockBodyRadius * Math.Sin(i / 60.0 * Math.Tau) + dirtyRect.Center.X,
                        (1 - MinuteTickRelativeLength) * clockBodyRadius * -Math.Cos(i / 60.0 * Math.Tau) + dirtyRect.Center.Y
                        );
                    tickStop = new(
                        clockBodyRadius * Math.Sin(i / 60.0 * Math.Tau) + dirtyRect.Center.X,
                        clockBodyRadius * -Math.Cos(i / 60.0 * Math.Tau) + dirtyRect.Center.Y
                        );
                    canvas.StrokeSize = (float)(MinuteTickRelativeStrokeWidth * clockRadius);
                    canvas.DrawLine(tickStart, tickStop);
                }
            }

            canvas.StrokeSize = (float)(HourHandRelativeStrokeWidth * clockRadius);
            canvas.DrawLine(
                dirtyRect.Center,
                hourHandVector
                );
            canvas.StrokeSize = (float)(MinuteHandRelativeStrokeWidth * clockRadius);
            canvas.DrawLine(
                dirtyRect.Center,
                minuteHandVector
                );
            canvas.StrokeSize = (float)(SecondHandRelativeStrokeWidth * clockRadius);
            canvas.DrawLine(
                dirtyRect.Center,
                secondHandVector
                );
        }
    }
}
