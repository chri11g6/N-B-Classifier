using System.Collections;
using System.Data;
using NBC.models;

namespace NBC.logic {
	internal class NBCConvet {
		private Stack<string> columnNames = new Stack<string>();
		private List<NBCColumn> querys = new List<NBCColumn>();
		private string outputName = string.Empty;

		private DataTable table;

		public NBCConvet(DataTable table) {
			this.table = table;
		}

		public List<NBCColumn> Create(){
			GetColumnName();
			GetOutput();
			GetInput();
			return querys;
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

			NBCColumn column = new NBCColumn {Name = outputName};

			foreach(var item in outputQuery){
				NBCItem nbcItem = new NBCItem {
					Output = item.Output,
					Count = item.Count
				};

				column.item.Add(nbcItem);
			}

			querys.Add(column);

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

				NBCColumn column = new NBCColumn {Name = inputName};

				foreach(var item in query){
					NBCItem nbcItem = new NBCItem {
						Input = item.Input,
						Output = item.Output,
						Count = item.Count
					};

					column.item.Add(nbcItem);
				}

				foreach(var item in queryTotal){
					NBCItem nbcItem = new NBCItem {
						Input = item.Input,
						Count = item.Count
					};

					column.itemNoOut.Add(nbcItem);
				}

				querys.Add(column);
			}
		}

	}

	
}