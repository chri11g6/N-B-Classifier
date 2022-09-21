using System.Data;

namespace NBC.data.database {
	public class NBCTable : INBCTable {
		private DataTable data;

		private List<string> outputList = new List<string>();

		public NBCTable() {
			data = new DataTable();

			DataColumn input = data.Columns.Add("Input");
			DataColumn output = data.Columns.Add("Output");
			DataColumn probability = data.Columns.Add("Probability");
			DataColumn columnNumber = data.Columns.Add("ColumnNumber");

			probability.DataType = typeof(double);
			columnNumber.DataType = typeof(int);
		}

        public void AddOutput(string output) {
            outputList.Add(output);
        }

        public void AddRowData(string input, string? output, double probability, int columnNumber){
			DataRow row = data.NewRow();

			row[0] = input;
			row[1] = output;
			row[2] = probability;
			row[3] = columnNumber;

			data.Rows.Add(row);
		}

        public void Clear() {
            data.Clear();
			outputList.Clear();
        }

        public List<string> GetOutputList() {
            return new List<string>(outputList);
        }

        public double GetProbability(string input, string? output, int columnNumber) {
			if(output is null)
				return (double) data.Select($"Input = '{input}' AND Output = '' AND ColumnNumber = '{columnNumber}'").FirstOrDefault()[2];

			return (double) data.Select($"Input = '{input}' AND Output = '{output}' AND ColumnNumber = '{columnNumber}'").FirstOrDefault()[2];
		}
	}
}