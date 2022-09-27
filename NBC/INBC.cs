namespace NBC {
	public interface INBC {
		public void LoadData(string path);
		public Dictionary<string, double> Compute(string inputData);
	}
}