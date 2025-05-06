using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizMaker
{
    public class Logic
    {       
        public static void SaveQuizToXmlFile(List<QnA> newQuestions, string path)
        {
            List<QnA> allQuestions;

            // If file exists, load existing questions, otherwise start with empty list
            if (File.Exists(path))
            {
                allQuestions = LoadQuizFromXmlFile(path);
            }
            else
            {
                allQuestions = new List<QnA>();
            }

            // Add new questions to the list
            allQuestions.AddRange(newQuestions);

            // Serialize and save all questions back to the file
            XmlSerializer serializer = new XmlSerializer(typeof(List<QnA>));
            using (FileStream file = File.Create(path))
            {
                serializer.Serialize(file, allQuestions);
            }
        }

        //It takes the raw input string from the user (e.g. "1,3") and the number of answers to validate against.
        public static List<int> ParseCorrectAnswerIndices(string userInput, int totalNumberOfOptions)//public and static- Ican call it without creating an instance
        {
            List<int> correctAnswerIndices = new List<int>(); //create a new list to store the final, converted, validated answer indices
            foreach (string answerIndexText in userInput.Split(','))//We split the user’s input string by comma.Each piece(e.g. "1", " 3") becomes a loop variable answerIndexText
            {
                if (int.TryParse(answerIndexText.Trim(), out int oneBasedIndex))//" 3" becomes "3" and parse it into int.
                {
                    int zeroBasedIndex = oneBasedIndex - Constants.ZERO_BASED_OFFSET;//"1" becomes 0
                    if (zeroBasedIndex >= 0 && zeroBasedIndex < totalNumberOfOptions)//For 4 answers, valid are 0, 1, 2, 3
                    {
                        correctAnswerIndices.Add(zeroBasedIndex);//If valid, we add it to the final list of correct indices
                    }
                }

            }
            return correctAnswerIndices; //It returns a list of int — the validated correct answer indices (zero-based).
        }

        public static List<QnA> LoadQuizFromXmlFile(string path)
        {
            if (!File.Exists(path)) //File.Exists Method is an existing method in the .NET-framework
            {
                Console.WriteLine("Quiz file not found. Please create a quiz first.");
                return new List<QnA>();
            }

            try
            {
                //Creates a serializer for a **list of QnA objects**.
                //This knows how to read from an XML file and turn the data into a usable `List < QnA >` object.
                XmlSerializer serializer = new XmlSerializer(typeof(List<QnA>));
                //Opens the XML file in read-only mode.
                //"using" makes sure the file stream is properly closed afterward(even if an error occurs).
                using (FileStream file = File.OpenRead(path))
                {
                    return (List<QnA>)serializer.Deserialize(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load quiz. Error: " + ex.Message);
                return new List<QnA>();
            }
        }
        public static List<QnA> SelectRandomQuestions(List<QnA> allQuestions, int count)
        {
            Random rng = new Random(); //rng random number generator
            //rnt.Next Gives a random integer (each time it’s different).
            //OrderBy(q => rng.Next()) Shuffles the questions randomly by assigning each question a random number.
            //Take(count) Picks the first count items (which are now randomized).
            //ToList()	Converts the result back into a list.
            return allQuestions.OrderBy(q => rng.Next()).Take(count).ToList();
        }

    }

}
        