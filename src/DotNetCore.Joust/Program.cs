using System;
using System.Collections.Generic;
using System.IO;


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

            //declare an integer for storing the square footage
            int squareFootage;

            //check to see if the input is a valid integer and keep asking for it if not
            while (int.TryParse(footageString, out squareFootage) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the square footage required and press Enter: ");
                footageString = Console.ReadLine();
            }

            //promt the user for number of rooms and store the input to a string
            Console.WriteLine("\nPlease enter the number of rooms and press Enter: ");
            String roomsString = Console.ReadLine();

            //declare an integer for storing the square footage
            int numRooms;

            while (int.TryParse(roomsString, out numRooms) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the number of rooms and press Enter: ");
                roomsString = Console.ReadLine();
            }

            Console.WriteLine("\nPlease enter the hourly cost of labor and press Enter: ");
            String laborString = Console.ReadLine();

            int hoursLabor;

            while (int.TryParse(laborString, out hoursLabor) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the hourly cost of labor and press Enter: ");
                laborString = Console.ReadLine();
            }

            Console.WriteLine("\nPlease enter the desired grade of carpet and press Enter: ");
            String gradeString = Console.ReadLine();

            int carpetGrade;

            while (int.TryParse(gradeString, out carpetGrade) == false)
            {
                Console.WriteLine("\nThat was not a valid entry, please try again...");
                Console.WriteLine("\nPlease enter the desired grade of carpet and press Enter: ");
                gradeString = Console.ReadLine();
            }

            Console.WriteLine("\nSquare Footage: " + squareFootage + "\nNumber of Rooms: " + numRooms + 
            "\nHourly Cost of Labor: " + hoursLabor + "\nCarpet Grade: " + carpetGrade);

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

            //Console.WriteLine("\n\nfile path1: " + csvFiles[temps[0]]);

            //TextFieldParser csvParser = new TextFieldParser(csvFiles[temps[0]]);

            List<String[]> csvValues = new List<String[]>();
            int[] totalValues = new int[totalNames];

            for(int l = 0; l < totalNames; l++)
            {
                String[] tempValues = File.ReadAllText(csvFiles[temps[l]]).Split(',', '\n');
                totalValues[l] = tempValues.Length;
                csvValues.Add(tempValues);
                //Console.WriteLine("totalValues" + l + ": " + totalValues[l]);
                //Console.WriteLine(totalNames);
            }

            Console.WriteLine("value1: " + csvValues[0][0]);
            Console.WriteLine("value2: " + csvValues[0][1]);
            Console.WriteLine("value3: " + csvValues[0][2]);
            Console.WriteLine("value4: " + csvValues[0][3]);
            Console.WriteLine("value5: " + csvValues[0][4]);
            Console.WriteLine("value6: " + csvValues[0][5]);

            List<String[]> cheapestCarpets = new List<String[]>();

            int z = 0;

            int thisCompany = 0;
            String thisID = null;
            int thisGrade = 0;
            int thisLength = 0;
            int thisWidth = 0;
            float thisPrice = 0;
            int thisSquareFootage = 0;
            float thisCostRatio = 0;

            int lastCompany;
            String lastID = null;
            int lastGrade;
            int lastLength;
            int lastWidth;
            float lastPrice;
            int lastSquareFootage;
            float lastCostRatio;

            //while(thisGrade <= 9)
            //{

            for(int n = 0; n < totalNames; n++)
            {
                z = 0;
                //bool firstPass = true;

                while(z < totalValues[n] - 1)
                {
                    
                    if (thisID == null)
                    {
                        thisCompany = n;
                        thisID = csvValues[n][z];
                        z++;
                        thisGrade = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisLength = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisWidth = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisPrice = float.Parse(csvValues[n][z]);
                        z++;
                        thisSquareFootage = thisLength * thisWidth;
                        thisCostRatio = thisSquareFootage / thisPrice;
                    }
                    else
                    {
                        lastCompany = thisCompany;
                        lastID = thisID;
                        lastGrade = thisGrade;
                        lastLength = thisLength;
                        lastWidth = thisWidth;
                        lastPrice = thisPrice;
                        lastSquareFootage = thisSquareFootage;
                        lastCostRatio = thisCostRatio;

                        //Console.WriteLine(z + "totalValues: " + totalValues[n]);

                        thisCompany = n;
                        thisID = csvValues[n][z];
                        z++;
                        thisGrade = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisLength = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisWidth = Int32.Parse(csvValues[n][z]);
                        z++;
                        thisPrice = float.Parse(csvValues[n][z]);
                        z++;
                        thisSquareFootage = thisLength * thisWidth;
                        thisCostRatio = thisSquareFootage / thisPrice;
                        
                        

                        if (lastGrade == carpetGrade && lastCostRatio > thisCostRatio && lastSquareFootage >= squareFootage /*&& lastSquareFootage < (squareFootage * 1.15)*/)
                        {
                            //set the last carpet as the cheapest one for now
                            String[] tempCarpet = {lastID, lastGrade.ToString(), lastLength.ToString(), lastWidth.ToString(), lastPrice.ToString(), csvList[temps[n]][0]};
                            cheapestCarpets.Clear();
                            cheapestCarpets.Add(tempCarpet);
                            //Console.WriteLine(tempCarpet[0] + " " + tempCarpet[1] + " " + tempCarpet[2] + " " + tempCarpet[3] + " " + tempCarpet[4] + " " + tempCarpet[5]);
                            //Console.WriteLine(cheapestCarpets[0][0]);
                        }

                        else if (thisGrade == carpetGrade && thisCostRatio > lastCostRatio && thisSquareFootage >= squareFootage /*&& thisSquareFootage < (squareFootage * 1.15)*/)
                        {
                            //set this carpet as the cheapest one for now
                            String[] tempCarpet = {thisID, thisGrade.ToString(), thisLength.ToString(), thisWidth.ToString(), thisPrice.ToString(), csvList[temps[n]][0]};
                            cheapestCarpets.Clear();
                            cheapestCarpets.Add(tempCarpet);
                            //Console.WriteLine(cheapestCarpets[0][0]);
                        }
                        
                        

                    }
                }
            }

            //Console.WriteLine(cheapestCarpets[0][0]);

            float totalCost = float.Parse(cheapestCarpets[0][1]) + (hoursLabor / 2) + ((hoursLabor / 2) * numRooms);
            totalCost /= .60f;
            Console.WriteLine(totalCost);

            //thisGrade++;

            //}



        }
    }
}
