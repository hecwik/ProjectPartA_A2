using System;

namespace ProjectPartA_A2
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
                            RemoveArticle(articles);
                            break;
                        case 3:
                            PrintReceiptPrice(articles);
                            break;

                        case 4:
                            PrintReceiptName(articles);
                            break;

                        case 5:
                            Console.WriteLine("Thank you for using the shopping list!");
                            menuRunning = false;
                            break;
                        default:
                            Console.WriteLine("Please choose between options 1-5.");
                            break;
                    }
                }
            }
            Environment.Exit(0);

        }

        private static bool ReadArticles()
        {
            try
            {
                Console.Write("\nHow many articles would you like to buy (between 1-10)?: ");

                // tryparse console readline input
                if (int.TryParse(Console.ReadLine(), out int sNrOfArticles))
                {
                    // check if input is between 0 and 11
                    if (sNrOfArticles > 0 && sNrOfArticles <= 10)
                    {
                        
                        // set nrArticles to the input amount of articles selected by the user
                        nrArticles = sNrOfArticles;

                        string articleString = $"You want to purchase {nrArticles}";

                        // checks how many articles, if there are more than one, go to else and print "articles" plural instead of article singular
                        if (nrArticles == 1)
                        {
                            
                            Console.WriteLine($"{articleString} article");
                            AddArticle();
                            return true;
                        }
                        else
                        {
                            Console.WriteLine($"{articleString} articles.");
                            AddArticle();
                            return true;
                        }
                    }
                    else if (sNrOfArticles > 10)
                    {
                        Console.WriteLine($"\n{sNrOfArticles} are too many articles. Maximum number of articles is {_maxNrArticles}.\n");
                        Console.WriteLine("\nPress anywhere to continue...\n");
                        Console.ReadKey();
                        return false;
                    }
                    else if (sNrOfArticles <= 0)
                    {
                        Console.WriteLine($"\nYou have to add an article.\n");
                        Console.ReadKey();
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
            // create new uninitialized article object
            Article myArticle = new Article();
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
                                        // initialize article object
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
                                        Console.WriteLine("Press anyhwere to continue...");
                                        Console.ReadKey();
                                        i--;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"\nName of article is too long! Maximum {_maxArticleNameLength} characters allowed.");
                                    Console.WriteLine("Press anyhwere to continue...");
                                    Console.ReadKey();
                                    i--;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Article name or price was not entered, please try again.");
                                Console.WriteLine("Press anyhwere to continue...");
                                Console.ReadKey();
                                i--;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error! There must be a space between name and price.");
                            Console.WriteLine("Press anyhwere to continue...");
                            Console.ReadKey();
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
        }

        private static bool RemoveArticle(Article[] articles)
        {
            Console.WriteLine("Which article in the list would you like to remove? Enter its name: ");

            // program waits for user input
            string sInput = Console.ReadLine();

            if (sInput != string.Empty && sInput.Length <= _maxArticleNameLength)
            {
                // iterate through all elements in articles.Length to find the matching element
                for (int i = 0; i < nrArticles; i++)
                {
                    // check if name and price are NOT null
                    if (articles[i].Name != null && articles[i].Price != 0)
                    {
                        // check if user input and name of article are equal
                        if (sInput.ToUpper() == articles[i].Name.ToUpper())
                        {
                            Console.WriteLine($"{articles[i].Name} has been removed!");
                            Console.WriteLine("Press anywhere to continue...");
                            Console.ReadKey();

                            // replace the current index value with an uninitialized Article
                            articles[i] = new Article();
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Either the name of the article is too long (above {_maxArticleNameLength} characters) or you have used invalid characters.");
                Console.WriteLine("Press anywhere to continue...");
                Console.ReadKey();
            }

            return false;
        }

        // method that prints elements in the article[] articles array sorted by Price
        private static void PrintReceiptPrice(Article[] articles)
        {
            try
            {
                int countArticles = 0;
                decimal totalPrice = 0;

                // counter for the number of articles bought
                foreach (var item in articles)
                {
                    if (item.Name != null && item.Price != 0)
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

                    // invoke sort price method
                    SortPrice();

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
        // method that prints the receipt with articles sorted by Name
        private static void PrintReceiptName(Article[] articles)
        {
            try
            {
                int countArticles = 0;
                decimal totalPrice = 0;

                // counter for the number of articles bought
                for (int i = 0; i < articles.Length; i++)
                {
                    if (articles[i].Name != null && articles[i].Price != 0)
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

                    // invoke sort method
                    SortName();

                    // add the prices of the items in the array together
                    foreach (var item in articles)
                    {
                        totalPrice += item.Price;
                    }

                    Console.WriteLine($"\nTotal purchase: {totalPrice:C}");

                    // calculate included VAT in price
                    Console.WriteLine($"\nIncludes VAT: {totalPrice * _vat:C}");
                    Console.WriteLine("\nREMEMBER TO ALWAYS KEEP YOUR RECEIPT :)\n");
                    Console.WriteLine("Press anywhere to continue...");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("The shopping list is empty!");
                    Console.WriteLine("Press anywhere to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }

        public static void SortName()
        {
            Article tmp;
            int column;
            int row;

            int countReceipt = 0;

            // sort articles
            //loop through length-1 because we compare to the number after the current number, which makes it
            // unnecessary to go to the last position because there is nothing to compare to after it
            
            for (column = 0; column < articles.Length - 1; column++)
            {
                // add i to not go to the last element, since it has already been shifted 
                for (row = 0; row < articles.Length - (1 + column); row++)
                {
                    // check if name of the current and the next element are NOT null
                    // to avoid NullReferenceException
                    if (articles[row].Name != null && articles[row + 1].Name != null)
                    {
                        // since the elements to compare are strings,
                        // here i am using the compareTo-method to see if the instance (the first letter, a char),
                        // precedes the first letter in the string to compare with
                        if (articles[row].Name[0].CompareTo(articles[row + 1].Name[0]) < 0)
                        {
                            // if the instance precedes the first letter of the element on pos + 1, swap the elements
                            tmp = articles[row + 1];
                            articles[row + 1] = articles[row];
                            articles[row] = tmp;
                        }
                    }
                }
                // check so the counter and console writeline are NOT printing any default elements
                if (articles[row].Name != default && articles[row].Price != default)
                {
                    countReceipt++;
                    Console.WriteLine($"{countReceipt,-2} {articles[row].Name,-10} {articles[row].Price:C}");
                }
            }
            
        }

        public static void SortPrice()
        {
            Article tmp;
            int column;
            int row;

            int countReceipt = 0;

            // sort articles
            //loop through length-1 because we compare to the number after the current number, which makes it
            // unnecessary to go to the last position because there is nothing to compare to after it

            for (column = 0; column < articles.Length - 1; column++)
            {
                // add i to not go to the last element, since it has already been shifted 
                for (row = 0; row < articles.Length - (1 + column); row++)
                {
                    // check if name of the current and the next element are NOT null
                    // to avoid NullReferenceException
                    if (articles[row].Name != null && articles[row + 1].Name != null)
                    {
                        // since the elements to compare are strings,
                        // here i am using the compareTo-method to see if the instance (the first letter, a char),
                        // precedes the first letter in the string to compare with
                        if (articles[column].Price < articles[row].Price)
                        {
                            // if the instance precedes the first letter of the element on pos + 1, swap the elements
                            tmp = articles[row + 1];
                            articles[row + 1] = articles[row];
                            articles[row] = tmp;
                        }
                    }
                }
                // check so the counter and console writeline are NOT printing any default elements
                if (articles[row].Name != default && articles[row].Price != default)
                {
                    countReceipt++;
                    Console.WriteLine($"{countReceipt,-2} {articles[row].Name,-10} {articles[row].Price:C}");
                }
            }
        }
    }
}
