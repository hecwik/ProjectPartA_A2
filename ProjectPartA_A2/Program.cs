using System;

namespace ProjectPartA_A1
{
    class Program
    {
        public struct Article
        {
            public string Name;
            public decimal Price;
        }

        const int _maxNrArticles = 10;
        const int _maxArticleNameLength = 20;
        const decimal _vat = 0.25M;

        static Article[] articles = new Article[_maxNrArticles];
        static int nrArticles;

        static void Main(string[] args)
        {
            ShoppingMenu();
        }

        public static void ShoppingMenu()
        {
            bool menuRunning = true;
            while (menuRunning)
            {
                Console.Clear();
                Console.WriteLine("\n\t== Welcome to Project Part A! ==");
                Console.WriteLine("\n1. Add an article");
                Console.WriteLine("\n2. Remove an article");
                Console.WriteLine("\n3. Print receipt sorted by price");
                Console.WriteLine("\n4. Print receipt sorted by name");
                Console.WriteLine("\n5. Quit");

                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int result))
                {
                    switch (result)
                    {
                        case 1:
                            ReadArticles();
                            break;
                        case 2:

                            break;
                        case 3:
                            PrintReceiptPrice(articles);
                            break;

                        case 4:
                            PrintReceiptName(articles);
                            break;

                        case 5:
                            Console.WriteLine("Thank you for using the shopping list!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Please choose between options 1-5.");
                            break;
                    }
                }
            }

        }

        private static bool ReadArticles()
        {
            try
            {
                Console.Write("\nHow many articles would you like to buy (between 1-10)?: ");

                // tryparse console readline input
                if (int.TryParse(Console.ReadLine(), out int amountOfArticles))
                {
                    // check if input is between 0 and 11
                    if (amountOfArticles > 0 && amountOfArticles <= 10)
                    {
                        // set nrArticles to the input amount of articles selected by the user
                        nrArticles = amountOfArticles;

                        // checks how many articles, if there are more than one, go to else and print "articles" plural instead of article singular
                        if (nrArticles == 1)
                        {
                            Console.WriteLine($"\nYou want to purchase {nrArticles} article.\n");
                            AddArticle();
                            return true;
                        }

                        else
                        {
                            Console.WriteLine($"\nYou want to purchase {nrArticles} articles.\n");
                            AddArticle();
                            return true;
                        }
                    }
                    else if (amountOfArticles > 10)
                    {
                        Console.WriteLine($"\n{amountOfArticles} are too many articles. Maximum number of articles is 10.\n");
                        return false;
                    }
                    else if (amountOfArticles <= 0)
                    {
                        Console.WriteLine($"\nYou have to add an article.\n");
                        return false;
                    }
                    return false;
                }

                // if tryparse fails, display message
                else
                {
                    Console.WriteLine("\nWrong input - please enter a number between 1-10.");
                    return false;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
            return false;
        }
        // method for adding articles
        public static void AddArticle()
        {
            bool adding = false;
            try
            {
                while (adding == false)
                {
                    // uninitialized string array used for splitting string
                    string[] articleArray;

                    // for-loop that iterates through the number of articles asked for in ReadArticle() method
                    for (int i = 1; i <= nrArticles; i++)
                    {
                        Console.WriteLine($"Please enter an article and price for article #{i} in the format 'Apple 2,25'");
                        // wait for user input with console.readline
                        string articleInput = Console.ReadLine();

                        // check if input string contains a white space
                        if (articleInput.Contains(" "))
                        {
                            // add articleInput to the string[] array using split method with white space as their separator
                            articleArray = articleInput.Split(" ");

                            // check if the name is not empty or white space, and if price input is not empty
                            if (articleArray[0] != " " && articleArray[0] != string.Empty && articleArray[1] != string.Empty)
                            {
                                // check if input string is shorter than 20 characters and longer than 0
                                if (articleInput.Length > 1 || articleInput.Length <= _maxArticleNameLength)
                                {
                                    // tryparse the decimal value of the second string[] element
                                    if (decimal.TryParse(articleArray[1], out decimal decimalResult))
                                    {
                                        // create new instance of Article
                                        Article myArticle = new Article();

                                        myArticle.Name = articleArray[0];
                                        articles[i].Name = myArticle.Name;

                                        // assign tryparse out-variable to Price of Article
                                        myArticle.Price = decimalResult;

                                        // add Price of Article to array element's Price
                                        articles[i].Price = myArticle.Price;

                                        Console.WriteLine($"\nYou have added {articles[i].Name} {articles[i].Price:C} to the shopping list.\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nError! The price has to be entered in the format '2,25'.\n");
                                        i--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"\nName of article is too long! Maximum {_maxArticleNameLength} characters allowed.");
                                    i--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Article name or price was not entered, please try again.");
                                i--;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error! There must be a space between name and price.");
                            i--;
                        }
                    }
                    // set adding to true and exit the while loop
                    adding = true;
                    Console.WriteLine("\nPress anywhere to go back to the menu...");
                    Console.ReadLine();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
            adding = false;
        }

        // method that prints elements in the article[] articles array
        private static void PrintReceiptPrice(Article[] articles)
        {
            try
            {
                int countArticles = 0;
                decimal totalPrice = 0;

                // counter for the number of articles bought
                foreach (var item in articles)
                {
                    if (item.Name != string.Empty && item.Price != 0)
                    {
                        countArticles++;
                    }
                }

                // checks if shopping list contains any articles
                if (countArticles > 0)
                {
                    Console.Clear();
                    Console.WriteLine("\n\t==YOUR RECEIPT==");
                    Console.WriteLine($"\nDate of purchase: {DateTime.Now}");
                    Console.WriteLine($"\nNumber of items purchased: {countArticles}");
                    Console.WriteLine($"\n{"#",-2} {"Name",-10} {"Price"}");

                    // add the prices of the items in the array together
                    foreach (var item in articles)
                    {
                        totalPrice += item.Price;
                    }

                    // counter for the number of objects in the receipt
                    int countReceipt = 0;

                    for (int i = 0; i < articles.Length - 1; i++)
                    {
                        for (int j = i + 1; j < articles.Length; j++)
                        {
                            if (articles[i].Price < articles[j].Price)
                            {
                                Article temp = articles[i];
                                articles[i] = articles[j];
                                articles[j] = temp;
                            }
                        }
                        // check if article price is not 0
                        if (articles[i].Price != 0)
                        {
                            countReceipt++;
                            Console.WriteLine($"{countReceipt,-2} {articles[i].Name,-10} {articles[i].Price:C}");
                        }

                    }

                    Console.WriteLine($"\nTotal purchase: {totalPrice:C}");

                    // calculate included VAT in price
                    Console.WriteLine($"\nIncludes VAT: {totalPrice * _vat:C}");
                    Console.WriteLine("\nREMEMBER TO ALWAYS KEEP YOUR RECEIPT :)");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("The shopping list is empty!");
                    Console.ReadLine();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }
        private static void PrintReceiptName(Article[] articles)
        {
            try
            {
                int countArticles = 0;
                decimal totalPrice = 0;

                // counter for the number of articles bought
                for (int i = 0; i < nrArticles; i++)
                {
                    if (articles[i].Name != string.Empty && articles[i].Price != 0)
                    {
                        countArticles++;
                    }
                }

                // checks if shopping list contains any articles
                if (countArticles > 0)
                {

                    Console.WriteLine("\n\t==YOUR RECEIPT==");
                    Console.WriteLine($"\nDate of purchase: {DateTime.Now}");
                    Console.WriteLine($"\nNumber of items purchased: {countArticles}");
                    Console.WriteLine($"\n{"#",-2} {"Name",-10} {"Price"}");

                    // add the prices of the items in the array together
                    foreach (var item in articles)
                    {
                        totalPrice += item.Price;
                    }

                    // counter for the number of objects in the receipt
                    int countReceipt = 0;



                    for (int i = 0; i < articles.Length - 1; i++)
                    {


                        for (int j = i + 1; j < articles.Length; j++)
                        {

                            // compare elements content, articles[] null. why?

                            if (articles[i].Name.CompareTo(articles[j].Name) < 0)
                            {
                                Article temp = articles[j];
                                articles[j] = articles[i];
                                articles[i] = temp;
                            }
                        }
                        // check if article name is not empty

                        if (articles[i].Name != string.Empty)
                        {
                            countReceipt++;
                            Console.WriteLine($"{countReceipt,-2} {articles[i].Name,-10} {articles[i].Price:C}");
                        }


                    }

                    Console.WriteLine($"\nTotal purchase: {totalPrice:C}");

                    // calculate included VAT in price
                    Console.WriteLine($"\nIncludes VAT: {totalPrice * _vat:C}");
                    Console.WriteLine("\nREMEMBER TO ALWAYS KEEP YOUR RECEIPT :)");

                }
                else
                {
                    Console.WriteLine("The shopping list is empty!");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }
    }
}
