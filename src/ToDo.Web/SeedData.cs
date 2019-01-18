using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Core.Entities;
using ToDo.Infrastructure.Data;

namespace ToDo.Web
{
    public static class SeedData
    {
        public static int PopulateTestData(AppDbContext context)
        {
            var toDos = context.ToDoItems.ToList();
            foreach (var item in toDos)
            {
                context.Remove(item);
            }
            context.SaveChanges();

            Random rand = new Random(Guid.NewGuid().GetHashCode());
            const int numberOfItems = 100;
            for (int n = 0; n < numberOfItems; n++)
            {
                string userName = UserNames.GetRandomName();

                var item = new ToDoItem
                {
                    Description = TextGenerator.GetText(10, 25),
                    DueDate = DateTime.Now.AddSeconds(rand.Next(60 * 60 * 4, 60 * 60 * 24 * 7)),
                    Hours = rand.Next(1, 8),
                    Owner = userName,
                    Title = TextGenerator.GetText(3, 10),
                    Avatar = $"https://api.adorable.io/avatars/285/{userName}.png"
                };

                context.Add(item);
            }
            context.SaveChanges();

            return numberOfItems;
        }

        private static class TextGenerator
        {
            private static string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            private static Random rand = new Random(Guid.NewGuid().GetHashCode());

            public static string GetText(int minimumNumberOfWords, int maximumNumberOfWords)
            {
                int numberOfWords = rand.Next(minimumNumberOfWords, maximumNumberOfWords);

                IEnumerable<string> words = loremIpsum.Split();
                if (numberOfWords < words.Count())
                {
                    words = words.Take(numberOfWords);
                }

                return string.Join(" ", words);
            }
        }

        private static class UserNames
        {
            public static string GetRandomName()
            {
                string name = names[rand.Next(0, names.Length - 1)].Trim();

                name = name.Replace(",", " ");
                name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
                return name;
            }

            private static Random rand = new Random(Guid.NewGuid().GetHashCode());
            private static string[] names = new string[]{
"madame,domenica,fleury",
"mrs,vilma,valli              ",
"ms,isabella,roy              ",
"mr,blake,tremblay            ",
"mr,andy,köhne                ",
"ms,sibylla,schall            ",
"miss,hafsa,stokke            ",
"ms,amy,wright                ",
"mr,michael,wade              ",
"mr,cecil,sullivan            ",
"mrs,dalila,araújo            ",
"ms,rachel,gilbert            ",
"mr,jesus,mora                ",
"mr,aleksi,autio              ",
"ms,caitlin,jackson           ",
"miss,stefania,baur           ",
"mr,mille,petersen            ",
"mr,viljar,husevåg            ",
"mr,antoine,fortin            ",
"miss,olivia,savela           ",
"mr,mason,moore               ",
"mr,ryder,singh               ",
"mr,jostein,støle             ",
"mr,oscar,amiri               ",
"ms,marina,sæle               ",
"miss,ella,clarke             ",
"ms,benta,da conceição        ",
"mr,alexandre,bernard         ",
"miss,تینا,رضاییان           ",
"miss,katie,hall              ",
"mr,leonard,rice              ",
"mr,rodrigo,cano              ",
"monsieur,enzo,lecomte        ",
"miss,marie,richardson        ",
"mr,joaquin,navarro           ",
"madame,stefania,dufour       ",
"miss,victoria,harris         ",
"mr,thomas,chu                ",
"miss,thea,nürnberger         ",
"mr,giray,doğan               ",
"mr,kurt,doyle                ",
"ms,zoe,taylor                ",
"mr,peetu,maunu               ",
"mrs,megan,reyes              ",
"mr,mohamad,rydningen         ",
"mrs,venla,lammi              ",
"mr,vernon,miller             ",
"mr,niilo,sakala              ",
"ms,rose,gill                 ",
"ms,madison,bates             ",
"ms,delores,mckinney          ",
"mr,tyler,armstrong           ",
"ms,lenita,castro             ",
"madame,martina,renaud        ",
"mr,levi,walker               ",
"mr,deniz,abacı               ",
"miss,dolores,flores          ",
"mr,önal,kahveci              ",
"ms,cecilie,thomsen           ",
"mr,connor,evans              ",
"mrs,sofie,poulsen            ",
"mr,nicholas,elliott          ",
"ms,carla,caldeira            ",
"miss,avery,andersen          ",
"mr,mason,li                  ",
"mrs,janet,reyes              ",
"mrs,beitske,gül              ",
"miss,lila,muller             ",
"mr,flynn,walker              ",
"mr,clifford,smith            ",
"miss,priscilla,jimenez       ",
"mr,raul,caldwell             ",
"mrs,ella,douglas             ",
"mr,gökhan,kulaksızoğlu       ",
"mr,christian,reed            ",
"ms,gisélida,sales            ",
"mr,hermann-josef,willms      ",
"mr,fernando,silveira         ",
"mrs,keira,hall               ",
"ms,isabella,mitchelle        ",
"miss,kelly,carlson           ",
"mademoiselle,alena,leroux    ",
"mademoiselle,paula,morel     ",
"mrs,veronica,navarro         ",
"mrs,عسل,حسینی               ",
"mr,امیر,کوتی                ",
"mrs,آوین,نكو نظر            ",
"ms,erika,gjøen               ",
"ms,gabrielle,ringvold        ",
"miss,esma,ekşioğlu           ",
"mrs,marta,mendes             ",
"miss,ragna,van de kolk       ",
"mr,ernesto,leon              ",
"mr,raphaël,dufour            ",
"mr,leon,kim                  ",
"mr,charly,gaillard           ",
"miss,emy,rolland             ",
"mrs,sheila,holt              ",
"miss,tâmara,moraes           ",
"mrs,sara,bradley             "};
        }
    }
}
