using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace DesignPatterns
{
    public class SingleResponsabilityPrinciple
    {
        private readonly List<String> entries = new List<String>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}: {text}");
            return count; //memento pattern
        }
        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }
        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(SingleResponsabilityPrinciple j, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, j.ToString());
        }
    }
    public class Demo
    {
        static void Main(string[] args)
        {
            var j = new SingleResponsabilityPrinciple();
            j.AddEntry("Its sunny today");
            j.AddEntry("I drank some coffe");
            WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
