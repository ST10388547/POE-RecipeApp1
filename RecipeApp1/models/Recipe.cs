namespace RecipeApp
{
    public class Recipe
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public Recipe()
        {
            Name = string.Empty;
            Ingredients = new List<string>();
            Steps = new List<string>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
