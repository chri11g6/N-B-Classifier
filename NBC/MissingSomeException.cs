namespace NBC {
	public class MissingSomeException : Exception {
		public override string Message {
            get {
                return "Missing some of input";
            }
        }
	}
}