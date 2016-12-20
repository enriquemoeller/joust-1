using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DotNetCore.Joust
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Welcome the user to the program
            Console.WriteLine("Welcome to the Carpet Quote Generator!");

            //prompt user for square footage and store the input to a string
            Console.WriteLine("\nPlease enter the square footage required and press Enter: ");
            String footageString = Console.ReadLine();

            //declare an array of integers to store the user input
            int[] userInputs = new int[4];

            //check to see if the input is a valid integer and keep asking for it if not
            while (int.TryParse(footageString, out userInputs[0]) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the square footage required and press Enter: ");
                footageString = Console.ReadLine();
            }

            //promt the user for number of rooms and store the input to a string
            Console.WriteLine("\nPlease enter the number of rooms and press Enter: ");
            String roomsString = Console.ReadLine();

            while (int.TryParse(roomsString, out userInputs[1]) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the number of rooms and press Enter: ");
                roomsString = Console.ReadLine();
            }

            Console.WriteLine("\nPlease enter the hourly cost of labor and press Enter: ");
            String laborString = Console.ReadLine();

            while (int.TryParse(laborString, out userInputs[2]) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the hourly cost of labor and press Enter: ");
                laborString = Console.ReadLine();
            }

            Console.WriteLine("\nPlease enter the desired grade of carpet and press Enter: ");
            String gradeString = Console.ReadLine();

            while (int.TryParse(gradeString, out userInputs[3]) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the desired grade of carpet and press Enter: ");
                gradeString = Console.ReadLine();
            }

            //Create a temporary path that finds the current directory and navigates up by 2 folders
            String tempPath = Path.GetFullPath(Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\"));
            
            //Create a path that combines the temporary path above and adds the data folder to the end of it
            String path = Path.Combine(tempPath, @"data\");

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
            }

            //declare 3 new integers for counting and storing the total unique amount of company names
            int x = 0;
            int i = 0;
            int totalNames = 0;
            
            //first count the number of unique company names in the list and store in total names
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

            //reset i to 0 for counting purposes
            i = 0;

            //loop until i is equal to number of files
            while(i < numFiles)
            {
                //create 3 new strings to store the current company name, current company date, and the last company date
                String tempComp = csvList[i][0];
                String tempDate = null;
                String lastDate = null;

                //loop as many times as there are files starting from the index i
                for(int j = i; j < numFiles; j++)
                {
                    //check to see if the current company stored is the same as the next one in the list
                    if(tempComp == csvList[j][0])
                    {
                        //check to see if the current date is null or not and store it into last date if it is not
                        if(tempDate != null)
                        {
                            lastDate = tempDate;
                        }
                        //if it is null, store the current i index into the temps array
                        else
                        {
                            temps[x] = i;
                        }
                        //piece togeth the date parts and store into the temp date string
                        tempDate = csvList[j][1] + csvList[j][2] + csvList[j][3];

                        //check to see if the last date was null and if not, check iff the temp date is greater than last date, if so, 
                        //store the i value into temps at the x index
                        if(lastDate != null)
                        {
                            if(Int32.Parse(tempDate) > Int32.Parse(lastDate))
                            {
                                temps[x] = i;
                            }
                        }

                        //add 1 to i
                        i++;
                    }
                }

                //add 1 to x
                x++;
            }

            //create a list of string arrays to hold the data in the csv files
            List<String[]> csvLines = new List<String[]>();

            //create an array of integers the size of the amount of company names that will store the total number of data lines for each file
            int[] totValues = new int[totalNames];

            //loops as many times as there are unique company names
            for(int l = 0; l < totalNames; l++)
            {
                //Read all the text from the current csv file and store it into a temporary array
                String[] tempValues = File.ReadAllText(csvFiles[temps[l]]).Split('\n');
                //set the total data values of this current csv file company l index into the total values array
                totValues[l] = tempValues.Length - 1;
                //remove the last item from the tempvalues array as it is an empty return line
                tempValues = tempValues.Take(totValues[l]).ToArray();
                //add the temp values data to the csv lines list
                csvLines.Add(tempValues);
            }

            //declare a new list to store all carpets
            List<Carpet> allCarpets = new List<Carpet>();

            //loop as many times as there are unique company names
            for(int j = 0; j < totalNames; j++)
            {
                //loop through all items in the csvLines file at this index j
                foreach (String line in csvLines[j])
                {
                    //store the data from the current line into an array using comma as split
                    String[] tempValues = line.Split(',');
                    //calculate and store the square footage
                    int sqFootage = Int32.Parse(tempValues[2]) * Int32.Parse(tempValues[3]);
                    //calculate and store the price per square foot
                    float pricePerSqFoot = float.Parse(tempValues[4]) / sqFootage;
                    //add all the values to the carpets list as a new carpet object
                    allCarpets.Add(new Carpet {carpetId = tempValues[0], grade = Int32.Parse(tempValues[1]), length = Int32.Parse(tempValues[2]), 
                    width = Int32.Parse(tempValues[3]), price = float.Parse(tempValues[4]), squareFootage = sqFootage, 
                    pricePerSquareFoot = pricePerSqFoot, companyIndex = temps[j]});
                }
            }
            
            //create a new carpet list that stores all carpets in ascending order by price per square foot
            List<Carpet> carpetsByPricePerSqFoot = allCarpets.OrderBy(k => k.pricePerSquareFoot).ToList();

            //create a new list of carpets for storing the carpets selected for current quote
            List<Carpet> carpetsForThisQuote = new List<Carpet>();

            //create a new list of quotes for storing potential quotes
            List<Quote> potentialQuotes = new List<Quote>();

            //create a new list for counting purposes
            int countCarpets = 0;

            //create a new integer with the current grade the user chose
            int thisGrade = userInputs[3];

            //create a new carpet list that stores all carpets with the grade chosen by the user
            List<Carpet> carpetsBySelectedGrade = carpetsByPricePerSqFoot.Where(k => k.grade == userInputs[3]).ToList();

            //loop until grade is greater than 9
            while(thisGrade < 10)
            {
                //create a new integer that stores the number of carpets in the ordered by grade list at this point in time
                int initialCount = carpetsBySelectedGrade.Count;

                //loop until the carpet cont is equal to or more than the initial count
                while(countCarpets < initialCount)
                {
                    //set the ordered by grade list to the original set of values from the old list using the current grade
                    carpetsBySelectedGrade = carpetsByPricePerSqFoot.Where(k => k.grade == thisGrade).ToList();

                    //loop until the count of carpets ordered by grade is equal to or less than 1
                    while (carpetsBySelectedGrade.Count > 1)
                    {
                        //declare an integer to store the accumulate square footage of current quote and set to 0
                        int accumulatedSqFootage = 0;

                        //loop until the accumulated square footage is equal to or more than the user's required square footage or
                        //until the count for carpets by grade is equal to or less than 0
                        while (accumulatedSqFootage < userInputs[0] && carpetsBySelectedGrade.Count > 0)
                        {
                            //check to see if carpet count is less than the number of carpets in the ordered by grade list
                            if(countCarpets < carpetsBySelectedGrade.Count())
                            {
                                //add the current carpet selected in ordered by grade list to the carpets for quote list
                                carpetsForThisQuote.Add(carpetsBySelectedGrade[countCarpets]);

                                //remove the carpet that was just added to the quote list
                                carpetsBySelectedGrade.RemoveAt(countCarpets);

                                //set the accumulated footage to the sum of all square footage in the carpets for quote list
                                accumulatedSqFootage = carpetsForThisQuote.Sum(k => k.squareFootage);
                            }

                            else
                            {
                                //remove the value at the 0 input in the carpets by grade list to avoid infinite looping
                                carpetsBySelectedGrade.RemoveAt(0);
                            }
                        }

                        //check to see if the square footage sum of the carpets for quote are greater than or equal to user's requirement
                        if(carpetsForThisQuote.Sum(k => k.squareFootage) >= userInputs[0])
                        {
                            //create 3 temporary floats for storing calculated material cost, labor cost, and total price
                            float materialCostTemp = carpetsForThisQuote.Sum(k => k.price);
                            float laborCostTemp = (carpetsForThisQuote.Count() * (0.5f * userInputs[2])) + (userInputs[1] * (0.5f * userInputs[2]));
                            float priceTemp = materialCostTemp + laborCostTemp;

                            //add a new quote to the potential quotes list using the current data acquired from the loops above
                            potentialQuotes.Add(new Quote {Price = priceTemp, MaterialCost = materialCostTemp, LaborCost = laborCostTemp, 
                                RollOrders = carpetsForThisQuote.Select(k => k.carpetId).ToArray()});
                        }
                    }
                    //add 1 to the carpets count
                    countCarpets++;
                }
                //add 1 to the grade
                thisGrade++;
            }

            //create a quote called cheapest quote and store the first quote that shows up in the potential quotes list ordered by price
            Quote cheapestQuote = potentialQuotes.OrderBy(k => k.Price).FirstOrDefault();

            //display the contents of the quote to the user by contatenating the strings and values and using a foreach for the roll orders
            Console.WriteLine("\nCheapest Quote: \n" + "\nPrice: $" + cheapestQuote.Price + "\nMaterial Cost: $" + cheapestQuote.MaterialCost + 
            "\nLaborCost: $" + cheapestQuote.LaborCost + "\nRollOrders: ");
            
            foreach(string s in cheapestQuote.RollOrders)
            {
                Console.WriteLine(s);
            }
        }
    }
}
