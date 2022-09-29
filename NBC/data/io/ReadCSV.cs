using System.Data;

namespace NBC.data.io {
	public class ReadCSV : IReadCSV{
        public DataTable CSVtoDataTable(string strFilePath, char csvDelimiter) {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath)) {
                string[] headers = sr.ReadLine().Split(csvDelimiter);
                foreach (string header in headers) {
                    try {
                        dt.Columns.Add(header);
                    } catch { }
                }
                while (!sr.EndOfStream) {
                    string[] rows = sr.ReadLine().Split(csvDelimiter);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++) {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }

            }
            return dt;
        }
    }
}