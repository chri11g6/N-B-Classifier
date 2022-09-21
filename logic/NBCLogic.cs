using System.Data;
using NBC.data;

namespace NBC.logic {
	public class NBCLogic {
		private INBCTable db = DataFacadePattern.NBCTable;
		private IReadCSV reader = DataFacadePattern.ReadCSV;

		public NBCLogic() {
			db.Clear();
		}

		public void LoadData(string path){
			DataTable table = reader.CSVtoDataTable(path, ',');

			NBCConvet convet = new NBCConvet(table);

			convet.Create();
		}
		
		public Dictionary<string, double> Compute(string inputData){
			Dictionary<string, double> outputProbabilityList = new Dictionary<string, double>();
			double total = 1d;
			string[] datas = inputData.Split(',');

			foreach(string output in db.GetOutputList()){
				outputProbabilityList.Add(output, 1d);
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
	}
}