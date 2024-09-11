using System.Text;
using System.Windows;
using Microsoft.VisualBasic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;

namespace recipePOE
{
    public partial class MainWindow : Window

    {
        //Code Attribution
        //This dictionary was made with the help of a Udemy course
        //https://www.udemy.com/course/complete-csharp-masterclass/learn/lecture/38370058#overview
        //Denis Panjuta
        //https://www.udemy.com/user/denispanjuta/

        //This dictionary stores the recipe by name
        private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
        private Recipe currentRecipe;
        private int ingredientCounter;
        private int stepsCounter;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void btnToEnterRec_Click(object sender, RoutedEventArgs e)
        {
            //this button will take the user to the enter recipe panel
            homePanel.Visibility = Visibility.Collapsed;
            enterRecipe.Visibility = Visibility.Visible;
        }



        private void btnToViewRec_Click(object sender, RoutedEventArgs e)
        {
            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            //taking the user back to the home page where they can click the button to view recipes
            searchRecipe.Visibility = Visibility.Visible;
            enterRecipe.Visibility = Visibility.Collapsed;
            homePanel.Visibility = Visibility.Collapsed;
        }



        private void btnToScale_Click(object sender, RoutedEventArgs e)
        {
            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            //this button takes the user to the scaling screen when they click the scale recipe button on the home screen
            homePanel.Visibility = Visibility.Collapsed;
            scaleRecipe.Visibility = Visibility.Visible;
        }



        private void btnToResetRec_Click(object sender, RoutedEventArgs e)
        {
            homePanel.Visibility= Visibility.Collapsed;
            resetRec.Visibility = Visibility.Visible;
        }



        private void btnToClearStorage_Click(object sender, RoutedEventArgs e)
        {
            //method for clearing the recipes stored in the dictionary
            MessageBox.Show("The Recipe Storage has been cleared");
            //Code Attribution
            //This code was taken from Stack Overflow
            //https://stackoverflow.com/questions/5311124/how-to-empty-a-list-in-c
            //Oyvind Brathen
            //https://stackoverflow.com/users/303476/%c3%98yvind-br%c3%a5then
            recipes.Clear();
        }

        //public delegate void TotalCalsMax(int totalCals);
        //public event TotalCalsMax TotalCaloriesMax;
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string name = recName.Text.Trim();

            //error message for if the user doesn't enter a recipe name
            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a valid recipe name.");
                return;
            }

            //if the user has not entered a number or has not entered a number higher than 0, error message will be displayed
            if (!int.TryParse(tbNumIngreds.Text.Trim(), out int numIngreds) || numIngreds <= 0)
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
                return;
            }

            //if the user has not entered a number or has not entered a number higher than 0, error message will be displayed
            if (!int.TryParse(tbNumSteps.Text.Trim(), out int numSteps) || numSteps <= 0)
            {
                MessageBox.Show("Please enter a valid number of steps.");
                return;
            }

            currentRecipe = new Recipe(name, numIngreds, numSteps);
            currentRecipe.maxCals += maxCals;
            //dictionary storing by recipes name
            recipes[name] = currentRecipe;

            //resetting the counters
            ingredientCounter = 0;
            stepsCounter = 0;

            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            enterRecipe.Visibility = Visibility.Collapsed;
            recipeDetails.Visibility = Visibility.Visible;
        }




        public void maxCals(int totalCals)
        {
            //Code Attribution
            //This method was made using Microsoft
            //https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/delegates/using-delegates
            //Bill Wagner
            //https://github.com/BillWagner
            MessageBox.Show($"Your recipe contains more than 300 calories. It contains {totalCals}kcal");
        }



        private void btnAddSteps_Click(object sender, RoutedEventArgs e)
        {
            //trim for no whitespace
            string step = tbSteps.Text.Trim();

            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0
            if (string.IsNullOrEmpty(step))
            {
                MessageBox.Show("Please enter a valid step.");
                return;
            }

            //Add the step to the current recipe
            currentRecipe.Steps.Add(step);
            stepsCounter++;

            //checking if all steps have been added to the recipe
            if (stepsCounter >= currentRecipe.NumSteps)
            {
                pnlSteps.Visibility = Visibility.Collapsed;
                homePanel.Visibility = Visibility.Visible;
                MessageBox.Show("Recipe successfully added!");

                ClearInputs();
            }
            else
            {
                tbSteps.Clear();
            }
        }



        private void btnAddIngred_Click(object sender, RoutedEventArgs e)
        {
            //getting the ingredient name
            //trim for no whitespace
            string ingredName = tbIngred.Text.Trim();

            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0
            if (string.IsNullOrEmpty(ingredName))
            {
                MessageBox.Show("Please enter a valid ingredient name.");
                return;
            }

            if (!double.TryParse(tbIngredQuantity.Text.Trim(), out double quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            //getting the unit of measurement
            //trim for no whitespace
            string unit = tbIngredUnit.Text.Trim();

            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0
            if (string.IsNullOrEmpty(unit))
            {
                MessageBox.Show("Please enter a valid unit.");
                return;
            }

            //getting the selected food group from the combobox
            if (!(cbFoodGroup.SelectedItem is ComboBoxItem selectedItem))
            {
                MessageBox.Show("Please select a food group.");
                return;
            }

            string foodGroup = selectedItem.Content.ToString();
            //get the calories for each ingredient
            if (!int.TryParse(tbCalories.Text.Trim(), out int cals) || cals < 0)
            {
                MessageBox.Show("Please be sure to enter the calories for the ingredient");
                return;
            }

            //getting ingredient details from ingredient class
            Ingredient ingredient = new Ingredient(ingredName, quantity, unit, foodGroup, cals);
            currentRecipe.Ingredients.Add(ingredient);

            //counter for the ingredients entered
            ingredientCounter++;

            //checking if the total calories for the recipe is more than 300
            currentRecipe.CalsMaxCheck();

            if (ingredientCounter >= currentRecipe.NumIngredients)
            {
                recipeDetails.Visibility = Visibility.Collapsed;
                pnlSteps.Visibility = Visibility.Visible;
                
                ClearInputs();
            }
            else
            {
                tbIngred.Clear();
                tbIngredQuantity.Clear();
                tbIngredUnit.Clear();
                tbCalories.Clear();
                cbFoodGroup.SelectedIndex = -1;
            }
        }



        private void ClearInputs()
        {
            //clearing all textboxes for next input, this method is called when the button is pressed
            recName.Text = "";
            tbNumIngreds.Text = "";
            tbNumSteps.Text = "";
            tbIngred.Text = "";
            tbIngredQuantity.Text = "";
            tbIngredUnit.Text = "";
            tbCalories.Text = "";
            cbFoodGroup.SelectedIndex = -1;
            tbSteps.Text = "";
        }



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //this will find the recipe name entered by the user
            string name = recipeName.Text;
            //searching the dictionary, looking for the recipe name that the user entered
            if (recipes.TryGetValue(name, out Recipe recipe))
            {
                //Code Attribution
                //This string builder was made using TutorialsTeacher
                //https://www.tutorialsteacher.com/csharp/csharp-stringbuilder
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Recipe Name: {recipe.Name}\n");
                sb.AppendLine("Ingredients:");

                foreach (var ingred in recipe.Ingredients)
                {
                    //this is how the recipe will display
                    sb.AppendLine($"- {ingred.Name}, {ingred.Quantity} {ingred.Unit} ({ingred.FoodGroup})\n");
                }
                sb.AppendLine("Steps:");
                //steps that are stored in the list, will be displayed one by one
                foreach (var step in recipe.Steps)
                {
                    sb.AppendLine($"- {step}");
                }
                
                MessageBox.Show(sb.ToString());
            }
            else
            {
                //error message for when a recipe name cannot be found
                MessageBox.Show("Recipe not found.");
            }

            //calling method to clear text in search tb
            recipeName.Clear();
        }



        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            //this will take the user back to the home panel
            homePanel.Visibility = Visibility.Visible;
            enterRecipe.Visibility = Visibility.Collapsed;
            searchRecipe.Visibility = Visibility.Collapsed;
        }



        private void btnHome1_Click(object sender, RoutedEventArgs e)
        {
            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            //this will take the user back to the home panel
            homePanel.Visibility = Visibility.Visible;
            enterRecipe.Visibility = Visibility.Collapsed;
            searchRecipe.Visibility = Visibility.Collapsed;
        }



        private void btnDisplayRecipes_Click(object sender, RoutedEventArgs e)
        //method to display all recipes stored in the dictionary
        {
            if (recipes.Count == 0)
            //error message for if there are no recipes stored
            {
                MessageBox.Show("Please enter a recipe, none were found");
                return;
            }

            //Code Attribution
            //This code was taken from StackOverflow
            //https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
            //Dan
            //https://stackoverflow.com/users/4601149/dan

            var sortedRecs = recipes.Values.OrderBy(r => r.Name);

            StringBuilder sb = new StringBuilder();
            //Code Attribution
            //This string builder was made using TutorialsTeacher
            //https://www.tutorialsteacher.com/csharp/csharp-stringbuilder
            foreach (var recipe in sortedRecs)
            {
                int totalCals = 0;
                sb.AppendLine($"Recipe Name: {recipe.Name}");
                sb.AppendLine("\nIngredients:");
                foreach (var ingred in recipe.Ingredients)
                {
                    sb.AppendLine($"- {ingred.Name}, {ingred.Quantity} {ingred.Unit} ({ingred.FoodGroup}) - {ingred.Cals}kcal");
                    totalCals += ingred.Cals;
                }
                sb.AppendLine("\n-----------------------------------\n" +
                    $"Total Calories: {totalCals}kcal\n" +
                    "-----------------------------------\n");
                sb.AppendLine("Steps:");
                foreach (var step in recipe.Steps)
                {
                    sb.AppendLine($"- {step}");
                }
                sb.AppendLine();
            }
            MessageBox.Show(sb.ToString());
        }



        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your steps for the recipe have been entered");

            ClearInputs();

            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            recipeDetails.Visibility = Visibility.Collapsed;
            pnlSteps.Visibility = Visibility.Collapsed;
            homePanel.Visibility = Visibility.Visible;
        }



        private void btnScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            //trim for no whitespace
            string name = recScale.Text.Trim();


            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0

            if (string.IsNullOrEmpty(name) || !recipes.ContainsKey(name))
            {
                MessageBox.Show($"{name} was not found, please enter another recipe name.");
                return;
            }

            if (!(cbRecScale.SelectedItem is ComboBoxItem selectedItem))
            {
                MessageBox.Show("Please select one of the 3 scaling options");
                return;
            }

            double scaleAmount = Convert.ToDouble(selectedItem.Content);

            Recipe recipeScaled = recipes[name];

            //Code Attribution
            //The scaling calculations were made with the help of a Udemy course
            //https://www.udemy.com/course/complete-csharp-masterclass/learn/lecture/38370058#overview
            //Denis Panjuta
            //https://www.udemy.com/user/denispanjuta/
            foreach (var ingredient in recipeScaled.Ingredients)
            {
                ingredient.Quantity *= scaleAmount;
                ingredient.Cals = Convert.ToInt32(ingredient.Cals * scaleAmount);
            }

            MessageBox.Show($"Your recipe: {name} has been scaled by {scaleAmount}");

            recScale.Clear();
            cbRecScale.SelectedIndex = -1;

            homePanel.Visibility = Visibility.Visible;
            scaleRecipe.Visibility = Visibility.Collapsed;
            
        }

        private void btnToHome_Click(object sender, RoutedEventArgs e)
        {
            //Code Attribution
            //This code was taken from Microsoft
            //https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta
            //Apptacular Appa
            //https://learn.microsoft.com/en-us/users/apptacularapps-8305/

            homePanel.Visibility = Visibility.Visible;
            scaleRecipe.Visibility = Visibility.Collapsed;
        }

        private void btnResetRec_Click(object sender, RoutedEventArgs e)
        {
            //trim for no whitespace
            string name = recNameReset.Text.Trim();

            //Code Attribution
            //this code was taken from microsoft
            //https://learn.microsoft.com/en-us/dotnet/api/system.string.isnullorempty?view=net-8.0

            if (string.IsNullOrEmpty(name) || !recipes.ContainsKey(name))
            {
                //if recipe could not be found by name, this error message will be displayed
                MessageBox.Show($"{name} is not a recipe in your storage");
                return;
            }
            Recipe resetRecipe = recipes[name];

            foreach (var ingredient in resetRecipe.Ingredients)
            {
                ingredient.ResetQuantity();
            }
            MessageBox.Show($"{name} quantities have been reset to their original value");

            recNameReset.Text = "";
        }
       

        private void btnResetAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(var recipe in recipes.Values)
            {
                foreach(var ingredient in recipe.Ingredients)
                {
                    ingredient.ResetQuantity();
                }
            }
            MessageBox.Show("All recipes have been reset to their original quantities");
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            resetRec.Visibility = Visibility.Collapsed;
            homePanel.Visibility = Visibility.Visible;
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

