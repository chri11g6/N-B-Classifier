using NBC.logic;

var NBC = new NBCLogic();

NBC.LoadData(@"ML.csv");

NBC.Train();

NBC.Compute("Sunny;Cool;High;true");

Console.WriteLine("Hej med dig");