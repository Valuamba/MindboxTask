using System;

namespace GeometryLibrary
{
    public interface IShape
    {
        double GetArea();
    }

    public class Circle : IShape
    {
        private double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double GetArea()
        {
            return Math.PI * radius * radius;
        }
    }

    public class Triangle : IShape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public double GetArea()
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public bool IsRightAngleTriangle()
        {
            double[] sides = new double[] { a, b, c };
            Array.Sort(sides);
            return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
        }
    }

    public class ShapeFactory
    {
        public enum ShapeType { Circle, Triangle };

        public static IShape CreateShape(ShapeType shapeType, params double[] args)
        {
            switch (shapeType)
            {
                case ShapeType.Circle:
                    return new Circle(args[0]);
                case ShapeType.Triangle:
                    return new Triangle(args[0], args[1], args[2]);
                default:
                    throw new ArgumentException("Invalid shape type.");
            }
        }
    }
}


using System;
using GeometryLibrary;

class Program
{
    static void Main(string[] args)
    {
        IShape circle = ShapeFactory.CreateShape(ShapeFactory.ShapeType.Circle, 5);
        Console.WriteLine($"Circle area: {circle.GetArea()}");

        IShape triangle = ShapeFactory.CreateShape(ShapeFactory.ShapeType.Triangle, 3, 4, 5);
        Console.WriteLine($"Triangle area: {triangle.GetArea()}, right angle: {((Triangle)triangle).IsRightAngleTriangle()}");
    }
}
