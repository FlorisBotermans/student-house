using System.Linq;

namespace StudentHouse.Models
{
    public interface IMealRepository
    {
        IQueryable<Meal> Meals { get; }
        bool AddMeal(Meal meal);
        bool EditMeal(Meal meal);
        bool DeleteMeal(int mealId);
        bool AddGuestToMeal(int mealId, Student student);
        bool DeleteGuestFromMeal(int mealId, Student student);
    }
}
