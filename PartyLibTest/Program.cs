using PartyLib.Bases;
using PartyLib.Helpers;

Console.Write("Creator URL: ");
string creatorURL = Console.ReadLine();
Creator creator = new Creator(creatorURL);
Console.WriteLine(creator.Name);
Console.WriteLine(creator.PartyDomain);
Console.WriteLine(creator.Service);

Console.Write("Posts Test: ");
int posts = Int32.Parse(Console.ReadLine());
ScraperHelper funcs = new ScraperHelper(creator, posts);
Console.WriteLine("Pages: " + MathHelper.DoPageMath(creator, posts).Pages);
Console.WriteLine("Leftover Posts: " + MathHelper.DoPageMath(creator, posts).LeftoverPosts);
Console.WriteLine("Is Single Page?: " + MathHelper.DoPageMath(creator, posts).IsSinglePage);