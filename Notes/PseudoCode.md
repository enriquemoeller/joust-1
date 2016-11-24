User will input 4 integers:
    square footage required
    number of rooms
    hourly labor cost
    desired grade of carpet

Create string with the filepath to the folder with the data files
string path = Path.Combine(Environment.CurrentDirectory, @"data\");
can also try System.IO.Directory.GetCurrentDirectory() if environment.currentdirec doesnt work.

Create array with all the csv file names
string[] csvFiles = Directory.GetFiles(path, "*.csv");

Create an integer that stores the number of files in the data folder
int numFiles = csvFiles.Length;

Store each csv file name into a list of arrays where each name is split by periods
list<string[]> csvList = new List<string[]>();
int i = 0;
foreach (string s in csvFiles)
{
    csvList.Add(s.Split('.'));
}

string[] csvRecents;

for(int i = 0; i < csvList.Count(); i++)
{
    int[] temps;
    string tempComp = csvList[i][0];
    for(int j = i; j < csvList.Count(); j++)
    {
        if(tempComp == csvList[j][0])
        {
            temps[0] = csvList[j][1];
            temps[1] = csvList[j][2];
            temps[2] = csvList[j][3];
            i++;
        }
    }
}

Use the desired grade of carpet or higher first to find all values that match it

Find the cheapest possible carpet from the list

If a higher grade exists and is cheaper, use that but only if it comes out cheaper to stick to that grade

Multiply length and width of each carpet starting with cheapest one until you find one that satisfies 
the square footage divided by number of rooms

Once the cheapest carpet that satisfies the square footage of room is found, mark it off and find the next
cheapest until all rooms have been accounted for.

total labor = find sum of number of carpets times half labor cost and number of rooms times half labor cost

total carpet = find sum of prices of the carpets selected

total cost = sum of labor and carpet divided by .6 to include the 40% margin.