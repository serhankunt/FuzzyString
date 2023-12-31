﻿using System;
using System.Linq;
namespace FuzzyString
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            string[] names = {
                "Serhan",
                "Betül",
                "Özge",
                "Mustafa",
                "Necla",
                "Nurhan",
                "Franny"
            };
           
            string searchString = "Fran";
            int maxDistance = 2;
           
            var matches = from name in names
                          let distance = LevenshteinDistance(name, searchString)
                          where distance <= maxDistance
                          select new
                          {
                              Name = name,
                              Distance = distance
                          };
           
            foreach (var match in matches)
            {
                Console.WriteLine("Matching string: {0}, Distance: {1},Searching Name:{2}", match.Name, match.Distance,searchString);
            }
        }
        public static int LevenshteinDistance(string s, string t)
        {
            
            if (s == t) return 0;
            if (s.Length == 0) return t.Length;
            if (t.Length == 0) return s.Length;
           
            int[,] distance = new int[s.Length + 1, t.Length + 1];
            for (int i = 0; i <= s.Length; i++) distance[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) distance[0, j] = j;
           
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1), distance[i - 1, j - 1] + cost);
                }
            }
           
            return distance[s.Length, t.Length];
        }
    }
}