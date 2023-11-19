using PartyLib;
using PartyLib.Bases;

Console.Write("Creator URL: ");
string creatorURL = Console.ReadLine();
Creator creator = new Creator(creatorURL);
Console.WriteLine(creator.Name);
Console.WriteLine(creator.PartyDomain);
Console.WriteLine(creator.Service);

Console.Write("Posts Test: ");
int posts = Int32.Parse(Console.ReadLine());
ScraperFunctions funcs = new ScraperFunctions(creator, posts);
Console.WriteLine("Pages: " + funcs.DoPageMath().Item1);
Console.WriteLine("Leftover Posts: " + funcs.DoPageMath().Item2);
Console.WriteLine("Is Single Page?: " + funcs.DoPageMath().Item3);