using System.Data;
using NBC.data;
using NBC.models;

namespace NBC.logic {
	public class NBCLogic {
		private DataTable table;
		private ReadCSV reader = new ReadCSV();
		private List<NBCColumn> querys;

		public void LoadData(string path){
			table = reader.CSVtoDataTable(path, ';');
		}

		public void Train(){
			if(table is null)
				return;

			NBCConvet convet = new NBCConvet(table);

			querys = convet.Create();

			SetProbabilityInput();
			SetProbabilityOutput();
		}
		
		public void Compute(string inputData){
			string[] datas = inputData.Split(';');

			NBCColumn outputData = new NBCColumn {Name = "Output data"};


			if(datas.Length != querys.Count -1)
				return;

			for(int i = 1; i < querys.Count; i++){
				
			}
		}

		private void SetProbabilityInput(){
			NBCColumn outputQuery = querys[0];

			for(int i = 1; i < querys.Count; i++){
				NBCColumn query = querys[i];

				foreach(NBCItem item in query.item){
					NBCItem outputItem = null;

					foreach(NBCItem itemOut in outputQuery.item){
						if(itemOut.Output == item.Output){
							outputItem = itemOut;
							break;
						}
					}

					item.OutputData = outputItem;

					item.Probability = item.Count / (double)outputItem.Count;
				}
			}
		}

		private void SetProbabilityOutput() {
			NBCColumn outputQuery = querys[0];

			foreach(NBCItem item in outputQuery.item){
				item.Probability = 1d;

				for(int i = 1; i < querys.Count; i++){
					NBCColumn query = querys[i];

					foreach(NBCItem itemIn in query.item){
						if(itemIn.Output == item.Output){
							item.Probability *= (double)itemIn.Probability;
						}
					}
				}
			}
		}
	}
}