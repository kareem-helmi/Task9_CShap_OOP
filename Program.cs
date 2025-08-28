
using System;
using System.Collections.Generic;

namespace FullAssignment
{
    // Part01 - Problem: Weekdays enum
    enum Weekdays { Monday = 1, Tuesday, Wednesday, Thursday, Friday }

    // Part01 - Problem: Grades enum with short underlying type
    enum Grades : short { F = -1, D = 0, C = 1, B = 2, A = 3 }

    // Part01 - Problem: Gender enums
    enum GenderByte : byte { Male = 1, Female = 2, Other = 3 }
    enum GenderDefault { Male = 1, Female = 2, Other = 3 }

    // Part01 - Problem: Department class (used by Person and Employee)
    class Department : IEquatable<Department>
    {
        public string Name { get; set; }
        public Department(string name) => Name = name;
        public override string ToString() => Name ?? "";
        public bool Equals(Department other) => other != null && Name == other.Name;
        public override bool Equals(object obj) => Equals(obj as Department);
        public override int GetHashCode() => (Name ?? "").GetHashCode();
    }

    // Part01 - Problem: Person class with Department property
    class Person
    {
        public string Name { get; set; }
        public Department Department { get; set; }
        public override string ToString() => $"{Name} (Dept: {Department})";
    }

    // Part01 - Problem: BaseEmployee and Child (sealed Salary)
    class BaseEmployee
    {
        // virtual property to allow overriding in derived classes
        public virtual decimal Salary { get; set; }
        public string Name { get; set; }
        public BaseEmployee() { }
        public BaseEmployee(string name, decimal salary) { Name = name; Salary = salary; }
        public override string ToString() => $"{Name} - Salary: {Salary}";
    }

    class Child : BaseEmployee
    {
        // sealed override - this prevents further overrides in classes that would derive from Child
        public sealed override decimal Salary { get; set; }
        public Child() { }
        public Child(string name, decimal salary) : base(name, salary) { Salary = salary; }

        // Problem: DisplaySalary method uses sealed Salary property
        public void DisplaySalary() => Console.WriteLine($"{Name} salary (sealed): {Salary}");
    }

    // Part01 - Problem: Utility class for static methods
    static class Utility
    {
        // perimeter of rectangle
        public static double RectanglePerimeter(double length, double width) => 2 * (length + width);

        // temperature conversions
        public static double CelsiusToFahrenheit(double c) => (c * 9.0 / 5.0) + 32.0;
        public static double FahrenheitToCelsius(double f) => (f - 32.0) * 5.0 / 9.0;
    }

    // Part01 - Problem: ComplexNumber with operator overloading for *
    class ComplexNumber
    {
        public double Real { get; }
        public double Imag { get; }
        public ComplexNumber(double real, double imag) { Real = real; Imag = imag; }
        public override string ToString() => $"{Real} + {Imag}i";

        // multiplication operator for complex numbers
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double real = a.Real * b.Real - a.Imag * b.Imag;
            double imag = a.Real * b.Imag + a.Imag * b.Real;
            return new ComplexNumber(real, imag);
        }
    }

    // Part01 - Problem: Employee class with Equals override and Department
    class Employee : IEquatable<Employee>
    {
        public int Id { get; }
        public string Name { get; }
        public Department Department { get; }

        public Employee(int id, string name, Department dept)
        {
            Id = id; Name = name; Department = dept;
        }

        // override Equals to compare by Id (identity)
        public bool Equals(Employee other) => other != null && other.Id == Id;
        public override bool Equals(object obj) => Equals(obj as Employee);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => $"Employee #{Id}: {Name} (Dept: {Department})";
    }

    // Part01 - Helper class: generic Max
    static class Helper
    {
        // Generic method that returns greater value; requires IComparable to compare
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }
    }

    // Part01 - Helper2 generic with SearchArray and ReplaceArray
    static class Helper2<T>
    {
        // SearchArray: returns index of first match or -1
        public static int SearchArray(T[] arr, T value)
        {
            var eq = EqualityComparer<T>.Default;
            for (int i = 0; i < arr.Length; i++)
                if (eq.Equals(arr[i], value)) return i;
            return -1;
        }

        // ReplaceArray: replace all occurrences of oldValue with newValue
        public static void ReplaceArray(T[] arr, T oldValue, T newValue)
        {
            var eq = EqualityComparer<T>.Default;
            for (int i = 0; i < arr.Length; i++)
                if (eq.Equals(arr[i], oldValue)) arr[i] = newValue;
        }
    }

    // Part01 - Rectangle struct and non-generic swap demo (struct)
    struct Rectangle
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public override string ToString() => $"L={Length},W={Width}";
    }

    // Part01 - Circle struct and class to demonstrate == vs Equals
    struct CircleStruct : IEquatable<CircleStruct>
    {
        public double Radius { get; set; }
        public string Color { get; set; }

        public bool Equals(CircleStruct other) => Radius == other.Radius && Color == other.Color;
        public override bool Equals(object obj) => obj is CircleStruct cs && Equals(cs);
        public override int GetHashCode() => HashCode.Combine(Radius, Color);

        public static bool operator ==(CircleStruct a, CircleStruct b) => a.Equals(b);
        public static bool operator !=(CircleStruct a, CircleStruct b) => !a.Equals(b);

        public override string ToString() => $"CircleStruct(R={Radius},C={Color})";
    }

    class CircleClass
    {
        public double Radius { get; set; }
        public string Color { get; set; }
        public CircleClass(double r, string c) { Radius = r; Color = c; }
        public override bool Equals(object obj) => obj is CircleClass other && Radius == other.Radius && Color == other.Color;
        public override int GetHashCode() => HashCode.Combine(Radius, Color);
        public override string ToString() => $"CircleClass(R={Radius},C={Color})";
    }

    // Part03 - Event-driven demo: simple publisher/subscriber
    // Define a delegate and an event, and show subscription and raising the event.
    class EventPublisher
    {
        // Define delegate and event
        public delegate void SimpleEventHandler(object sender, string message);
        public event SimpleEventHandler OnSimpleEvent;

        public void RaiseEvent(string message)
        {
            OnSimpleEvent?.Invoke(this, message);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Part01: Enums ===");
            // Weekdays
            foreach (Weekdays d in Enum.GetValues(typeof(Weekdays)))
                Console.WriteLine($"{d} = {(int)d}");

            // Grades (short underlying)
            Console.WriteLine("\nGrades:");
            foreach (Grades g in Enum.GetValues(typeof(Grades)))
                Console.WriteLine($"{g} = {(short)g}");

            // Gender memory usage demo
            Console.WriteLine($"\nSize of GenderByte underlying type: {sizeof(byte)} byte(s)");
            Console.WriteLine($"Size of default enum underlying type (int): {sizeof(int)} byte(s)");

            // Part01: Person + Department
            var depIT = new Department("IT");
            var depHR = new Department("HR");
            var person1 = new Person { Name = "Ali", Department = depIT };
            var person2 = new Person { Name = "Sara", Department = depHR };
            Console.WriteLine("\nPersons:");
            Console.WriteLine(person1);
            Console.WriteLine(person2);

            // Child with sealed Salary and DisplaySalary
            var child = new Child("Samir", 4500m);
            child.DisplaySalary();

            // Utility static method usage (perimeter + temp conversions)
            Console.WriteLine($"\nRectangle perimeter (5 x 3): {Utility.RectanglePerimeter(5, 3)}");
            Console.WriteLine($"Celsius 25 -> Fahrenheit: {Utility.CelsiusToFahrenheit(25):F2}");
            Console.WriteLine($"Fahrenheit 77 -> Celsius: {Utility.FahrenheitToCelsius(77):F2}");

            // ComplexNumber multiplication
            var c1 = new ComplexNumber(2, 3);
            var c2 = new ComplexNumber(1, 4);
            var c3 = c1 * c2;
            Console.WriteLine($"\nComplex multiply: {c1} * {c2} = {c3}");

            // Enum.TryParse demo
            Console.WriteLine("\nEnum.TryParse demo (input 'A'):");
            if (Enum.TryParse("A", out Grades parsedGrade))
                Console.WriteLine($"Parsed grade: {parsedGrade} ({(short)parsedGrade})");
            else
                Console.WriteLine("Invalid grade input");

            // Helper Max generic demo
            Console.WriteLine("\nGeneric Max demos:");
            Console.WriteLine($"Max(3, 7) = {Helper.Max(3, 7)}");
            Console.WriteLine($"Max(3.5, 2.1) = {Helper.Max(3.5, 2.1)}");
            Console.WriteLine($"Max(\"apple\",\"banana\") = {Helper.Max("apple", "banana")}");

            // Helper2 ReplaceArray demo
            int[] ints = { 1, 2, 2, 3 };
            Helper2<int>.ReplaceArray(ints, 2, 9);
            Console.WriteLine("\nReplaceArray ints: " + string.Join(",", ints));
            string[] strs = { "a", "b", "a" };
            Helper2<string>.ReplaceArray(strs, "a", "z");
            Console.WriteLine("ReplaceArray strings: " + string.Join(",", strs));

            // Rectangle swap (non-generic)
            var r1 = new Rectangle { Length = 2, Width = 3 };
            var r2 = new Rectangle { Length = 5, Width = 6 };
            Console.WriteLine($"\nBefore swap: r1={r1}, r2={r2}");
            // non-generic swap using temp
            var tmp = r1; r1 = r2; r2 = tmp;
            Console.WriteLine($"After swap: r1={r1}, r2={r2}");

            // Circle struct vs class equality
            var cs1 = new CircleStruct { Radius = 2, Color = "Red" };
            var cs2 = new CircleStruct { Radius = 2, Color = "Red" };
            Console.WriteLine($"\nCircleStruct: == -> {cs1 == cs2}, Equals -> {cs1.Equals(cs2)}");

            var cc1 = new CircleClass(2, "Red");
            var cc2 = new CircleClass(2, "Red");
            Console.WriteLine($"CircleClass: == -> {(cc1 == cc2 ? "true" : "false (reference compare)")}, Equals -> {cc1.Equals(cc2)}");

            // Employee array and search using Equals
            var empArr = new Employee[]
            {
                new Employee(1, "Ahmed", depIT),
                new Employee(2, "Mona", depHR),
                new Employee(3, "Khaled", depIT)
            };
            var target = new Employee(2, "Mona", depHR);
            int idx = Helper2<Employee>.SearchArray(empArr, target);
            Console.WriteLine($"\nSearchArray for Employee with Id=2 returned index: {idx} (expected 1)");

            // Part03 - Event-driven demo
            Console.WriteLine("\nEvent-driven demo:");
            var pub = new EventPublisher();
            // subscribe to event
            pub.OnSimpleEvent += (sender, message) => Console.WriteLine($"Event received from {sender.GetType().Name}: {message}");
            // raise event
            pub.RaiseEvent("Hello from publisher!");

            Console.WriteLine("\n=== End of demo ===");
        }
    }
}


