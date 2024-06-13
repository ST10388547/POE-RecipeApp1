using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> _recipes;
        private List<string> _currentIngredients;
        private List<string> _currentSteps;

        public MainWindow()
        {
            InitializeComponent();
            _recipes = new List<Recipe>();
            _currentIngredients = new List<string>();
            _currentSteps = new List<string>();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StepTextBox.Text))
            {
                _currentIngredients.Add(StepTextBox.Text);
                IngredientsListBox.Items.Add(StepTextBox.Text);
                StepTextBox.Clear();
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(IngredientTextBox.Text))
            {
                _currentSteps.Add(IngredientTextBox.Text);
                StepsListBox.Items.Add(IngredientTextBox.Text);
                IngredientTextBox.Clear();
            }
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RecipeNameTextBox.Text) && _currentIngredients.Any() && _currentSteps.Any())
            {
                var newRecipe = new Recipe
                {
                    Name = RecipeNameTextBox.Text,
                    Ingredients = new List<string>(_currentIngredients),
                    Steps = new List<string>(_currentSteps)
                };

                _recipes.Add(newRecipe);
                RefreshRecipeList();
                ClearRecipeInputs();
            }
            else
            {
                MessageBox.Show("Please provide a recipe name, ingredients, and steps.");
            }
        }

        private void FilterRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            string filterText = FilterTextBox.Text.ToLower();
            var filteredRecipes = _recipes.Where(r => r.Name.ToLower().Contains(filterText)).OrderBy(r => r.Name).ToList();
            RecipesListBox.ItemsSource = filteredRecipes;
        }

        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesListBox.SelectedItem is Recipe selectedRecipe)
            {
                DisplayRecipeDetails(selectedRecipe);
            }
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            RecipeDetailsTextBlock.Text = $"Name: {recipe.Name}\n\nIngredients:\n";
            RecipeDetailsTextBlock.Text += string.Join("\n", recipe.Ingredients);
            RecipeDetailsTextBlock.Text += "\n\nSteps:\n";
            RecipeDetailsTextBlock.Text += string.Join("\n", recipe.Steps);
        }

        private void RefreshRecipeList()
        {
            RecipesListBox.ItemsSource = _recipes.OrderBy(r => r.Name).ToList();
        }

        private void ClearRecipeInputs()
        {
            RecipeNameTextBox.Clear();
            IngredientTextBox.Clear();
            StepTextBox.Clear();
            IngredientsListBox.Items.Clear();
            StepsListBox.Items.Clear();
            _currentIngredients.Clear();
            _currentSteps.Clear();
        }
    }
}
