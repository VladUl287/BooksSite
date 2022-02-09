using System;
using System.ComponentModel.DataAnnotations;

namespace api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MinMax : ValidationAttribute
    {
        public int Min { get; }
        public int Max { get; }

        public MinMax(int min = 0, int max = 100)
        {
            Min = min;
            Max = max;
        }

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int number = (int)value;

                if (number < Max && number > Min)
                {
                    return true;
                }
                else
                {
                    this.ErrorMessage = "Некорректные размеры";
                }
            }

            return false;
        }
    }
}