using System.Data;
using NBC.data;
using NBC.dto;

namespace NBC.logic {
	internal class NBCConvet {
		private INBCTable db = DataFacadePattern.NBCTable;
		private Stack<string> columnNames = new Stack<string>();
		private string outputName = string.Empty;


		private DataTable table;

		public NBCConvet(DataTable table) {
			this.table = table;
		}

		public void Create(){
			GetColumnName();
			GetOutput();
			GetInput();
		}

		private void GetColumnName(){
			table.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList().ForEach(x => columnNames.Push(x));
		}

		private void GetOutput(){
			outputName = columnNames.Pop();

			var outputQuery = from r in table.AsEnumerable()
						group r by new {
							Output = r.Field<string>(outputName)
						} into g
						select new {
							g.Key.Output,
							Count = g.Count()
						};

			foreach(var item in outputQuery){
				double probability = item.Count / (double)table.Rows.Count;
				db.AddOutput(item.Output, new OutputData{ Probability = probability, Count = item.Count});
			}
		}

		private void GetInput(){
			while (columnNames.Count > 0){
				string inputName = columnNames.Pop();

				var query = from r in table.AsEnumerable()
							group r by new {
								Input = r.Field<string>(inputName),
								Output = r.Field<string>(outputName)
							} into g
							select new {
								g.Key.Input,
								g.Key.Output,
								Count = g.Count()
							};
				var queryTotal = from r in table.AsEnumerable()
							group r by new {
								Input = r.Field<string>(inputName)
							} into g
							select new {
								g.Key.Input,
								Count = g.Count()
							};

				foreach(var item in query){
					int countOutput = db.GetOutput(item.Output).Count;

					double probability = item.Count / (double)countOutput;

					db.AddRowData(item.Input, item.Output, probability, columnNames.Count);
				}

				foreach(var item in queryTotal){
					double probability = item.Count / (double)table.Rows.Count;

					db.AddRowData(item.Input, string.Empty, probability, columnNames.Count);
				}
			}
		}

	}

	
}