using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }
        public static List<int> MaxValIndex(int[] arr, List<int> l)
        {
            int max = int.MinValue;

            foreach(int i in l)
            {
                if(max < arr[i])
                    max = arr[i];
            }
            
            List<int> r = new List<int>();
            foreach(int i in l)
            {
                if(max == arr[i])
                    r.Add(i);
            }
            return r;
        }
         private static List<int> MinValIndex(int[] arr, List<int> l)
        {
            int min = int.MaxValue;

            foreach(int i in l)
            {
                if(min > arr[i])
                    min = arr[i];
            }
            
            List<int> r = new List<int>();
            foreach(int i in l)
            {
                if(min == arr[i])
                    r.Add(i);
            }
            return r;
        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int len = carbs.Length;
            int[] cal = new int[len];

            for(int i=0;i<len;i++)
                cal[i] = (protein[i] +carbs[i])*5 + (fat[i] * 9);
            
            int dp_length = dietPlans.Length;
            int[] res = new int[dp_length];

            for(int i=0;i<dp_length;i++)
            {
                List<int> list = new List<int>();
                for(int j=0;j<len;j++)
                    list.Add(j);

                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    if(dietPlans[i][j] == 't')
                        list = MinValIndex(cal, list);
                    else if(dietPlans[i][j] == 'p')
                        list = MinValIndex(protein, list);
                    else if(dietPlans[i][j] == 'c')
                        list = MinValIndex(carbs, list);
                    else if(dietPlans[i][j] == 'f')
                        list = MinValIndex(fat, list);
                    else if(dietPlans[i][j] == 'T')
                        list = MaxValIndex(cal, list);
                    else if(dietPlans[i][j] == 'P')
                        list = MaxValIndex(protein, list);
                    else if(dietPlans[i][j] == 'C')
                        list = MaxValIndex(carbs, list);
                    else if(dietPlans[i][j] == 'F')
                        list = MaxValIndex(fat, list);
                }

                res[i] = list.Min();
            }

            
            return res;
        }
    }
}
