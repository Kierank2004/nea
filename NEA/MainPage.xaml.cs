using System;
using System.IO;

namespace NEA;
using Microsoft.Maui.Controls;

public partial class MainPage : ContentPage
{
    List<Virus> viruses = new List<Virus>()
    {
        new Virus("Ebola", 2, 0.7, 21, 0.5, 0.99),
        new Virus("Influenza", 2, 0.5, 7, 0.95, 0.01),
        new Virus("Zika", 7, 0.5, 14, 0.9, 0.01),
        new Virus("HIV", 10, 0.001, 5475, 0.001, 0.8),//
        new Virus("Measles", 10, 0.9, 14, 0.98, 0.02),
        new Virus("SARS-CoV-2", 5, 1.5, 14, 0.95, 0.02),
        new Virus("Rotavirus", 2, 0.9, 7, 0.99, 0.005),//
        new Virus("Norovirus", 1, 0.8, 3, 0.9, 0.01),
        new Virus("Herpes simplex", 3, 0.2, 21, 0.99, 0.001),
        new Virus("Hepatitis A", 2, 0.5, 30, 0.95, 0.01),
        new Virus("Dengue", 5, 0.6, 14, 0.9, 0.01),
        new Virus("Chikungunya", 5, 0.4, 21, 0.9, 0.005),
        new Virus("West Nile", 3, 0.3, 14, 0.7, 0.1),
        new Virus("Yellow fever", 3, 0.5, 14, 0.9, 0.05),//
        new Virus("Rabies", 30, 0.99, 60, 0.001, 0.99),
        new Virus("Custom", 0, 0, 0, 0, 0)
    };
    private void OnStartClicked(object sender, EventArgs e)
    {
        Dictionary<string, Virus> virusDictionary = viruses.ToDictionary(virus => virus.Virusname, virus => virus);
        int Generation = 1;
        string pickedVirus = VirusPicked.SelectedItem.ToString();  int dayssimulating = Convert.ToInt16(SimulationLength.Text); Virus selectedVirus = virusDictionary[pickedVirus]; 
        if (pickedVirus == "Custom")
        {
            string CustomVName = Viurs_Name_Custom.Text;
            selectedVirus.Virusname = CustomVName; selectedVirus.Incubationtimes = Convert.ToInt16(Virus_Incubation_Period_Custom.SelectedItem);
            selectedVirus.Infectionrate = Convert.ToDouble(Virus_Infection_Period_Custom.SelectedItem) / 100; selectedVirus.RecoveryTime = Convert.ToInt16(Virus_Recovery_Rate_Custom.SelectedItem); 
            selectedVirus.RecoveryChance = Convert.ToDouble(Virus_Recovery_Chance_Custom.SelectedItem) / 100; selectedVirus.Fatalityrate = Convert.ToDouble(Virus_Fatality_Rate_Custom.SelectedItem) / 100;
        }
        Viurs_Name.Text = $"Name: {selectedVirus.Virusname}"; Virus_Incubation_Period.Text = $"Incubation Period: {selectedVirus.Incubationtimes}"; Virus_Infection_Period.Text = $"Infection Rate: {selectedVirus.Infectionrate}"; 
        Virus_Recovery_Chance.Text = $"Recovery Chance: {selectedVirus.RecoveryChance}"; Virus_Recovery_Rate.Text = $"Recovery Time:: {selectedVirus.RecoveryTime}";  Virus_Fatality_Rate.Text = $"Fatality Rate:: {selectedVirus.Fatalityrate}";
        Days_Simulated.Text = $"Day Simulated: {Generation}"; 
        DrawInitialGeneration(Generation, selectedVirus, dayssimulating);
    }

    private void OnImportClicked(object sender, EventArgs e)
    {
        DisplayAlert("Error", "Please ammend the file with the Errors Locations above.", "OK");
        Dictionary<string, Virus> virusDictionary = viruses.ToDictionary(virus => virus.Virusname, virus => virus); int Generation = 1;
        Convert.ToInt16(SimulationLength.Text);
        // Viurs_Name.Text = $"Name: {selectedVirus.Virusname}"; Virus_Incubation_Period.Text = $"Incubation Period: {selectedVirus.Incubationtimes}"; Virus_Infection_Period.Text = $"Infection Rate: {selectedVirus.Infectionrate}";
        // Virus_Recovery_Chance.Text = $"Recovery Chance: {selectedVirus.RecoveryChance}"; Virus_Recovery_Rate.Text = $"Recovery Time:: {selectedVirus.RecoveryTime}";  Virus_Fatality_Rate.Text = $"Fatality Rate:: {selectedVirus.Fatalityrate}";
        // Days_Simulated.Text = $"Day Simulated: {Generation}";
        bool MakeFile = false;
        int generation = 1;
        int DaysSimulated;
        string Temp;
        double Percent;
        bool NotValid = false;
        Random random = new Random();

        // Read a file to import a simulation and the other surrounding information.
        string FileName = Console.ReadLine();
        FileName = FileName + ".txt";
        do
        {
            Console.WriteLine("How many days would you like simulated?");
            Temp = Console.ReadLine();
        } while (!int.TryParse(Temp, out DaysSimulated) || DaysSimulated <= 0 || DaysSimulated >= 40173);
        int[] Susceptible = new int[DaysSimulated];
        int[] Exposed = new int[DaysSimulated];
        int[] Infected = new int[DaysSimulated];
        int[] Recovered = new int[DaysSimulated];


        string[] lines = File.ReadAllLines(FileName);
        int Height = lines.Length;
        int Width = lines[0].Length;
        Console.WriteLine(lines[0]);
        int population = Width * Height;
        Percent = (double)population / Height;
        Cell[,] currentGrid = new Cell[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            string line = lines[i];
            for (int j = 0; j < Width; j++)
            {
                char c = line[j];
                if (c == 's')
                {
                    Console.Write(c);
                    double randomDouble = random.NextDouble();
                    double randomHyg = random.NextDouble();
                    double randomVax = random.NextDouble();
                    double RandMask = random.NextDouble();
                    currentGrid[i, j] = new Susceptible("Cell"+Width+Height, 0, randomHyg, randomVax, RandMask, 0);
                }
                else if (c == 'e')
                {
                    Console.Write(c);
                    double randomDouble = random.NextDouble();
                    double randomHyg = random.NextDouble();
                    double randomVax = random.NextDouble();
                    double RandMask = random.NextDouble();
                    currentGrid[i, j] = new Exposed("Cell"+Width+Height, 0, randomHyg, randomVax, RandMask, 0);
                }
                else if (c == 'i')
                {
                    Console.Write(c);
                    currentGrid[i, j] = new Infected("Cell"+Width+Height, 0, 0, 0, 0, 1);
                }
                else if (c == 'r')
                {
                    Console.Write(c);
                    currentGrid[i, j] = new Removed("Cell"+Width+Height, 0, 0, 0, 0, 0);
                }
                else
                {
                    Console.WriteLine("Error in the File on Line {0}, {1}", i, j);
                    NotValid = true;
                }
            }
            Console.WriteLine("");
        }
        if (!NotValid)
        {
            Virus ActiveVirus;
        }
        else
        {
        }
    }
    
    private void VirusListIntaliser()
    {
        string[] Vname = new string[viruses.Count];
        for (int i = 0; i < viruses.Count; i++)
        {
            Vname[i] = viruses[i].Virusname;
        }
        VirusPicked.ItemsSource = Vname;
    }
    static int GatherSusNeighbours(Cell[,] currentGrid, int width, int height)
    {
        int SusNeighbours = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (currentGrid[i,j].IndividualState == "S")
                {
                    SusNeighbours++;
                }
            }
        }
        return SusNeighbours;
    }
    static int GatherExposedNeighbours(Cell[,] currentGrid, int Width, int Height)
    {
        int ExposedNeighbours = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (currentGrid[i, j].IndividualState == "E")
                {
                    ExposedNeighbours++;
                }
            }
        }
        return ExposedNeighbours;
    }
    static int GatherInfectNeighbours(Cell[,] currentGrid, int Width, int Height)
    {
        int InfNeighbours = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (currentGrid[i, j].IndividualState == "I")
                {
                    InfNeighbours++;
                }
            }
        }
        return InfNeighbours;
    }

    static int GatherRecfectNeighbours(Cell[,] currentGrid, int Width, int Height)
    {
        int RecNeighbours = 0;
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (currentGrid[i, j].IndividualState == "R")
                {
                    RecNeighbours++;
                }
            }
        }
        return RecNeighbours;
    }
    private void PickerSetup()
    {
        string[] boolValues = new string[] { "True", "False" };
        int[] Xcoord = Enumerable.Range(1,44).ToArray();
        int[] Vsettings = Enumerable.Range(1,100).ToArray();
        int[] Ycoord = Enumerable.Range(1,26).ToArray();
        int[] Infection_Percentage = Enumerable.Range(1, 100).ToArray();
        FileBoolPicker.ItemsSource = boolValues;
        Height.ItemsSource = Xcoord;
        Width.ItemsSource = Ycoord;
        Initial_infection_rate.ItemsSource = Infection_Percentage;
        Virus_Incubation_Period_Custom.ItemsSource = Vsettings;
        Virus_Infection_Period_Custom.ItemsSource = Vsettings;
        Virus_Recovery_Chance_Custom.ItemsSource = Vsettings;
        Virus_Recovery_Rate_Custom.ItemsSource = Vsettings;
        Virus_Fatality_Rate_Custom.ItemsSource = Vsettings;
    }

    private void OnEndClick(object sender, EventArgs e)
    {
        DisplayAlert("Shutting Down", "Thank you using my program", "Close");
        Environment.Exit(10000);
    }
    public void SimDayEnd()
    {
        DisplayAlert("Shutting Down", "Thank you using my program", "Close");
        Environment.Exit(10000);
    }

    private void DrawInitialGeneration(int Generation, Virus selectedVirus, int dayssimulating)
    {
        // Initialising counting the cells
        int[] Susceptible = new int[dayssimulating];
        int[] Exposed = new int[dayssimulating];
        int[] Infected = new int[dayssimulating];
        int[] Recovered = new int[dayssimulating];
        string[] gridText = new String[dayssimulating]; 
        // Taking the width and height from the menu.       
        string temp = Width.SelectedItem.ToString(); int width = Convert.ToInt16(temp);
        temp = Height.SelectedItem.ToString(); int height = Convert.ToInt16(temp);
        // Getting Variables set up to create first grid
        Random random = new Random();
        temp = Initial_infection_rate.SelectedItem.ToString(); double Percent = Convert.ToDouble(temp) / 100;
        Cell[,] currentGrid = new Cell[width, height]; // creating initial gird 
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                double randomDouble = random.NextDouble();
                double randomHyg = random.NextDouble();
                double randomVax = random.NextDouble();
                double RandMask = random.NextDouble();
                if (randomDouble < Percent)  //  infecting the matrix with non brokies.
                {
                    currentGrid[row, col] = new Exposed("Cell"+Width+Height,0, randomHyg, randomVax, RandMask, 0);
                    gridText[Generation] += $"{currentGrid[row, col].IndividualState} ";
                }
                else
                {
                    currentGrid[row, col] = new Susceptible("Cell"+Width+Height,0, randomHyg, randomVax, RandMask, 0);
                    gridText[Generation] += $"{currentGrid[row, col].IndividualState} ";
                }
            }

            gridText[Generation] += "\n";
        }
        GridLabel.Text = gridText[Generation];
        Susceptible[Generation] = GatherSusNeighbours(currentGrid, width, height);
        Exposed[Generation] = GatherExposedNeighbours(currentGrid, width, height);
        Infected[Generation] = GatherInfectNeighbours(currentGrid, width, height);
        Recovered[Generation] = GatherRecfectNeighbours(currentGrid, width, height);
        while (Generation != dayssimulating)
        {
            Cell[,] Newgrid = new Cell[width, height]; int i, j;
            for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++) // loops through matrix and assigns the grid to newgrid
                {
                    Newgrid[i, j] = currentGrid[i, j]; // assiging grid to new grid because the rules are dependent on the state of the grid at the start of the loop
                }
            }
           for (i = 0; i < width; i++)
            {
                for (j = 0; j < height; j++) // loops through the matrix and applies the rules!  
                { 
                    RuleImplamentation(Newgrid, currentGrid, i, j, width, height, selectedVirus);
                }
            }
            for (int l = 0; l < height; l++)
            {
                for (int k = 0; k < width; k++)
                {
                    gridText[Generation] += $"{Newgrid[l, k].IndividualState} ";
                }

                gridText[Generation] += "\n";
            }
            // if (FileBoolPicker.SelectedItem.ToString() == "True")
            // {
            //     Writecsv(FileNameLogger.Text, Susceptible, Exposed, Infected, Recovered);
            // }
            Susceptible[Generation] = GatherSusNeighbours(currentGrid, width, height);
            SusceptibleCount.Text = $"{Susceptible[Generation]}";
            Exposed[Generation] = GatherExposedNeighbours(currentGrid, width, height);
            Exposed_count.Text = $"{Exposed[Generation]}";
            Infected[Generation] = GatherInfectNeighbours(currentGrid, width, height);
            Infected_count.Text = $"{Infected[Generation]}";
            Recovered[Generation] = GatherRecfectNeighbours(currentGrid, width, height);
            Recovered_count.Text = $"{Recovered[Generation]}";
       GridLabel.Text = gridText[Generation];   
       Days_Simulated.Text = $"Day Simulated: {Generation}";
        }
        SimDayEnd();
    } 
    static void RuleImplamentation(Cell[,] Newgrid, Cell[,] currentGrid, int i, int j, int width, int height, Virus selectedVirus) // I want to implement the rules recurvsively but not sure how to :(
    {
        int x, y; double TransmissionRate; int Widdy = width; int hitty = height; // I use this to hitty the griddy >:)
        Random random = new Random();
        // Uses Moore Neighbourhood to get infected neighbours
        double randomDouble;
        int manhatdist = 1; // Allows people to change how many cells are checked.
        int ineighbour = 0;
        for (x = -manhatdist; x < manhatdist; x++)
        {
            for (y = -manhatdist; y < manhatdist; y++)
            {
                int col = (i + x + Widdy) % Widdy;
                int row = (j + y + hitty) % hitty;
                if (currentGrid[col, row].Infectious = true)
                {
                    ineighbour++;
                }            
            }
        }

        if (currentGrid[i, j].IndividualState == "I") // if infected it removes it self from the count. Mike knows how to remove heroin from peoples houses.
        {
            ineighbour--;
        }
        if (currentGrid[i, j].IndividualState == "E")
        {
            currentGrid[i, j].Age++;
        }
        // Actually implements simulation rules
        TransmissionRate = (ineighbour * selectedVirus.Infectionrate * (1 - currentGrid[x, j].MaskProtectionRate) * (1 - currentGrid[x, j].HygeineRating) * (1 - currentGrid[x, j].VaxProtectionRate)) / (manhatdist * 8);
        randomDouble = random.NextDouble();
        if (Newgrid[i, j].IndividualState == "S" && (randomDouble < TransmissionRate /*Infection Rate */ && ineighbour > 1) /*I need to get the neighbours and put int into here > 1 */)  //  checks if cell is susceptible and then also checks if the random number is small than the infection rate.
        {
            currentGrid[i, j].IndividualState = "E";
            currentGrid[i, j].Infectious = true;
        }
        else if (Newgrid[i, j].IndividualState == "E" && (Newgrid[i, j].Age > selectedVirus.Incubationtimes))
        {
            currentGrid[i, j].IndividualState = "I";
        }
        else if (Newgrid[i, j].IndividualState == "I" && (randomDouble > selectedVirus.RecoveryChance || Newgrid[i, j].InfectedAge > selectedVirus.RecoveryTime))
        {
            currentGrid[i, j].IndividualState = "R";
            currentGrid[i, j].Infectious = false;
        }
        Newgrid[i, j] = currentGrid[i, j];
    }
    void Writecsv(string FileCreateNamer, int[] Susceptible, int[] Exposed, int[] Infected, int[] Recovered)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, $"{FileCreateNamer}.txt");
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write the column headers
            writer.WriteLine("Generation, Suscetible Cells, Exposed Cells, Infected Cells, Recovered.");
            // Write the data rows
            for (int i = 0; i < Susceptible.Length; i++)
            {
                writer.WriteLine($"{i}, {Susceptible[i]}, {Exposed[i]}, {Infected[i]}, {Recovered[i]}");
            }   
        }
        
    }
    public MainPage()
    {
        InitializeComponent();
        PickerSetup();
        VirusListIntaliser();
    }
}