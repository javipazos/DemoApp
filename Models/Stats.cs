using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp.Models
{
    public class Stats
    {
        private char WordSeparator = ' ';
        private List<string> _words = new List<string>();
        private List<string> _lines = new List<string>();
        public int WordCount { get; set; }
        public int LineCount { get; set; }
        public decimal MeanLettersPerWord { get; set; }
        public int ModeLettersPerWord { get; set; }
        public decimal MedianLettersPerWord { get; set; }
        public char MostCommonLetter { get; set; }

        public Stats(List<string> lines)
        {
            if (lines is null || !lines.Any())
                return;
            _lines = lines;
            GetWords();
            CalculateWordCount();
            CalculateLineCount();
            CalculateModeLettersPerWord();
            CalculateMedianLettersPerWord();
            CalculateMostCommonLetter();
            CalculateMeanLettersPerWord();
        }

        public Stats()
        {
        }

        private void GetWords()
        {
            _lines.ForEach(l => _words.AddRange(l.Split(WordSeparator)));
        }
        private void CalculateWordCount()
        {
            WordCount = _words.Count;
        }
        private void CalculateLineCount()
        {
            LineCount = _lines.Count;
        }
        private void CalculateMeanLettersPerWord()
        {
            MeanLettersPerWord = _words.Sum(w => w.Length) / WordCount;
        }


        private void CalculateMedianLettersPerWord()
        {
            var middle = _words.GroupBy(w => w.Length).Count() / 2;
            if (_words.Count % 2 != 0)
                MedianLettersPerWord = _words.GroupBy(w => w.Length).OrderBy(g => g.Key).ElementAt(middle).Key;
            else
            {
                var firstMiddle = _words.GroupBy(w => w.Length).OrderBy(g => g.Key).ElementAt(middle - 1).Key;
                var secondMiddle = _words.GroupBy(w => w.Length).OrderBy(g => g.Key).ElementAt(middle).Key;
                MedianLettersPerWord = (firstMiddle + secondMiddle) / 2;
            }
        }

        private void CalculateModeLettersPerWord()
        {
            var groups = _words.GroupBy(w => w.Length);
            int maxCount = groups.Max(g => g.Count());
            ModeLettersPerWord = groups.First(g => g.Count() == maxCount).Key;
        }
        private void CalculateMostCommonLetter()
        {
            var letterCounter = new Dictionary<char, int>();
            foreach (var word in _words)
            {
                foreach (var letter in word.ToLower())
                {
                    if (letterCounter.ContainsKey(letter))
                        letterCounter[letter]++;
                    else
                        letterCounter.Add(letter, 1);
                }
            }
            MostCommonLetter = letterCounter.OrderByDescending(l => l.Value).First().Key;
        }



    }
}