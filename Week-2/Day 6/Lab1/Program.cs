using System;
using System.Xml.Linq;
using static RgbColor;

public struct RgbColor
{
    public byte R, G, B;
    public RgbColor(byte r, byte g, byte b) 
    {
        R = r; 
        G = g;
        B = b;
    }
    // TODO: override ToString() -> "#RRGGBB"

    public  override string ToString()
    {
        return $"#{R:X2}{G:X2}{B:X2}";
    }

    public enum NamedColor
    {
        Red,Black,Green,Blue,White
    }
    public class Pixel
    {
        public RgbColor Color;
       
    }

    public class Program
    {
        public static RgbColor FromNamed (NamedColor name)
        {
            switch (name)
            {
                case NamedColor.Red:
                    return new RgbColor(255, 0, 0);

                case NamedColor.Green:
                    return new RgbColor(0, 255, 0);

                case NamedColor.Blue:
                    return new RgbColor(0, 0, 255);

                case NamedColor.White:
                    return new RgbColor(255, 255, 255);

                case NamedColor.Black:
                    return new RgbColor(0, 0, 0);

                default:
                    return new RgbColor(0, 0, 0);
            }
        }
        static void Main()
        {

            RgbColor a = FromNamed(NamedColor.Red);

            RgbColor b = a;       // Value copy

            b.R = 1;              // Modify only b

            Console.WriteLine("-- struct copy --");
            Console.WriteLine($"a = {a}");
            Console.WriteLine($"b = {b}");


            Pixel p1 = new Pixel();
            p1.Color = FromNamed(NamedColor.Red);

            Pixel p2 = p1;        // Reference copy

            p2.Color = FromNamed(NamedColor.Green);

            Console.WriteLine();
            Console.WriteLine("-- class/reference copy --");
            Console.WriteLine($"p1.Color = {p1.Color}");
            Console.WriteLine($"p2.Color = {p2.Color}");

        }
    }
}