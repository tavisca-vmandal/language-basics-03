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
        
        public static List<int> CreateList(int length)
        {
            List<int> indexList = new List<int>();

            for(int j=0;j<length;j++)
                indexList.Add(j);
            
             return indexList;
        }
        public static List<int> MaximumValueIndex(int[] nutrients, List<int> indexList)
        {
            int maximum = int.MinValue;

            foreach(int i in indexList)//find maximum value meal 
            {
                if(maximum < nutrients[i])
                    maximum = nutrients[i];
            }
            
            List<int> updatedList = new List<int>();

            foreach(int i in indexList)//create a list for same maximum value meals
            {
                if(maximum == nutrients[i])
                    updatedList.Add(i);
            }
            return updatedList;
        }
         private static List<int> MinimumValueIndex(int[] nutrients, List<int> indexList)
        {
            int min = int.MaxValue;

            foreach(int i in indexList)//find minimum value meal 
            {
                if(min > nutrients[i])
                    min = nutrients[i];
            }
            
            List<int> updatedList = new List<int>();

            foreach(int i in indexList)//create a list for same minimum value meals
            {
                if(min == nutrients[i])
                    updatedList.Add(i);
            }
            return updatedList;
        }
        public static int[] CalculateCalorie(int[] protein, int[] carbs, int[] fat,int length)
        {
            //calculate calorie array according to protein,fat and carbs

            int[] calorie=new int[length];

            for(int i=0;i<length;i++)
                calorie[i] = (protein[i] +carbs[i])*5 + (fat[i] * 9);

            return calorie;

        }
        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            int length = carbs.Length;
            int[] calorie = CalculateCalorie(protein,carbs,fat,length);//calculate calorie array according to protein,fat and carbs

            int lengthOfDietPlan = dietPlans.Length;
            int[] resultantIndex = new int[lengthOfDietPlan];//resultantInde array for each DietPlan

            for(int i=0;i<lengthOfDietPlan;i++)
            {

                List<int> indexList = CreateList(length);//list to keep track of the index of meals

                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    if(dietPlans[i][j] == 't')
                        indexList = MinimumValueIndex(calorie, indexList);
                    else if(dietPlans[i][j] == 'p')
                        indexList = MinimumValueIndex(protein, indexList);
                    else if(dietPlans[i][j] == 'c')
                        indexList = MinimumValueIndex(carbs, indexList);
                    else if(dietPlans[i][j] == 'f')
                        indexList = MinimumValueIndex(fat, indexList);
                    else if(dietPlans[i][j] == 'T')
                        indexList = MaximumValueIndex(calorie, indexList);
                    else if(dietPlans[i][j] == 'P')
                        indexList = MaximumValueIndex(protein, indexList);
                    else if(dietPlans[i][j] == 'C')
                        indexList = MaximumValueIndex(carbs, indexList);
                    else if(dietPlans[i][j] == 'F')
                        indexList = MaximumValueIndex(fat, indexList);
                }

                resultantIndex[i] = indexList.Min();
            }
            return resultantIndex;
        }
    }
}
