using System.Data;
using NBC.data;
using NBC.data.database;
using NBC.data.io;
using NBC.logic;

namespace NBC;
public class NBC : INBC {
    private INBCTable db;
    private IReadCSV reader = new ReadCSV();


    public Dictionary<string, double> Compute(string inputData) {
        Dictionary<string, double> outputProbabilityList = new Dictionary<string, double>();
			double total = 1d;
			string[] datas = inputData.Split(',');

			foreach(var output in db.GetOutputList()){
				outputProbabilityList.Add(output.Key, output.Value.Probability);
			}

			for(int i = 0; i < datas.Length; i++){
				string input = datas[i];

				foreach(string output in outputProbabilityList.Keys){
					outputProbabilityList[output] *= db.GetProbability(input, output, i);
				}

				total *= db.GetProbability(input, null, i);
			}
			
			foreach(var output in outputProbabilityList){
				outputProbabilityList[output.Key] = output.Value / total;
			}

			return outputProbabilityList;
    }

    public void LoadData(string path) {
        DataTable table = reader.CSVtoDataTable(path, ',');

        NBCConvet convet = new NBCConvet(table);

        db = convet.Create();
    }
}
