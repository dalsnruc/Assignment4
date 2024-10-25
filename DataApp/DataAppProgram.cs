using DataLayer;

var dataService = new DataService();


PrintCategories(dataService);

var category = new Category
{
    Id = 9,
    Name = "123",
    Description = "numbers"

};

dataService.UpdateCategory(category);

PrintCategories(dataService);



static void PrintProducts(IDataService dataService)
{
    foreach (var e in dataService.GetProducts())
    {
        Console.WriteLine($"{e.Id}, {e.Name}, {e.Category.Name}");
    }

}
static void PrintCategories(IDataService dataService)
{
    foreach (var e in dataService.GetCategories())
        {
            Console.WriteLine($"{e.Id}, {e.Name}, {e.Description}");
        }

}