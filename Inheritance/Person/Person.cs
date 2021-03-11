using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        public string name;
        public int age;

        public Person(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Name: {this.name}, Age: {this.age}");
            return stringBuilder.ToString().TrimEnd();
        }

    }
}
