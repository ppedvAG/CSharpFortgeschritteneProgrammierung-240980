

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestProject.BusinessLogic
{
    public class Calculator
    {
        internal int Add(int a, int b)
        {
            return a + b;
        }

        internal int CrossTotal(int number)
        {
            return number.ToString().Sum(e => (int)char.GetNumericValue(e));
        }
    }
}