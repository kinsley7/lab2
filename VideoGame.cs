using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

/**       
 *--------------------------------------------------------------------
 * 	   File name: VideoGame
 * 	Project name: CrowdisLab2
 *--------------------------------------------------------------------
 * Author’s name and email:	 kinsley crowdis crowdis@etsu.edu			
 *          Course-Section: CSCI 2910-800
 *           Creation Date:	09/08/2023
 * -------------------------------------------------------------------
 */

namespace CrowdisLab2
{
    internal class VideoGame : IComparable<VideoGame>
    {
        string Name { get; set; }
        string Platform { get; set; }
        int Year { get; set; }
        string Genre { get; set; }
        string Publisher { get; set; }
        string NASales { get; set; }
        string EUSales { get; set; }
        string JPSales { get; set; }
        string OtherSales { get; set; }
        string GlobalSales { get; set; }


        public VideoGame(string name, string platform, int year, string genre, string publisher, string naSales, string euSales, string jpSales, string otherSales, string globalSales)
        {
            Name = name;
            Platform = platform;
            Year = year;
            Genre = genre;
            Publisher = publisher;
            NASales = naSales;
            EUSales = euSales;
            JPSales = jpSales;
            OtherSales = otherSales;
            GlobalSales = globalSales;
        }

        public override string ToString()
        {

            return "Name: " + Name + "\n" + "Platform: " + Platform + "\n" + "Year: " + Year + "\n" + "Genre: " + Genre + "\n"
                + "Publisher: " + Publisher + "\n" + "Global Sales: " + GlobalSales + "\n ----------------- \n ";
        }

        //sorts by name by default for lab 1 methods
        public int CompareTo(VideoGame other)
        {
            return Name.CompareTo(other.Name);
        }

        /* - other methods from lab 1 -
        //shows games and stats for the publisher that is being searched for
        public static void PublisherData(List<VideoGame> videogame, string userInput)
        {
            //total video games # (used later to calculate %)
            var gameNumber = videogame.Count;


            //search for what the user enters.. get that.. put it in list
            List<VideoGame> publisherGames = videogame.Where(x => x.Publisher.ToLower().Equals(userInput.ToLower())).ToList<VideoGame>();

            // this is the # of total video games by the publisher that is being searched for
            float numPublisher = publisherGames.Count;

            // the amount of games the publisher has out of all of the games in the list
            var percent = numPublisher / gameNumber * 100;

            //writes out all of the publisher's games
            publisherGames.ForEach(Console.WriteLine);

            Console.WriteLine($"There are {numPublisher} {userInput} games out of {gameNumber} total games. {Math.Round(percent, 2)}% of games are published by {userInput}.");

        }

        //same thing as above but for genre
        public static void GenreData(List<VideoGame> videogame, string userInput)
        {
            //total video games # (used later to calculate %)
            var gameNumber = videogame.Count;


            //search for what the user enters.. get that.. put it in list
            List<VideoGame> genreGames = videogame.Where(x => x.Genre.ToLower().Equals(userInput.ToLower())).ToList<VideoGame>();

            float numGenre = genreGames.Count;
            var percent = numGenre / gameNumber * 100;

            genreGames.ForEach(Console.WriteLine);



            Console.WriteLine($"There are {numGenre} {userInput} games out of {gameNumber} total games. {Math.Round(percent, 2)}% of games are {userInput}.");


        }
        */

        //STACK
        //view top global sellers and top 3 sellers for each region.
        public static void TopSellers(List<VideoGame> videogame)
        {

            IList<VideoGame> games = new List<VideoGame>(videogame); //have to create new ilist so it can be sorted by sales rather than name

            var highestSelling = games.OrderBy(x => x.GlobalSales); //sorts by global sales 

            Stack<VideoGame> videoGames = new Stack<VideoGame>(highestSelling.ToList()); //turns the sorted var we created above into a list then into a stack 

            Console.WriteLine("------- Global BestSellers -------\n");
            Console.WriteLine();

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(videoGames.Peek());
                if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                    videoGames.Pop();
            }


            Console.Write("Would you like to view more? ");
            string userInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
        begin:
            if (userInput == "y")
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(videoGames.Peek());
                    if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                        videoGames.Pop();
                }
                Console.Write("Would you like to view more? ");
                userInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                goto begin;

            }
            else if (userInput == "n")
                Console.WriteLine();
            else
            {
                Console.WriteLine("Please enter 'yes' or 'no'");
                userInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                goto begin;
            }

            //display top 3 for country of choice (NA, EU, JP, OTHER)
            Console.Write("What region would like to see? (NA, EU, JP, or Other) If you would like to skip type 'skip': ");
            string userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
        beginRegion:
            switch (userRegionInput)
            {
                case "n":
                    Console.WriteLine("\n-------Highest Selling Games in NA-------\n");
                    var highestSellingNA = games.OrderBy(x => x.NASales); //sorts by NAsales 
                    videoGames = new Stack<VideoGame>(highestSellingNA.ToList());

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(videoGames.Peek());
                        if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                            videoGames.Pop();
                    }

                    Console.Write("Would you like to see another? (NA, EU, JP, Other, or Skip): ");
                    userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                    goto beginRegion;

                    break;
                case "e":
                    Console.WriteLine("\n-------Highest Selling Games in EU-------\n");
                    var highestSellingEU = games.OrderBy(x => x.EUSales); //sorts by EUsales 
                    videoGames = new Stack<VideoGame>(highestSellingEU.ToList());

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(videoGames.Peek());
                        if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                            videoGames.Pop();
                    }

                    Console.Write("Would you like to see another? (NA, EU, JP, Other, or Skip): ");
                    userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                    goto beginRegion;

                    break;
                case "j":
                    Console.WriteLine("\n-------Highest Selling Games in JP-------\n");
                    var highestSellingJP = games.OrderBy(x => x.JPSales); //sorts by JPsales 
                    videoGames = new Stack<VideoGame>(highestSellingJP.ToList());

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(videoGames.Peek());
                        if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                            videoGames.Pop();
                    }

                    Console.Write("Would you like to see another? (NA, EU, JP, Other, or Skip): ");
                    userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                    goto beginRegion;

                    break;
                case "o":
                    Console.WriteLine("\n-------Highest Selling Games in Other Regions-------\n");
                    var highestSellingOther = games.OrderBy(x => x.JPSales); //sorts by Other sales 
                    videoGames = new Stack<VideoGame>(highestSellingOther.ToList());

                    for (int i = 0; i < 3; i++)
                    {
                        Console.WriteLine(videoGames.Peek());
                        if (videoGames.Any(o => o == videoGames.First()))  //from stack overflow: https://stackoverflow.com/questions/5307172/check-if-all-items-are-the-same-in-a-list  skips the first item but compares the rest to make sure they are not the same bc there are duplicates
                            videoGames.Pop();
                    }

                    Console.Write("Would you like to see another? (NA, EU, JP, Other, or Skip): ");
                    userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                    goto beginRegion;

                    break;
                case "s":
                    Console.WriteLine("Thanks for viewing!");
                    break;
                default:
                    Console.WriteLine("Please enter 'NA', 'EU', 'JP', 'Other' or 'Skip'.");
                    userRegionInput = (Console.ReadLine().Trim().ToLower()).Substring(0, 1);
                    goto beginRegion;
                    break;
            }

        }

        //QUEUE 
        // have user enter a year and display the games published that year (if no year entered a random year is selected).
        public static void YearReleased(List<VideoGame> videogame)
        {
            Queue<VideoGame> queue = new Queue<VideoGame>(videogame);

            Console.Write("Enter a year between 1980 - 2021 to display games published that year. Leave blank to display games from a random year: ");
            string input = Console.ReadLine();
            int year;
            beginYear:
            if (int.TryParse(input, out year)) //takes the input and outputs it as the int year
            {
                Console.WriteLine($"----Games published in {year}----\n");
                var games = videogame.Where(game => game.Year == year);
                
                foreach (var game in games)
                    Console.WriteLine(game.Name);

            }
            else if (input == "" || input == " ")
            {
                Console.WriteLine("Displaying games from a random year...");
                Random random = new Random();
                int randomYear = random.Next(1980, 2021);

                Console.WriteLine($"----Games published in {randomYear}----\n");

                var games = videogame.Where(game => game.Year == randomYear);

                foreach (var game in games)
                    Console.WriteLine(game.Name);

            }
            else
            {
                Console.Write("Invalid Input. Leave blank or enter a valid year (1980 - 2021): ");
                input = Console.ReadLine();
                goto beginYear;
            }
        }

    //DICTIONARY publisher : most popular genre.
    //shows publisher and what genre the majority of their games are (and lists the number of games that are that genre <-- havent done)
    //whenever publisher = a publisher it needs to count the different amount of genres and total them. then the genre with the most is outputted. repeat this again for the next publisher in the list
    public static void PublisherGenre(List<VideoGame> videogame)
    {
        //from chatgpt(i was very stuck🤓) - this grabs the publisher attribute and groups it by genre then it goes into a dictionary.
        var genreCountsByPublisher = from v in videogame
                                     group v by v.Publisher into publisherGroup
                                     select new
                                     {
                                         Publisher = publisherGroup.Key,
                                         genreCounts = publisherGroup.GroupBy(v => v.Genre).ToDictionary(g => g.Key, p => p.Count()), // counts all of the genres for each publisher(used for seeing the amount of games the publisher published for that genre).
                                         mostCommonGenre = publisherGroup.GroupBy(v => v.Genre).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault() //finds the genre with the most amount of games or the first genre if there is multiple that are the same.
                                     };

        //sorts it by publisher name (need to find how to take the genreCounts and add it to this?)
        var sortedGenreCountsByPublisher = new SortedDictionary<string, string>();
        foreach (var publisher in genreCountsByPublisher.OrderBy(p => p.Publisher))
        {
            sortedGenreCountsByPublisher.Add(publisher.Publisher, publisher.mostCommonGenre);
        }


        //lets us see the most common genre overall
        var genreCountsOverall = videogame.GroupBy(v => v.Genre).ToDictionary(x => x.Key, x => x.Count()); //genre is our key, the count of the genres is our value  (x => x.Key, x => x.Count())
        var mostCommonGenreOverall = genreCountsOverall.OrderByDescending(g => g.Value).First().Key; //orders ^genreCountsOverall by most to least, and grabbing the first key (which would be the key with the highest value)


        Console.WriteLine("-------Each Publisher's Most Common Genre-------\n");

        //display each publisher and their most common genre
        foreach (var publisher in sortedGenreCountsByPublisher)
        {
            Console.WriteLine($"{publisher.Key}'s most common genre: {publisher.Value}\n");

        }

        Console.WriteLine($"The most common genre overall is {mostCommonGenreOverall.ToLower()}!\n");
    }


}
}


