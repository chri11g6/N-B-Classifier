using NBC.logic;

var NBC = new NBCLogic();

NBC.LoadData(@"ML.csv");

Dictionary<string, double> outputs = NBC.Compute("Sunny,Cool,High,true");

foreach(var output in outputs){
	Console.WriteLine($"{output.Key} => {output.Value}");
}
