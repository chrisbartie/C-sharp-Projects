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

namespace recipePOE_Part2

   
{
    //delegate is being used for adding ingredients to the recipe (each ingredients cals and food group)
    public delegate void Ingredient_del(string name, int cals, string foodGroup);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window

    {
        private Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();
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
            enterRecipe.Visibility= Visibility.Collapsed;
            homePanel.Visibility = Visibility.Collapsed;


        }

        private void btnToScale_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnToResetRec_Click(object sender, RoutedEventArgs e)
        {

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

        private int numSteps;
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbNumSteps.Text, out numSteps) || numSteps <= 0)
            {
                MessageBox.Show("You may only enter a number");
                return;
            }
            if (!int.TryParse(tbNumIngreds.Text, out int numIngreds) || numIngreds <= 0)
            {
                MessageBox.Show("You may only enter a number");
                return;
            }

            Dictionary<string, (int Cals, string foodGroup)> ingreds = new Dictionary<string, (int Cals, string foodGroup)>();

            //adding ingredient name, ingredient calories and ingredient food group to recipe
            Ingredient_del addIngred = (name, cals, foodGroup) =>
            {
                if (!ingreds.ContainsKey(name))
                {
                    //the ingredient will be added to dictionary if its not there already
                    ingreds[name] = (cals, foodGroup);
                }
                else
                {
                    //if ingredient is already in dictionary, this message will be displayed
                    MessageBox.Show($"You have already entered {name}");
                }
            };
           

            for (int i = 0; i < numIngreds; i++)
            {
                //Code Attribution
                //This code was taken from Code Project
                //https://www.codeproject.com/Questions/5357607/Using-a-visual-basic-input-box-in-Csharp
                //glennPattonWork3
                //https://www.codeproject.com/script/Membership/View.aspx?mid=5833582

                //this is an input box that will pop up according to the amount of ingredients the user has stated there are in a recipe
                string ingredient = Interaction.InputBox($"Enter Ingredient {i + 1}: ", "Enter Ingredient", "", -1, -1);
                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    MessageBox.Show("Enter an Ingredient");
                    return;
                }
                if (!int.TryParse(Interaction.InputBox($"Enter Calories for {ingredient}: ", "Enter Calories", "", -1, -1), out int calories))
                {
                    MessageBox.Show($"Please enter the calories for {ingredient}");
                    return;
                }



                string FoodGroup = Interaction.InputBox($"Enter food group for {ingredient}: ", "Food Group", "", -1, -1);
                if (string.IsNullOrWhiteSpace(FoodGroup))
                {
                    MessageBox.Show("Please be sure to enter a food group");
                    return;
                }
                //delegate being used for the adding of ingredient 
                addIngred(ingredient, calories, FoodGroup); 
                
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numSteps; i++)
            {

            
                //Code Attribution
                //This code was taken from Code Project
                //https://www.codeproject.com/Questions/5357607/Using-a-visual-basic-input-box-in-Csharp
                //glennPattonWork3
                //https://www.codeproject.com/script/Membership/View.aspx?mid=5833582

                //this is an input box that will pop up according to the amount of steps the user has stated there are in a recipe
                string step = Interaction.InputBox($"Enter Step {i+1}: ", "Enter Step", "", -1, -1);
                if (string.IsNullOrWhiteSpace(step))
                {
                    MessageBox.Show("Enter a Step");
                    return;
                }
                sb.AppendLine(step);
            }

            

            //getting all input from user from text boxes
            string recipeName = recName.Text;
            string steps = sb.ToString();
            

            if (recipes.ContainsKey(recipeName))
            {
                //if recipe name exists in system, error message will be displayed
                MessageBox.Show("This recipe name has been entered already.");
                return;
            }

            Recipe newRecipe = new Recipe(recipeName, steps, ingreds);
            recipes.Add(recipeName, newRecipe);

            //displaying details of the recipe that has just been added
            StringBuilder recDetails = new StringBuilder();
            recDetails.AppendLine($"Recipe Name: {newRecipe.recipeName}, has been added to The Recipe Storage\n\n");
            recDetails.AppendLine($"Ingredients: ");
            foreach (var ingredient in newRecipe.ingreds) 
            {
                recDetails.AppendLine($"{ingredient.Key} - {ingredient.Value.Cals}kcal ({ingredient.Value.foodGroup})");
            }
            recDetails.AppendLine($"\nSteps:\n{newRecipe.steps}");

            MessageBox.Show(recDetails.ToString());

            ClearInputs();

            
        }

        private void ClearInputs()
        {
            //clearing all textboxes for next input
            recName.Text = "";
            tbNumIngreds.Text = "";
            tbNumSteps.Text = "";
            
           
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //this will find the recipe name entered by the user
            string recSearch = recipeName.Text;

            //searching the dictionary, looking for the recipe name that the user entered
            if(recipes.ContainsKey(recSearch))
            {
                Recipe recipe = recipes[recSearch];

                //when recipe name is found, all of the details that were entered with it, will be displayed
                StringBuilder recDetails = new StringBuilder();
                recDetails.AppendLine($"Recipe Name: {recipe.recipeName}\n\n");
                recDetails.AppendLine("Ingredients:");
                foreach(KeyValuePair<string, (int Cals, string foodGroup)> ingredient in recipe.ingreds)
                {
                    recDetails.AppendLine($"{ingredient.Key} - {ingredient.Value.Cals}kcal ({ingredient.Value.foodGroup})");
                }
                recDetails.AppendLine($"\nSteps:\n {recipe.steps}");

                MessageBox.Show(recDetails.ToString());
            }
            else
            {
                MessageBox.Show("Recipe could not be found, search again");
            }
            //calling method to clear text in search tb
            clearSearch();
        }

        //once action is complete, the text in search tb will be cleared
        private void clearSearch()
        {
            recipeName.Text = "";
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //this will take the user back to the home panel
            homePanel.Visibility = Visibility.Visible;
            enterRecipe.Visibility = Visibility.Collapsed;
            searchRecipe.Visibility = Visibility.Collapsed;
        }

        private void btnHome1_Click(object sender, RoutedEventArgs e)
        {
            //this will take the user back to the home panel
            homePanel.Visibility = Visibility.Visible;
            enterRecipe.Visibility = Visibility.Collapsed;
            searchRecipe.Visibility = Visibility.Collapsed;
        }

        private void btnDisplayRecipes_Click(object sender, RoutedEventArgs e)
        {
            if(recipes.Count == 0)
            {
                MessageBox.Show("Please enter a recipe, none were found");
                return;
            }

            //Code Attribution
            //This code was taken from StackOverflow
            //https://stackoverflow.com/questions/289/how-do-you-sort-a-dictionary-by-value
            //Dan
            //https://stackoverflow.com/users/4601149/dan
            StringBuilder recs = new StringBuilder();
            //sorting the dictionary in alphabetical order
            foreach (var recPair in recipes.OrderBy(recPair => recPair.Key))
            {
                string recipeName = recPair.Key;
                Recipe recipe = recPair.Value;

                int totalCals = 0;
                recs.AppendLine($"Recipe Name: {recipe.recipeName}\n" +
                              $"Ingredients:");

                foreach(var ingredient in recipe.ingreds)
                {
                    string ingredName = ingredient.Key;
                    int cals = ingredient.Value.Cals;
                    string foodGroup = ingredient.Value.foodGroup;

                    recs.AppendLine($"{ingredName} - {cals}kcal ({foodGroup})");

                    totalCals += cals;
                }
                recs.AppendLine($"Steps:\n{recipe.steps}\n");

                if (totalCals > 300)
                {
                    recs.AppendLine("----------------------------------\n" +
                        $"Total Calories: {totalCals}kcal (EXCEEDS 300kcal!)\n" +
                        "----------------------------------\n");
                }
                else
                {
                    recs.AppendLine("----------------------------------\n" +
                        $"Total Calories: {totalCals}kcal\n" +
                        "----------------------------------\n");
                }


            }
            MessageBox.Show(recs.ToString(), "All Recipes stored in the system");
        }

    }
}

//Reference List
//https://stackoverflow.com/q/289 [Accessed 14 May 2024].
//sean. 2010. ‘Answer to “How do you sort a dictionary by value?”’ [Online]. Available at: https://stackoverflow.com/a/4157151 [Accessed 14 May 2024].
//How to change visibility of item in multiple instances of same user control - microsoft q&a [s.a.]. [Online]. Available at: https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta [Accessed 13 May 2024].
//[Solved] using a visual basic input box in c# - codeproject [s.a.]. [Online]. Available at: https://www.codeproject.com/Questions/5357607/Using-a-visual-basic-input-box-in-Csharp [Accessed 14 May 2024].
//https://stackoverflow.com/q/289 [Accessed 14 May 2024].
//sean. 2010. ‘Answer to “How do you sort a dictionary by value?”’ [Online]. Available at: https://stackoverflow.com/a/4157151 [Accessed 14 May 2024].
//How to change visibility of item in multiple instances of same user control - microsoft q&a [s.a.]. [Online]. Available at: https://learn.microsoft.com/en-us/answers/questions/41154/how-to-change-visibility-of-item-in-multiple-insta [Accessed 13 May 2024].
//[Solved] using a visual basic input box in c# - codeproject [s.a.]. [Online]. Available at: https://www.codeproject.com/Questions/5357607/Using-a-visual-basic-input-box-in-Csharp [Accessed 14 May 2024].
