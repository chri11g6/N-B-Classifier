using NBC.dto;

namespace NBC.data {
	public interface INBCTable {
		public double GetProbability(string input, string? output, int columnNumber);
		public void AddRowData(string input, string? output, double probability, int columnNumber);

		public void AddOutput(string output, OutputData data);
		public int GetConutOfInput();
		public Dictionary<string, OutputData> GetOutputList();
		public OutputData GetOutput(string output);

		public bool DataExistInCol(string input, int columnNumber);


		public void Clear();
	}
}