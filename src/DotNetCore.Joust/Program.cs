using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetCore.Joust
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Welcome the user and prompt them for the 4 inputs necessary
            Console.WriteLine("Welcome to the Carpet Quote Generator!");
            Console.WriteLine("Please enter the square footage required and press Enter: ");
            String footageString = Console.ReadLine();
            Console.WriteLine("Please enter the number of rooms and press Enter: ");
            String roomsString = Console.ReadLine();
            Console.WriteLine("Please enter the hourly cost of labor and press Enter: ");
            String laborString = Console.ReadLine();
            Console.WriteLine("Please enter the desired grade of carpet and press Enter: ");
            String gradeString = Console.ReadLine();

            Console.WriteLine("Square Footage: " + footageString + "\nNumber of Rooms: " + roomsString + 
            "\nHourly Cost of Labor: " + laborString + "\nCarpet Grade: " + gradeString);

            //Create a temporary path that finds the current directory and navigates up by 2 folders
            String tempPath = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\"));
            
            //Create a path that combines the temporary path above and adds the data folder to the end of it
            String path = Path.Combine(tempPath, @"data\");

            //Console.WriteLine("\npath: " + path);

            //create an array that stores each csv full file path and extension
            String[] csvFiles = Directory.GetFiles(path, "*.csv");

            //declare and integer to store the total number of csv files found
            int numFiles = csvFiles.Length;

            //create a list that stores all the csv files including their full path and extension
            List<String[]> csvList = new List<String[]>();

            //cycle through each of the strings in the array using a foreach statement
            foreach (string s in csvFiles)
            {
                //remove the file path from each of the strings
                String tempS = s.Remove(0, path.Length);

                //store each string that is split up based on the periods
                csvList.Add(tempS.Split('.'));

                //Console.WriteLine(tempS + "\n");
            }

            //Console.WriteLine(numFiles);

            
            //String[] csvRecents;
            //String tempDate = null;
            //String lastDate = null;
            int x = 0;
            int i = 0;
            int totalNames = 0;
            

            Console.WriteLine("Number of Files: " + numFiles);

            //first count the number of unique company names in the list
            while(i < numFiles)
            {
                String tempName = csvList[i][0];

                for(int k = i; k < numFiles; k++)
                {
                    if(tempName == csvList[k][0])
                    {
                        i++;
                    }
                }
                totalNames++;
            }

            //create an array of integers that has the number of ints as there are company names
            int[] temps = new int[totalNames];

            Console.WriteLine("Total Companies: " + totalNames);

            //reset i to 0
            i = 0;

            while(i < numFiles)
            {
                String tempComp = csvList[i][0];
                String tempDate = null;
                String lastDate = null;

                for(int j = i; j < numFiles; j++)
                {
                    if(tempComp == csvList[j][0])
                    {
                        if(tempDate != null)
                        {
                            lastDate = tempDate;
                        }
                        else
                        {
                            temps[x] = i;
                        }
                        tempDate = csvList[j][1] + csvList[j][2] + csvList[j][3];

                        if(lastDate != null)
                        {
                            if(Int32.Parse(tempDate) > Int32.Parse(lastDate))
                            {
                                temps[x] = i;
                            }
                            else
                            {
                                //do nothing
                            }
                        }

                        //Console.WriteLine(csvList[j][0] + " " + tempDate + "\n");
                        //temps[j] = Int32.Parse(tempDate);

                        i++;
                    }
                }
                x++;
            }

            Console.WriteLine("\nfirst: " + temps[0] + "\nsecond: " + temps[1] + "\nthird: " + temps[2] + 
            "\nfourth: " + temps[3] + "\nfifth: " + temps[4]);

            

        }
    }
}
