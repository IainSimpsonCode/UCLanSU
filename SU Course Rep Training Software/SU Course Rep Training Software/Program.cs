using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU_Course_Rep_Training_Software
{
    internal class Program
    {

        class MatchingRecords
        {
            public readonly int QuizIndex = 0;
            public readonly int RepIndex = 0;

            public MatchingRecords(int quizIndex, int repIndex)
            {
                QuizIndex = quizIndex;
                RepIndex = repIndex;
            }
        }


        static void Main(string[] args)
        {
            List<string> failedMatches = new List<string>();
            List<string> unmatchedRecords = new List<string>();
            List<MatchingRecords> matchedRecords = new List<MatchingRecords>();

            List<CSVFile> courseRepFiles = new List<CSVFile>();
            courseRepFiles.Add(new CSVFile("Engineering Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Engineering Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Art Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Art Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Business Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Business Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Health Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Health Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Law Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Law Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Med Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Med Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Nursing Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Nursing Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Pharmacy Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Pharmacy Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Psychology Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Psychology Reps CSV.csv", 18));
            courseRepFiles.Add(new CSVFile("Vet Rep File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\Training\Vet Reps CSV.csv", 18));
            CSVFile QuizFile = new CSVFile("Quiz File", @"C:\Users\Iain Simpson\Desktop\UCLan SU\Admin Role\CRep Training CSV.csv", 4);

            // Create a list of emails and corresponding dates of people who have finished the quiz
            List<QuizEmail> completedReps = new List<QuizEmail>();
            foreach (List<string> courseReps in QuizFile.ImportedData)
            {
                completedReps.Add(new QuizEmail(courseReps[2], courseReps[3], courseReps[1]));
            }

            // Check each person who has completed the quiz against each person in the course rep files
            for (int i = 0; i < completedReps.Count; i++)
            {
                Console.WriteLine("Checking: " + completedReps[i].emailAddress);
                foreach (CSVFile file in courseRepFiles)
                {
                    Console.WriteLine(" File: " + file.Name);
                    foreach (List<string> possibleMatch in file.ImportedData)
                    {
                        //Console.WriteLine("     " + possibleMatch[5].ToLower() + " = " + completedReps[i].emailAddress.ToLower());
                        if (possibleMatch[5].ToLower().Trim() == completedReps[i].emailAddress.ToLower().Trim())
                        {
                            Console.WriteLine("     - Match Found");
                            // Match found
                            possibleMatch[14] = completedReps[i].dateCompleted;
                            completedReps[i].matchFound = true;
                        }
                    }
                }
            }

            Console.WriteLine("\n\n=========================================\n\n");

            // Print any reps who didnt get matched
            int count = 0;
            foreach (QuizEmail courseRep in completedReps)
            {
                if (courseRep.matchFound == false)
                {
                    Console.WriteLine(courseRep.rawData + ", " + courseRep.emailAddress);
                    count++;
                }
            }
            Console.WriteLine("Number of unmatched records: " + count);

            Console.WriteLine("\n\n=========================================\n\n");

            // Export all files with updated information
            foreach (CSVFile file in courseRepFiles)
            {
                file.Export();
                Console.WriteLine(file.Name + " Exported");
            }

            Console.ReadLine();
        }
    }
}
