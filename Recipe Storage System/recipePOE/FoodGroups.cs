using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recipePOE_Part2
{
    public class FoodGroups : ObservableCollection<string>
    {
        public FoodGroups()
        {
            Add("Starchy Foods");
            Add("Vegetables and fruits");
            Add("Dry beans, peas, lentils and soya");
            Add("Chicken, fish, meat and eggs");
            Add("Milk and dairy products");
            Add("Fats and oil");
            Add("Water");
        }
    }
}
