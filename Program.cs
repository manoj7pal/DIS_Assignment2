using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DIS_Assignment2_Final
{
    class Program
    {
        /*

            Question 1:
            Given a sorted array of distinct integers and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order.
            Note: The algorithm should have run time complexity of O (log n).
            Hint: Use binary search
            Example 1:
            Input: nums = [1,3,5,6], target = 5
            Output: 2
            Example 2:
            Input: nums = [1,3,5,6], target = 2
            Output: 1
            Example 3:
            Input: nums = [1,3,5,6], target = 7
            Output: 4
            */

        public static int SearchInsert(int[] nums, int target)
        {
            //Sort the  Dictionary the input array, and find the resp indexes
            Array.Sort(nums);
            int min = 0;
            int max = nums.Length - 1;
            int middle;

            try
            {
                while (min <= max)
                {
                    middle = (min + max) / 2;

                    //Checks the middle, and left and right side of the midlle element 
                    if (target == nums[middle])
                    {
                        return middle;
                    }
                    else if (target < nums[middle])
                    {
                        max = middle - 1;
                    }
                    else if (target > nums[middle])
                    {
                        min = middle + 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return min;
        }

        /*

        Question 2

        Given a string paragraph and a string array of the banned words banned, return the most frequent word that is not banned. It is guaranteed there is at least one word that is not banned, and that the answer is unique.
        The words in paragraph are case-insensitive and the answer should be returned in lowercase.
        Example 1:
        Input: paragraph = "Bob hit a ball, the hit BALL flew far after it was hit.", banned = ["hit"]
        Output: "ball"
        Explanation: "hit" occurs 3 times, but it is a banned word. "ball" occurs twice (and no other word does), so it is the most frequent non-banned word in the paragraph. 
        Note that words in the paragraph are not case sensitive, that punctuation is ignored (even if adjacent to words, such as "ball,"), and that "hit" isn't the answer even though it occurs more because it is banned.
        Example 2:
        Input: paragraph = "a.", banned = []
        Output: "a"
        */

        public static string MostCommonWord(string paragraph, string[] banned)
        {
            char[] delim = { ' ', ',', ';', '!', '.', '?', '\'' };
            string[] words;
            HashSet<string> ban_words;
            Dictionary<string, int> word_cnts;
            string word = "", res="";

            try
            {
                words = paragraph.Split(delim);
                ban_words = new HashSet<string>();

                //Lowercase and add banned word in the dict
                foreach (var ban_wrd in banned)
                {
                    ban_words.Add(ban_wrd.ToLower());
                }

                word_cnts = new Dictionary<string, int>();

                //If not banned word, then add in the list, then return the key 
                foreach (var temp_word in words)
		        {
                    word = temp_word.ToLower();

                    if (ban_words.Contains(word) || word.Length < 1) 
                        continue;

                    if (!word_cnts.TryAdd(word, 1))
                    {
                        word_cnts[word]++;
                    }
                }

                // Convert to list and Sort Desc
                var result = word_cnts.ToList();
                result.Sort((a, b) => b.Value.CompareTo(a.Value));

                res =  result.First().Key;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return res;
        }

        /*
        Question 3:
        Given an array of integers arr, a lucky integer is an integer that has a frequency in the array equal to its value.
        Return the largest lucky integer in the array. If there is no lucky integer return -1.

        Example 1:
        Input: arr = [2,2,3,4]
        Output: 2
        Explanation: The only lucky number in the array is 2 because frequency[2] == 2.
        Example 2:
        Input: arr = [1,2,2,3,3,3]
        Output: 3
        Explanation: 1, 2 and 3 are all lucky numbers, return the largest of them.
        Example 3:
        Input: arr = [2,2,2,3,3]
        Output: -1
        Explanation: There are no lucky numbers in the array.
         */

        public static int FindLucky(int[] arr)
        {
            int lucky_num = -1000;
            bool[] visit = new bool[arr.Length];
            int count;

            try
            {
                // Go through array and count the frequencies
                if (arr.Length >= 1 && arr.Length <= 500)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] >= 1 && arr[i] <= 500)
                        {
                            if (visit[i] == true)
                                continue;
                        }
                        else
                            return -1;

                        // Frequency Counts
                        count = 1;
                        for (int j = i + 1; j < arr.Length; j++)
                        {
                            if (arr[i] == arr[j])
                            {
                                visit[j] = true;
                                count++;
                            }
                        }
                        if (arr[i] == count)
                            lucky_num = arr[i];
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return lucky_num;
        }

        /*

        Question 4:
        You are playing the Bulls and Cows game with your friend.
        You write down a secret number and ask your friend to guess what the number is. When your friend makes a guess, you provide a hint with the following info:
        •	The number of "bulls", which are digits in the guess that are in the correct position.
        •	The number of "cows", which are digits in the guess that are in your secret number but are located in the wrong position. Specifically, the non-bull digits in the guess that could be rearranged such that they become bulls.
        Given the secret number secret and your friend's guess guess, return the hint for your friend's guess.
        The hint should be formatted as "xAyB", where x is the number of bulls and y is the number of cows. Note that both secret and guess may contain duplicate digits.

        Example 1:
        Input: secret = "1807", guess = "7810"
        Output: "1A3B"
        Explanation: Bulls relate to a '|' and cows are underlined:
        "1807"
          |
        "7810"
        */

        public static string GetHint(string secret, string guess)
        {
            char[] sec_key = secret.ToCharArray();
            char[] guess_key = guess.ToCharArray();
            var sec_isNumeric = int.TryParse(secret, out _);
            var gus_isNumeric = int.TryParse(guess, out _);
            int temp1 = 0;
            int temp2 = 0;
            string op = "";

            try
            {
                if (guess_key.Length >= 1 && guess_key.Length <= 1000 && sec_key.Length >= 1 && sec_key.Length <= 1000 &&  sec_key.Length == guess_key.Length && sec_isNumeric && gus_isNumeric)
                {
                    for (int i = 0; i < sec_key.Length; i++)
                    {
                        if (sec_key[i] == guess_key[i])
                        {
                            temp1 += 1;
                        }
                        else
                        {
                            temp2 += 1;
                        }
                    }
                    op = String.Concat(temp1, 'A', temp2, 'B');
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return op;
        }

        /*
        Question 5:
        You are given a string s. We want to partition the string into as many parts as possible so that each letter appears in at most one part.
        Return a list of integers representing the size of these parts.
        Example 1:
        Input: s = "ababcbacadefegdehijhklij"
        Output: [9,7,8]
        Explanation:
        The partition is "ababcbaca", "defegde", "hijhklij".
        This is a partition so that each letter appears in at most one part.
        A partition like "ababcbacadefegde", "hijhklij" is incorrect, because it splits s into less parts.
        */

        public static List<int> PartitionLabels(string s)
        {
            int[] lst_idx = new int[26];
            List<int> result = null;
            int move = 0;
            int cur_lst_idx = 0;

            //Truncate 'a' and assign the index accordingly
            try
            {
                for (int i = 0; i <= s.Length - 1; i++)
                    lst_idx[s[i] - 'a'] = i;

                result = new List<int>();
                move = 0;
                cur_lst_idx = 0;

                //Traverse and get the max index
                for (int i = 0; i <= s.Length - 1; i++)
                {
                    cur_lst_idx = Math.Max(cur_lst_idx, lst_idx[s[i] - 'a']);

                    if (cur_lst_idx == i)
                    {
                        result.Add(i + 1 - move);
                        move = i + 1;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        /*
        Question 6
        You are given a string s of lowercase English letters and an array widths denoting how many pixels wide each lowercase English letter is. Specifically, widths[0] is the width of 'a', widths[1] is the width of 'b', and so on.
        You are trying to write s across several lines, where each line is no longer than 100 pixels. Starting at the beginning of s, write as many letters on the first line such that the total width does not exceed 100 pixels. Then, from where you stopped in s, continue writing as many letters as you can on the second line. Continue this process until you have written all of s.
        Return an array result of length 2 where:
             •	result[0] is the total number of lines.
             •	result[1] is the width of the last line in pixels.

        Example 1:
        Input: widths = [10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10], s = "abcdefghijklmnopqrstuvwxyz"
        Output: [3,60]
        Explanation: You can write s as follows:
                     abcdefghij  	 // 100 pixels wide
                     klmnopqrst  	 // 100 pixels wide
                     uvwxyz      	 // 60 pixels wide
                     There are a total of 3 lines, and the last line is 60 pixels wide.
         Example 2:
         Input: widths = [4,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10], 
         s = "bbbcccdddaaa"
         Output: [2,4]
         Explanation: You can write s as follows:
                      bbbcccdddaa  	  // 98 pixels wide
                      a           	 // 4 pixels wide
                      There are a total of 2 lines, and the last line is 4 pixels wide.
         */

        public static List<int> NumberOfLines(int[] widths, string s)
        {

            int addition = 0;
            int row = 0;
            char[] s1 = s.ToCharArray();
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            List<int> res = new List<int>();

            try
            {
                for (char c = 'a'; c <= 'z'; c++)
                {
                    int val = c - 'a';
                    dictionary.Add(c, val);
                }

                foreach (char c in s1)
                {
                    addition = addition + widths[dictionary[c]];

                    if (addition >= 100)
                    {
                        row = row + 1;
                        addition = 0;

                        if(addition>100)
                            addition = widths[dictionary[c]];
                    }
                }

                res.Add(row);
                res.Add(addition);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return res;
        }

        /*

        Question 7:
        Given a string bulls_string10 containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.
        An input string is valid if:
            1.	Open brackets must be closed by the same type of brackets.
            2.	Open brackets must be closed in the correct order.

        Example 1:
        Input: bulls_string10 = "()"
        Output: true
        Example 2:
        Input: bulls_string10  = "()[]{}"
        Output: true
        Example 3:
        Input: bulls_string10  = "(]"
        Output: false
        */

        public static bool IsValid(string bulls_string10)
        {
            StringBuilder sb = new StringBuilder(bulls_string10);

            try
            {
                if ((sb.Replace("{}", "").Replace("()", "").Replace("[]", "")).Length == 0 && bulls_string10.Length >= 1 && bulls_string10.Length <= 10000 && Regex.IsMatch(bulls_string10, "[{}()\\[\\]]"))
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /*
         Question 8
        International Morse Code defines a standard encoding where each letter is mapped to a series of dots and dashes, as follows:
        •	'a' maps to ".-",
        •	'b' maps to "-...",
        •	'c' maps to "-.-.", and so on.
        For convenience, the full table for the 26 letters of the English alphabet is given below:
        [".-","-...","-.-.","-..",".","..-.","--.","....","..",".---","-.-",".-..","--","-.","---",".--.","--.-",".-.","...","-","..-","...-",".--","-..-","-.--","--.."]
        Given an array of strings words where each word can be written as a concatenation of the Morse code of each letter.
            •	For example, "cab" can be written as "-.-..--...", which is the concatenation of "-.-.", ".-", and "-...". We will call such a concatenation the transformation of a word.
        Return the number of different transformations among all words we have.

        Example 1:
        Input: words = ["gin","zen","gig","msg"]
        Output: 2
        Explanation: The transformation of each word is:
        "gin" -> "--...-."
        "zen" -> "--...-."
        "gig" -> "--...--."
        "msg" -> "--...--."
        There are 2 different transformations: "--...-." and "--...--.".
        */

        public static int UniqueMorseRepresentations(string[] words)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            Dictionary<char, string> morse = new Dictionary<char, string>()
            {
                {'a' , ".-"}, {'b' , "-..."}, {'c' , "-.-."},{'d' , "-.."},{'e' , "."},{'f' , "..-."},{'g' , "--."},{'h' , "...."},{'i' , ".."},
                {'j' , ".---"},{'k' , "-.-"},{'l' , ".-.."},{'m' , "--"},{'n' , "-."},{'o' , "---"},{'p' , ".--."},{'q' , "--.-"},{'r' , ".-."},
                {'s' , "..."},{'t' , "-"},{'u' , "..-"},{'v' , "...-"},{'w' , ".--"},{'x' , "-..-"},{'y' , "-.--"},{'z' , "--.."}
            };
            string value = "";

            try
            {
                if (words.Length >= 1 && words.Length <= 100)
                {
                    foreach (string word in words)
                    {
                        value = "";

                        if (word.Length >= 1 && word.Length <= 12 && word.ToLower() == word)
                        {
                            foreach (char c in word)
                            {
                                value += morse[c];
                            }
                        }
                        else new Exception();

                        if (res.ContainsKey(value))
                            res[value] += 1;
                        else
                            res[value] = 1;
                    }
                }
                else new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input: " + e.Message);
            }

            return res.Count;
        }

        /*

        Question 9:
        You are given an n x n integer matrix grid where each value grid[i][j] represents the elevation at that point (i, j).
        The rain starts to fall. At time t, the depth of the water everywhere is t. You can swim from a square to another 4-directionally adjacent square if and only if the elevation of both squares individually are at most t. You can swim infinite distances in zero time. Of course, you must stay within the boundaries of the grid during your swim.
        Return the least time until you can reach the bottom right square (n - 1, n - 1) if you start at the top left square (0, 0).
        Example 1:
        Input: grid = [[0,1,2,3,4],[24,23,22,21,5],[12,13,14,15,16],[11,17,18,19,20],[10,9,8,7,6]]
        Output: 16
        Explanation: The final route is shown.
        We need to wait until time 16 so that (0, 0) and (4, 4) are connected.
        */

        public static int SwimInWater(int[][] grid)
        {
            int len = grid.Length;
            int[][] distances = new int[len][];

            if (grid is null)
                return 0;

            try
            {
                for (int i = 0; i < len; i++)
                    distances[i] = new int[len];

                // Calculate the distance for last row w.r.t to destination
                for (int i = len - 1; i >= 0; i--)
                {
                    if (i + 1 == len)
                        distances[len - 1][i] = grid[len - 1][i];
                    else
                        distances[len - 1][i] = Math.Max(grid[len - 1][ i], distances[len - 1][i + 1]);
                }

                // Calculate the dist for other rows
                for (int i = len - 2; i >= 0; i--)
                {
                    for (int j = 0; j < len; j++)
                        distances[i][j] = Math.Max(grid[i][j], distances[i + 1][j]);

                    for (int j = 0; j < len; j++)
                        cal_dist(i, j, grid, distances);
                }

                //Inner Recursive function to calculate the shortest distance
                 void cal_dist(int i, int j, int[][] grid, int[][] distances)
                {
                    //Recursive process to find the minimum path to reach the destination
                    if (i > 0 && distances[i - 1][j] > distances[i][j])
                    {
                        distances[i - 1][j] = Math.Max(distances[i][j], grid[i - 1][j]);
                        cal_dist(i - 1, j, grid, distances);
                    }

                    if (i < grid.Length - 1 && distances[i + 1][j] > distances[i][j])
                    {
                        distances[i + 1][j] = Math.Max(distances[i][j], grid[i + 1][j]);
                        cal_dist(i + 1, j, grid, distances);
                    }

                    if (j > 0 && distances[i][j - 1] > distances[i][j])
                    {
                        distances[i][j - 1] = Math.Max(distances[i][j], grid[i][j - 1]);
                        cal_dist(i, j - 1, grid, distances);
                    }

                    if (j < grid.Length - 1 && distances[i][j + 1] > distances[i][j])
                    {
                        distances[i][j + 1] = Math.Max(distances[i][j], grid[i][j + 1]);
                        cal_dist(i, j + 1, grid, distances);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return distances[0][0];
        }

        /*

        Question 10:
        Given two strings word1 and word2, return the minimum number of operations required to convert word1 to word2.
        You have the following three operations permitted on a word:
        •	Insert a character
        •	Delete a character
        •	Replace a character
         Note: Try to come up with a solution that has polynomial runtime, then try to optimize it to quadratic runtime.
        Example 1:
        Input: word1 = "horse", word2 = "ros"
        Output: 3
        Explanation: 
        horse -> rorse (replace 'h' with 'r')
        rorse -> rose (remove 'r')
        rose -> ros (remove 'e')
        */

        public static int MinDistance(string word1, string word2)
        {
            int len_word1 = word1.Length;
            int len_word2 = word2.Length;

            //Previous computation results
            int[,] prev_comp = new int[2, len_word1 + 1];

            try
            {
                //2nd string Empty 
                for (int i = 0; i <= len_word1; i++)
                    prev_comp[0, i] = i;

                //Prev Computation for each char in 2nd string
                for (int i = 1; i <= len_word2; i++)
                {
                    //Compare: second string and  first string
                    for (int j = 0; j <= len_word1; j++)
                    {
                        if (j == 0)
                            prev_comp[i % 2, j] = i;

                        else if (word1[j - 1] == word2[i - 1])
                        {
                            prev_comp[i % 2, j] = prev_comp[(i - 1) % 2, j - 1];
                        }
                        else
                        {
                            prev_comp[i % 2, j] = 1 + Math.Min(prev_comp[(i - 1) % 2, j], Math.Min(prev_comp[i % 2, j - 1], prev_comp[(i - 1) % 2, j - 1]));
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return prev_comp[len_word2 % 2, len_word1];
        }

    
        static void Main(string[] args)
        {

            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 2, 3, 12 };
            Console.WriteLine("Enter the target number:");
            int target = Int32.Parse(Console.ReadLine());
            int pos = SearchInsert(nums1, target);
            Console.WriteLine("Insert Position of the target is : {0}", pos);
            Console.WriteLine("");

            //Question2:
            Console.WriteLine("Question 2");
            string paragraph = "Bob hit a ball, the hit BALL flew far after it was hit.";
            string[] banned = { "hit" };
            string commonWord = MostCommonWord(paragraph, banned);
            Console.WriteLine("Most frequent word is {0}:", commonWord);
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] arr1 = { 2, 2, 3, 4 };
            int lucky_number = FindLucky(arr1);
            Console.WriteLine("The Lucky number in the given array is {0}", lucky_number);
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string secret = "1807";
            string guess = "7810";
            string hint = GetHint(secret, guess);
            Console.WriteLine("Hint for the guess is :{0}", hint);
            Console.WriteLine();


            //Question 5:
            Console.WriteLine("Question 5");
            string s = "ababcbacadefegdehijhklij";
            List<int> part = PartitionLabels(s);
            Console.WriteLine("Partation lengths are:");
            for (int i = 0; i < part.Count; i++)
            {
                Console.Write(part[i] + "\t");
            }
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] widths = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            string bulls_string9 = "abcdefghijklmnopqrstuvwxyz";
            List<int> lines = NumberOfLines(widths, bulls_string9);
            Console.WriteLine("Lines Required to print:");
            for (int i = 0; i < lines.Count; i++)
            {
                Console.Write(lines[i] + "\t");
            }
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string bulls_string10 = "()[]{}";
            bool isvalid = IsValid(bulls_string10);
            if (isvalid)
                Console.WriteLine("Valid String");
            else
                Console.WriteLine("String is not Valid");

            Console.WriteLine();
            Console.WriteLine();


            //Question 8:
            Console.WriteLine("Question 8");
            string[] bulls_string13 = { "gin", "zen", "gig", "msg" };
            int diffwords = UniqueMorseRepresentations(bulls_string13);
            Console.WriteLine("Number of with unique code are: {0}", diffwords);
            Console.WriteLine();
            Console.WriteLine();

            //Question 9:
            //The driver code and the definition was incorrect - corrected it as per the assignment question.
            Console.WriteLine("Question 9");
            int[][] grid = { new int[] { 0, 1, 2, 3, 4 }, new int[] { 24, 23, 22, 21, 5 }, new int[] { 12, 13, 14, 15, 16 }, new int[] { 11, 17, 18, 19, 20 }, new int[] { 10, 9, 8, 7, 6 } };
            //int[,] grid = { { 0, 1, 2, 3, 4 }, { 24, 23, 22, 21, 5 }, { 12, 13, 14, 15, 16 },  { 11, 17, 18, 19, 20 }, { 10, 9, 8, 7, 6 } };
            int time = SwimInWater(grid);
            Console.WriteLine("Minimum time required is: {0}", time);
            Console.WriteLine();

            //Question 10:
            Console.WriteLine("Question 10");
            string word1 = "horse";
            string word2 = "ros";
            int minLen = MinDistance(word1, word2);
            Console.WriteLine("Minimum number of operations required are {0}", minLen);
            Console.WriteLine();
        }
    }
}
