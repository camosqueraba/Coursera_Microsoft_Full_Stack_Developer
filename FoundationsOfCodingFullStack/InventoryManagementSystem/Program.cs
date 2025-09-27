using System;
class Program
{
    static int productIdCounter = 1;
    static List<Product> products = new List<Product>();

    static void Main()
    {
        int selectedOption = 0;


        do
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1 - Add new product");
            Console.WriteLine("2 - Update product stock");
            Console.WriteLine("3 - View all products");
            Console.WriteLine("4 - Remove product");
            Console.WriteLine("5 - Exit");
            selectedOption = Convert.ToInt16(Console.ReadLine());

            switch (selectedOption)
            {
                case 1:
                    AddNewProduct();
                    break;
                case 2:
                    UpdateProductStock();
                    break;
                case 3:
                    ViewAllProducts();
                    break;
                case 4:
                    RemoveProduct();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

        } while (selectedOption != 5);
    }


    private static void AddNewProduct()
    {
        try
        {
            Console.WriteLine("Adding new product...");

            Console.WriteLine("Entry product name:");
            string name = Console.ReadLine();

            Console.WriteLine("Entry product price:");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price format.");
                return;
            }

            Console.WriteLine("Entry initial stock:");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Invalid stock format.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product name cannot be empty.");
            }
            else if (price <= 0)
            {
                Console.WriteLine("Product price must be greater than zero.");
            }
            else if (stock < 0)
            {
                Console.WriteLine("Initial stock cannot be negative.");
            }
            else if (products.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Product with this name already exists.");
            }
            else
            {
                products.Add(new Product(productIdCounter++, name, price, stock));
                Console.WriteLine($"Product added: ID={productIdCounter}, Name={name}, Precio={price}, Stock={stock}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

    }

    private static void UpdateProductStock()
    {
        try
        {
            Console.WriteLine("Updating product stock...");
            Console.WriteLine("Entry product ID:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid product ID format.");
                return;
            }

            Console.WriteLine("Entry new stock quantity:");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Invalid stock quantity format.");
                return;
            }

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null && stock >= 0)
            {
                product.Stock = stock;
                Console.WriteLine($"Product ID={id} stock updated to {stock}");
            }
            else
            {
                Console.WriteLine("Product not found or invalid stock.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

    }

    private static void ViewAllProducts()
    {
        Console.WriteLine("Viewing all products...");
        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
        }
    }

    private static void RemoveProduct()
    {
        try
        {
            Console.WriteLine("Removing product...");
            Console.WriteLine("Entry product ID:");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid product ID format.");
                return;
            }

            var product = products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine($"Product ID={id} removed.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }

}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }

    public Product(int id, string name, double price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
}