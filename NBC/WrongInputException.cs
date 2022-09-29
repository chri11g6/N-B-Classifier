namespace NBC {
	public class WrongInputException : Exception {
		public override string Message {
            get {
                return "Wrong input";
            }
        }
	}
}