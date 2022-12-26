﻿using SkiaSharp;

namespace ScottPlot;

/// <summary>
/// Describes text using methods from the new font provider.
/// Holds all customization required to style and draw text.
/// </summary>
public class Label2 // TODO: rename later
{
    public string Text { get; set; } = string.Empty;
    public Font Font { get; set; } = Font.Default;
    public string FontName { get => Font.Name; set => Font = Font.WithName(value); }
    public float FontSize { get => Font.Size; set => Font = Font.WithSize(value); }
    public FontWeight FontWeight { get => Font.GetNearestWeight(); set => Font = Font.WithWeight(value); }
    public bool Bold { get => Font.Weight > (float)FontWeight.Normal; set => Font = Font.WithBold(value); }
    public bool Italic { get => Font.Italic; set => Font = Font.WithItalic(value); }
    public Color Color { get; set; } = Colors.Black;
    public Color BackgroundColor { get; set; } = Colors.Transparent;
    public Color BorderColor { get; set; } = Colors.Black;
    public float BorderWidth { get; set; } = 0;
    public bool Outline { get; set; } = false;
    public float OutlineWidth { get; set; } = 0;
    public bool AntiAlias { get; set; } = true;

    public Alignment Alignment { get; set; } = Alignment.UpperLeft;
    public HorizontalAlignment HorizontalAlignment
    {
        get => Alignment.X;
        set => Alignment = Alignment.WithHorizontalAlignment(value);
    }
    public VerticalAlignment VerticalAlignment
    {
        get => Alignment.Y;
        set => Alignment.WithVerticalAlignment(value);
    }

    public SKPaint MakePaint()
    {
        return new SKPaint()
        {
            Color = SKColors.White.WithAlpha(100),
            TextAlign = Alignment.X.ToSKTextAlign(),
            Typeface = SKTypeface.FromFamilyName(FontName),
            TextSize = FontSize,
            IsAntialias = AntiAlias,
        };
    }

    public PixelRect GetRectangle(Pixel pixel)
    {
        using SKPaint paint = MakePaint();
        SKRect textBounds = new();
        paint.MeasureText(Text, ref textBounds);

        (double xOffsetFraction, double yOffsetFraction) = Alignment.GetOffset();
        pixel = new Pixel(
            x: pixel.X - textBounds.Width * (float)xOffsetFraction,
            y: pixel.Y
        );

        return new PixelRect(
            left: pixel.X,
            right: pixel.X + textBounds.Width,
            bottom: pixel.Y + textBounds.Height,
            top: pixel.Y);
    }

    public void Draw(SKCanvas canvas, Pixel pixel)
    {
        using SKPaint paint = MakePaint();
        SKRect textBounds = new();
        paint.MeasureText(Text, ref textBounds);
        canvas.DrawText(Text, pixel.X, pixel.Y + textBounds.Height, paint);
    }
}
