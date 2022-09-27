using System.Data;

namespace NBC.data {
	public interface IReadCSV {
		public DataTable CSVtoDataTable(string strFilePath, char csvDelimiter);
	}
}