using NBC;

INBC nbc = new NBC.NBC();

nbc.LoadData(@"ML.csv");

Dictionary<string, double> outputs = nbc.Compute("Sunny,Cool,High,true");

foreach(var output in outputs){
	Console.WriteLine($"{output.Key} => {output.Value}");
}