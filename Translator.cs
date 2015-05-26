﻿using System;
using System.Collections.Generic;
using System.IO;

namespace toTraditionalC
{
    public class Translator
    {
        private static DateTime _lastWriteTime;
        private static List<string[]> _map = new List<string[]>();

        public static string Translate(string src)
        {
            UpdateMap();
            foreach (string[] pair in _map)
            {
                try
                {
                    src = src.Replace(pair[0], pair[1]);
                }
                catch (ArgumentNullException)
                {
                }
                catch (ArgumentException)
                {
                }
            }
            return src;
        }

        private static void UpdateMap()
        {

            if (_lastWriteTime == null || _lastWriteTime != new FileInfo("map.csv").LastWriteTime)
            {
                _lastWriteTime = new FileInfo("map.csv").LastWriteTime;
                _map.Clear();
                using (StreamReader sr = new StreamReader("map.csv"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(',');
                        _map.Add(new[] { tokens[0].Trim(), tokens[1].Trim()});
                    }
                }
                Console.WriteLine("updated");
            }
            
        }
    }
}