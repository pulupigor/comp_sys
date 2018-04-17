using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Arithmetic_operations
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exec = true;
            do
            {
                Console.OutputEncoding = Encoding.Default;
                try
                {
                    Console.WriteLine("Entet wishful operation 1- * , 2- / , 3- float , 4-exit");
                    int operation = int.Parse(Console.ReadLine());
                    switch (operation)
                    {
                        case 1:
                            int multiplicand = int.Parse(Console.ReadLine());
                            int multiplier = int.Parse(Console.ReadLine());
                            Console.WriteLine("Result is: " + Multiplication.ShiftingRight(multiplicand, multiplier));
                            Console.ReadKey();
                            break;
                        case 2:
                            int dividend = int.Parse(Console.ReadLine());
                            int divisor = int.Parse(Console.ReadLine());
                            Console.WriteLine("Result is:" + Division.ShiftingRight(dividend, divisor));
                            Console.ReadKey();
                            break;
                        case 3:
                            float multiplicandF = float.Parse(Console.ReadLine());
                            float multiplierF = float.Parse(Console.ReadLine());
                            Console.WriteLine("Result is: {0}", IEEE.MultiplyIEEE(multiplicandF,multiplierF));
                            Console.ReadKey();
                            break;
                        case 4:
                            exec = false;
                            break;
                        default: break;
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Too big value!!!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong format!!!");
                }
            }
            while (exec);
        }
    }
    public class Multiplication
    {
        public static long ShiftingRight(int multiplicand, int multiplier)
        {
            bool isMultiplierNegative = multiplier < 0;
            if (isMultiplierNegative)
                multiplier = ~multiplier + 1;
            long shiftedMultiplicand = multiplicand;
            long product = 0;
            shiftedMultiplicand <<= 32;
            string noAction = " no op";
            string action = " product = product + multiplicand ";
            bool isBit1;

            Console.WriteLine("{0,-16} {4,-44} {1,32} {2,32} {3,64} \n", "Iteration", "Multiplier", "Multiplicand", "Product", "Action");
            Console.WriteLine("{0,-16}                                              {1,32} {2,32} {3,64} \n", "Initial values:", Convert.ToString(multiplier, 2), Convert.ToString(multiplicand, 2), Convert.ToString(product, 2));

            for (int i = 0; i < 32; i++)//перебираємо всі біти множника
            {
                isBit1 = (multiplier & 1) == 1;
                if (isBit1)
                    product += shiftedMultiplicand;
                Console.WriteLine("{0,-16} {4,-44} {1,32} {2,32} {3,64} ", i, Convert.ToString(multiplier, 2), Convert.ToString(multiplicand, 2), Convert.ToString(product, 2), Convert.ToByte(isBit1) + " -> " + (isBit1 ? action : noAction));
                product >>= 1;
                multiplier >>= 1;
                Console.WriteLine("                 {3,-44} {0,32} {1,32} {2,64} \n", Convert.ToString(multiplier, 2), Convert.ToString(multiplicand, 2), Convert.ToString(product, 2), "Multiplier / Product shift right");
            }
            if (isMultiplierNegative)
                product = ~product + 1;
            return product;
        }
    }
    public class Division
    {
        public static int ShiftingRight(int dividend, int divisor)
        {
            int quotient = 0;
            long remainder = 0, divisorr;
            bool isDividendNegative = dividend < 0;
            bool isDivisorNegative = divisor < 0;
            divisorr = divisor;
            divisorr <<= 16;
            if (isDividendNegative)
                dividend = ~dividend + 1;
            if (isDivisorNegative)
                divisorr = ~divisorr + 1;

            remainder += dividend;//розміщуємо ділене у правій частині залишку
            Console.WriteLine("{0,88}" + " {1,32} " + " {2,64}\n", "Quotient", "Divisor", "Remainder");
            Console.WriteLine("{0,88}" + " {1,32} " + " {2,64}\n", Convert.ToString(quotient, 2), Convert.ToString(divisorr, 2), Convert.ToString(remainder, 2));
            for (int i = 0; i < 17; i++)
            {
                remainder -= divisorr;
                Console.WriteLine("Remainder=Remainder - Divisor                           {0,32}" + " {1,32} " + " {2,64}", Convert.ToString(quotient, 2), Convert.ToString(divisorr, 2), Convert.ToString(remainder, 2));
                if (remainder >= 0)
                {
                    quotient <<= 1;
                    quotient |= 1;
                    Console.WriteLine("Remainder >= 0 -> Shift quotient << on 1 bit,Set Q0=1   {0,32}" + " {1,32} " + " {2,64}", Convert.ToString(quotient, 2), Convert.ToString(divisorr, 2), Convert.ToString(remainder, 2));
                }
                else
                {
                    quotient <<= 1;
                    remainder += divisorr;
                    Console.WriteLine("Remainder < 0 -> Rem+=div ,Shift quotient left on 1 bit {0,32}" + " {1,32} " + " {2,64}", Convert.ToString(quotient, 2), Convert.ToString(divisorr, 2), Convert.ToString(remainder, 2));
                }
                divisorr >>= 1;
                Console.WriteLine("Shift divisor >> 1                                      {0,32}" + " {1,32} " + " {2,64}\n", Convert.ToString(quotient, 2), Convert.ToString(divisorr, 2), Convert.ToString(remainder, 2));
            }
            if (isDivisorNegative ^ isDividendNegative)
            {
                quotient = ~quotient + 1;
            }
            Console.WriteLine("Remainder: " + remainder);
            return quotient;
        }
    }
    public class IEEE
    {
        public static float MultiplyIEEE(float multiplicand, float multiplier)
        {
            byte[] multiplicandBit = BitConverter.GetBytes(multiplicand);
            byte[] multiplierBit = BitConverter.GetBytes(multiplier);
            int mantissaA, mantissaB;
            int signA, signB;
            int expA, expB;
            int resA = BitConverter.ToInt32(multiplicandBit, 0);
            int resB = BitConverter.ToInt32(multiplierBit, 0);
            const int bias = 127;

            signA = resA & Convert.ToInt32("10000000000000000000000000000000", 2);
            signB = resB & Convert.ToInt32("10000000000000000000000000000000", 2);
            expA = resA & Convert.ToInt32("01111111100000000000000000000000", 2);
            expA >>= 23;
            expB = resB & Convert.ToInt32("01111111100000000000000000000000", 2);
            expB >>= 23;
            mantissaA = resA & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);
            mantissaB = resB & Convert.ToInt32("00000000011111111111111111111111", 2) | Convert.ToInt32("00000000100000000000000000000000", 2);

            if (multiplicand == 0 || multiplier == 0)
            {
                return 0f;
            }

            Console.WriteLine("INITIAL VALUES:");
            Console.WriteLine("Multiplicand : Significand = " + Convert.ToString(signA, 2) + " Exponent = " + Convert.ToString(expA, 2) + " Mantissa = " + Convert.ToString(mantissaA, 2));
            Console.WriteLine("Multiplier   : Significand = " + Convert.ToString(signB, 2) + " Exponent = " + Convert.ToString(expB, 2) + " Mantissa = " + Convert.ToString(mantissaB, 2));

            Console.WriteLine("COMPUTE EXPONENTS:");
            int exponent = expA + expB - bias;
            Console.WriteLine(Convert.ToString(expA, 2) + "(2) * " + Convert.ToString(expB, 2) + "(2) - 127(10) = " + Convert.ToString(exponent, 2) + "\n");

            Console.WriteLine("MULTIPLY SIGNIFICANDS:");
            int significand = signA ^ signB;
            Console.WriteLine(Convert.ToString(signA, 2) + " XOR " + Convert.ToString(signB, 2) + " = " + Convert.ToString(significand, 2) + "\n");

            Console.WriteLine("NORMALIZE RESULT:");
            long mantisaLong = ShiftRightForIEEE(mantissaA, mantissaB);//перемноження мантис
            int mantisa = 0;
            Console.WriteLine("Mantissa = " + Convert.ToString(mantisaLong, 2));
            if ((mantisaLong & 0x800000000000) == 0x800000000000)//чи є 47 біт "1"
            {
                Console.WriteLine("Exponent = " + exponent + " +1");
                exponent++;
            }
            else
                mantisaLong <<= 1;

            for (int i = 0; i < 24; i++) //цикл для запису результуючих бітів мантиси
            {
                if ((mantisaLong & 0x1000000) == 0x1000000)
                {
                    mantisa |= 0x800000;
                }
                if (i == 23)
                    break;
                mantisa >>= 1;
                mantisaLong >>= 1;
            }
            mantisa &= ~(1 << 23); //скидання неявної 1
            Console.WriteLine("Final mantissa = " + "1." + Convert.ToString(mantisa, 2) + "\n");

            Console.WriteLine("FINAL RESULT:");
            Console.WriteLine(Convert.ToString(significand, 2) + " " + Convert.ToString(exponent, 2) + " " + Convert.ToString(mantisa, 2));
            int res = significand | (exponent << 23) | mantisa;
            byte[] b = BitConverter.GetBytes(res);
            return BitConverter.ToSingle(b, 0);
        }
        public static long ShiftRightForIEEE(int multiplicand, int multiplier)
        {
            bool isMultiplierNegative = multiplier < 0;
            if (isMultiplierNegative)
                multiplier = ~multiplier + 1;
            long shiftedMultiplicand = multiplicand;
            long product = 0;
            shiftedMultiplicand <<= 32;

            for (int i = 0; i < 32; i++)
            {
                if ((multiplier & 1) == 1)
                    product += shiftedMultiplicand;
                product >>= 1;
                multiplier >>= 1;
            }
            if (isMultiplierNegative)
                product = ~product + 1;
            return product;
        }
    }
}