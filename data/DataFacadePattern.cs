using NBC.data.database;
using NBC.data.io;

namespace NBC.data {
	public class DataFacadePattern {
		private static INBCTable table = new NBCTable();
		private static IReadCSV cvsReader = new ReadCSV();

		public static INBCTable NBCTable {
			get {return table;}
		}

		public static IReadCSV ReadCSV {
			get {return cvsReader;}
		}
	}
}