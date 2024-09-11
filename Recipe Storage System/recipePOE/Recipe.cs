﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace recipePOE
{
    public class Recipe
    {

        //Code Attribution
        //This delegate was made using Microsoft
        //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates
        //Bill Wagner
        //https://github.com/BillWagner
         
        public delegate void maxCalsDelegate(int totalCals);

        public event maxCalsDelegate maxCals;
        //code attribution
        //these automatic properties were built with help from gunnarpeipman
        //https://gunnarpeipman.com/csharp-automatic-properties/
        //Gunnar Peipman
        //https://gunnarpeipman.com/bio/

        //and

        //https://www.udemy.com/course/complete-csharp-masterclass/learn/lecture/38370058#overview
        //Denis Panjuta
        //https://www.udemy.com/user/denispanjuta/

        //holding and storing the information on the intial add recipe screen
        public string Name { get; set; }
        public int NumIngredients { get; set; }
        public int NumSteps { get; set; }

        //Code Attribution
        //This list was made using StackOverflow
        //https://www.geeksforgeeks.org/c-sharp-list-class/
        //jason
        //https://stackoverflow.com/users/45914/jason
        //storing the ingredient information
        public List<Ingredient> Ingredients { get; set; }


        //Code Attribution
        //This list was made using StackOverflow
        //https://www.geeksforgeeks.org/c-sharp-list-class/
        //jason
        //https://stackoverflow.com/users/45914/jason
        //storing the steps entered by the user
        public List<string> Steps { get; set; }

        //constructor initializing a recipe object with all of the recipe information and taking in the ingredient list
        public Recipe(string name, int numIngredients, int numSteps)
        {
            Name = name;
            NumIngredients = numIngredients;
            NumSteps = numSteps;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void CalsMaxCheck()
        {
            //Code Attribution
            //This method was made using Microsoft
            //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates
            //Bill Wagner
            //https://github.com/BillWagner
            int totalCals = 0;
            foreach(var ingredient in Ingredients)
            {
                totalCals += ingredient.Cals;
            }
            if(totalCals > 300)
            {
                if(maxCals != null)
                {
                    maxCals(totalCals);
                }
            }
        }
    }
}


//Reference List
//C# | list class, 31 October 2018. [Online]. Available at: https://www.geeksforgeeks.org/c-sharp-list-class/ [Accessed 20 April 2024].
//BillWagner. 2024. Divide strings using string.split (c# guide) - c#, 8 March 2024. [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split [Accessed 26 April 2024].
//BillWagner. 2023.Using delegates - c#, 31 July 2023. [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates [Accessed 29 May 2024].
//Chand, M. [s.a.]. WPF combobox. [Online]. Available at: https://www.c-sharpcorner.com/uploadfile/mahesh/wpf-combobox/ [Accessed 23 May 2024].
//dotnet-bot. [s.a.]. StringBuilder.append method (system.text). [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-8.0 [Accessed 24 April 2024].
//dotnet - bot. [s.a.] b.String.isnullorempty(string) method(system). [Online].Available at: https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0 [Accessed 29 May 2024].
//jason. 2011. ‘Answer to “Instantiate A Property Of Type List<>”’. [Online]. Available at: https://stackoverflow.com/a/5344950 [Accessed 29 May 2024].
//Life, S. 2022. ‘What are the different food groups? A simple explanation.’, 2 March. [Online]. Available at: https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/ [Accessed 28 May 2024].
//Mikejo5000. 2023.Create, run, and customize c# unit tests - visual studio (windows), 3 November 2023. [Online]. Available at: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022 [Accessed 29 May 2024].
//ncarandini. 2023. Unit testing c# with mstest and .net - .net, 18 March 2023. [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest [Accessed 29 May 2024].
//Peipman, G. 2007. Automatic properties in c#, 16 November 2007. [Online]. Available at: https://gunnarpeipman.com/csharp-automatic-properties/ [Accessed 29 March 2024].
//C# stringbuilder [s.a.]. [Online]. Available at: https://www.tutorialsteacher.com/csharp/csharp-stringbuilder [Accessed 27 May 2024].
//Complete c# masterclass: learn wpf, databases, linq, unity & more! [s.a.]. [Online]. Available at: https://www.udemy.com/course/complete-csharp-masterclass/ [Accessed 23 April 2024].
//C# | list class, 31 October 2018. [Online]. Available at: https://www.geeksforgeeks.org/c-sharp-list-class/ [Accessed 29 April 2024].
//BillWagner. 2024. Divide strings using string.split (c# guide) - c#, 8 March 2024. [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/csharp/how-to/parse-strings-using-split [Accessed 23 April 2024].
//Chand, M. [s.a.]. WPF combobox. [Online]. Available at: https://www.c-sharpcorner.com/uploadfile/mahesh/wpf-combobox/ [Accessed 23 May 2024].
//dotnet-bot. [s.a.]. StringBuilder.append method (system.text). [Online]. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-8.0 [Accessed 19 April 2024].
//jason. 2011. ‘Answer to “Instantiate A Property Of Type List<>”’. [Online]. Available at: https://stackoverflow.com/a/5344950 [Accessed 29 May 2024].
//Life, S. 2022. ‘What are the different food groups? A simple explanation.’, 2 March. [Online]. Available at: https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/ [Accessed 28 May 2024].
//Peipman, G. 2007. Automatic properties in c#, 16 November 2007. [Online]. Available at: https://gunnarpeipman.com/csharp-automatic-properties/ [Accessed 20 April 2024].
//C# stringbuilder [s.a.]. [Online]. Available at: https://www.tutorialsteacher.com/csharp/csharp-stringbuilder [Accessed 27 May 2024].
//Complete c# masterclass: learn wpf, databases, linq, unity & more! [s.a.]. [Online]. Available at: https://www.udemy.com/course/complete-csharp-masterclass/ [Accessed 20 April 2024].
