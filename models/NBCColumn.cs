namespace NBC.models {
	internal class NBCColumn {
		public string Name { set; get; } = String.Empty;
		public List<NBCItem> item { set; get; } = new List<NBCItem>();
		public List<NBCItem> itemNoOut { set; get; } = new List<NBCItem>();
	}
}