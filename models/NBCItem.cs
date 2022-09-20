namespace NBC.models {
	internal class NBCItem {
		public string Input { set; get; } = String.Empty;
		public string Output { set; get; } = String.Empty;
		public int Count { set; get; }

		public double Probability { set; get; }
	}
}