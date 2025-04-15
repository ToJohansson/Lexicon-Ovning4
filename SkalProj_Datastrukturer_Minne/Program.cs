using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;

namespace SkalProj_Datastrukturer_Minne;

class Program
{
    /*
        Frågor:
    1. Hur fungerar stacken och heapen? Förklara gärna med exempel eller skiss på dess grundläggande funktion
     svar: 
       Stacken; är som en hög med smutsiga mattallrikar. du tar en tallrik och staplar den högst upp. du kan inte ta en annan tallrik än den senaste högst uppe, det som kallas LIFO, last in first out. stack är statiskt minne, den är snabb och effektiv och rensar/städar sig själv så fort den är klar och inte används mer.
      Heap; är en trädstruktur, kan ha flera child noder. data sparas längre här och frigörs med garbage collector. Ett exempel kan vara när man spelar data spel. din progress sparas så du kan komma tillbaka vid ett senare tillfälle och fortsätta där du avslutade senast. så lite långsammare än stack men mer flexibel, hantering av mycket data eller när den ska sparas länge.
    

    2. Vad är Value Types respektive Reference Types och vad skiljer dem åt?
    svar: 
        value types är mindre data, sådant som kan vara kortlivat och immutable, sparas på stacken. Reference types sparas då på heapen. Med keywoed "new" så allokeras plats på heapen, vill man nu spara denna nya instans så sparas inte objektet utan en referens till objektet där den är sparad på heapen. Så den är mutable, flera variabler kan ha samma referens sparad och på sätt komma åt samma instans på heapen. en annan stor skillnad är att stacken hanterar städning själv direkt när value types är använd medans referens typer på heap lever längre till garabage collector tar hand om dem.
    
    3. Följande metoder(se bild nedan) genererar olika svar. Den första returnerar 3, den andra returnerar 4, varför?

        Här används value types.
        När vi skriver y = x, så kopieras värdet av x in i y.
        Det betyder att x och y är helt oberoende av varandra – två olika platser i minnet.
        När vi sedan ändrar y = 4, påverkas inte x.
        Alltså kommer x fortfarande vara 3 när vi returnerar det.

        Här används en reference type, t.ex.ett objekt som MyInt.
        När vi skriver y = x, så kopierar vi en referens – alltså en "pekare" till samma objekt i heapen.

        Det betyder att både x och y pekar på samma instans av MyInt.
        När vi ändrar y.value = 4, så ändrar vi egentligen värdet i objektet som båda refererar till.
        Så även om vi returnerar x.value, så får vi 4, eftersom både x och y pekar på samma data.
    */

    /// <summary>
    /// The main method, vill handle the menues for the program
    /// </summary>
    /// <param name="args"></param>
    static void Main()
    {

        while (true)
        {
            Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                + "\n1. Examine a List"
                + "\n2. Examine a Queue"
                + "\n3. Examine a Stack"
                + "\n4. CheckParenthesis"
                + "\n0. Exit the application");
            char input = ' '; //Creates the character input to be used with the switch-case below.
            try
            {
                input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
            }
            catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
            {
                Console.Clear();
                Console.WriteLine("Please enter some input!");
            }
            switch (input)
            {
                case '1':
                    ExamineList();
                    break;
                case '2':
                    ExamineQueue();
                    break;
                case '3':
                    ExamineStack();
                    break;
                case '4':
                    CheckParanthesis();
                    break;
                /*
                 * Extend the menu to include the recursive 
                 * and iterative exercises.
                 */
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter some valid input (0, 1, 2, 3, 4)");
                    break;
            }
        }
    }

    /// <summary>
    /// Examines the datastructure List
    /// </summary>
    static void ExamineList()
    {
        /*
         * Loop this method untill the user inputs something to exit to main menue.
         * Create a switch statement with cases '+' and '-'
         * '+': Add the rest of the input to the list (The user could write +Adam and "Adam" would be added to the list)
         * '-': Remove the rest of the input from the list (The user could write -Adam and "Adam" would be removed from the list)
         * In both cases, look at the count and capacity of the list
         * As a default case, tell them to use only + or -
         * Below you can see some inspirational code to begin working.
        */
        bool isInnerLoop = true;
        bool isOuterLoop = true;
        List<string> theList = new List<string>();
        do
        {
            do
            {
                Console.WriteLine("input something that start with either + or - in a sentence.\n" +
                    "press 0 to go back.");
                string input = Console.ReadLine();
                char nav = input[0];
                string value = input.Substring(1);

                switch (nav)
                {
                    case '+':
                        theList.Add(value);
                        isInnerLoop = false;
                        break;
                    case '-':
                        theList.Remove(value);
                        isInnerLoop = false;
                        break;
                    case '0':
                        isInnerLoop = false;
                        isOuterLoop = false;
                        break;
                    default:
                        Console.WriteLine("You must use + or - at the beginning of the sentence.");
                        break;
                }
            } while (isInnerLoop);
            foreach (string item in theList)
            {
                /*
                    i min utskrift ser jag att listans kapacitet ökas när count är == capacity.
                    varje gång listans kapacitet ökas, så ökas den med dubbelt. 4 -> 8 -> 16 osv...
                    Eftersom en ny lista skapas om varje gång vi kommer upp i kapacitet, så skapas den nya listan 
                    med mer kapacitet än den förra för att inte behövas skapas på nytt varje gång vi lägger till ett element.
                    Detta är vid mindre datahantering, aningens ineffektivt, fast å andra sidan väldigt viktigt när vi hanterar 
                    väldigt stora listor. vad vi ser är att vår lista ökar och minskar dynamiskt. men bakom kulisserna skapas 
                    en ny lista som är dubbelt så stor än sin föregångare med alla dess element. 
                    När man tar bort element ur listan så är kapaciteten den samma. alltså en ny lista med mindre kapacitet skapas
                    aldrig, har den ökat från 16 till 32 kommer den inte backa tillbaka till 16. Detta gör att vi undviker 
                    onödiga omallokeringar och kopieringar fram och tillbaka.
                    Arrays är bra att använda när man vet hur stor den ska vara eftersom den är statisk. Den är generellt snabbare 
                    än List på att iterera genom sig och hämta individuella element via indexering. List är dynamisk, så vid
                    tillfällen man behöver ta bort eller lägga till nya element till listan eller använda färdiga metoder, 
                    är List ett bättre alternativ. 
                 */
                Console.WriteLine($"item: {item}, count: {theList.Count}, capacity: {theList.Capacity}");
            }

        } while (isOuterLoop);
    }

    /// <summary>
    /// Examines the datastructure Queue
    /// </summary>
    static void ExamineQueue()
    {
        /*
         * Loop this method untill the user inputs something to exit to main menue.
         * Create a switch with cases to enqueue items or dequeue items
         * Make sure to look at the queue after Enqueueing and Dequeueing to see how it behaves
        */
        Queue<string> nameQueue = new Queue<string>();
        bool isRunning = true;
        do
        {
            Console.WriteLine("1. Add customer to queue.\n" +
                              "2. Handle customer.\n" +
                              "0. Back to menu.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    nameQueue.Enqueue(name);
                    Console.WriteLine($"{name} ställer sig i kön.");
                    break;
                case "2":
                    try
                    {
                        Console.WriteLine($"{nameQueue.Peek()} blir expedierad och lämnar kön.");
                        nameQueue.Dequeue();
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Kön är tom.");
                        break;
                    }
                case "0":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Wrong input.");
                    break;
            }

        } while (isRunning);
    }

    /// <summary>
    /// Examines the datastructure Stack
    /// </summary>
    static void ExamineStack()
    {
        /*
         * Loop this method until the user inputs something to exit to main menue.
         * Create a switch with cases to push or pop items
         * Make sure to look at the stack after pushing and and poping to see how it behaves
        */
        Stack<string> nameQueue = new Stack<string>();
        bool isRunning = true;
        do
        {
            Console.WriteLine("***STACK***\n" +
                              "1. Add customer to queue.\n" +
                              "2. Handle customer.\n" +
                              "3. Reverse text.\n" +
                              "0. Back to menu.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    nameQueue.Push(name);
                    Console.WriteLine($"{name} ställer sig i kön.");
                    break;
                case "2":
                    try
                    {
                        Console.WriteLine($"{nameQueue.Peek()} blir expedierad och lämnar kön.");
                        nameQueue.Pop();
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Kön är tom.");
                        break;
                    }
                case "3":
                    ReverseText();
                    break;
                case "0":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Wrong input.");
                    break;
            }
            // Det blir dumt att använda Stack för att hantera en kö eftersom den är LIFO,
            // last in first out. alltså den som står sist i kön får gå före de som väntat
            // längst. Vilket är lite orättvist. 

        } while (isRunning);
    }

    private static void ReverseText()
    {
        Stack<char> chStack = new Stack<char>();
        Console.WriteLine("write a text you want to reverse.");
        string str = Console.ReadLine();
        foreach (var c in str)
        {
            chStack.Push(c);
        }
        while (chStack.Count != 0)
        {
            Console.Write(chStack.Pop());
        }
        Console.ReadKey();
    }

    static void CheckParanthesis()
    {
        /*
         * Use this method to check if the paranthesis in a string is Correct or incorrect.
         * Example of correct: (()), {}, [({})],  List<int> list = new List<int>() { 1, 2, 3, 4 };
         * Example of incorrect: (()]), [), {[()}],  List<int> list = new List<int>() { 1, 2, 3, 4 );
         */

    }

}

