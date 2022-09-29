using System.Data;
using NBC.dto;

namespace NBC.data.database {
	public class NBCTable : INBCTable {
		private DataTable data;

		private Dictionary<string, OutputData> outputList = new Dictionary<string, OutputData>();

		public NBCTable() {
			data = new DataTable();

			DataColumn input = data.Columns.Add("Input");
			DataColumn output = data.Columns.Add("Output");
			DataColumn probability = data.Columns.Add("Probability");
			DataColumn columnNumber = data.Columns.Add("ColumnNumber");

			probability.DataType = typeof(double);
			columnNumber.DataType = typeof(int);
		}

        public void AddOutput(string output, OutputData data) {
            outputList.Add(output, data);
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

        public bool DataExistInCol(string input, int columnNumber) {
            DataRow[] datas = data.Select($"Input = '{input}' AND ColumnNumber = '{columnNumber}'");

			return datas.Length != 0;
        }

        public int GetConutOfInput() {
            return (int) data.Select("ColumnNumber = MAX(ColumnNumber)").FirstOrDefault()[3] + 1;
        }

        public OutputData GetOutput(string output) {
            return outputList[output];
        }

        public Dictionary<string, OutputData> GetOutputList() {
            return new Dictionary<string, OutputData>(outputList);
        }

        public double GetProbability(string input, string? output, int columnNumber) {
			if(output is null)
				return (double) data.Select($"Input = '{input}' AND Output = '' AND ColumnNumber = '{columnNumber}'").FirstOrDefault()[2];

			return (double) data.Select($"Input = '{input}' AND Output = '{output}' AND ColumnNumber = '{columnNumber}'").FirstOrDefault()[2];
		}
	}
}